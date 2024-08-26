using BaseLibary.Responses;
using ServerLibary.Model.Params;
using ServerLibary.Model.Responses;

namespace ClientLibary.Services.Client.Constracts
{
    public interface IGioHangClientServices
    {
        Task<List<SanPhamGioHangResp>> DanhSachSanPhamGioHang();// id khach hang
        Task<GeneralRespone> ThemSanPhamVaoGioHang(ThemSanPhamGioHangParam param);// id khach hang
        Task<List<KichThuocVaMauSacSanPhamDonHangResp>> DanhSachKichThuocVaMauSacSanPhamDonHang(int? id); // id khach hang
        Task<GeneralRespone> CapNhatSanPhamGioHang(CapNhatSoLuongSanPhamGioHang param);
        Task<GeneralRespone> CapNhatSoLuongSanPhamGioHang(CapNhatSoLuongParam param); // id gio hang -> moi lan goi tang so luong len 1
        Task<GeneralRespone> XoaSanPhamGioHang(int? id);// id gio hang
        Task<GeneralRespone> ThayDoiTrangThaiGioHang(int? id);// id gio hang
        Task<List<MauSacGioHangResp>> DanhSachMauSacGioHangClient(int? id);// id khach hang
        Task<List<KichThuocGiohangResp>> DanhSachKichThuocGioHangClient(int? id);// id khach hang
    }
}
