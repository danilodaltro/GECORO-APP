using System.Collections.Generic;

namespace GECORO.Domain
{
    public class Contrato
    {
        public int Id {get; set;}
        public int ClienteId {get; set;}
        public Cliente Cliente {get; set;}
        public string NuContrato {get; set;}
        public IEnumerable<Parcela> Parcelas {get;set;}
        public decimal SaldoDevedor {get; set;}
        public decimal ValorTotal {get; set;}
    }
}