using System.Threading.Tasks;
using AutoMapper;
using Hastnama.Ekipchi.Business.Service;
using Hastnama.Ekipchi.Common.General;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.Role;
using Hastnama.Ekipchi.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Hastnama.Ekipchi.Api.Areas.Admin
{
    public class RoleController : BaseAdminController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PagingOptions pagingOptions, string query)
        {
            var role = await _unitOfWork.RoleService.GetRoleAsync(pagingOptions, query);

            return Ok(role.MapTo<RoleDto>(_mapper));
        }


        [HttpGet("{id}", Name = "GetPermission")]
        public async Task<IActionResult> Get(int id)
        {
            var role = await _unitOfWork.RoleService.GetRoleAsync(id);

            if (role is null)
                return NotFound(new ApiMessage {Message = PersianErrorMessage.RoleNotFound});

            return Ok(_mapper.Map<RoleDto>(role));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _unitOfWork.RoleService.GetRoleAsync(id);

            #region Validation

            if (role is null)
                return NotFound(new ApiMessage {Message = PersianErrorMessage.RoleNotFound});

            if (role.IsVital)
                return NotFound(new ApiMessage {Message = PersianErrorMessage.RoleNotFound});

            if (await _unitOfWork.UserInRoleService.IsRoleExistInUser(id))
                return NotFound(new ApiMessage {Message = PersianErrorMessage.RoleNotFound});

            #endregion

            _unitOfWork.RoleService.Delete(role);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateRoleDto createRole)
        {
            var role = new Role {Name = createRole.Name};

            await _unitOfWork.RoleService.AddAsync(role);

            foreach (var permissionId in createRole.PermissionId)
            {
                if (await _unitOfWork.PermissionService.HasPermissionExist(permissionId))
                    return NotFound(new ApiMessage());

                await _unitOfWork.RolePermissionService.AddAsync(new RolePermission
                    {RoleId = role.Id, PermissionId = permissionId});
            }

            return Created(Url.Link("GetPermission", new {id = role.Id}), _mapper.Map<RoleDto>(role));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateRoleDto updateRoleDto)
        {
            var role = await _unitOfWork.RoleService.GetRoleAsync(id);

            if (role is null)
                return NotFound(new ApiMessage {Message = PersianErrorMessage.RoleNotFound});

            role.Name = updateRoleDto.Name;

            _unitOfWork.RoleService.Edit(role);

            var rolePermission = await _unitOfWork.RolePermissionService.GetRolePermissionListAsync(id);

            _unitOfWork.RolePermissionService.RemoveRange(rolePermission);


            foreach (var permissionId in updateRoleDto.PermissionId)
            {
                if (await _unitOfWork.PermissionService.HasPermissionExist(permissionId))
                    return NotFound(new ApiMessage());

                await _unitOfWork.RolePermissionService.AddAsync(new RolePermission
                    {RoleId = role.Id, PermissionId = permissionId});
            }

            return NoContent();
        }
    }
}