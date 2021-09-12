using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GECORO.Application.Dto
{
    public class ContratoDto
    {
        
        public int Id {get; set;}

        [Required(ErrorMessage = "O cliente do contrato deve ser informado.")]
        public int ClienteId {get; set;}
        
        public ClienteDto Cliente {get; set;}

        [Required(ErrorMessage = "O número do contrato deve ser informado."),
        StringLength(10, MinimumLength = 10, ErrorMessage = "O número do contrato deve ter 10 digitos.")]
        public string NuContrato {get; set;}

        public IEnumerable<ParcelaDto> Parcelas {get;set;}
        public decimal SaldoDevedor {get; set;}
        [Required(ErrorMessage = "O valor total do contrato deve ser informado.")]
        public decimal ValorTotal {get; set;}
    }
}