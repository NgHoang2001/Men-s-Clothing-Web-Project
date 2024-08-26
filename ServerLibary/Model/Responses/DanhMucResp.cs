namespace ServerLibary.Model.Responses
{
    public class DanhMucChild
    {
        public int? Id { get; set; }
        public string? Ten { get; set; }
        public int? Id_DanhMuc_Cha { get; set; }
        public HinhAnhResp? HinhAnh { get; set; }
        public HinhAnhResp? HinhAnhBanner { get; set; }

    }
    public class DanhMucResp
    {
        public int? Id { get; set; }
        public string? Ten { get; set; }
        public int? Id_DanhMuc_Cha { get; set; }
        public List<DanhMucChild>? DanhMucChildren { get; set; }
        public HinhAnhResp? HinhAnh { get; set; }
        public HinhAnhResp? HinhAnhBanner { get; set; }
    }
}
