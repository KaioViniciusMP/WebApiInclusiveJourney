using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiInclusiveJourney.Application.DTO.Request;
using WebApiInclusiveJourney.Application.DTO.Response;
using WebApiInclusiveJourney.Repository.Models;

namespace WebApiInclusiveJourney.Application.IServices
{
    public interface IPlaceService
    {
        List<CategoriesResponse> GetCategories();
        List<ZonesResponse> GetZones();
        List<PlacesResponse> GetPlacesForZones(int zoneCode);
        List<PlacesResponse> GetPlacesForCategories(int categorieCode);
        bool RegisterPlace(RequestPlace request);
        FavoritePlaceResponse FavoriteAndRemovedPlaceFavorited(FavoriteAndRemovedPlaceFavoritedRequest request);
        List<PlacesResponse> GetFavoritePlace();
        RegistrarImagePlaceResponse RegistrarImagePlace(RegistrarImagePlaceRequest request);
        List<PlacesResponse> GetFavoritePlaceOfUser(GetFavoritePlaceOfUserRequest request);
    }
}
