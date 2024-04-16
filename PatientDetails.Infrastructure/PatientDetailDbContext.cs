using Microsoft.EntityFrameworkCore;
using PatientDetails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDetails.Infrastructure
{
    public class PatientDetailDbContext : DbContext
    {
        public PatientDetailDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<PatientDetail> patientDetails { get; set; }
        public DbSet<AppointmentDetails> appointmentDetails { get; set; }
        public DbSet<City> cities { get; set; }
        public DbSet<State> states { get; set; }
        public DbSet<Country> countries { get; set; }
    }
}
