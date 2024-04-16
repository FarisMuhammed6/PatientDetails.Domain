
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDetails.Application.AppointmentDetailsDTOs
{
    public class AppointmentDetailsDto
    {
        public int Id { get; set; }
        public int TokenNumber { get; set; }
        public string? DoctorNmae { get; set; }
        public int PatientId { get; set; }

        public List<PatientDetailDTOs.PatientDetailDto> ?PatientDetails { get; set; }
       
    }
}
