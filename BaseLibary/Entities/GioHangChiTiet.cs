using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibary.Entities
{
    public class GioHangChiTiet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("KhachHang")]
        public int? Id_KhachHang { get; set; }
        [ForeignKey("SanPhamChiTiet")]
        public int? Id_SanPhamChiTiet { get; set; }
        [ForeignKey("TrangThaiGioHang")]
        public int? Id_TrangThaiGioHang { get; set; }
        public int SoLuong { get; set; }
        public bool IsChon { get; set; } = true;

        public virtual KhachHang? KhachHang { get; set; }
        public virtual SanPhamChiTiet? SanPhamChiTiet { get; set; }
        public virtual TrangThaiGioHang? TrangThaiGioHang { get; set; }
    }
}
