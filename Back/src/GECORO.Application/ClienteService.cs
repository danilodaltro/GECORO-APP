using System;
using System.Threading.Tasks;
using AutoMapper;
using GECORO.Application.Contracts;
using GECORO.Application.Dto;
using GECORO.Domain;
using GECORO.Persistence.Contracts;

namespace GECORO.Application
{
    public class ClienteService : IClienteService
    {
        private readonly IGeneralPersist generalPersist;
        private readonly IClientePersist clientePersist;
        private readonly IMapper mapper;

        public ClienteService(IGeneralPersist generalPersist, IClientePersist clientePersist, IMapper mapper)
        {
            this.generalPersist = generalPersist;
            this.clientePersist = clientePersist;
            this.mapper = mapper;
        }

        public async Task<ClienteDto> AddCliente(ClienteDto model)
        {
            try
            {
                var cliente = mapper.Map<Cliente>(model);
                generalPersist.Add<Cliente>(cliente);
                if (await generalPersist.SaveChangesAsync())
                {
                    var clienteRetorno = await clientePersist.GetClienteByIdAsync(cliente.Id);

                    return mapper.Map<ClienteDto>(clienteRetorno);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClienteDto> UpdateCliente(int clienteId, ClienteDto model)
        {
            try
            {
                var cliente = await clientePersist.GetClienteByIdAsync(clienteId);
                if (cliente == null) return null;

                model.Id = cliente.Id;
                mapper.Map(model, cliente);

                generalPersist.Update<Cliente>(cliente);

                if (await generalPersist.SaveChangesAsync())
                {
                    var clienteRetorno = await clientePersist.GetClienteByIdAsync(cliente.Id);
                    return mapper.Map<ClienteDto>(clienteRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteCliente(int clienteId)
        {
            try
            {
                Cliente cliente = await clientePersist.GetClienteByIdAsync(clienteId);
                if (cliente == null) throw new Exception("O cliente a ser deletado n??o foi encontrado.");

                generalPersist.Delete<Cliente>(cliente);
                return await generalPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClienteDto[]> GetAllClientesAsync()
        {
            try
            {
                var clientes = await clientePersist.GetAllClientesAsync();
                if (clientes == null) return null;

                return mapper.Map<ClienteDto[]>(clientes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClienteDto[]> GetAllClientesByVendedorAsync(int vendedorId)
        {
            try
            {
                var cliente = await clientePersist.GetAllClientesByVendedor(vendedorId);
                if (cliente == null) return null;

                return mapper.Map<ClienteDto[]>(cliente);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClienteDto> GetClienteByIdAsync(int clienteId)
        {
            try
            {
                var cliente = await clientePersist.GetClienteByIdAsync(clienteId);
                if (cliente == null) return null;

                return mapper.Map<ClienteDto>(cliente);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClienteDto> GetClienteByCPFAsync(string cpf)
        {
            try
            {
                var cliente = await clientePersist.GetClienteByCPFAsync(cpf);
                if (cliente == null) return null;

                return mapper.Map<ClienteDto>(cliente);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}