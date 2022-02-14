using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rehber.Data;
using Rehber.Dtos;
using Rehber.Helpers;
using Rehber.Models;
using System.Collections.Generic;
using System.Linq;

namespace Rehber.Controllers
{
    [EnableCors("CorsApi")]
    [Route("api/cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private IAppRepository _appRepository;
        private IMapper _mapper;
        public CitiesController(IAppRepository appRepository, IMapper mapper)
        {
            _appRepository = appRepository;
            _mapper = mapper;
        }

        public ActionResult GetCities()
        {
            //var cities = _appRepository.GetCities().Select(c => new CityForListDto { Description=c.Description,Name=c.Name,Id=c.Id,PhotoUrl=c.Photos.FirstOrDefault(x=>x.IsMain==true).Url});
            var cities = _appRepository.GetCities();
            var citiesToReturn = _mapper.Map<List<CityForListDto>>(cities);
            foreach(var item in citiesToReturn)
            {
                if(!string.IsNullOrEmpty(item.PhotoUrl))
                item.QrCode = QrGenerator.generateQrCode(item.PhotoUrl);
                //item.QrCode = QrGenerator.generateQrCode(("http://localhost:4200/cityDetail2/" + item.Id.ToString()));

            }
            return Ok(citiesToReturn);
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody] City city)
        {
            _appRepository.Add(city);
            _appRepository.SaveAll();
            return Ok(city);
        }


        [HttpGet]
        [Route("detail")]
        public ActionResult GetCitiesById(int id)
        {
            var city = _appRepository.GetCityById(id);
            var cityToReturn = _mapper.Map<CityForDetailDto>(city);
            return Ok(cityToReturn);
        }

        [HttpGet]
        [Route("photos")]
        public ActionResult GetPhotosByCity(int cityId)
        {
            //var cities = _appRepository.GetCities().Select(c => new CityForListDto { Description=c.Description,Name=c.Name,Id=c.Id,PhotoUrl=c.Photos.FirstOrDefault(p=>p.IsMain==true).Url});
            var photos = _appRepository.GetPhotosByCity(cityId);
            //var cityToReturn = _mapper.Map<CityForDetailDto>(city);
            return Ok(photos);
        }
    }
}
