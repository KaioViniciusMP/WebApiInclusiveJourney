using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiInclusiveJourney.Application.DTO.Request;
using WebApiInclusiveJourney.Application.DTO.Response;
using WebApiInclusiveJourney.Application.IServices;
using WebApiInclusiveJourney.Repository;
using WebApiInclusiveJourney.Repository.Models;

namespace WebApiInclusiveJourney.Application.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly WebApiInclusiveJourneyContext _ctx;
        public PlaceService(WebApiInclusiveJourneyContext context)
        {
            _ctx = context;
        }

        public List<CategoriesResponse> GetCategories()
        {
            try
            {
                var categories = _ctx.tabCategories.ToList();

                var result = categories.Select(categories => new CategoriesResponse{
                    Codigo = categories.Codigo,
                    Name = categories.Name,
                }).ToList();

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<ZonesResponse> GetZones()
        {
            try
            {
                var zones = _ctx.tabZone.ToList();

                var result = zones.Select(zone => new ZonesResponse
                {
                    codigo = zone.Codigo,
                    name = zone.Name,
                }).ToList();

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<PlacesResponse> GetPlacesForZones(int zoneCode)
        {
            try
            {
                var zones = _ctx.tabPlaces.Where(c => c.ZoneCode == zoneCode).ToList();

                var result = zones.Select(zone => new PlacesResponse
                {
                    ZoneCode = zoneCode,
                    Cep = zone.Cep,
                    City = zone.City,
                    Codigo = zone.Codigo,
                    Complement = zone.Complement,
                    Description = zone.Description,
                    LocalAssessment = zone.LocalAssessment,
                    NameLocal = zone.NameLocal,
                    Neighborhood = zone.Neighborhood,
                    NumberHome = zone.NumberHome,
                    OpeningHours = zone.OpeningHours,
                    State = zone.State,
                    Street = zone.Street,
                    TypeAcessibility = zone.TypeAcessibility,
                    ZoneCategorie = zone.ZoneCategorie
                    
                }).ToList();

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public List<PlacesResponse> GetPlacesForCategories(int categorieCode)
        {
            try
            {
                var zones = _ctx.tabPlaces.Where(c => c.ZoneCategorie == categorieCode).ToList();

                var result = zones.Select(zone => new PlacesResponse
                {
                    ZoneCode = categorieCode,
                    Cep = zone.Cep,
                    City = zone.City,
                    Codigo = zone.Codigo,
                    Complement = zone.Complement,
                    Description = zone.Description,
                    LocalAssessment = zone.LocalAssessment,
                    NameLocal = zone.NameLocal,
                    Neighborhood = zone.Neighborhood,
                    NumberHome = zone.NumberHome,
                    OpeningHours = zone.OpeningHours,
                    State = zone.State,
                    Street = zone.Street,
                    TypeAcessibility = zone.TypeAcessibility,
                    ZoneCategorie = zone.ZoneCategorie
                    
                }).ToList();

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool RegisterPlace(RequestPlace request)
        {
            try
            {
                TabPlaces tabPlaces = new TabPlaces
                {
                    Cep = request.Cep,
                    City = request.City,
                    Street = request.Street,
                    Neighborhood = request.Neighborhood,
                    Complement = request.Complement,
                    NameLocal = request.NameLocal,
                    LocalAssessment = request.LocalAssessment,
                    State = request.State,
                    ZoneCode = request.ZoneCode,
                    Description = request.Description,
                    NumberHome = request.NumberHome,
                    OpeningHours = request.OpeningHours,
                    TypeAcessibility = request.TypeAcessibility,
                    ZoneCategorie = request.ZoneCategorie
                };

                _ctx.tabPlaces.Add(tabPlaces); 
                _ctx.SaveChanges(); 
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region
        //public bool InserirLugar(LugarRequest request)
        //{
        //    try
        //    {
        //        var lugar = new TabLugar()
        //        {
        //            bairro = request.bairro,
        //            cep = request.cep,
        //            cidade = request.cidade,
        //            complemento = request.complemento,
        //            dataCadastro = request.dataCadastro,
        //            nome = request.nome,
        //            numero = request.numero,
        //            rua = request.rua,
        //            uf = request.uf,
        //            usuarioCodigo = request.usuarioCodigo,
        //            zona = request.zona,
        //        };

        //        _context.tabLugar.Add(lugar);
        //        _context.SaveChanges();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        //public List<LugarResponse> BuscarLugares()
        //{
        //    try
        //    {
        //        var lugares = _context.tabLugar.ToList();

        //        var lugaresResponse = lugares.Select(lugar => new LugarResponse
        //        {
        //            codigo = lugar.codigo,
        //            nome = lugar.nome,
        //            dataCadastro = lugar.dataCadastro,
        //            rua = lugar.rua,
        //            numero = lugar.numero,
        //            complemento = lugar.complemento,
        //            bairro = lugar.bairro,
        //            cidade = lugar.cidade,
        //            cep = lugar.cep,
        //            uf = lugar.uf,
        //            usuarioCodigo = lugar.usuarioCodigo,
        //            zona = lugar.zona
        //        }).ToList();

        //        return lugaresResponse;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        //public List<LugarResponse> BuscarLugaresPorZona(BuscarLugaresPorZonaRequest request)
        //{
        //    try
        //    {
        //        var lugares = _context.tabLugar.ToList();

        //        var lugaresResponse = lugares.Where(c => c.zona == request.nomeZona).Select(lugar => new LugarResponse
        //        {
        //            codigo = lugar.codigo,
        //            nome = lugar.nome,
        //            dataCadastro = lugar.dataCadastro,
        //            rua = lugar.rua,
        //            numero = lugar.numero,
        //            complemento = lugar.complemento,
        //            bairro = lugar.bairro,
        //            cidade = lugar.cidade,
        //            cep = lugar.cep,
        //            uf = lugar.uf,
        //            usuarioCodigo = lugar.usuarioCodigo,
        //            zona = lugar.zona
        //        }).ToList();

        //        return lugaresResponse;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
        #endregion
    }
}
