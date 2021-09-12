using System.ComponentModel.DataAnnotations;

namespace GECORO.Domain
{
    public class RegraVendedor
    {
        public int Id {get; set;}
        [Required(ErrorMessage = "É necessário informar o vendedor qual será aplicado a regra.")]
        public int VendedorId {get; set;}
        public Vendedor Vendedor {get; set;}
        [Required(ErrorMessage = "O valor do saldo devedor é uma informação obrigatória.")]
        public decimal SaldoDevedor {get; set;}
        [Required(ErrorMessage = "O número de parcelas pagas é uma informação obrigatória.")]
        public int ParcelasPagas {get; set;}
    }
}