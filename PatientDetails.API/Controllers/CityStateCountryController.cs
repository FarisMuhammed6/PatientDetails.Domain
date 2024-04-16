using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientDetails.Application.CityDTOs;
using PatientDetails.Domain;
using PatientDetails.Infrastructure;

namespace PatientDetails.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityStateCountryController : ControllerBase
    {
        private readonly PatientDetailDbContext dbContext;

        public CityStateCountryController(PatientDetailDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        public IActionResult Create([FromBody] AddCityDto addCityDto)
        {
            var city = new City
            {

                Name = addCityDto.Name,
                StateId = addCityDto.StateId
            };
            dbContext.cities.Add(city);
            dbContext.SaveChanges();

            var cityDto = new AddCityDto
            {

                Name = city.Name,
                StateId = city.StateId
            };
            return Ok(cityDto);
        }
        /*
                [HttpPost("tttt")]
                public List<Domain.City> createcity(City city)
                {
                    dbContext.cities.Add(city);
                    dbContext.SaveChanges();
                    return dbContext.cities.ToList();
                }*/


        [HttpGet]
        public IActionResult Get()
        {
            /* var cityDomain = dbContext.cities.ToList();
             var stateDomain = dbContext.states.ToList();
             var countryDomain = dbContext.countries.ToList();
             var combinedData = (cityDomain, stateDomain, countryDomain);*/
            /*  var combinedData = new
              {
                  Cities = dbContext.cities.ToList(),
                  States = dbContext.states.ToList(),
                  Countries = dbContext.countries.ToList()
              };
              var cityStateCountryDto = new List<CityStateCountryDto>();
              foreach (var city in combinedData)
              {
                  cityStateCountryDto.Add(new CityStateCountryDto
                  {
                      Id = city.Id,
                      CityName = city.Name,
                      StateName = city.



                  });
              }*/
            var combinedData = new
            {
                Cities = dbContext.cities.ToList(),
                States = dbContext.states.ToList(),
                Countries = dbContext.countries.ToList()
            };

            var cityStateCountryDto = new List<CityStateCountryDto>();

            foreach (var city in combinedData.Cities)
            {
               
                var state = combinedData.States.FirstOrDefault(s => s.Id == city.StateId);
               
                var country = combinedData.Countries.FirstOrDefault(c => c.Id == state.CountryId);

                cityStateCountryDto.Add(new CityStateCountryDto
                {
                    Id = city.Id,
                    CityName = city.Name

                });
            }
            return Ok(cityStateCountryDto);
        }
        [HttpPut]
        [Route("{Id}")]
        public IActionResult Update([FromRoute] int Id, UpdateCityDto updateCityDto)
        {
            var city = dbContext.cities.Find(Id);
            if (city == null)
            {
                return NotFound();
            }
            city.Name = updateCityDto.Name;
            city.StateId = updateCityDto.StateId;
            dbContext.SaveChanges();

            return Ok(city);

        }
        [HttpDelete]
        [Route("{Id}")]
        public IActionResult Delete([FromRoute] int Id)
        {
            var city = dbContext.cities.Find(Id);
            if (city == null)
            {
                return NotFound();
            }
            dbContext.cities.Remove(city);
            dbContext.SaveChanges();
            return Ok(city);
        }
    }
}
