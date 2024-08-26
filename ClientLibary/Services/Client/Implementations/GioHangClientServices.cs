using BaseLibary.Responses;
using ClientLibary.Helpers;
using ClientLibary.Services.Client.Constracts;
using ServerLibary.Model.Params;
using ServerLibary.Model.Responses;
using System.Net.Http.Json;

namespace ClientLibary.Services.Client.Implementations
{
    public class GioHangClientServices : IGioHangClientServices
    {
        private GetHttpClient _httpClient;
        public const string AuthUrl = "api/GioHang/";
        private IUserAuthentication userAuthentication;
        public GioHangClientServices(GetHttpClient httpClient, IUserAuthentication userAuthentication)
        {
            this._httpClient = httpClient;
            this.userAuthentication = userAuthentication;
        }
        public async Task<GeneralRespone> CapNhatSanPhamGioHang(CapNhatSoLuongSanPhamGioHang param)
        {
            var httpClient = _httpClient.GetHttpClientA();
            var resp = await httpClient.PostAsJsonAsync($"{AuthUrl}cap-nhat-san-pham-gio-hang", param);
            if (!resp.IsSuccessStatusCode)
            {
                return new GeneralRespone(false, "Có lỗi xảy ra");
            }
            return await resp.Content.ReadFromJsonAsync<GeneralRespone>();
        }

        public async Task<GeneralRespone> CapNhatSoLuongSanPhamGioHang(CapNhatSoLuongParam param)
        {
            var httpClient = _httpClient.GetHttpClientA();
            if (param?.Id == null)
            {
                return new GeneralRespone(false, "Có lỗi xảy ra");
            }
            var resp = await httpClient.PostAsJsonAsync($"{AuthUrl}cap-nhat-so-luong-san-pham-gio-hang", param);
            if (!resp.IsSuccessStatusCode)
            {
                return new GeneralRespone(false, "Có lỗi xảy ra");
            }
            return await resp.Content.ReadFromJsonAsync<GeneralRespone>();
        }

        public async Task<List<KichThuocVaMauSacSanPhamDonHangResp>> DanhSachKichThuocVaMauSacSanPhamDonHang(int? id)
        {
            var respModel = new List<KichThuocVaMauSacSanPhamDonHangResp>();
            var httpClient = _httpClient.GetHttpClientA();
            if (id == null)
            {
                return await Task.FromResult(respModel);
            }
            var resp = await httpClient.GetAsync($"{AuthUrl}danh-sach-kich-thuoc-va-mau-sac-san-pham-don-hang?id={(int)id}");
            if (!resp.IsSuccessStatusCode)
            {
                return await Task.FromResult(respModel);
            }
            return await resp.Content.ReadFromJsonAsync<List<KichThuocVaMauSacSanPhamDonHangResp>>();
        }

        public async Task<List<SanPhamGioHangResp>> DanhSachSanPhamGioHang()
        {
            var respModel = new List<SanPhamGioHangResp>();
            var httpClient = _httpClient.GetHttpClientA();
            var user = await userAuthentication.GetUserInfoClient();
            if (user == null || user.Id_KhachHang == null)
            {
                return new List<SanPhamGioHangResp>();
            }
            var resp = await httpClient.GetAsync($"{AuthUrl}danh-sach-san-pham-gio-hang?id={(int)user.Id_KhachHang}");
            if (!resp.IsSuccessStatusCode)
            {
                return await Task.FromResult(respModel);
            }
            return await resp.Content.ReadFromJsonAsync<List<SanPhamGioHangResp>>();
        }

        public async Task<GeneralRespone> ThemSanPhamVaoGioHang(ThemSanPhamGioHangParam param)
        {
            var respModel = new List<SanPhamGioHangResp>();
            var httpClient = _httpClient.GetHttpClientA();
            if (param == null)
            {
                return new GeneralRespone(false, "Có lỗi xảy ra");
            }
            var resp = await httpClient.PostAsJsonAsync($"{AuthUrl}them-san-pham-vao-gio-hang", param);
            if (!resp.IsSuccessStatusCode)
            {
                return new GeneralRespone(false, "Có lỗi xảy ra");
            }
            return await resp.Content.ReadFromJsonAsync<GeneralRespone>();
        }

        public async Task<GeneralRespone> XoaSanPhamGioHang(int? id)
        {
            var respModel = new List<SanPhamGioHangResp>();
            var httpClient = _httpClient.GetHttpClientA();
            if (id == null)
            {
                return new GeneralRespone(false, "Có lỗi xảy ra");
            }
            var resp = await httpClient.DeleteAsync($"{AuthUrl}xoa-san-pham-gio-hang?id={(int)id}");
            if (!resp.IsSuccessStatusCode)
            {
                return new GeneralRespone(false, "Có lỗi xảy ra");
            }
            return await resp.Content.ReadFromJsonAsync<GeneralRespone>();
        }

        public async Task<List<MauSacGioHangResp>> DanhSachMauSacGioHangClient(int? id)
        {
            var resp = new List<MauSacGioHangResp>();
            var httpClient = _httpClient.GetHttpClientA();
            if (id == null)
            {
                return await Task.FromResult(resp);
            }
            var respServer = await httpClient.GetAsync($"{AuthUrl}danh-sach-mau-sac?id={(int)id}");
            if (!respServer.IsSuccessStatusCode)
            {
                return await Task.FromResult(resp);
            }
            return await respServer.Content.ReadFromJsonAsync<List<MauSacGioHangResp>>();
        }

        public async Task<List<KichThuocGiohangResp>> DanhSachKichThuocGioHangClient(int? id)
        {
            var resp = new List<KichThuocGiohangResp>();
            var httpClient = _httpClient.GetHttpClientA();
            if (id == null)
            {
                return await Task.FromResult(resp);
            }
            var respServer = await httpClient.GetAsync($"{AuthUrl}danh-sach-kich-thuoc?id={(int)id}");
            if (!respServer.IsSuccessStatusCode)
            {
                return await Task.FromResult(resp);
            }
            return await respServer.Content.ReadFromJsonAsync<List<KichThuocGiohangResp>>();
        }

        public async Task<GeneralRespone> ThayDoiTrangThaiGioHang(int? id)
        {
            var respModel = new List<SanPhamGioHangResp>();
            var httpClient = _httpClient.GetHttpClientA();
            var resp = await httpClient.GetAsync($"{AuthUrl}thay-doi-trang-thai-san-pham-gio-hang?id={(int)id}");
            if (!resp.IsSuccessStatusCode)
            {
                return new GeneralRespone(false, "Có lỗi xảy ra");
            }
            return await resp.Content.ReadFromJsonAsync<GeneralRespone>();
        }
    }
}
