using ClientLibary.Helpers;
using ClientLibary.Services.Client.Constracts;
using ServerLibary.Model.Params;
using ServerLibary.Model.Responses;
using System.Net.Http.Json;

namespace ClientLibary.Services.Client.Implementations
{
    public class SanPhamApiServices : ISanPhamApiServices
    {
        private GetHttpClient _httpClient;
        public const string AuthUrl = "api/SanPham/";
        public SanPhamApiServices(GetHttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<List<DanhMucResp>> DanhSachDanhMuc(DanhMucParam? param)
        {
            var resp = new List<DanhMucResp>();
            var httpClient = _httpClient.GetHttpClientA();
            var getResp = await httpClient.PostAsJsonAsync($"{AuthUrl}danh-sach-danh-muc", param);
            if (!getResp.IsSuccessStatusCode)
            {
                return resp;
            }
            return await getResp.Content.ReadFromJsonAsync<List<DanhMucResp>>();
        }

        public async Task<List<KichThuocResp>> DanhSachKichThuoc(int? id)
        {
            var resp = new List<KichThuocResp>();
            var httpClient = _httpClient.GetHttpClientA();
            var getResp = await httpClient.GetAsync($"{AuthUrl}danh-sach-kich-thuoc/{id}");
            if (!getResp.IsSuccessStatusCode)
            {
                return resp;
            }
            return await getResp.Content.ReadFromJsonAsync<List<KichThuocResp>>();
        }

        public async Task<List<MauSacResp>> DanhSachMauSac(int? id)
        {
            var resp = new List<MauSacResp>();
            var httpClient = _httpClient.GetHttpClientA();
            var getResp = await httpClient.GetAsync($"{AuthUrl}danh-sach-mau-sac/{id}");
            if (!getResp.IsSuccessStatusCode)
            {
                return resp;
            }
            return await getResp.Content.ReadFromJsonAsync<List<MauSacResp>>();
        }

        public async Task<List<SanPhamResp>> DanhSachSanPham(SanPhamParam? param)
        {
            var httpClient = _httpClient.GetHttpClientA();
            var getResp = await httpClient.PostAsJsonAsync($"{AuthUrl}danh-sach-san-pham", param);
            if (!getResp.IsSuccessStatusCode)
            {
                return new List<SanPhamResp>();
            }
            return await getResp.Content.ReadFromJsonAsync<List<SanPhamResp>>();
        }

        public async Task<SanPhamChiTietResp> SanPhamChiTiet(int? id_SanPham)
        {
            var httpClient = _httpClient.GetHttpClientA();
            var getResp = await httpClient.GetAsync($"{AuthUrl}san-pham-chi-tiet?id_Sp={id_SanPham}");
            if (!getResp.IsSuccessStatusCode)
            {
                return new SanPhamChiTietResp();
            }
            return await getResp.Content.ReadFromJsonAsync<SanPhamChiTietResp>();
        }

        public async Task<List<HinhAnhResp>> DanhSachBanner()
        {
            var httpClient = _httpClient.GetHttpClientA();
            var getResp = await httpClient.GetAsync($"{AuthUrl}danh-sach-banner");
            if (!getResp.IsSuccessStatusCode)
            {
                return new List<HinhAnhResp>();
            }
            return await getResp.Content.ReadFromJsonAsync<List<HinhAnhResp>>();
        }
    }
}
