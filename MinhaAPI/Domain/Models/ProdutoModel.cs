using System.Collections.Generic;

namespace CarrinhoCompraAPI.Domain.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public decimal PrecoUnitarioProduto { get; set; }

        public List<CompraProdutoModel> ComprasProduto { get; set; }
    }
}