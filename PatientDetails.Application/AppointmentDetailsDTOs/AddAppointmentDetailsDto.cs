using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDetails.Application.AppointmentDetailsDTOs
{
    public class AddAppointmentDetailsDto
    {
        public int TokenNumber { get; set; }
        public string? DoctorNmae { get; set; }
        public int PatientId { get; set; }
    }
}
