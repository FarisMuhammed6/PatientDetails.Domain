using PatientDetails.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDetails.Application.PatientDetailDTOs
{
    public class AddPatientDetailDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public int CityId { get; set; }
        
    }
}
