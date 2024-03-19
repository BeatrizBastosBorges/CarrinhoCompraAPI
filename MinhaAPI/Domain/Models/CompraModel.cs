namespace MinhaAPI.Domain.Models
{
    public class CompraModel
    {
        public int Id { get; set; }
        public double ValorTotalCompra { get; set; }
        public int QtdParcelas { get; set; }
        public double ValorParcelas { get; set; }
        public double ValorParcelaAuxiliar { get; set; }
    }
}