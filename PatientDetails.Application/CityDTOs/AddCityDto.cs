using PatientDetails.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDetails.Application.CityDTOs
{
    public class AddCityDto
    {
     
        public string? Name { get; set; }
        public int StateId { get; set; }       
 
        
    }
}
