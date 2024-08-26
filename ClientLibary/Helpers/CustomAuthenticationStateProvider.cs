using BaseLibary.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ClientLibary.Helpers
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private LocalStorageService localStorageService;
        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(LocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        // phương thức cho phép theo dõi state của người dùng hiện tại
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Lấy token có trong local của user
            var stringToken = localStorageService.GetToken();
            if (stringToken == null) { return await Task.FromResult(new AuthenticationState(anonymous)); }

            // Chuyển đối string token -> UserSession (token, refreshtoken)
            var deserializeToken = Serializations.DeserializeJsonString<UserSession>(stringToken.Result);
            if (deserializeToken == null) return await Task.FromResult(new AuthenticationState(anonymous));

            // Lấy thông tin có trong token của người dùng
            var getUserClaims = DecryptToken(deserializeToken.Token!);
            if (getUserClaims == null) return await Task.FromResult(new AuthenticationState(anonymous));

            // Thiết lập thông tin xác thức vào ứng dụng
            var claimPrincipal = SetClaimPrincial(getUserClaims);
            if (claimPrincipal == null) return await Task.FromResult(new AuthenticationState(anonymous));
            return await Task.FromResult(new AuthenticationState(claimPrincipal));
        }

        // Pt thông báo về sự thay đổi trong việc xác thực của người dùng
        // Dùng ở đâu ?
        // Nhận thông tin từ server -> lưu thông auth của người dùng
        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            var claimPrincipal = new ClaimsPrincipal();
            if (userSession.Token != null || userSession.RefreshToken != null)
            {
                var serializeSession = Serializations.SerializeObj(userSession);
                localStorageService.SetToken(serializeSession);
                var getUserClaims = DecryptToken(userSession.Token!);
                claimPrincipal = SetClaimPrincial(getUserClaims);
            }
            else
            {
                await localStorageService.RemoveToken();
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimPrincipal)));
        }

        // Đọc các thông tin có trong token
        private static CustomUserClaims DecryptToken(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken))
            {
                return new CustomUserClaims();
            }
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);
            var userId = token.Claims.FirstOrDefault(_ => _.Type.Equals(ClaimTypes.NameIdentifier));
            //var name = token.Claims.FirstOrDefault(_ => _.Type.Equals(ClaimTypes.Name));
            var email = token.Claims.FirstOrDefault(_ => _.Type.Equals(ClaimTypes.Email));
            var role = token.Claims.FirstOrDefault(_ => _.Type.Equals(ClaimTypes.Role));
            return new CustomUserClaims(userId!.Value!, email!.Value!, role!.Value!);
        }

        // Thiết lập claims principal với kiếu xác thực Jwt
        public static ClaimsPrincipal SetClaimPrincial(CustomUserClaims claims)
        {
            if (claims.Email is null) return new ClaimsPrincipal();
            return new ClaimsPrincipal(new ClaimsIdentity(

                new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, claims.Id!),
                    new Claim(ClaimTypes.Name, claims.Name!),
                    new Claim(ClaimTypes.Email, claims.Email!),
                    new Claim(ClaimTypes.Role, claims.Role!),
                }, "JwtAuth"));
        }
    }
}
