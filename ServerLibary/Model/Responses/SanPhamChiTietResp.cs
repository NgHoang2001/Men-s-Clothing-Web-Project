namespace ServerLibary.Model.Responses
{
    public class SanPhamChiTietResp
    {
        public SanPhamModel? SanPham { get; set; }
        public DanhMucResp? DanhMuc { get; set; }
        public List<SanPhamChiTietModel>? SanPhamChiTiet { get; set; }
    }

    public class SanPhamChiTietModel
    {
        public int? Id { get; set; }
        public int? SoLuong { get; set; }
        public MauSacResp? MauSac { get; set; }
        public KichThuocResp? KichThuoc { get; set; }
        public HinhAnhResp? HinhAnhResp { get; set; }
    }

}
