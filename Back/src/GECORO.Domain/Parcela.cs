using System.ComponentModel.DataAnnotations;

namespace GECORO.Domain
{
    public enum SituacaoParcela
    {
        Paga = 1,
        Aberta = 0
    }
    
    public class Parcela
    {
        public int Id {get; set;}
        
        [Required(ErrorMessage = "O contrato da parcela é uma informação obrigatória.")]
        public int ContratoId {get; set;}
        public Contrato Contrato {get; set;}

        [Required(ErrorMessage = "O valor da parcela é uma informação obrigatória.")]
        public decimal Valor {get; set;}

        [Required(ErrorMessage = "O número da parcela é uma informação obrigatória."),
        MinLength(1, ErrorMessage = "O número da parcela precisa ser maior do que 1.")]
        public int NuParcela {get; set;}

        [Required(ErrorMessage = "A situação da parcela é uma informação obrigatória.")]
        public SituacaoParcela StParcela {get; set;}
    }
}