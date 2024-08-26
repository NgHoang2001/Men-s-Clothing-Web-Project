using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibary.Entities
{
    public class HinhAnh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("SanPham")]
        public int? Id_SanPham { get; set; }
        [ForeignKey("DanhMuc")]
        public int? Id_DanhMuc { get; set; }
        public int? Id_SanPhamChiTiet { get; set; }
        public int? Id_MauSac { get; set; }
        [MaxLength(100)]
        [Column(TypeName = "VARCHAR")]
        public string? Url { get; set; }
        public int Kieu { get; set; } // 1: banner || 2: color || 3: logo
        public bool isBanner { get; set; } = false;
        public virtual SanPham? SanPham { get; set; }
        public virtual DanhMuc? DanhMuc { get; set; }
    }
}
