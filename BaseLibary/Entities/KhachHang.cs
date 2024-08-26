using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibary.Entities
{
    public class KhachHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("TaiKhoan")]
        public int? Id_TaiKhoan { get; set; }
        public bool? GioiTinh { get; set; } = null;
        [MaxLength(200)]
        public string? Ten { get; set; }
        [MaxLength(15)]
        [Column(TypeName = "VARCHAR")]
        public string? Sdt { get; set; } = null;
        [Column(TypeName = "Date")]
        public DateTime? NgaySinh { get; set; }

        public virtual TaiKhoan? TaiKhoan { get; set; }
        public virtual ICollection<DiaChi>? DiaChis { get; set; }
        public virtual ICollection<GioHangChiTiet>? GioHangChiTiets { get; set; }
        public virtual ICollection<DonHang>? DonHangs { get; set; }
    }
}
