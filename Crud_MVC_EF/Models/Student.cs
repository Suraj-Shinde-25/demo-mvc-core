using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crud_MVC_EF.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; } 

        [Required]
        [StringLength(30)]
        public string StudentName { get; set; }
        
        [Required]
        public string Gender { get; set; }
        
        [Required]
        public string Education { get; set; }

        public string? WorkStatus {  get; set; }    

        [Required]
        [StringLength(30)]
        public string StudentEmail { get; set; }

        [Required]
        [StringLength(20)]
        public string StudentPhone { get; set; }

        public int CountryId { get; set; }
        public int CityId { get; set; }

    }
}
