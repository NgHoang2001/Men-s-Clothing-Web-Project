using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibary.Entities
{
    public class SanPhamChiTiet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("SanPham")]
        public int Id_SanPham { get; set; }
        [ForeignKey("MauSac")]
        public int? Id_MauSac { get; set; }
        [ForeignKey("KichThuoc")]
        public int? Id_KichThuoc { get; set; }
        public int SoLuong { get; set; }
        public bool TrangThai { get; set; } = true;

        public virtual SanPham? SanPham { get; set; }
        public virtual MauSac? MauSac { get; set; }
        public virtual KichThuoc? KichThuoc { get; set; }
    }
}
