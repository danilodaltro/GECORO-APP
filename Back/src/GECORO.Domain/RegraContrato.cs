namespace GECORO.Domain
{
    public class RegraContrato
    {
        public int Id {get; set;}
        public int VendedorId {get; set;}
        public Vendedor Vendedor {get; set;}
        public decimal SaldoDevedor {get; set;}
        public int ParcelasPagas {get; set;}
    }
}