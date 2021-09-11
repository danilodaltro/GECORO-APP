using System.Collections.Generic;

namespace GECORO.Domain
{
    public class Vendedor
    {
        public int Id {get; set;}
        public string Codigo {get; set;}
        public string Nome {get; set;}
        public IEnumerable<VendedorCliente> VendedoresClientes {get; set;}
        public RegraContrato RegraContrato {get; set;}
    }
}