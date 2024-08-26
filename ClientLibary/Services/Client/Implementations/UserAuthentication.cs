using BaseLibary.DTOs;
using BaseLibary.Responses;
using ClientLibary.Helpers;
using ClientLibary.Services.Client.Constracts;
using ServerLibary.Model.Responses;
using System.Net.Http.Json;

namespace ClientLibary.Services.Client.Implementations
{
    public class UserAuthentication : IUserAuthentication
    {
        private GetHttpClient _httpClient;
        public const string AuthUrl = "api/Authentication/";
        public UserAuthentication(GetHttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<GeneralRespone> Register(Register register)
        {
            var httpClient = _httpClient.GetHttpClientA();
            var resp = await httpClient.PostAsJsonAsync($"{AuthUrl}register", register);
            if (!resp.IsSuccessStatusCode)
            {
                return new GeneralRespone(false, "Có lỗi xảy ra");
            }
            return new GeneralRespone(true, "Thành công");
        }

        public async Task<GeneralRespone> Login(Login login)
        {
            var httpClient = _httpClient.GetHttpClientA();
            var resp = await httpClient.PostAsJsonAsync($"{AuthUrl}login", login);
            if (!resp.IsSuccessStatusCode)
            {
                return new GeneralRespone(false, "Có lỗi xảy ra");
            }
            return new GeneralRespone(true, "Thành công");
        }

        public async Task<GeneralRespone> RefreshToken()
        {
            var httpClient = _httpClient.GetHttpClientA();
            var resp = await httpClient.GetAsync($"{AuthUrl}refresh-token");
            if (!resp.IsSuccessStatusCode)
            {
                return new GeneralRespone(false, "Có lỗi xảy ra");
            }
            return new GeneralRespone(true, "Thành công");
        }

        public async Task<GeneralRespone> Logout()
        {
            var httpClient = _httpClient.GetHttpClientA();
            var resp = await httpClient.GetAsync($"{AuthUrl}logout");
            if (!resp.IsSuccessStatusCode)
            {
                return new GeneralRespone(false, "Có lỗi xảy ra");
            }
            return new GeneralRespone(true, "Thành công");
        }

        public async Task<UserInfoResp> GetUserInfoClient()
        {

            var httpClient = _httpClient.GetHttpClientA();
            var resp = await httpClient.GetAsync($"{AuthUrl}current-user");
            if (!resp.IsSuccessStatusCode)
            {
                return new UserInfoResp();
            }
            return await resp.Content.ReadFromJsonAsync<UserInfoResp>();
        }
    }
}
