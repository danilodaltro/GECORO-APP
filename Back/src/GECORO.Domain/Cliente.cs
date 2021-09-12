using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GECORO.Domain
{
    public class Cliente
    {
        public int Id {get; set;}

        [Required( ErrorMessage = "O CPF do cliente é uma informação obrigatória."),
        StringLength(11, MinimumLength = 11)]
        public string CPF {get; set;}

        [Required( ErrorMessage = "O nome do cliente é uma informação obrigatória.")]
        public string Nome {get; set;}
        
        public IEnumerable<Contrato> Contratos {get; set;}
        public int VendedorId {get; set;}
        public VendedoresClientes VendedoresClientes {get; set;}
    }
}