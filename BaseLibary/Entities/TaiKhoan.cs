using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibary.Entities
{
    public class TaiKhoan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("VaiTro")]
        public int Id_VaiTro { get; set; }
        [MaxLength(100)]
        public string Email { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public bool TrangThai { get; set; } = true;

        public virtual VaiTro? VaiTro { get; set; }
    }
}
