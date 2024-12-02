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
                //var zones = _ctx.tabPlaces.Where(c => c.ZoneCode == zoneCode).ToList();
                var zones = (from tp in _ctx.tabPlaces
                             join tpf_pc in _ctx.TabPlaceFavorite_PersonCode
                             on tp.Codigo equals tpf_pc.PlacesCode into favorites // Join com a possibilidade de não ter correspondência
                             from favorite in favorites.DefaultIfEmpty() // Permite valores nulos no resultado
                             where tp.ZoneCode == zoneCode // Filtra pela zona
                             select new
                             {
                                 TabPlaceFavorite_PersonCode = favorite, // Pode ser nulo
                                 tabPlaces = tp
                             }).ToList();

                S3Service s3Service = new S3Service("ImagePlacesInclusiveJourney");

                var result = zones.Select(zone => new PlacesResponse
                {
                    ZoneCode = zoneCode,
                    Cep = zone.tabPlaces.Cep,
                    City = zone.tabPlaces.City,
                    Codigo = zone.tabPlaces.Codigo,
                    Complement = zone.tabPlaces.Complement,
                    Description = zone.tabPlaces.Description,
                    LocalAssessment = zone.tabPlaces.LocalAssessment,
                    NameLocal = zone.tabPlaces.NameLocal,
                    Neighborhood = zone.tabPlaces.Neighborhood,
                    NumberHome = zone.tabPlaces.NumberHome,
                    OpeningHours = zone.tabPlaces.OpeningHours,
                    State = zone.tabPlaces.State,
                    Street = zone.tabPlaces.Street,
                    IsFavorite = zone.TabPlaceFavorite_PersonCode != null,
                    TypeAcessibility = zone.tabPlaces.TypeAcessibility,
                    ZoneCategorie = zone.tabPlaces.ZoneCategorie,
                    ImageUrl = s3Service.GetUrlFile(zone.tabPlaces.ImageName, 24)

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
                //var zones = _ctx.tabPlaces.Where(c => c.ZoneCategorie == categorieCode).ToList();
                var zones = (from tp in _ctx.tabPlaces
                             join tpf_pc in _ctx.TabPlaceFavorite_PersonCode
                             on tp.Codigo equals tpf_pc.PlacesCode into favorites // Join com a possibilidade de não ter correspondência
                             from favorite in favorites.DefaultIfEmpty() // Permite valores nulos no resultado
                             where tp.ZoneCategorie == categorieCode // Filtra pela zona
                             select new
                             {
                                 TabPlaceFavorite_PersonCode = favorite, // Pode ser nulo
                                 tabPlaces = tp
                             }).ToList();
                S3Service s3Service = new S3Service("ImagePlacesInclusiveJourney");

                var result = zones.Select(zone => new PlacesResponse
                {
                    ZoneCode = categorieCode,
                    Cep = zone.tabPlaces.Cep,
                    City = zone.tabPlaces.City,
                    Codigo = zone.tabPlaces.Codigo,
                    Complement = zone.tabPlaces.Complement,
                    Description = zone.tabPlaces.Description,
                    LocalAssessment = zone.tabPlaces.LocalAssessment,
                    NameLocal = zone.tabPlaces.NameLocal,
                    Neighborhood = zone.tabPlaces.Neighborhood,
                    NumberHome = zone.tabPlaces.NumberHome,
                    OpeningHours = zone.tabPlaces.OpeningHours,
                    State = zone.tabPlaces.State,
                    Street = zone.tabPlaces.Street,
                    IsFavorite = zone.TabPlaceFavorite_PersonCode != null,
                    TypeAcessibility = zone.tabPlaces.TypeAcessibility,
                    ZoneCategorie = zone.tabPlaces.ZoneCategorie,
                    ImageUrl = s3Service.GetUrlFile(zone.tabPlaces.ImageName, 24)

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
                    ImageName = imageName,
                    IsFavorite = request.IsFavorite,
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

        public FavoritePlaceResponse FavoriteAndRemovedPlaceFavorited(FavoriteAndRemovedPlaceFavoritedRequest request)
        {
            FavoritePlaceResponse response = new FavoritePlaceResponse();

            try
            {
                var place = _ctx.TabPlaceFavorite_PersonCode.Where(c => c.PersonCode == request.PersonCode && c.PlacesCode == request.PlacesCode).FirstOrDefault();
                if (place != null)
                {
                    _ctx.Remove(place);
                    _ctx.SaveChanges();

                    response.status = true;
                    response.description = "place favorite successfully removed!";
                    return response;
                }


                TabPlaceFavorite_PersonCode placeFavorite_PersonCode = new TabPlaceFavorite_PersonCode
                {
                    PersonCode = request.PersonCode,
                    PlacesCode = request.PlacesCode
                };

                _ctx.TabPlaceFavorite_PersonCode.Add(placeFavorite_PersonCode);
                _ctx.SaveChanges();

                response.status = true;
                response.description = "insert favorite place successfully!";

                return response;
            }
            catch (Exception)
            {
                response.status = false;
                response.description = "Error removed favorite place!";
                return response;
            }
        }

        public List<PlacesResponse> GetFavoritePlace()
        {
            try
            {
                S3Service s3Service = new S3Service("ImagePlacesInclusiveJourney");

                var favoritePlaces = (from tpfpc in _ctx.TabPlaceFavorite_PersonCode
                             join tpls in _ctx.tabPlaces on tpfpc.PlacesCode equals tpls.Codigo
                             join tp in _ctx.tabPerson on tpfpc.PersonCode equals tp.Codigo
                             select new PlacesResponse
                             {
                                 ZoneCode = tpls.Codigo,
                                 Cep = tpls.Cep,
                                 City = tpls.City,
                                 Codigo = tpls.Codigo,
                                 Complement = tpls.Complement,
                                 Description = tpls.Description,
                                 LocalAssessment = tpls.LocalAssessment,
                                 NameLocal = tpls.NameLocal,
                                 Neighborhood = tpls.Neighborhood,
                                 NumberHome = tpls.NumberHome,
                                 OpeningHours = tpls.OpeningHours,
                                 State = tpls.State,
                                 Street = tpls.Street,
                                 IsFavorite = tpls.IsFavorite,
                                 TypeAcessibility = tpls.TypeAcessibility,
                                 ZoneCategorie = tpls.ZoneCategorie,
                                 ImageUrl = s3Service.GetUrlFile(tpls.ImageName, 24)
                             }).ToList();

                return favoritePlaces;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PlacesResponse> GetFavoritePlaceOfUser(GetFavoritePlaceOfUserRequest request)
        {
            try
            {
                S3Service s3Service = new S3Service("ImagePlacesInclusiveJourney");

                var zones = (from tpls in _ctx.tabPlaces
                             join tpfpc in _ctx.TabPlaceFavorite_PersonCode
                                 on new { PlaceCode = tpls.Codigo, PersonCode = request.PersonCode }
                                 equals new { PlaceCode = tpfpc.PlacesCode, PersonCode = tpfpc.PersonCode }
                                 into favorites // Left join
                             from favorite in favorites.DefaultIfEmpty()
                             select new PlacesResponse
                             {
                                 ZoneCode = tpls.Codigo,
                                 Cep = tpls.Cep,
                                 City = tpls.City,
                                 Codigo = tpls.Codigo,
                                 Complement = tpls.Complement,
                                 Description = tpls.Description,
                                 LocalAssessment = tpls.LocalAssessment,
                                 NameLocal = tpls.NameLocal,
                                 Neighborhood = tpls.Neighborhood,
                                 NumberHome = tpls.NumberHome,
                                 OpeningHours = tpls.OpeningHours,
                                 State = tpls.State,
                                 Street = tpls.Street,
                                 IsFavorite = favorite != null, // true se houver registro na tabela auxiliar
                                 TypeAcessibility = tpls.TypeAcessibility,
                                 ZoneCategorie = tpls.ZoneCategorie,
                                 ImageUrl = s3Service.GetUrlFile(tpls.ImageName, 24)
                             }).ToList();

                return zones;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
