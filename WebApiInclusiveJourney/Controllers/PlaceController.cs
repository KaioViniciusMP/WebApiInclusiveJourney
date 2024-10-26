using Microsoft.AspNetCore.Mvc;
using WebApiInclusiveJourney.Application.DTO.Request;
using WebApiInclusiveJourney.Application.DTO.Response;
using WebApiInclusiveJourney.Application.IServices;
using WebApiInclusiveJourney.Repository.Models;

namespace WebApiInclusiveJourney.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceService _placeService;
        public PlaceController(IPlaceService service)
        {
            _placeService = service;
        }

        //GetCategories - List<tabCategories>
        [HttpGet("/place/categories")]
        public IActionResult GetCategories()
        {
            var response = _placeService.GetCategories();
            if (response.Count < 1)
                return NoContent();
            else
                return Ok(response);
        }

        //GetZones- List<tabZone>
        [HttpGet("/place/zones")]
        public IActionResult GetZones()
        {
            var response = _placeService.GetZones();
            if (response.Count < 1)
                return NoContent();
            else
                return Ok(response);
        }


        //GetPlacesForZones - tabPlaces
        //GetPlacesForCategories - tabPlaces


        #region
        //[HttpPost]
        //public IActionResult InserirLugar([FromBody] LugarRequest request)
        //{
        //    bool resposta = _lugarServices.InserirLugar(request);
        //    if (resposta == false)
        //        return BadRequest();
        //    else
        //        return Ok();
        //}
        //[HttpGet]
        //public IActionResult BuscarLugares()
        //{
        //    List<LugarResponse> resposta = _lugarServices.BuscarLugares();
        //    if (resposta == null)
        //        return NoContent();
        //    else
        //        return Ok(resposta);
        //}
        //[HttpPost("/BuscarLugaresPorZona")]
        //public IActionResult BuscarLugaresPorZona(BuscarLugaresPorZonaRequest request)
        //{
        //    List<LugarResponse> resposta = _lugarServices.BuscarLugaresPorZona(request);
        //    if (resposta == null)
        //        return NoContent();
        //    else
        //        return Ok(resposta);
        //}
        #endregion
    }
}
