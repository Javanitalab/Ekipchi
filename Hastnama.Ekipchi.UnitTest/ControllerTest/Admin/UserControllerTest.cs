using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using Hastnama.Ekipchi.Api.Core.Extensions;
using Hastnama.Ekipchi.Common.Enum;
using Hastnama.Ekipchi.Common.Message;
using Hastnama.Ekipchi.Data.User;
using Hastnama.Ekipchi.UnitTest.ControllerTest.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Hastnama.Ekipchi.UnitTest.ControllerTest.Admin
{
    public class UserControllerTest : BaseControllerTest
    {
        
        #region GetTest

        [Fact]
        public async void NotLogin_UnSuccessFull_Get()
        {
            // Arrange
            var httpClient = new HttpClient();

            // Act
            var get = await httpClient.GetAsync($"{_baseUrl}/Admin/User?Page=2&Limit=2");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, get.StatusCode);
        }

        [Fact]
        public async void BadPaging_UnSuccessFull_Get()
        {
            // Arrange
            var httpClient = new HttpClient();
            httpClient.SetToken("Bearer", await GetAdminAccessToken());

            // Act
            var get = await httpClient.GetAsync($"{_baseUrl}/Admin/User?Page=9999&Limit=9999");
            var responseBody = await get.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<JObject>(responseBody);
            var message = Convert.ToString(responseObject["message"]);

            // Assert
            Assert.Contains(PersianErrorMessage.InvalidPagingOption, message);
            Assert.Equal(HttpStatusCode.BadRequest, get.StatusCode);
        }

        [Fact]
        public async void SuccessFull_Get()
        {
            // Arrange
            var httpClient = new HttpClient();
            httpClient.SetToken("Bearer", await GetAdminAccessToken());

            // Act
            var get = await httpClient.GetAsync($"{_baseUrl}/Admin/User?Page=0&Limit=10");

            // Assert
            Assert.Equal(HttpStatusCode.OK, get.StatusCode);
        }

        #endregion

        #region GetByIdTest

        [Fact]
        public async void NotLogin_UnSuccessFull_GetById()
        {
            // Arrange
            var httpClient = new HttpClient();

            // Act
            var response = await httpClient.GetAsync($"{_baseUrl}/Admin/User/1");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void InvalidId_UnSuccessFull_GetById()
        {
            // Arrange
            var httpClient = new HttpClient();
            var token = await GetAdminAccessToken(); ;
            httpClient.DefaultRequestHeaders.Authorization=new AuthenticationHeaderValue("Authorization",$"Bearer {token}");
            var id = Guid.Empty;

            // Act
            var response = await httpClient.GetAsync($"{_baseUrl}/Admin/User/{id}");
            var message = await ExtractMessage(response);


            // Assert
            Assert.Contains(PersianErrorMessage.InvalidUserId, message);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void BadId_UnSuccessFull_GetById()
        {
            // Arrange
            var httpClient = new HttpClient();
            httpClient.SetToken("Bearer", await GetAdminAccessToken());
            var id = Guid.NewGuid();

            // Act
            var response = await httpClient.GetAsync($"{_baseUrl}/Admin/User/{id}");
            var message = await ExtractMessage(response);


            // Assert
            Assert.Contains(PersianErrorMessage.UserNotFound, message);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        #endregion

        #region DeleteByIdTest

        [Fact]
        public async void NotLogin_UnSuccessFull_DeleteById()
        {
            // Arrange
            var httpClient = new HttpClient();
            var id = Guid.Empty;
            
            // Act
            var response = await httpClient.DeleteAsync($"{_baseUrl}/Admin/User/{id}");
            var responseBody = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<JObject>(responseBody);
            
            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void InvalidId_UnSuccessFull_DeleteById()
        {
            // Arrange
            var httpClient = new HttpClient();
            httpClient.SetToken("Bearer", await GetAdminAccessToken());
            var id = Guid.Empty;

            // Act
            var response = await httpClient.DeleteAsync($"{_baseUrl}/Admin/User/{id}");
            var message = await ExtractMessage(response);


            // Assert
            Assert.Contains(PersianErrorMessage.InvalidUserId, message);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void BadId_UnSuccessFull_DeleteById()
        {
            // Arrange
            var httpClient = new HttpClient();
            httpClient.SetToken("Bearer", await GetAdminAccessToken());
            var id = Guid.NewGuid();

            // Act
            var response = await httpClient.DeleteAsync($"{_baseUrl}/Admin/User/{id}");
            var message = await ExtractMessage(response);


            // Assert
            Assert.Contains(PersianErrorMessage.UserNotFound, message);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        #endregion

        #region PostTest

        [Fact]
        public async void NotLogin_UnSuccessFull_Post()
        {
            // Arrange
            var httpClient = new HttpClient();
            var body = JsonConvert.SerializeObject(new CreateUserDto());
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PostAsync($"{_baseUrl}/Admin/User", content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void EmptyBody_UnSuccessFull_Post()
        {
            // Arrange
            var httpClient = new HttpClient();
            httpClient.SetToken("Bearer", await GetAdminAccessToken());
            var body = JsonConvert.SerializeObject(new CreateUserDto());
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PostAsync($"{_baseUrl}/Admin/User/", content);
            var message = await ExtractMessage(response);

            // Assert
            Assert.Contains(PersianErrorMessage.InvalidMobile, message);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void DuplicateMobile_UnSuccessFull_Post()
        {
            // Arrange
            var httpClient = new HttpClient();
            httpClient.SetToken("Bearer", await GetAdminAccessToken());
            var body = JsonConvert.SerializeObject(new CreateUserDto
            {
                Email = "email@gmail.com", Mobile = "09367572636", Username = "test", Role = Role.User, Name = "test",
                Family = "test"
            });
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PostAsync($"{_baseUrl}/Admin/User/", content);
            var message = await ExtractMessage(response);

            // Assert
            Assert.Contains(PersianErrorMessage.InvalidMobile, message);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion

        #region PutTest

        [Fact]
        public async void NotLogin_UnSuccessFull_Put()
        {
            // Arrange
            var httpClient = new HttpClient();
            var body = JsonConvert.SerializeObject(new UpdateUserDto());
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PutAsync($"{_baseUrl}/Admin/User", content);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async void BadBody_UnSuccessFull_Put()
        {
            // Arrange
            var httpClient = new HttpClient();
            httpClient.SetToken("Bearer", await GetAdminAccessToken());
            var body = JsonConvert.SerializeObject(new UpdateUserDto());
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PutAsync($"{_baseUrl}/Admin/User", content);
            var message = await ExtractMessage(response);

            // Assert
            Assert.Contains(PersianErrorMessage.InvalidMobile, message);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void BadId_UnSuccessFull_Put()
        {
            // Arrange
            var httpClient = new HttpClient();
            httpClient.SetToken("Bearer", await GetAdminAccessToken());
            var body = JsonConvert.SerializeObject(new UpdateUserDto
            {
                Id = Guid.Empty, Email = "email@gmail.com", Mobile = "09367572636", 
                Username = "test", Role = Role.User, Name = "test", Family = "test"
            });
            var content = new StringContent(body, Encoding.UTF8, "application/json");

            // Act
            var response = await httpClient.PutAsync($"{_baseUrl}/Admin/User", content);
            var message = await ExtractMessage(response);

            // Assert
            Assert.Contains(PersianErrorMessage.InvalidUserId, message);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        #endregion
    }
}