using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDetails.Domain
{
    public class PatientDetail
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public int CityId { get; set; }
        [ForeignKey("CityId")]

        public City? City { get; set; }
    }
}
