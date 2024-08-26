using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibary.Entities
{
    public class SuKien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Ten { get; set; }
        public int Kieu { get; set; } = 1; // 1: phần trăm
        public int GiaTri { get; set; }
        public bool TrangThai { get; set; } = true;
        public DateTime ThoiGianTao { get; set; } = DateTime.Now;

        public virtual ICollection<SanPham_SuKien> Sukien_SanPhams { get; set; }
    }
}
