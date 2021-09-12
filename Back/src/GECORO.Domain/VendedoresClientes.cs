namespace GECORO.Domain
{
    public class VendedoresClientes
    {
        public int VendedorId {get; set;}
        public Vendedor Vendedor {get; set;}
        public int ClienteId {get; set;}
        public Cliente Cliente {get; set;}
    }
}