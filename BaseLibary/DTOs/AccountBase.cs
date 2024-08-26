using System.ComponentModel.DataAnnotations;

namespace BaseLibary.DTOs
{
    public class AccountBase
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
