using System.Collections.Generic;

namespace GECORO.Domain
{
    public class Cliente
    {
        public int Id {get; set;}
        public string CPF {get; set;}
        public string Nome {get; set;}
        public IEnumerable<Contrato> Contratos {get; set;}
        public VendedorCliente Vendedor;
    }
}