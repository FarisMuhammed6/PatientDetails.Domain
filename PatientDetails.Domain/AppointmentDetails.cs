using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDetails.Domain
{
    public class AppointmentDetails
    {
        public int Id { get; set; }
        public int TokenNumber { get; set; }
        public string? DoctorNmae { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public PatientDetail? PatientDetails { get; set; }
    }
}
