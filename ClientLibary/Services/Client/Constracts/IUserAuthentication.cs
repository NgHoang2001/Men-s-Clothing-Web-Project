using BaseLibary.DTOs;
using BaseLibary.Responses;
using ServerLibary.Model.Responses;

namespace ClientLibary.Services.Client.Constracts
{
    public interface IUserAuthentication
    {
        public Task<GeneralRespone> Login(Login login);
        public Task<GeneralRespone> Register(Register register);
        public Task<GeneralRespone> RefreshToken();
        public Task<GeneralRespone> Logout();
        public Task<UserInfoResp> GetUserInfoClient();

    }
}
