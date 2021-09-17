using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClosedXML.Excel;
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
        private readonly IClientePersist clientePersist;
        private readonly IVendedorPersist vendedorPersist;
        public ContratoService(IGeneralPersist generalPersist, 
                                IContratoPersist contratoPersist, 
                                IMapper mapper,
                                IClientePersist clientePersist,
                                IVendedorPersist vendedorPersist)
        {
            this.generalPersist = generalPersist;
            this.contratoPersist = contratoPersist;
            this.mapper = mapper;
            this.clientePersist = clientePersist;
            this.vendedorPersist = vendedorPersist;
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

        public async Task<ContratoDto[]> GetAllContratosByVendedorAsync(int vendedorId)
        {
            try
            {
                var contrato = await contratoPersist.GetAllContratosByVendedorAsync(vendedorId);
                if (contrato == null) return null;

                return mapper.Map<ContratoDto[]>(contrato);
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

        public async  Task<bool> ProcessaContratoViaPlanilha(string caminhoArquivo){
                        try{
                if(string.IsNullOrEmpty(caminhoArquivo))
                return false;

            var xls = new XLWorkbook(caminhoArquivo);
            if (xls != null)
            {
                var planilha = xls.Worksheets.First(w => w.Name == "Planilha1");
                int totalContratos = planilha.Rows().Count();
                List<Parcela> listParcelas = new List<Parcela>();
                for (int i = 2; i <= totalContratos; i++)
                {
                    Contrato contrato = new Contrato();

                    int parcelasTotais = Convert.ToInt32(planilha.Row(i).Cell(4).CachedValue.ToString()),
                        parcelasPagas = Convert.ToInt32(planilha.Row(i).Cell(5).CachedValue.ToString());

                    decimal valorParcelas = Convert.ToDecimal(planilha.Row(i).Cell(6).CachedValue.ToString());

                    contrato.NuContrato = planilha.Row(i).Cell(1).CachedValue.ToString();
                    contrato.ValorTotal = Convert.ToDecimal(planilha.Row(i).Cell(2).CachedValue.ToString());
                    contrato.SaldoDevedor = Convert.ToDecimal(planilha.Row(i).Cell(3).CachedValue.ToString());

                    
                    var cliente = await this.clientePersist
                                        .GetClienteByCPFAsync(planilha.Row(i).Cell(7).CachedValue.ToString());

                    if(cliente == null)
                        return false;

                    contrato.ClienteId = cliente.Id;
                               
                    var vendedor = await vendedorPersist
                                        .GetVendedorByRegraAsync(parcelasPagas,
                                                                 contrato.SaldoDevedor,
                                                                 cliente.CPF);

                    if(vendedor == null)
                        return false;

                    contrato.VendedorId = vendedor.Id;

                    for (int j = 1; j <= parcelasTotais; j++)
                    {
                        Parcela parcela = new Parcela();
                        parcela.NuParcela = j;
                        parcela.Valor = valorParcelas;
                        if (j <= parcelasPagas)
                            parcela.StParcela = SituacaoParcela.Paga;
                        else
                            parcela.StParcela = SituacaoParcela.Aberta;

                        listParcelas.Add(parcela);
                        //generalPersist.Add(parcela);
                    }
                    contrato.Parcelas = listParcelas;
                    generalPersist.Add(contrato);
                    return await generalPersist.SaveChangesAsync();
                }
            }
            return true;
            }
            catch(Exception ex){
                throw new Exception($"Erro ao processar contratos via planilha. Erro: {ex.Message}");
            }  
        }
    }
}