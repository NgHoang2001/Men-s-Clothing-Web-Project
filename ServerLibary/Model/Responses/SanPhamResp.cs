namespace ServerLibary.Model.Responses
{
    public class SanPhamModel
    {
        public int? Id { get; set; }
        public string? Ten { get; set; }
        public string? MoTa { get; set; }
        public string? ChatLieu { get; set; }
        public string? HuongDanSuDung { get; set; }
        public decimal? DonGia { get; set; }
        public bool? TrangThai { get; set; }
        public List<HinhAnhResp>? UrlImg { get; set; }
    }
    public class SanPhamResp
    {
        public List<SanPhamModel>? SanPham { get; set; }
        public DanhMucResp? DanhMuc { get; set; }
    }
}
