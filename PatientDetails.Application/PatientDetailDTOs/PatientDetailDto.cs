using PatientDetails.Application.AppointmentDetailsDTOs;
using PatientDetails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDetails.Application.PatientDetailDTOs
{
    public class PatientDetailDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public int CityId { get; set; }
        public string? CityName { get; set; }
        public List<AppointmentDetailsDto> ?appointmentDetails { get; set; }
        public List<CityDTOs.CityStateCountryDto> ?citystatecountry { get; set; }
    }
}
