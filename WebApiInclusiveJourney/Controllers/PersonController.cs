using Microsoft.AspNetCore.Mvc;
using WebApiInclusiveJourney.Application.DTO.Request;
using WebApiInclusiveJourney.Application.DTO.Response;
using WebApiInclusiveJourney.Application.IServices;

namespace WebApiInclusiveJourney.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        public PersonController(IPersonService service)
        {
            _personService = service;
        }
        [HttpPost("/person/register")]
        public IActionResult RegisterPerson([FromBody] PersonRequest request)
        {
            var resposta = _personService.RegisterPerson(request);
            if (resposta == null)
            {
                return BadRequest();
            }
            return Ok(resposta);
        }
        [HttpGet("/person/{PersonCode}")]
        public IActionResult GetPerson(int PersonCode)
        {
            var resposta = _personService.GetPerson(PersonCode);
            if (resposta == null)
            {
                return BadRequest();
            }
            return Ok(resposta);
        }
        [HttpPut("/person/updateperson/{PersonCode}")]
        public IActionResult UpdatePerson([FromBody] PersonRequest request, int PersonCode)
        {
            var resposta = _personService.UpdatePerson(request, PersonCode);
            if (resposta == null)
            {
                return BadRequest();
            }
            return Ok(resposta);
        }

        #region
        //[HttpGet("/BuscarTipoPessoa")]
        //public IActionResult BuscarTipoPessoa()
        //{
        //    var resposta = _personService.BuscarTipoPessoa();
        //    if (resposta == null)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(resposta);
        //}
        //[HttpGet("/BuscarDeficiencia")]
        //public IActionResult BuscarDeficiencia()
        //{
        //    var resposta = _personService.BuscarDeficiencia();
        //    if (resposta == null)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(resposta);
        //}
        //[HttpGet("/BuscarPessoas")]
        //public IActionResult BuscarPessoas()
        //{
        //    var resposta = _personService.BuscarPessoas();
        //    if (resposta == null)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(resposta);
        //}
        //[HttpGet("/BuscarGeneros")]
        //public IActionResult BuscarGeneros()
        //{
        //    var resposta = _personService.BuscarGeneros();
        //    if (resposta == null)
        //    {
        //        return BadRequest();
        //    }
        //    return Ok(resposta);
        //}


        #endregion
    }
}
