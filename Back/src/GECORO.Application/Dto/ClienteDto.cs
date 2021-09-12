using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GECORO.Application.Dto
{
    public class ClienteDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O CPF do cliente deve ser informado."),
        StringLength(11, MinimumLength = 11)]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O nome do cliente deve ser informado.")]
        public string Nome { get; set; }

        public IEnumerable<ContratoDto> Contratos { get; set; }
        [Required(ErrorMessage = "O vendedor designado ao cliente deve ser informado.")]
        public int VendedorId {get; set;}
        public VendedorDto Vendedor { get; set; }
    }
}