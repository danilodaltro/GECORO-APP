using GECORO.Domain;

namespace GECORO.Application.Dto
{
    public class VendedoresClientesDto
    {
        
        public int VendedorId {get; set;}
        public Vendedor Vendedor {get; set;}
        public int ClienteId {get; set;}
        public Cliente Cliente {get; set;}
    }
}