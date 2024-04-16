using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDetails.Application.PatientDetailDTOs
{
    public class UpdatePatientDetailsDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
    }
}
