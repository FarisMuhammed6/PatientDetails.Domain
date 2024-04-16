using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDetails.Application.CityDTOs
{
    public class StateDto
    {
        public int Id { get; set; }
        public string ?StateName { get; set; }
        public List<CountryDto> CountryList { get; set; }

    }
}
