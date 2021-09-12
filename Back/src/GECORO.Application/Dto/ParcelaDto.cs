using System.ComponentModel.DataAnnotations;
using GECORO.Domain;

namespace GECORO.Application.Dto
{
    public class ParcelaDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O contrado relacionado a parcela deve ser informado.")]
        public int ContratoId { get; set; }

        [Required(ErrorMessage = "O valor da parcela deve ser informado.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O número da parcela deve ser informado.")]
        public int NuParcela { get; set; }

        [Required(ErrorMessage = "A situação da parcela deve ser informada."), 
        EnumDataType(typeof(SituacaoParcela))]
        public SituacaoParcela StParcela { get; set; }
    }
}