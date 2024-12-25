using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Crud_MVC_EF.Models
{
    public class CityDTOW
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
}
