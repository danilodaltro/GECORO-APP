using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GECORO.Domain
{
    public class Contrato
    {
        public int Id {get; set;}

        [Required(ErrorMessage = "O cliente do contrato é uma informação obrigatória.")]
        public int ClienteId {get; set;}
        
        public Cliente Cliente {get; set;}

        [Required(ErrorMessage = "O número do contrato é uma informação obrigatória."),
        StringLength(10, MinimumLength = 10)]
        public string NuContrato {get; set;}

        public IEnumerable<Parcela> Parcelas {get;set;}
        public decimal SaldoDevedor {get; set;}
        [Required(ErrorMessage = "O valor total do contrato é uma informação obrigatória.")]
        public decimal ValorTotal {get; set;}
    }
}