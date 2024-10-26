﻿using System;
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
        bool FavoritePlace( int placeCode, FavoritePlaceRequest request);
        List<PlacesResponse> GetFavoritePlace();

        #region
        //bool InserirLugar(LugarRequest request);
        //List<LugarResponse> BuscarLugares();
        //List<LugarResponse> BuscarLugaresPorZona(BuscarLugaresPorZonaRequest request);
        #endregion
    }
}
