using System;
using System.Threading.Tasks;
using AutoMapper;
using GECORO.Application.Contracts;
using GECORO.Application.Dto;
using GECORO.Domain;
using GECORO.Persistence.Contracts;

namespace GECORO.Application
{
    public class ContratoService : IContratoService
    {
        private readonly IGeneralPersist generalPersist;
        private readonly IContratoPersist contratoPersist;
        private readonly IMapper mapper;
        public ContratoService(IGeneralPersist generalPersist, IContratoPersist contratoPersist, IMapper mapper)
        {
            this.generalPersist = generalPersist;
            this.contratoPersist = contratoPersist;
            this.mapper = mapper;
        }

        public async Task<ContratoDto> AddContrato(ContratoDto model)
        {
            try
            {
                var contrato = mapper.Map<Contrato>(model);

                generalPersist.Add<Contrato>(contrato);
                if (await generalPersist.SaveChangesAsync())
                {
                    var contratoRetorno = await contratoPersist.GetContratoByIdAsync(contrato.Id);
                    return mapper.Map<ContratoDto>(contratoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ContratoDto> UpdateContrato(int contratoId, ContratoDto model)
        {
            try
            {
                var contrato = await contratoPersist.GetContratoByIdAsync(contratoId);
                if (contrato == null) return null;

                model.Id = contrato.Id;
                mapper.Map(model,contrato);

                generalPersist.Update<Contrato>(contrato);

                if (await generalPersist.SaveChangesAsync())
                {
                    var contratoRetorno = await contratoPersist.GetContratoByIdAsync(contrato.Id);
                    return mapper.Map<ContratoDto>(contratoRetorno);
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteContrato(int contratoId)
        {
            try
            {
                Contrato contrato = await contratoPersist.GetContratoByIdAsync(contratoId);
                if (contrato == null) throw new Exception("O contrato a ser deletado não foi encontrado.");

                generalPersist.Delete<Contrato>(contrato);
                return await generalPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ContratoDto[]> GetAllContratosAsync()
        {
            try
            {
                var contratos = await contratoPersist.GetAllContratosAsync();
                if (contratos == null) return null;

                return mapper.Map<ContratoDto[]>(contratos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ContratoDto[]> GetAllContratosByClienteAsync(int clienteId)
        {
            try
            {
                var contratos = await contratoPersist.GetAllContratosByClienteAsync(clienteId);
                if (contratos == null) return null;

                return mapper.Map<ContratoDto[]>(contratos);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ContratoDto> GetContratoByNumeroAsync(string numeroContrato)
        {
            try
            {
                var contrato = await contratoPersist.GetContratoByNumeroAsync(numeroContrato);
                if (contrato == null) return null;

                return mapper.Map<ContratoDto>(contrato);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ContratoDto> GetContratoByIdAsync(int contratoId)
        {
            try
            {
                var contrato = await contratoPersist.GetContratoByIdAsync(contratoId);
                if (contrato == null) return null;

                return mapper.Map<ContratoDto>(contrato);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}