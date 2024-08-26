using System.ComponentModel.DataAnnotations;

namespace BaseLibary.DTOs
{
    public class Register : AccountBase
    {
        [Required]
        [DataType(DataType.Password)]
        public string PasswordTry { get; set; } = null!;
    }
}
