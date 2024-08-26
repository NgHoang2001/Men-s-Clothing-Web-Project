using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BaseLibary.Entities
{
    public class wards
    {
        [Key]
        public string code { get; set; } = null!;
        public string name { get; set; } = null!;
        public string? name_en { get; set; }
        public string full_name { get; set; } = null!;
        public string? full_name_en { get; set; }
        public string? code_name { get; set; }
        [ForeignKey("provinces")]
        public string? district_code { get; set; }
        [ForeignKey("administrative_units")]
        public int? administrative_unit_id { get; set; }

        public virtual administrative_units? Administrative_Units { get; set; }
        public virtual districts? Districts { get; set; }
    }
}
