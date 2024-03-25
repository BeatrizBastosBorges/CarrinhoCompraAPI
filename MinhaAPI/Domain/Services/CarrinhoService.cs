using MinhaAPI.Domain.Models;
using MinhaAPI.Infrastructure.Data.Repositories;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace MinhaAPI.Domain.Services
{
    public class CarrinhoService
    {
        private readonly CarrinhoRepository _carrinhoRepository;
        private readonly ProdutoRepository _produtoRepository;
        

        public CarrinhoService(CarrinhoRepository carrinhoRepository,
                               ProdutoRepository produtoRepository)
        {
            _carrinhoRepository = carrinhoRepository;
            _produtoRepository = produtoRepository;
        }

        // Lista todos o produtos que estão atualmente no carrinho
        public async Task<List<CarrinhoModel>> ListProdutosCarrinho()
        {
            List<CarrinhoModel> list = await _carrinhoRepository.ListProdutosCarrinho();
            return list;
        }

        // Pega um produto específico do carrinho
        public async Task<CarrinhoModel> GetProdutoCarrinho(int produtoId)
        {
            CarrinhoModel produto = await _carrinhoRepository.GetProdutoCarrinho(produtoId);

            if (produto == null)
                throw new ArgumentException("Produto não existe!");

            return produto;
        }

        // Adiciona um produto ao carrinho
        public async Task AddProduto(int produtoId, int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            var produto = await _produtoRepository.GetProduto(produtoId);

            if (produto == null)
            {
                throw new ArgumentException("Produto não encontrado.");
            }

            var produtoCarrinho = new CarrinhoModel
            {
                ProdutoId = produtoId,
                QtdProduto = quantidade,
                ValorTotalProduto = produto.PrecoUnitarioProduto * quantidade
            };

            await _carrinhoRepository.AddProduto(produtoCarrinho);
        }
        // Atualiza a quantidade de um produto do carrinho
        public async Task UpdateProdutoCarrinho(int carrinhoId, int produtoId, int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            var produtoCarrinho = await _carrinhoRepository.GetProdutoCarrinho(carrinhoId);
            if (produtoCarrinho == null)
                throw new ArgumentException("Compra não encontrada.");

            var produto = await _produtoRepository.GetProduto(produtoId);
            if (produto == null)
                throw new ArgumentException("Produto não encontrado.");

            produtoCarrinho.ProdutoId = produtoId;
            produtoCarrinho.QtdProduto = quantidade;
            produtoCarrinho.ValorTotalProduto = produto.PrecoUnitarioProduto * quantidade;

            await _carrinhoRepository.UpdateProdutoCarrinho(produtoCarrinho);
        }

        // Apaga um produto do carrinho
        public async Task<bool> DeleteProdutoCarrinho(int produtoId)
        {
            CarrinhoModel findProduto = await _carrinhoRepository.GetProdutoCarrinho(produtoId);

            if (findProduto == null)
                throw new ArgumentException("Produto não existe!");

            await _carrinhoRepository.DeleteProdutoCarrinho(produtoId);

            return true;
        }
    }
}