using System.Collections.Generic;

namespace CarrinhoCompraAPI.Domain.Models
{
    public class CompraModel
    {
        public int Id { get; set; }
        public decimal ValorTotalCompra { get; set; }
        public int QtdParcelas { get; set; }
        public decimal ValorParcelas { get; set; }
        public decimal ValorParcelaAuxiliar { get; set; }
        public decimal ValorAbatido { get; set; }
        public int QtdParcelasAtual { get; set; }
        public decimal ValorRestante { get; set; }

        public List<CompraProdutoModel> ComprasProduto { get; set; }
    }
}