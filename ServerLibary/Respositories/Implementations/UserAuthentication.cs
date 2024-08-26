using BaseLibary.DTOs;
using BaseLibary.Entities;
using BaseLibary.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServerLibary.Context;
using ServerLibary.Helpers;
using ServerLibary.Model.Responses;
using ServerLibary.Respositories.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ServerLibary.Respositories.Implementations
{
    public class UserAuthentication : IUserAuthentication
    {
        private DuAnWebCanifaDbContext _context;
        private IOptions<JwtSection> _options;
        public UserAuthentication(DuAnWebCanifaDbContext context, IOptions<JwtSection> options)
        {
            _context = context;
            _options = options;
        }
        public async Task<GeneralRespone> Register(Register register)
        {
            if (register == null)
            {
                return new GeneralRespone(false, "Model trống");
            }
            if (!register.Password.Equals(register.PasswordTry))
            {
                return new GeneralRespone(false, "Model trống");
            }
            var acc = await _context.TaiKhoans.FirstOrDefaultAsync(x => x.Email.Trim().Equals(register.Email.Trim()));
            if (acc != null)
            {
                return new GeneralRespone(false, "Email đã được dùng");
            }
            var vaiTro = await _context.VaiTros.FirstOrDefaultAsync(x => x.Ten.Contains("KhachHang"));
            if (vaiTro == null)
            {
                vaiTro = await AddObjToDatabase(new VaiTro
                {
                    Ten = "KhachHang"
                });
                //var accNew1 = await AddObjToDatabase(new TaiKhoan
                //{
                //    Email = register.Email.Trim(),
                //    MatKhau = BCrypt.Net.BCrypt.HashPassword(register.Password.Trim()),
                //    Id_VaiTro = vaitro.Id
                //});
                //await AddObjToDatabase(new KhachHang
                //{
                //    Id_TaiKhoan = accNew1.Id,

                //});
                //return new GeneralRespone(true, "Tạo tài khoản thành công");
            }
            var accNew = await AddObjToDatabase(new TaiKhoan
            {
                Email = register.Email.Trim(),
                MatKhau = BCrypt.Net.BCrypt.HashPassword(register.Password.Trim()),
                Id_VaiTro = vaiTro.Id
            });
            await AddObjToDatabase(new KhachHang
            {
                Id_TaiKhoan = accNew.Id,

            });
            return new GeneralRespone(true, "Tạo tài khoản thành công");
        }

        public async Task<LoginResponce> Login(Login login)
        {
            if (login == null)
            {
                return new LoginResponce(false, "Model trống");
            }

            var acc = await _context.TaiKhoans.FirstOrDefaultAsync(x => x.Email.Trim().Equals(login.Email.Trim()));
            if (acc == null)
            {
                return new LoginResponce(false, "Email/Password sai");
            }
            var isPass = BCrypt.Net.BCrypt.Verify(login.Password.Trim(), acc.MatKhau);
            if (!isPass)
            {
                return new LoginResponce(false, "Email/Password sai");
            }
            var taiKhoan = await _context.KhachHangs.Where(x => x.Id_TaiKhoan == acc.Id).FirstOrDefaultAsync();
            if (taiKhoan == null)
            {
                await AddObjToDatabase(new KhachHang
                {
                    Id_TaiKhoan = acc.Id,
                });
            }
            // tim role cua tai khoan
            var role = await _context.VaiTros.FirstOrDefaultAsync(x => x.Id == acc.Id_VaiTro);
            if (role == null)
            {
                return new LoginResponce(false, "Email/Password sai");
            }
            // jwt token
            var jwtToken = GenerationToken(acc, role.Ten);
            // refresh token
            var refreshToken = GenerateRefreshToken();
            // check RefreshTokenInfo
            var checkRefreshTokenInfo = await _context.RefreshTokenInfos.FirstOrDefaultAsync(x => x.Id_KhachHang == acc.Id);
            if (checkRefreshTokenInfo == null)
            {
                await AddObjToDatabase(new RefreshTokenInfo
                {
                    Id_KhachHang = acc.Id,
                    Token = refreshToken
                });
            }
            else
            {
                checkRefreshTokenInfo!.Token = refreshToken;
                await _context.SaveChangesAsync();
            }
            return new LoginResponce(true, "Đăng nhập thành công", jwtToken, refreshToken);
        }

        // Refreshtoken được gọi khi token hết hạn và người dùng đã có tài khoản.
        public async Task<LoginResponce> RefreshToken(RefreshToken token)
        {
            if (token == null)
            {
                return new LoginResponce(false, "Model trống");
            }
            var refreshToken = await _context.RefreshTokenInfos.FirstOrDefaultAsync(x => x.Token!.Equals(token.Token!));
            if (refreshToken == null)
            {
                return new LoginResponce(false, "Refresh Token trống");
            }
            var acc = await _context.TaiKhoans.Where(x => x.Id == refreshToken.Id_KhachHang).Include(x => x.VaiTro).FirstOrDefaultAsync();
            if (acc == null)
            {
                return new LoginResponce(false, "Refresh token không thể tạo mới do không thể tìm thấy người dùng");
            }
            var jwtToken = GenerationToken(acc, acc.VaiTro!.Ten!);
            var refreshTokenGenera = GenerateRefreshToken();
            var updateRefreshToken = await _context.RefreshTokenInfos.FirstOrDefaultAsync(x => x.Id_KhachHang == acc.Id);
            if (updateRefreshToken == null)
            {
                return new LoginResponce(false, "Refresh token không thể tạo mới do không thể tìm thấy người dùng");
            }
            updateRefreshToken.Token = refreshTokenGenera;
            await _context.SaveChangesAsync();
            return new LoginResponce(true, "Refresh token tạo mới thành công", jwtToken, refreshTokenGenera);
        }

        public void SetTokensInsideCookie(UserSession tokenDto, HttpContext context)
        {
            context.Response.Cookies.Append("accessToken", tokenDto.Token,
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddMinutes(5),
                    HttpOnly = true,
                    IsEssential = true,
                    Secure = true,
                    SameSite = SameSiteMode.None
                });

            context.Response.Cookies.Append("refreshToken", tokenDto.RefreshToken,
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(7),
                    HttpOnly = true,
                    IsEssential = true,
                    Secure = true,
                    SameSite = SameSiteMode.None
                });
        }
        public void SetOffTokensInsideCookie(HttpContext context)
        {
            context.Response.Cookies.Delete("accessToken",
                 new CookieOptions
                 {
                     HttpOnly = true,
                 });
            context.Response.Cookies.Delete("refreshToken",
                 new CookieOptions
                 {
                     HttpOnly = true,
                 });
            //context.Response.Cookies.Append("accessToken", tokenDto.Token,
            //    new CookieOptions
            //    {
            //        Expires = DateTimeOffset.UtcNow.AddMinutes(5),
            //        HttpOnly = true,
            //        IsEssential = true,
            //        Secure = true,
            //        SameSite = SameSiteMode.None
            //    });

            //context.Response.Cookies.Append("refreshToken", tokenDto.RefreshToken,
            //    new CookieOptions
            //    {
            //        Expires = DateTimeOffset.UtcNow.AddDays(7),
            //        HttpOnly = true,
            //        IsEssential = true,
            //        Secure = true,
            //        SameSite = SameSiteMode.None
            //    });
        }

        public async Task<UserInfoResp> GetUserInfoServices(HttpContext http)
        {
            var idTaiKhoan = http.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (idTaiKhoan == null)
            {
                return new UserInfoResp();
            }
            var tk = await _context.TaiKhoans.FirstOrDefaultAsync(x => x.Id == int.Parse(idTaiKhoan));
            if (tk == null)
            {
                return new UserInfoResp();
            }
            var kh = await _context.KhachHangs.FirstOrDefaultAsync(x => x.Id_TaiKhoan == tk.Id);
            if (kh == null)
            {
                return new UserInfoResp();
            }

            return new UserInfoResp
            {
                Id_TaiKhoan = tk?.Id,
                Email = tk?.Email,
                Id_KhachHang = kh?.Id,
                Name = kh?.Ten,
                Sdt = kh?.Sdt
            };
        }

        private string GenerationToken(TaiKhoan taiKhoan, string role)
        {
            // ma hoa key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key!));
            var crenditals = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // tao list claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,taiKhoan.Id.ToString()),
                new Claim(ClaimTypes.Email,taiKhoan.Email.ToString()),
                new Claim(ClaimTypes.Role,role.ToString()),
            };

            var token = new JwtSecurityToken(
                 issuer: _options.Value.Issuer!,
                    audience: _options.Value.Audience!,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: crenditals
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        private async Task<T> AddObjToDatabase<T>(T model)
        {
            if (model == null)
            {
                return default(T);
            }
            var tb = await _context.AddAsync(model);
            await _context.SaveChangesAsync();
            return (T)tb.Entity;
        }


    }
}
