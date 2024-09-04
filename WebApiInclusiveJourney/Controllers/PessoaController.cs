using Microsoft.AspNetCore.Mvc;
using WebApiInclusiveJourney.Application.DTO.Request;
using WebApiInclusiveJourney.Application.DTO.Response;
using WebApiInclusiveJourney.Application.IServices;

namespace WebApiInclusiveJourney.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : Controller
    {
        private readonly IPessoaService _usuarioService;
        public PessoaController(IPessoaService service)
        {
            _usuarioService = service;
        }
        [HttpPost]
        public IActionResult CadastrarPessoa([FromBody] PessoaRequest request)
        {
            var resposta = _usuarioService.CadastrarPessoa(request);
            if (resposta == null)
            {
                return BadRequest();
            }
            return Ok(resposta);
        }
    }
}
