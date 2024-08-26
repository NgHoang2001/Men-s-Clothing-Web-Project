using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibary.Entities
{
    public class SanPham
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("DanhMuc")]
        public int? Id_DanhMuc { get; set; }
        [ForeignKey("DoiTuong")]
        public int? Id_DoiTuong { get; set; }
        //[ForeignKey("TrangThaiSanPham")]
        //public int? Id_TrangThaiSanPham { get; set; }
        public string? Ten { get; set; }
        public string? MoTa { get; set; }
        public string? ChatLieu { get; set; }
        public string? HuongDanSuDung { get; set; }
        public int DonGia { get; set; }
        public bool TrangThai { get; set; } = true;

        public virtual DanhMuc? DanhMuc { get; set; }
        public virtual DoiTuong? DoiTuong { get; set; }
        //public virtual TrangThaiSanPham? TrangThaiSanPham { get; set; }
        public virtual ICollection<HinhAnh>? HinhAnhs { get; set; }
        public virtual ICollection<SanPhamChiTiet>? SanPhamChiTiets { get; set; }
        public virtual ICollection<SanPham_SuKien>? SanPham_SuKiens { get; set; }
    }
}
