using System.Collections.Generic;
using System.Threading.Tasks;
using GECORO.Domain;

namespace GECORO.Application.UseCases
{
    public interface IProcessamentoContratosService
    {
        public bool ProcessarContratos(string caminhoArquivo);
    }
}