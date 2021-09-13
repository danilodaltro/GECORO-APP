using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GECORO.Domain
{
    public class Vendedor
    {
        public int Id {get; set;}
        [Required(ErrorMessage = "O código do vendedor é uma informação obrigatória."),
        StringLength(5, MinimumLength = 5, ErrorMessage = "O código deve ser formado por 5 digitos.")]
        public string Codigo {get; set;}
        [Required(ErrorMessage = "O nome do vendedor é uma informação obrigatória.")]
        public string Nome {get; set;}
        public IEnumerable<Cliente> Clientes {get; set;}
        public RegraVendedor RegraVendedor {get; set;}
    }
}