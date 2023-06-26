using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Aplicacion.Dtos;
using WebApi.Aplicacion.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoRepositorio _productoRepositorio;

        public ProductosController(IProductoRepositorio productoRepositorio)
        {
            _productoRepositorio = productoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductoRespDto>>> Get()
        {
            return Ok(await _productoRepositorio.ObtenerTodos());
        }
    }
}
