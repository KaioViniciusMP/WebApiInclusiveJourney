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

                var result = categories.Select(categories => new CategoriesResponse
                {
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
                S3Service s3Service = new S3Service("ImagePlacesInclusiveJourney");

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
                    ZoneCategorie = zone.ZoneCategorie,
                    ImageUrl = s3Service.GetUrlFile(zone.ImageName, 24)

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
                S3Service s3Service = new S3Service("ImagePlacesInclusiveJourney");

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
                    ZoneCategorie = zone.ZoneCategorie,
                    ImageUrl = s3Service.GetUrlFile(zone.ImageName, 24)

                }).ToList();

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public RegistrarImagePlaceResponse RegistrarImagePlace(RegistrarImagePlaceRequest request)
        {
            RegistrarImagePlaceResponse response = new RegistrarImagePlaceResponse();

            try
            {
                string imageName = request.ImageName ?? Guid.NewGuid().ToString() + ".jpg";
                S3Service s3Service = new S3Service("ImagePlacesInclusiveJourney");

                using (var imageStream = new MemoryStream(Convert.FromBase64String(request.ImageStream)))
                {
                    s3Service.Upload(imageStream, imageName);
                }

                var place = _ctx.tabPlaces.Where(c => c.Codigo == request.placeCode).FirstOrDefault();

                if (place != null)
                {
                    place.ImageName = imageName;

                    _ctx.tabPlaces.Update(place);
                    _ctx.SaveChanges();

                    var result = new PlacesResponse
                    {
                        ZoneCode = place.ZoneCode,
                        Cep = place.Cep,
                        City = place.City,
                        Codigo = place.Codigo,
                        Complement = place.Complement,
                        Description = place.Description,
                        LocalAssessment = place.LocalAssessment,
                        NameLocal = place.NameLocal,
                        Neighborhood = place.Neighborhood,
                        NumberHome = place.NumberHome,
                        OpeningHours = place.OpeningHours,
                        State = place.State,
                        Street = place.Street,
                        TypeAcessibility = place.TypeAcessibility,
                        ZoneCategorie = place.ZoneCategorie,
                        ImageUrl = s3Service.GetUrlFile(place.ImageName, 24) // Retorna a URL da imagem no S3
                    };

                    response.status = true;
                    response.message = "Foto incluída com sucesso!";
                    response.preview = result;
                }
                else
                {
                    response.status = false;
                    response.message = "Local não encontrado!";
                    response.preview = null;
                }

                return response;
            }
            catch (Exception ex)
            {
                response.status = false;
                response.message = $"Ocorreu algum erro ao incluir a foto!!! err: {ex}";
                response.preview = null;

                return response;
            }
        }

        public bool RegisterPlace(RequestPlace request)
        {
            try
            {
                string imageName = request.ImageName ?? Guid.NewGuid().ToString() + ".jpg";
                S3Service s3Service = new S3Service("ImagePlacesInclusiveJourney");

                using (var imageStream = new MemoryStream(Convert.FromBase64String(request.ImageStream)))
                {
                    s3Service.Upload(imageStream, imageName);
                }

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
                    ZoneCategorie = request.ZoneCategorie,
                    IsFavorite = request.IsFavorite,
                    ImageName = imageName
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

        public bool FavoritePlace(int placeCode, FavoritePlaceRequest request)
        {
            try
            {
                var zones = _ctx.tabPlaces.Where(c => c.Codigo == placeCode).FirstOrDefault();
                if (zones == null)
                    return false;

                zones.IsFavorite = request.isFavorite;

                _ctx.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<PlacesResponse> GetFavoritePlace()
        {
            try
            {
                var zones = _ctx.tabPlaces.Where(c => c.IsFavorite == true).ToList();
                S3Service s3Service = new S3Service("ImagePlacesInclusiveJourney");

                var result = zones.Select(zone => new PlacesResponse
                {
                    ZoneCode = zones.FirstOrDefault().Codigo,
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
                    ZoneCategorie = zone.ZoneCategorie,
                    ImageUrl = s3Service.GetUrlFile(zone.ImageName, 24)

                }).ToList();

                return result;
            }
            catch (Exception)
            {
                return null;
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
