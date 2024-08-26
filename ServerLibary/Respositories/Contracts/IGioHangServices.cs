using BaseLibary.Responses;
using ServerLibary.Model.Params;
using ServerLibary.Model.Responses;

namespace ServerLibary.Respositories.Contracts
{
    public interface IGioHangServices
    {
        Task<List<SanPhamGioHangResp>> DanhSachSanPhamGioHang(int? id);// id khach hang
        Task<GeneralRespone> ThemSanPhamVaoGioHang(ThemSanPhamGioHangParam param);// id khach hang
        Task<List<KichThuocVaMauSacSanPhamDonHangResp>> DanhSachKichThuocVaMauSacSanPhamDonHang(int? id); // id khach hang
        Task<GeneralRespone> CapNhatSanPhamGioHang(int id, int? idKichThuoc, int? idMauSac); // id gio hang
        Task<GeneralRespone> CapNhatSoLuongSanPhamGioHang(CapNhatSoLuongParam param); // id gio hang -> moi lan goi tang so luong len 1
        Task<GeneralRespone> XoaSanPhamGioHang(int? id);// id gio hang
        Task<GeneralRespone> ThayDoiTrangThaiGioHang(int? id);// id gio hang
        Task<List<KichThuocGiohangResp>> DanhSachKichThuocSanPhamGioHang(int? id);// id khách hàng
        Task<List<MauSacGioHangResp>> DanhSachMauSacSanPhamGioHang(int? id);// id khách hàng
    }
}
