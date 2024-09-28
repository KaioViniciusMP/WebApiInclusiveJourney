using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiInclusiveJourney.Application.DTO.Request;
using WebApiInclusiveJourney.Application.DTO.Response;
using WebApiInclusiveJourney.Application.Services;

namespace WebApiInclusiveJourney.Application.IServices
{
    public interface IUsuarioService 
    {
        CadastrarUsuarioResponse CadastrarUsuario(UsuarioRequest request);
    }
}
