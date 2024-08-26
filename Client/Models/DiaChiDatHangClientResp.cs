namespace Client.Models
{
    public class DiaChiDatHangClientResp
    {
        public int? Id { get; set; }
        public string? Url { get; set; } // Địa chỉ chuỗi : xã, phường, tp
        public string? DiaChiChiTiet { get; set; } // số nhà ...
        public string? TenNguoiNhan { get; set; }
        public string? Province_code { get; set; }
        public string? District_code { get; set; }
        public string? Ward_code { get; set; }
        public string? GhiChu { get; set; }
        public string? Email { get; set; }
        public string? Sdt { get; set; }
        public bool? IsDefault { get; set; }
        public bool IsChon { get; set; } = false;
    }
}
