using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibary.Entities
{
    public class DiaChi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("KhachHang")]
        public int Id_KhachHang { get; set; }
        public string? Url { get; set; }
        public string? Province_code { get; set; }
        public string? District_code { get; set; }
        public string? Ward_code { get; set; }
        public string? DiaChiChiTiet { get; set; }
        public string? GhiChu { get; set; }
        public string? Email { get; set; }
        public string TenNguoiNhan { get; set; } = null!;
        public string Sdt { get; set; } = null!;
        public bool IsDefalut { get; set; } = false;
        public virtual KhachHang? KhachHang { get; set; }

    }
}
