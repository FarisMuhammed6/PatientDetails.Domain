using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDetails.Domain
{
    public class City
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int StateId { get; set; }
        [ForeignKey("StateId")]
        public State? State { get; set; }
    }
}
