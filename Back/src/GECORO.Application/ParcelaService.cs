using System;
using System.Threading.Tasks;
using AutoMapper;
using GECORO.Application.Contracts;
using GECORO.Application.Dto;
using GECORO.Domain;
using GECORO.Persistence.Contracts;

namespace GECORO.Application
{
    public class ParcelaService : IParcelaService
    {
        private readonly IGeneralPersist generalPersist;
        private readonly IMapper mapper;
        private readonly IParcelaPersist parcelaPersist;
        public ParcelaService(IGeneralPersist generalPersist, IParcelaPersist parcelaPersist, IMapper mapper)
        {
            this.parcelaPersist = parcelaPersist;
            this.generalPersist = generalPersist;
            this.mapper = mapper;
        }

        public async Task<ParcelaDto> AddParcela(ParcelaDto model)
        {
            try
            {
                var parcela = mapper.Map<Parcela>(model);
                generalPersist.Add<Parcela>(parcela);
                if (await generalPersist.SaveChangesAsync())
                {
                    var parcelaRetorno = await parcelaPersist.GetParcelaByIdAsync(parcela.Id);
                    return mapper.Map<ParcelaDto>(parcelaRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ParcelaDto> UpdateParcela(int parcelaId, ParcelaDto model)
        {
            try
            {
                var parcela = await parcelaPersist.GetParcelaByIdAsync(parcelaId);
                if (parcela == null) return null;

                model.Id = parcela.Id;
                mapper.Map(model, parcela);

                generalPersist.Update<Parcela>(parcela);

                if (await generalPersist.SaveChangesAsync())
                {
                    var parcelaRetorno = await parcelaPersist.GetParcelaByIdAsync(parcela.Id);
                    return mapper.Map<ParcelaDto>(parcelaRetorno);
                }
                return null;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteParcela(int parcelaId)
        {
            try
            {
                Parcela parcela = await parcelaPersist.GetParcelaByIdAsync(parcelaId);
                if (parcela == null) throw new Exception("A parcela a ser deletada não foi encontrada.");

                generalPersist.Delete<Parcela>(parcela);
                return await generalPersist.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ParcelaDto[]> GetAllParcelasByContratoAsync(int contratoId)
        {
            try
            {
                var parcelas = await parcelaPersist.GetAllParcelasByContratoAsync(contratoId);
                if (parcelas == null) return null;

                return mapper.Map<ParcelaDto[]>(parcelas);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ParcelaDto> GetParcelaById(int parcelaId)
        {
            try
            {
                var parcela = await parcelaPersist.GetParcelaByIdAsync(parcelaId);
                if (parcela == null) return null;

                return mapper.Map<ParcelaDto>(parcela);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}