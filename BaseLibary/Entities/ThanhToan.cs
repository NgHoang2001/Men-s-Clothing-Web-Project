using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibary.Entities
{
    public class ThanhToan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("DonHang")]
        public int Id_DonHang { get; set; }
        public string Kieu { get; set; } // value: Tiền mặt
        public decimal TongTienChuaThanhToan { get; set; } // tổng số tiền chưa thanh toán
        public decimal TongTienDaThanhToan { get; set; } // tổng số tiền đã thanh toán
        public DateTime? ThoiGianTao { get; set; }
        public bool TrangThai { get; set; } = false; // false: chưa thanh toán || true : đã thanh toán
        public virtual DonHang DonHang { get; set; }
    }
}
