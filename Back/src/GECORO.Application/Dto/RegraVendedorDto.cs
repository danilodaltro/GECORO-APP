
using System.ComponentModel.DataAnnotations;

namespace GECORO.Application.Dto
{
    public class RegraVendedorDto
    {
        public int Id { get; set; }
        public int VendedorId { get; set; }
        public VendedorDto Vendedor { get; set; }
        [Required(ErrorMessage = "O vendedor vinculado ao cliente precisa ser informado.")]
        public decimal SaldoDevedor { get; set; }
        [Required(ErrorMessage = "O cliente vinculado ao vendedor precisa ser informado.")]
        public int ParcelasPagas { get; set; }
    }
}