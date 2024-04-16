using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientDetails.Application.AppointmentDetailsDTOs;
using PatientDetails.Application.CityDTOs;
using PatientDetails.Application.PatientDetailDTOs;
using PatientDetails.Domain;
using PatientDetails.Infrastructure;

namespace PatientDetails.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentDetailsController : ControllerBase
    {
        private readonly PatientDetailDbContext dbcontext;

        public AppointmentDetailsController(PatientDetailDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        /* [HttpGet]
         public IActionResult Get()
         {
             var appointmentDomain = dbcontext.appointmentDetails.ToList(); 

             var appointmentDetailsDto = new List<AppointmentDetailsDto>();
             foreach(var d in appointmentDomain)
             {
                 appointmentDetailsDto.Add(new AppointmentDetailsDto()
                 {
                     Id = d.Id,
                     TokenNumber = d.TokenNumber,
                     DoctorNmae = d.DoctorNmae,
                     PatientId = d.PatientId,
                 });
             }
             return Ok(appointmentDetailsDto);
         }*/
        [HttpPost]
        public IActionResult Create([FromBody] AddAppointmentDetailsDto addAppointmentDetailsDto)
        {
            var appontmentDetailsDomain = new AppointmentDetails
            {
                TokenNumber = addAppointmentDetailsDto.TokenNumber,
                DoctorNmae = addAppointmentDetailsDto.DoctorNmae,
                PatientId = addAppointmentDetailsDto.PatientId
            };
            dbcontext.appointmentDetails.Add(appontmentDetailsDomain);
            dbcontext.SaveChanges();
            var appointmentdetailsDto = new AppointmentDetailsDto
            {
                Id = appontmentDetailsDomain.TokenNumber,
                TokenNumber = appontmentDetailsDomain.TokenNumber,
                DoctorNmae = appontmentDetailsDomain.DoctorNmae,
                PatientId = appontmentDetailsDomain.PatientId

            };
            return CreatedAtAction(nameof(Get), new { Id = appointmentdetailsDto.Id }, appointmentdetailsDto);
        }
        /*[HttpGet]
        public IActionResult Get()
        {
            var patientDetails = dbcontext.patientDetails.ToList();
            var appointmentDetails = dbcontext.appointmentDetails.ToList();
            var appointmentDetailDto = new List<AppointmentDetailsDto>();

            foreach (var appointmentDetail in appointmentDetails)
            {
                var patientDetail = patientDetails.FirstOrDefault(p => p.Id == appointmentDetail.PatientId);
                if (patientDetail != null)
                {
                    appointmentDetailDto.Add(new AppointmentDetailsDto()
                    {
                        Id = appointmentDetail.Id,
                        PatientId = appointmentDetail.PatientId,
                        DoctorNmae = appointmentDetail.DoctorNmae,
                        TokenNumber = appointmentDetail.TokenNumber                       
                    });
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok(appointmentDetailDto);
        }*/
        [HttpGet]
        public IActionResult Get()
        {
                var appointmentDetailDto = (
                from appointmentDetail in dbcontext.appointmentDetails.ToList()
                join patientDetail in dbcontext.patientDetails.ToList() on appointmentDetail.PatientId equals patientDetail.Id
                join citystatecountryD in dbcontext.cities.ToList() on patientDetail.CityId equals citystatecountryD.Id
                join statess in dbcontext.states.ToList() on citystatecountryD.StateId equals statess.Id
                join countries in dbcontext.countries.ToList() on statess.CountryId equals countries.Id
                
                select new AppointmentDetailsDto
                {
                    Id = appointmentDetail.Id,
                    TokenNumber = appointmentDetail.TokenNumber,
                    DoctorNmae = appointmentDetail.DoctorNmae,
                    PatientId = appointmentDetail.PatientId,
                    PatientDetails = new List<Application.PatientDetailDTOs.PatientDetailDto>
                   {
                new Application.PatientDetailDTOs.PatientDetailDto
                {
                   
                    Id = patientDetail.Id,
                    FirstName = patientDetail.FirstName,
                    LastName = patientDetail.LastName,                   
                    Gender = patientDetail.Gender,
                    DateOfBirth = patientDetail.DateOfBirth,
                    Address = patientDetail.Address,
                    citystatecountry = new List<CityStateCountryDto>
                    {
                        new Application.CityDTOs.CityStateCountryDto
                        {
                            Id = citystatecountryD.Id,
                            CityName = citystatecountryD.Name,
                            States = new List<StateDto>
                            {
                                new Application.CityDTOs.StateDto
                                {
                                    Id = statess.Id,
                                    StateName = statess.Name,
                                    CountryList = new List<CountryDto>
                                    {
                                        new Application.CityDTOs.CountryDto
                                        {
                                           Id = countries.Id,
                                           Name = countries.Name
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                    }
                }
            ).ToList();

            return Ok(appointmentDetailDto);
        }
        [HttpPut]
        [Route("{Id}")]
        public IActionResult Update([FromRoute]int Id, UpdateAppointmentDetailsDto updateAppointmentDetailsDto)
        {
            var appointmentDetails = dbcontext.appointmentDetails.Find(Id);
            if (appointmentDetails == null)
            {
                return NotFound();
               
            }
            appointmentDetails.DoctorNmae = updateAppointmentDetailsDto.DoctorNmae;
            dbcontext.SaveChanges();

            return Ok(appointmentDetails);
        }
        [HttpDelete]
        [Route("{Id}")]
        public IActionResult Delete([FromRoute] int Id)
        {
            var appointmentDetail = dbcontext.appointmentDetails.Find(Id);
            if (appointmentDetail == null)
            {
                return NotFound();
            }


            dbcontext.appointmentDetails.Remove(appointmentDetail);

            dbcontext.SaveChanges();

            return Ok(appointmentDetail);
        }
       

    }
}
