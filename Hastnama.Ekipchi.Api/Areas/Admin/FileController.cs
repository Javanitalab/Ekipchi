using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Api.Core.Environment;
using Hastnama.Ekipchi.Api.Core.Extensions;
using Hastnama.Ekipchi.Api.Core.FileProcessor;
using Hastnama.Ekipchi.Business.Service;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Helper;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.File;
using Hastnama.Ekipchi.DataAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
#pragma warning disable 618

namespace Hastnama.Ekipchi.Api.Areas.Admin
{
    [Area("Admin")]
    [Route("[Area]/[Controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class FileController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageProcessingService _imageProcessingService;

        public FileController(IHostingEnvironment env,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IImageProcessingService imageProcessingService)
        {
            Argument.IsNotNull(() => env);
            _env = env;

            Argument.IsNotNull(() => unitOfWork);
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageProcessingService = imageProcessingService;
        }


        /// <summary>
        /// uploading files
        /// </summary>
        /// <remarks>
        ///
        /// Types can be one of this: Avatar, Image, Video, Music, Document, Other
        /// count validate with [X-MultiSelect] Header can between 1 to 20
        ///
        /// </remarks>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="200">Get Files list Uploaded</response>
        /// <response code="400">If validation failure.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Produces(typeof(List<UserFileDto>))]
        [ProducesErrorResponseType(typeof(ApiMessage))]
        [HttpPost]
        [Consumes("multipart/form-data")]
        [Route("[Action]")]
        public async Task<IActionResult> Upload([FromForm] FilesUploadDto model)
        {
            if (model.Files.Count != model.LocalId.Count)
                return BadRequest(new ApiMessage {Message = ResponseMessage.InvalidLocalId});

            var validateModelResult = ValidateModel(model);

            if (validateModelResult is BadRequestResult)
            {
                return validateModelResult;
            }

            string folder;
            string url;

            switch (model.Type)
            {
                case "Image":
                    folder = ApplicationStaticPath.Images;
                    url = ApplicationStaticPath.Clients.Image;
                    break;

                case "Avatar":
                    folder = ApplicationStaticPath.Avatars;
                    url = ApplicationStaticPath.Clients.Avatar;
                    break;

                case "Video":
                    folder = ApplicationStaticPath.Videos;
                    url = ApplicationStaticPath.Clients.Video;
                    break;

                case "Music":
                    folder = ApplicationStaticPath.Musics;
                    url = ApplicationStaticPath.Clients.Music;
                    break;

                case "Document":
                    folder = ApplicationStaticPath.Documents;
                    url = ApplicationStaticPath.Clients.Document;
                    break;

                case "Other":
                    folder = ApplicationStaticPath.Others;
                    url = ApplicationStaticPath.Clients.Other;
                    break;

                default:
                    folder = ApplicationStaticPath.Others;
                    url = ApplicationStaticPath.Clients.Other;
                    break;
            }

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var result = new List<UserFileDto>();

            for (int i = 0; i < model.Files.Count; i++)
            {
                var tempPath = Path.Combine(folder, model.Files[i].FileName);
                var fileName = Path.GetFileNameWithoutExtension(tempPath);
                var extension = Path.GetExtension(tempPath);
                var uniqueId = Guid.NewGuid().ToString("N");
                var newName = $"{uniqueId}{extension}";
                var filePath = Path.Combine(_env.ContentRootPath, folder, newName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Files[i].CopyTo(fileStream);
                }

                using (var thumbnailFileStream = new FileStream(filePath, FileMode.Open))
                {
                    model.Files[i].CopyTo(thumbnailFileStream);

                    if (model.Type == "Image" && extension != ".svg")
                    {
                        var thumbName = $"{uniqueId}_t{extension}";
                        var thumbPath = Path.Combine(folder, thumbName);
                        _imageProcessingService.MakeThumbnail(thumbnailFileStream, thumbPath);
                    }
                }

                var fileUrl = $"{url}/{newName}";
                var userFile = new UserFile
                {
                    Type = model.Type,
                    Name = fileName,
                    IsPrivate = model.IsPrivate,
                    Size = model.Files[i].Length,
                    UniqueId = uniqueId,
                    Url = fileUrl,
                    MediaType = model.Files[i].ContentType,
                    Path = Path.Combine(folder, newName),
                    LocalId = model.LocalId[i]
                };

                await _unitOfWork.FilesService.AddAsync(userFile);
                var dto = _mapper.Map<UserFileDto>(userFile);
                result.Add(dto);
            }

            await _unitOfWork.SaveChangesAsync();
            return Json(result);
        }

        /// <summary>
        /// Download file
        /// </summary>
        /// <param name="id">file unique id</param>
        /// <returns></returns>
        /// <response code="200">Get File stream</response>
        /// <response code="404">If File not found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [HttpGet("[Action]/{id}")]
        [ProducesErrorResponseType(typeof(ApiMessage))]
        [AllowAnonymous]
        public async Task<IActionResult> Download(string id)
        {
            var file = await _unitOfWork.FilesService.GetUserFileAsync(id);
            if (file == null)
                return NotFound(new ApiMessage
                {
                    Message = ResponseMessage.FileNotFound
                });

            var contentType = file.MediaType;
            var filePath = Path.Combine(_env.ContentRootPath, file.Path);
            var fileName = $"{file.Name}{Path.GetExtension(filePath)}";

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }

            memory.Position = 0;

            return File(memory, contentType, fileName, true);
        }

        /// <summary>
        /// get file data
        /// </summary>
        /// <param name="id">file unique id</param>
        /// <returns></returns>
        /// <response code="200">Get File data</response>
        /// <response code="404">If File not found.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [Produces(typeof(UserFileDto))]
        [ProducesErrorResponseType(typeof(ApiMessage))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var file = await _unitOfWork.FilesService.GetUserFileAsync(id);
            if (file == null)
                return NotFound(new ApiMessage
                {
                    Message = ResponseMessage.FileNotFound
                });

            var result = _mapper.Map<UserFileDto>(file);
            return Json(result);
        }

        /// <summary>
        /// get files data
        /// </summary>
        /// <param name="pagingOptions">PagingOptions</param>
        /// <param name="category">files type</param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// category can be one of this: Avatar, Image, Video, Music, Document, Other
        ///
        /// </remarks>
        /// <response code="200">if message Successfully return</response>
        /// <response code="400">If out of range page and limit</response>
        /// <response code="404">If category not found.</response>
        /// <response code="500">If an unexpected error happen</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Produces(typeof(PagedList<UserFileDto>))]
        [ProducesErrorResponseType(typeof(ApiMessage))]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PagingOptions pagingOptions, string category)
        {
            var files = await _unitOfWork.FilesService.GetList(pagingOptions.Page.Value, pagingOptions.Limit.Value,
                category);
            if (files == null)
                return NotFound(new ApiMessage
                {
                    Message = ResponseMessage.FileNotFound
                });

            return Json(files.MapTo<UserFileDto>(_mapper));
        }

