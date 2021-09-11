namespace GECORO.Domain
{
    public enum SituacaoParcela
    {
        Paga,
        Aberta
    }
    
    public class Parcela
    {
        public int Id {get; set;}
        public int ContratoId {get; set;}
        public decimal Valor {get; set;}
        public int NuParcela {get; set;}
        public SituacaoParcela StParcela {get; set;}
    }
}