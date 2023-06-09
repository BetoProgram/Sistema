namespace WebApi.Aplicacion.Interfaces
{
    public interface ISimpleCrud<RQ, RS, ID>
    {
        Task<RS> ObtenerById(ID id);
        Task<IEnumerable<RS>> ObtenerTodos();
        Task Crear(RQ request);
        Task Actualizar(ID id, RQ request);
        Task Eliminar(ID id);
        Task<ID> MaximoId();
    }
}
