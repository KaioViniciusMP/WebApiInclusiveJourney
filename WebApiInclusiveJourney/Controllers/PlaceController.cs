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
        [HttpGet("/place/placesForZones/{zoneCode}")]
        public IActionResult GetPlacesForZones(int zoneCode)
        {
            var response = _placeService.GetPlacesForZones(zoneCode);
            if (response.Count < 1)
                return NoContent();
            else
                return Ok(response);
        }

        //GetPlacesForCategories - tabPlaces
        [HttpGet("/place/placesForCategories/{categorieCode}")]
        public IActionResult GetPlacesForCategories(int categorieCode)
        {
            var response = _placeService.GetPlacesForCategories(categorieCode);
            if (response.Count < 1)
                return NoContent();
            else
                return Ok(response);
        }
        
        [HttpPost("/place/registerPlace")]
        public IActionResult RegisterPlace(RequestPlace request)
        {
            var response = _placeService.RegisterPlace(request);
            if (response == false)
                return BadRequest();
            else
                return Ok();
        }

        [HttpPost("/place/FavoriteAndRemovedPlaceFavorited")]
        public IActionResult FavoriteAndRemovedPlaceFavorited([FromBody] FavoriteAndRemovedPlaceFavoritedRequest request)
        {
            var response = _placeService.FavoriteAndRemovedPlaceFavorited(request);
            if (response.status == false)
                return BadRequest(response.description);
            else
                return Ok(response.description);
        }
        
        [HttpGet("/place/getFavoritePlace")]
        public IActionResult GetFavoritePlace()
        {
            var response = _placeService.GetFavoritePlace();
            if (response.Count < 1)
                return NoContent();
            else
                return Ok(response);
        }
        
        [HttpPost("/place/RegistrarImagePlace")]
        public IActionResult RegistrarImagePlace(RegistrarImagePlaceRequest request)
        {
            RegistrarImagePlaceResponse response = _placeService.RegistrarImagePlace(request);
            if (response.status != true)
                return BadRequest(response);
            else
                return Ok(response);
        }
        
        [HttpPost("/place/GetFavoritePlaceOfUser")]
        public IActionResult GetFavoritePlaceOfUser(GetFavoritePlaceOfUserRequest request)
        {
            List<PlacesResponse> response = _placeService.GetFavoritePlaceOfUser(request);
            if (response.Count > 0)
                return Ok(response);
            else
                return BadRequest(response);
        }

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
