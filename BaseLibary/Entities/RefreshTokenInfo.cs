using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibary.Entities
{
    public class RefreshTokenInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Token { get; set; }
        // id tai khoan khach hang
        public int Id_KhachHang { get; set; }
    }

}
