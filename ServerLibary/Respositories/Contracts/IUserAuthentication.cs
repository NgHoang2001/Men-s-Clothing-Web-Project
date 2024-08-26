using BaseLibary.DTOs;
using BaseLibary.Responses;
using Microsoft.AspNetCore.Http;
using ServerLibary.Model.Responses;

namespace ServerLibary.Respositories.Contracts
{
    public interface IUserAuthentication
    {
        public Task<LoginResponce> Login(Login login);
        public Task<GeneralRespone> Register(Register register);
        public Task<LoginResponce> RefreshToken(RefreshToken token);
        void SetTokensInsideCookie(UserSession tokenDto, HttpContext context);
        void SetOffTokensInsideCookie(HttpContext context);
        public Task<UserInfoResp> GetUserInfoServices(HttpContext http);
    }
}
