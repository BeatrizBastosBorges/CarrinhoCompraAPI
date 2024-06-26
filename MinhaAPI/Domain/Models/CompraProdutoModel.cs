﻿namespace CarrinhoCompraAPI.Domain.Models
{
    public class CompraProdutoModel
    {
        public int Id { get; set; }

        public int CompraId { get; set; }
        public CompraModel Compra { get; set; }

        public int ProdutoId { get; set; }
        public ProdutoModel Produto { get; set; }

        public int QtdProduto { get; set; }
        public decimal ValorTotalProduto { get; set; }
        public int QtdParcelas { get; set; }
        public decimal ValorParcelasProduto { get; set; }
        public decimal ValorParcelaAuxiliarProduto { get; set; }
        public decimal ValorAbatidoProduto { get; set; }
        public int QtdParcelasAtual { get; set; }
        public decimal ValorRestanteProduto { get; set; }
    }
}
