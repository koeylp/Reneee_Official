using System.ComponentModel.DataAnnotations;

namespace Reneee.Domain.Entities
{
    public class District
    {
        [Key]
        public string code { get; set; }
        public string full_name_en { get; set; }
        public string province_code { get; set; }
    }
}