        /// <summary>
        /// Delete file data
        /// </summary>
        /// <param name="id">file unique id</param>
        /// <returns></returns>
        /// <response code="204">when delete</response>
        /// <response code="404">If File not found.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var file = await _unitOfWork.FilesService.GetUserFileAsync(id);
            if (file == null)
                return NotFound(new ApiMessage
                {
                    Message = ResponseMessage.FileNotFound
                });

            if (System.IO.File.Exists(file.Path))
            {
                var fileInfo = new FileInfo(file.Path);

                if (file.Type == "Image")
                {
                    var name = fileInfo.Name;
                    var extension = fileInfo.Extension;
                    var directory = fileInfo.DirectoryName;
                    var fullname = $"{name}_t.{extension}";
                    var path = Path.Combine(directory, fullname);
                    var thumb = new FileInfo(path);
                    thumb.Delete();
                }

                fileInfo.Delete();
            }

            _unitOfWork.FilesService.Delete(file);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        private IActionResult ValidateModel(FilesUploadDto model)
        {
            var filesCount = 1;

            if (HttpContext.HasFileCount())
            {
                filesCount = Convert.ToInt32(HttpContext.GetFilesCount());
            }

            if (model.Files == null || model.Files.Count < 1 || model.Files.Count > filesCount)
            {
                return BadRequest(new ApiMessage()
                {
                    Message = "Files count validation failed. check files count again."
                });
            }

            var isValid = true;

            switch (model.Type)
            {
                case "Avatar":
                    foreach (var file in model.Files)
                    {
                        isValid = IsValidFile(file, new List<string> {".jpg", ".jpeg", ".png", ".gif", ".svg"});
                    }

                    break;

                case "Image":
                    foreach (var file in model.Files)
                    {
                        isValid = IsValidFile(file, new List<string> {".jpg", ".jpeg", ".png", ".gif", ".svg"});
                    }

                    break;

                case "Video":
                    foreach (var file in model.Files)
                    {
                        isValid = IsValidFile(file, new List<string> {".mp4", ".wmv", ".mkv", ".mov"});
                    }

                    break;

                case "Music":
                    foreach (var file in model.Files)
                    {
                        isValid = IsValidFile(file, new List<string> {".mp3", ".m4a", ".wave", ".wma"});
                    }

                    break;

                case "Document":
                    foreach (var file in model.Files)
                    {
                        isValid = IsValidFile(file,
                            new List<string> {".doc", ".docx", ".xls", ".xlsx", ".pdf", ".ppt", ".pptx"});
                    }

                    break;

                case "Other":
                    break;
            }

            if (isValid)
            {
                return Ok();
            }

            return BadRequest(new ApiMessage()
            {
                Message = "Files Type validation failed. check files type again."
            });
        }

        private bool IsValidFile(IFormFile file, IEnumerable<string> allowedExtensions)
        {
            if (file == null || file.Length == 0)
            {
                return false;
            }

            var fileExtension = Path.GetExtension(file.FileName);
            return !string.IsNullOrWhiteSpace(fileExtension) &&
                   allowedExtensions.Any(ext => fileExtension.Equals(ext, StringComparison.OrdinalIgnoreCase));
        }
    }
}