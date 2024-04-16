using PatientDetails.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDetails.Application.CityDTOs
{
    public class CityStateCountryDto
    {
        public int Id { get; set; }
        public string? CityName { get; set; }

        public List<CityDTOs.StateDto> ?States { get; set; }

    }
}
