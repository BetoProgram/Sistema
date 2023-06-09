﻿using Mapster;
using System.Net;
using WebApi.Aplicacion.Commons.Exceptions;
using WebApi.Aplicacion.Dtos;
using WebApi.Aplicacion.Interfaces;
using WebApi.Dominio.Entidades;
using WebApi.Persistencia.Repositorios;

namespace WebApi.Aplicacion.Servicios
{
    public class ClienteService : IClienteRepositorio
    {
        private readonly IRepositoryBaseAsync<Cliente> _clienteRepo;

        public ClienteService(IRepositoryBaseAsync<Cliente> clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        public async Task<ClienteResponseDto> ObtenerById(int id)
        {
            var cliente = await _clienteRepo.GetByIdAsync(id);

            if(cliente == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, new
                {
                    mensaje = "No se encuentra el registro seleccionado"
                });
            }

            return cliente.Adapt<ClienteResponseDto>();
        }

        public async Task<IEnumerable<ClienteResponseDto>> ObtenerTodos()
        {
            var clientes = await _clienteRepo.GetAsync(
                null,
                o => o.OrderBy(x => x.Nombre),
                string.Empty,
                true
             );

            return clientes.Adapt<IReadOnlyList<ClienteResponseDto>>();
        }

        public async Task Actualizar(int id, ClienteRequestDto request)
        {
            var cliente = await _clienteRepo.GetEntityAsync(x => x.Id == id);

            if(cliente == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, new
                {
                    mensaje = "No se encuentra el registro seleccionado"
                });
            }
            request.Id = id;
            var clienteAct = request.Adapt<Cliente>();

            await _clienteRepo.UpdateAsync(clienteAct);
        }

        public async Task Crear(ClienteRequestDto request)
        {
            var cliente = request.Adapt<Cliente>();
            await _clienteRepo.AddAsync(cliente);
        }

        public async Task Eliminar(int id)
        {
            var cliente = await _clienteRepo.GetEntityAsync(x => x.Id == id);

            if (cliente == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, new
                {
                    mensaje = "No se encuentra el registro seleccionado"
                });
            }

            await _clienteRepo.DeleteAsync(cliente);

        }

        public Task<int> MaximoId()
        {
            throw new NotImplementedException();
        }
    }
}
