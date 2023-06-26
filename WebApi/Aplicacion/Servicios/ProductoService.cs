using Mapster;
using System.Net;
using WebApi.Aplicacion.Commons.Exceptions;
using WebApi.Aplicacion.Dtos;
using WebApi.Aplicacion.Interfaces;
using WebApi.Dominio.Entidades;
using WebApi.Persistencia.Repositorios;

namespace WebApi.Aplicacion.Servicios
{
    public class ProductoService : IProductoRepositorio
    {
        private readonly IRepositoryBaseAsync<Producto> _productoRepo;

        public ProductoService(IRepositoryBaseAsync<Producto> productoRepo)
        {
            _productoRepo = productoRepo;
        }

        public async Task<ProductoRespDto> ObtenerById(int id)
        {
            var producto = await _productoRepo.GetByIdAsync(id);

            if(producto is null)
            {
                throw new CustomException(HttpStatusCode.NotFound, new
                {
                    mensaje = "No se encuentra el producto seleccionado"
                });
            }

            return producto.Adapt<ProductoRespDto>();
        }

        public async Task<IEnumerable<ProductoRespDto>> ObtenerTodos()
        {
            var productos = await _productoRepo.GetAllAsync();
            return productos.Adapt<IReadOnlyList<ProductoRespDto>>();
        }

        public async Task Crear(ProductoRequestDto request)
        {
            var producto = request.Adapt<Producto>();

            await _productoRepo.AddAsync(producto);
        }

        public async Task Actualizar(int id, ProductoRequestDto request)
        {
            var producto = await _productoRepo.GetByIdAsync(id);

            if (producto is null)
            {
                throw new CustomException(HttpStatusCode.NotFound, new
                {
                    mensaje = "No se encuentra el producto seleccionado"
                });
            }

            var productoActualizar = request.Adapt<Producto>();

            await _productoRepo.UpdateAsync(productoActualizar);
        }

        public async Task Eliminar(int id)
        {
            var producto = await _productoRepo.GetByIdAsync(id);

            if (producto is null)
            {
                throw new CustomException(HttpStatusCode.NotFound, new
                {
                    mensaje = "No se encuentra el producto seleccionado"
                });
            }

            await _productoRepo.DeleteAsync(producto);
        }

        public async Task<int> MaximoId()
        {
            var producto = await _productoRepo.GetAllAsync();
            return producto.Max(x => x.Id);
        }

        
    }
}
