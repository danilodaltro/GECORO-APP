using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GECORO.Application.Dto
{
    public class VendedorDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo {0} deve ser informado."),
        StringLength(5, MinimumLength = 5, ErrorMessage = "O código do vendedor deve possuir cinco dígitos.")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "O nome do cliente deve ser informado.")]
        public string Nome { get; set; }
        public IEnumerable<ClienteDto> Clientes { get; set; }
        public RegraVendedorDto RegraVendedor { get; set; }
    }
}