using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using GECORO.Application.Contracts;
using GECORO.Application.Dto;
using GECORO.Domain;

namespace GECORO.Application.UseCases
{
    public abstract class ProcessamentoContratos : IProcessamentoContratosService
    {
        private readonly IClienteService clienteService;
        private readonly IVendedorService vendedorService;
        private readonly IParcelaService parcelaService;
        private readonly IContratoService contratoService;
        public ProcessamentoContratos(string caminhoArquivo,
                                    IClienteService clienteService,
                                    IVendedorService vendedorService,
                                    IParcelaService parcelaService,
                                    IContratoService contratoService)
        {
            this.vendedorService = vendedorService;
            this.clienteService = clienteService;
            this.parcelaService = parcelaService;
            this.contratoService = contratoService;
        }

        public bool ProcessarContratos(string caminhoArquivo)
        {
            try
            {
                if (string.IsNullOrEmpty(caminhoArquivo))
                    return false;

                List<ParcelaDto> parcelas = new List<ParcelaDto>();
                var xls = new XLWorkbook(caminhoArquivo);
                if (xls != null)
                {
                    var planilha = xls.Worksheets.First(w => w.Name == "Planilha1");
                    int totalContratos = planilha.Rows().Count();
                    List<Contrato> listaContratos = new List<Contrato>();
                    for (int i = 2; i < totalContratos; i++)
                    {
                        ContratoDto contrato = new ContratoDto();

                        int parcelasTotais = Convert.ToInt32(planilha.Row(i).Cell(4).ToString()),
                            parcelasPagas = Convert.ToInt32(planilha.Row(i).Cell(5).ToString());

                        decimal valorParcelas = Convert.ToDecimal(planilha.Row(i).Cell(6).ToString());

                        contrato.NuContrato = planilha.Row(i).Cell(1).ToString();
                        contrato.ValorTotal = Convert.ToDecimal(planilha.Row(i).Cell(2).ToString());
                        contrato.SaldoDevedor = Convert.ToDecimal(planilha.Row(i).Cell(3).ToString());

                        contrato.ClienteId = this.clienteService
                                            .GetClienteByCPFAsync(planilha.Row(i).Cell(7).ToString()).Id;

                        contrato.VendedorId = vendedorService
                        .GetVendedorByRegraAsync(parcelasPagas, contrato.SaldoDevedor).Result?.Id;

                        var contratoAdicionado = contratoService.AddContrato(contrato).Result;

                        for (int j = 1; j <= parcelasTotais; j++)
                        {
                            ParcelaDto parcela = new ParcelaDto();
                            parcela.ContratoId = contratoAdicionado.Id;
                            parcela.NuParcela = j;
                            parcela.Valor = valorParcelas;
                            if (j <= parcelasPagas)
                                parcela.StParcela = SituacaoParcela.Paga;
                            else
                                parcela.StParcela = SituacaoParcela.Aberta;

                            parcelaService.AddParcela(parcela);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao processar contratos via planilha. Erro: {ex.Message}");
            }
        }
    }
}