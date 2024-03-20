using System.Collections.Generic;

namespace MinhaAPI.Domain.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public double PrecoUnitarioProduto { get; set; }

        public List<CompraProdutoModel> ComprasProduto { get; set; }
    }
}