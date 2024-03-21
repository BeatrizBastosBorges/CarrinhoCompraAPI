using System.Collections.Generic;

namespace MinhaAPI.Domain.Models
{
    public class CompraModel
    {
        public int Id { get; set; }
        public double ValorTotalCompra { get; set; }
        public int QtdParcelas { get; set; }
        public double ValorParcelas { get; set; }
        public double ValorParcelaAuxiliar { get; set; }
        public double ValorAbatido { get; set; }
        public int QtdParcelasAtual { get; set; }
        public double ValorRestante { get; set; }

        public List<CompraProdutoModel> ComprasProduto { get; set; }
    }
}