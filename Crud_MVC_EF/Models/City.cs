using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Crud_MVC_EF.Models
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }
        
        [Required]
        public int CountryId { get; set; }

        [Required]
        [StringLength(20)]
        public string CityName { get; set; }
    }
}
