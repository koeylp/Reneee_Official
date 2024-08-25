using System.ComponentModel.DataAnnotations;

namespace Reneee.Domain.Entities
{
    public class Province
    {
        [Key]
        public string code { get; set; }
        public string full_name_en { get; set; }
    }
}
