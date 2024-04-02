namespace CarrinhoCompraAPI.Domain.Models
{
    public class CarrinhoModel
    {
        public int Id { get; set; }
        public int QtdProduto { get; set; }
        public decimal ValorTotalProduto { get; set; }

        public int ProdutoId { get; set; }
        public ProdutoModel Produto { get; set; }
    }
}