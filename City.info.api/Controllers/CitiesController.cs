using AutoMapper;
using City.info.api.Models;
using City.info.api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace City.info.api.Controllers
{
    [ApiController]
    [Route("api/cities")]
    [Authorize]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository,IMapper mapper)
        { 
            _cityInfoRepository= cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper =mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
         
        //[HttpGet("api/cities")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> GetCities()
        {
            //return Ok( new JsonResult(CitiesDataStore.current.Cities ));
            
            var cities =await _cityInfoRepository.GetCitiesAsync();
            //var result = new List<CityWithoutPointOfInterestDto>();
            //foreach (var c in cities)
            //{
            //    result.Add(new CityWithoutPointOfInterestDto() { 
            //        Id = c.Id,
            //        Description = c.Description,
            //        Name = c.Name,
            //    });
            //}

            var result = _mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cities);

            return Ok(result);

        }

        [HttpGet("{id}",Name = "GetCity")]
        public async Task<IActionResult> GetCity(int id,
            bool includePointOfInterest=true)
        {
            //CitiesDataStore.current.Cities.Where(c => c.Id == id) // dont have null return

            //var res = _CitiesDataStore.Cities.FirstOrDefault(c => c.Id == id);  // have null return
            //if (res == null) {
            //    return NotFound(); 
            //}
            var city = await _cityInfoRepository.GetCityAsync(id, includePointOfInterest);

            if (includePointOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }
            return Ok(_mapper.Map<CityWithoutPointOfInterestDto>(city));   
           
        }
    }
}
