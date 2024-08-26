using ServerLibary.Model.Params;
using ServerLibary.Model.Responses;

namespace ClientLibary.Services.Client.Constracts
{
    public interface ISanPhamApiServices
    {
        Task<List<DanhMucResp>> DanhSachDanhMuc(DanhMucParam? param);
        Task<List<SanPhamResp>> DanhSachSanPham(SanPhamParam? param);
        Task<SanPhamChiTietResp> SanPhamChiTiet(int? id_SanPham);
        Task<List<KichThuocResp>> DanhSachKichThuoc(int? id);
        Task<List<MauSacResp>> DanhSachMauSac(int? id);
        Task<List<HinhAnhResp>> DanhSachBanner();
    }
}
