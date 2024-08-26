namespace ServerLibary.Model.Responses
{
    public class HinhAnhResp
    {
        public int? Id { get; set; }
        public int? Id_SanPham { get; set; }
        public int? Id_DanhMuc { get; set; }
        public int? Id_MauSac { get; set; }
        public int? Id_SanPhamChiTiet { get; set; }
        public string? Url { get; set; }
        public int? Kieu { get; set; }
        public bool IsBanner { get; set; } = false;
    }
}
