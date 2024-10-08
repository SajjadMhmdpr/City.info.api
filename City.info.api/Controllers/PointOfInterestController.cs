using AutoMapper;
using City.info.api.Entities;
using City.info.api.Models;
using City.info.api.Repositories;
using City.info.api.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace City.info.api.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointOfInterest")]
    //[Route("api/cities/pointOfInterest")]
    public class PointOfInterestController : ControllerBase
    {
        private readonly ILogger<PointOfInterestController> _Logger;
        private readonly IMailService _MailService;
        private readonly ICityInfoRepository _CityInfoRepository;
        private readonly IMapper _Mapper;

        public PointOfInterestController(
            ILogger<PointOfInterestController> logger,
            IMailService mailService,
            ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _MailService = mailService ?? throw new ArgumentNullException();
            _CityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException();
            _Mapper = mapper ?? throw new ArgumentNullException();
        }

        [HttpGet("getAllPoint")]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetAllPointOfInterest()
        {
            //var res = _CitiesDataStore.Cities.Select(c => c.PointOfInterest).ToList();
            //if (!res.Any())
            //{
            //    return NotFound();
            //}
            return Ok();

        }
        //[HttpGet("cityId")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointOfInterest(int cityId)
        {
            if (!await _CityInfoRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }
            var points = await _CityInfoRepository.GetPointsOfInterestForCityAsync(cityId);
           
            return Ok( _Mapper.Map<IEnumerable<PointOfInterestDto>>(points) );

        }

        [HttpGet("{pointId}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointId)
        {

            if (!await _CityInfoRepository.CityExistAsync(cityId))
            {
                _Logger.LogInformation($"city with id {cityId} wasnt exist");
                return NotFound();
            }

            var point =await _CityInfoRepository.GetPointOfInterestForCityAsync(cityId,pointId);

            if (point == null)
            {
                _Logger.LogInformation($"PointOfInterest with id {pointId} wasnt exist");
                return NotFound();
            }

            return Ok(_Mapper.Map<PointOfInterestDto>(point));

        }


        #region Post

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(
         int cityId,
         PointOfInterestForCreateDto pointOfInterest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!await _CityInfoRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }

            var finalPoint = _Mapper.Map<PointOfInterest>(pointOfInterest);

            await _CityInfoRepository.AddPointOfInterestAsync(cityId, finalPoint);
            await _CityInfoRepository.SaveChangesAsync();

            var createdPoint = _Mapper.Map<PointOfInterestDto>(finalPoint);

            return CreatedAtAction("GetPointOfInterest", new
            {
                cityId = cityId,
                pointId = finalPoint.Id
            },
          createdPoint
          );

            //var maxPointOfInterestId = _CitiesDataStore.Cities.SelectMany(c => c.PointOfInterest).Max(p => p.Id);




        }

        #endregion

        #region Put

        [HttpPut("{pointOfInterestId}")]
        public async Task<ActionResult<PointOfInterestDto>> UpdatePointOfInterest(
            int cityId,
            int pointOfInterestId,
            PointOfInterestForUpdateDto pointOfInterest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //find city
            //var city = _CitiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null) { 
            //    return NotFound();
            //}
            if (!await _CityInfoRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }

            //find point of interest
            if (!await _CityInfoRepository.PointOfInterestExistAsync(cityId,pointOfInterestId))
            {
                return NotFound();
            }

            var point = _Mapper.Map<PointOfInterest>(pointOfInterest);
            var finalPoint = await _CityInfoRepository.UpdatePointOfInterestAsync(cityId,pointOfInterestId, point);
            await _CityInfoRepository.SaveChangesAsync();

            var updatedPoint = _Mapper.Map<PointOfInterestDto>(finalPoint);
            //_Mapper.Map(finalPoint,updatedPoint);

            return RedirectToAction("GetPointOfInterest", new {
                cityId = cityId,
                pointId = finalPoint.Id
            });

        }

        #endregion

        #region Edit with patch
        [HttpPatch("{pointOfInterestId}")]
        public async Task<ActionResult<PointOfInterestDto>> PartiallyUpdatePointOfInterest(
            int cityId,
            int pointOfInterestId,
            JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            //find city
            if (! await _CityInfoRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }

            //find point of interest
            var pointFromStore = await _CityInfoRepository.GetPointOfInterestForCityAsync(cityId,pointOfInterestId);
            if (pointFromStore == null)
            {
                return NotFound();
            }

            var pointToPatch = _Mapper.Map<PointOfInterestForUpdateDto>(pointFromStore);

            patchDocument.ApplyTo(pointToPatch,ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!TryValidateModel(pointToPatch)) 
            {
                return BadRequest(ModelState);
            }

            _Mapper.Map(pointToPatch, pointFromStore);
            await _CityInfoRepository.SaveChangesAsync();

            return RedirectToAction("GetPointOfInterest", new
            {
                cityId = cityId,
                pointId = pointFromStore.Id
            });

        }
        #endregion

        #region Delete
        [HttpDelete("{pointOfInterestId}")]
        public async Task<ActionResult> DeletePointOfInterest(int cityId,int pointOfInterestId) 
        {
            //find city
            if (! await _CityInfoRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }

            //find point of interest
            var pointEntity = await _CityInfoRepository.GetPointOfInterestForCityAsync(cityId,
                pointOfInterestId);
            if (pointEntity == null)
            {
                return NotFound();
            }

            _CityInfoRepository.DeletePointOfInterestAsync(pointEntity);
            await _CityInfoRepository.SaveChangesAsync();
            

            _MailService.Send(
                    $"Delete an point of interest",
                    $"point of interest with name:{pointEntity.Name} and id:{pointOfInterestId} is deleted"
                );

            //return NoContent();
            return RedirectToAction("GetCity","Cities", new
            {
                id = cityId,
            });


        }
        #endregion

    }
}
