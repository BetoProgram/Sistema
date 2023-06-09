using Microsoft.AspNetCore.Mvc;
using WebApi.Aplicacion.Dtos;
using WebApi.Aplicacion.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepositorio _clienteRepo;

        public ClientesController(IClienteRepositorio clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        // GET: api/<ClientesController>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ClienteResponseDto>>> Get()
        {
            return Ok(await _clienteRepo.ObtenerTodos());
        }

        // GET api/<ClientesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteResponseDto>> Get(int id)
        {
            return Ok(await _clienteRepo.ObtenerById(id));
        }

        // POST api/<ClientesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClienteRequestDto request)
        {
            await _clienteRepo.Crear(request);
            return Ok();
        }

        //// PUT api/<ClientesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ClientesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
