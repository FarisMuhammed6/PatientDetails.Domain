using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientDetails.Application.AppointmentDetailsDTOs;
using PatientDetails.Application.PatientDetailDTOs;
using PatientDetails.Domain;
using PatientDetails.Infrastructure;
using System.Security.Cryptography.Xml;

namespace PatientDetails.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientDetailController : ControllerBase
    {
        private readonly PatientDetailDbContext dbcontext;

        public PatientDetailController(PatientDetailDbContext patientDetailDbContext)
        {
            this.dbcontext = patientDetailDbContext;
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddPatientDetailDto addPatientDetailDto)
        {
            var patientDetailDomain = new PatientDetail
            {
                FirstName = addPatientDetailDto.FirstName,
                LastName = addPatientDetailDto.LastName,
                DateOfBirth = addPatientDetailDto.DateOfBirth,
                Address = addPatientDetailDto.Address,
                Gender = addPatientDetailDto.Gender,
                CityId = addPatientDetailDto.CityId



            };
            dbcontext.Add(patientDetailDomain);
            dbcontext.SaveChanges();

            var patientDetailDto = new PatientDetail
            {
                Id = patientDetailDomain.Id,
                FirstName = patientDetailDomain.FirstName,
                LastName = patientDetailDomain.LastName,
                DateOfBirth = patientDetailDomain.DateOfBirth,
                Address = patientDetailDomain.Address,
                Gender = patientDetailDomain.Gender,
                CityId = patientDetailDomain.CityId
            };
            return CreatedAtAction(nameof(Get), new { id = patientDetailDto.Id }, patientDetailDto);
        }
        /* [HttpGet]
         public IActionResult Get()
         {
             var patientDetailDomain = dbcontext.patientDetails.ToList();
             var patientDetailDto = new List<PatientDetailDto>();

             foreach(var p in patientDetailDomain)
             {
                 patientDetailDto.Add(new PatientDetailDto()
                 {
                     Id=p.Id,
                     FirstName = p.FirstName,
                     LastName = p.LastName,
                     DateOfBirth = p.DateOfBirth,
                     Address = p.Address,
                     Gender = p.Gender,
                     CityId = p.CityId

                 });                
             }
             return Ok(patientDetailDto);
         }*/

        [HttpGet]
        public IActionResult Get()
        {
            var cities = dbcontext.cities.ToList();
            var patientDetails = dbcontext.patientDetails.ToList();

            var patientDetailDto = new List<PatientDetailDto>();

            foreach (var patientDetail in patientDetails)
            {
                var city = cities.FirstOrDefault(c => c.Id == patientDetail.CityId);


                if (city != null)
                {
                    patientDetailDto.Add(new PatientDetailDto()
                    {
                        Id = patientDetail.Id,
                        FirstName = patientDetail.FirstName,
                        LastName = patientDetail.LastName,
                        DateOfBirth = patientDetail.DateOfBirth,
                        Address = patientDetail.Address,
                        Gender = patientDetail.Gender,
                        CityId = patientDetail.CityId,
                        CityName = city.Name
                    });
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok(patientDetailDto);
        }
        [HttpPut]
        [Route("{Id}")]
        public IActionResult Update([FromRoute] int Id, UpdatePatientDetailsDto updatePatientDetailsDto)
        {
            var patientDetails = dbcontext.patientDetails.Find(Id);
            if (patientDetails == null)
            {
                return NotFound();
            }
            patientDetails.FirstName = updatePatientDetailsDto.FirstName;
            patientDetails.LastName = updatePatientDetailsDto.LastName;
            patientDetails.DateOfBirth = updatePatientDetailsDto.DateOfBirth;
            patientDetails.Gender = updatePatientDetailsDto.Gender;
            patientDetails.Address = updatePatientDetailsDto.Address;
            dbcontext.SaveChanges();

            return Ok(patientDetails);
        }
        [HttpDelete]
        [Route("{Id}")]
        public IActionResult Delete([FromRoute] int Id)
        {
            var patientDetail = dbcontext.patientDetails.Find(Id);
            if (patientDetail == null)
            {
                return NotFound();
            }


            dbcontext.patientDetails.Remove(patientDetail);

            dbcontext.SaveChanges();

            return Ok(patientDetail);
        }
        [HttpGet("Get Appointments")]
        public IActionResult GetAllAppointments()
        {
            var patientDetailDto = (
                from patientDetail in dbcontext.patientDetails.ToList()
                join appointDetail in dbcontext.appointmentDetails.ToList() on patientDetail.Id equals appointDetail.PatientId into appointmentGroup
                select new PatientDetailDto
                {
                    Id = patientDetail.Id,
                    FirstName = patientDetail.FirstName,
                    LastName = patientDetail.LastName,
                    Gender = patientDetail.Gender,
                    DateOfBirth = patientDetail.DateOfBirth,
                    Address = patientDetail.Address,
                    appointmentDetails = appointmentGroup.Select(appointment => new AppointmentDetailsDto
                    {
                        Id = appointment.Id,
                        TokenNumber = appointment.TokenNumber,
                        DoctorNmae = appointment.DoctorNmae
                    }).ToList()
                });

            return Ok(patientDetailDto);
        }
        /*[HttpGet("By Cities")]
        public IActionResult GetPatientDetails()
        {
            var patiendtls = (
            from PatientDetails in dbcontext.patientDetails.ToList()
            join cities in dbcontext.cities.ToList() on PatientDetails.CityId equals cities.Id

            select new PatientDetailDto
            {
                Id = PatientDetails.Id,
                FirstName = PatientDetails.FirstName,
                LastName = PatientDetails.LastName,
                Gender = PatientDetails.Gender,
                DateOfBirth = PatientDetails.DateOfBirth,
                Address = PatientDetails.Address,
                CityName = cities.Name,
                citystatecountry = new List<Application.CityDTOs.CityStateCountryDto>
                {
                    new Application.CityDTOs.CityStateCountryDto
                    {
                        Id= cities.Id,
                        CityName = cities.Name
                    }
                }
            });
            return Ok(patiendtls);
        }*/
    }
}