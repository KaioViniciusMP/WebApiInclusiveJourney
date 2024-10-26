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
    public class PersonService : IPersonService
    {
        private readonly WebApiInclusiveJourneyContext _ctx;
        public PersonService(WebApiInclusiveJourneyContext context)
        {
            _ctx = context;
        }

        public PersonResponse RegisterPerson(PersonRequest request)
        {
            try
            {
                var person = new TabPerson()
                {
                    Email = request.Email,
                    Name = request.Name,
                    Password = request.Password,
                    Role = request.Role,
                    FullName = request.FullName,
                    DateOfBirth = request.DateOfBirth,
                    Gender = request.Gender,
                    DisabilityType = request.DisabilityType,
                    PostalCode = request.PostalCode,
                    Street = request.Street,
                    AdditionalInfo = request.AdditionalInfo,
                    Neighborhood = request.Neighborhood,
                    City = request.City,
                    Number = request.Number,
                    State = request.State,
                    Username = request.Username,
                    UserDescription = request.UserDescription,
                    Avatar = request.Avatar
                };

                _ctx.tabPerson.Add(person);
                _ctx.SaveChanges();

                var response = new PersonResponse
                {
                    Codigo = person.Codigo,
                    Email = person.Email,
                    Role = person.Role,
                    Name = person.Name,
                    FullName = person.FullName,
                    DateOfBirth = person.DateOfBirth,
                    Gender = person.Gender,
                    DisabilityType = person.DisabilityType,
                    PostalCode = person.PostalCode,
                    Street = person.Street,
                    AdditionalInfo = person.AdditionalInfo,
                    Neighborhood = person.Neighborhood,
                    City = person.City,
                    Number = person.Number,
                    State = person.State,
                    Username = person.Username,
                    UserDescription = person.UserDescription,
                    Avatar = person.Avatar
                };

                return response;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) if necessary
                return null;
            }
        }

        public PersonResponse GetPerson(int personCode)
        {
            try
            {
                var person = _ctx.tabPerson.Where(c => c.Codigo == personCode)
                                               .Select(person => new PersonResponse
                                               {
                                                   Codigo = person.Codigo,
                                                   Email = person.Email ?? string.Empty,
                                                   Role = person.Role ?? string.Empty,
                                                   Password = person.Password ?? string.Empty,
                                                   Name = person.Name ?? string.Empty,
                                                   FullName = person.FullName ?? string.Empty,
                                                   DateOfBirth = person.DateOfBirth,
                                                   Gender = person.Gender ?? string.Empty,
                                                   DisabilityType = person.DisabilityType ?? string.Empty,
                                                   PostalCode = person.PostalCode ?? string.Empty,
                                                   Street = person.Street ?? string.Empty,
                                                   AdditionalInfo = person.AdditionalInfo ?? string.Empty,
                                                   Neighborhood = person.Neighborhood ?? string.Empty,
                                                   City = person.City ?? string.Empty,
                                                   Number = person.Number ?? string.Empty,
                                                   State = person.State ?? string.Empty,
                                                   Username = person.Username ?? string.Empty,
                                                   UserDescription = person.UserDescription ?? string.Empty,
                                                   Avatar = person.Avatar ?? string.Empty
                                               }).FirstOrDefault(); 

                return person;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PersonResponse UpdatePerson(PersonRequest request, int personCode)
        {
            try
            {
                var person = _ctx.tabPerson.Where(c => c.Codigo == personCode).FirstOrDefault();
                if (person == null)
                    return null;

                person.Email = request.Email;
                person.Name = request.Name;
                person.Password = request.Password; 
                person.Role = request.Role;
                person.FullName = request.FullName;
                person.DateOfBirth = request.DateOfBirth;
                person.Gender = request.Gender;
                person.DisabilityType = request.DisabilityType;
                person.PostalCode = request.PostalCode;
                person.Street = request.Street;
                person.AdditionalInfo = request.AdditionalInfo;
                person.Neighborhood = request.Neighborhood;
                person.City = request.City;
                person.Number = request.Number;
                person.State = request.State;
                person.Username = request.Username;
                person.UserDescription = request.UserDescription;
                person.Avatar = request.Avatar;

                _ctx.SaveChanges();

                var response = new PersonResponse
                {
                    Codigo = person.Codigo,
                    Email = person.Email,
                    Role = person.Role,
                    Name = person.Name,
                    FullName = person.FullName,
                    DateOfBirth = person.DateOfBirth,
                    Gender = person.Gender,
                    DisabilityType = person.DisabilityType,
                    PostalCode = person.PostalCode,
                    Street = person.Street,
                    AdditionalInfo = person.AdditionalInfo,
                    Neighborhood = person.Neighborhood,
                    City = person.City,
                    Number = person.Number,
                    State = person.State,
                    Username = person.Username,
                    UserDescription = person.UserDescription,
                    Avatar = person.Avatar
                };

                return response; 
            }
            catch (Exception ex)
            {
                return null; 
            }
        }

        #region 
        //public PessoaResponse CadastrarPessoa(PessoaRequest request)
        //{
        //    try
        //    {
        //        var pessoa = new TabPessoa()
        //        {
        //            Bairro = request.Bairro,
        //            Cep = request.Cep,
        //            Cidade = request.Cidade,
        //            Complemento = request.Complemento,
        //            DataNascimento = request.DataNascimento,
        //            GeneroCodigo = request.GeneroCodigo,
        //            NomeCompleto = request.NomeCompleto,
        //            Numero = request.Numero,
        //            PessoaTipoCodigo = request.PessoaTipoCodigo,
        //            TipoDeficienciaCodigo = request.TipoDeficienciaCodigo,
        //            Uf = request.Uf,
        //            Rua = request.Rua,
        //            usuarioCodigo = request.usuarioCodigo
        //        };

        //        _context.tabPessoa.Add(pessoa);
        //        _context.SaveChanges();

        //        var pessoaTipo = _context.tabPessoaTipo
        //                                  .FirstOrDefault(pt => pt.Codigo == pessoa.PessoaTipoCodigo)?.TipoPessoa;

        //        var genero = _context.tabGenero
        //                             .FirstOrDefault(g => g.Codigo == pessoa.GeneroCodigo)?.Genero;

        //        var tipoDeficiencia = _context.tabTipoDeficiencia
        //                                      .FirstOrDefault(td => td.Codigo == pessoa.TipoDeficienciaCodigo)?.Deficiencia;

        //        var response = new PessoaResponse
        //        {
        //            Codigo = pessoa.Codigo,
        //            NomeCompleto = pessoa.NomeCompleto,
        //            DataNascimento = pessoa.DataNascimento,
        //            Genero = genero,
        //            Rua = pessoa.Rua,
        //            Numero = pessoa.Numero,
        //            Complemento = pessoa.Complemento,
        //            Bairro = pessoa.Bairro,
        //            Cidade = pessoa.Cidade,
        //            Cep = pessoa.Cep,
        //            Uf = pessoa.Uf,
        //            PessoaTipo = pessoaTipo,
        //            TipoDeficiencia = tipoDeficiencia,
        //            usuarioCodigo = request.usuarioCodigo
        //        };

        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //public List<TabPessoaTipo> BuscarTipoPessoa()
        //{
        //    var response = _context.tabPessoaTipo.ToList();
        //    return response;
        //}
        //public List<TabTipoDeficiencia> BuscarDeficiencia()
        //{
        //    var response = _context.tabTipoDeficiencia.ToList();
        //    return response;
        //}
        //public List<TabPessoa> BuscarPessoas()
        //{
        //    var response = _context.tabPessoa.ToList();
        //    return response;
        //}
        //public List<TabGenero> BuscarGeneros()
        //{
        //    var response = _context.tabGenero.ToList();
        //    return response;
        //}
        #endregion
    }
}
