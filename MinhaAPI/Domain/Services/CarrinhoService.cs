using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using CarrinhoCompraAPI.Domain.Models;
using CarrinhoCompraAPI.Infrastructure.Data.Repositories;

namespace CarrinhoCompraAPI.Domain.Services
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
                ValorTotalProduto = Math.Round((produto.PrecoUnitarioProduto * quantidade), 2)
            };

            await _carrinhoRepository.AddProduto(produtoCarrinho);
        }
        // Atualiza a quantidade de um produto do carrinho
        public async Task UpdateProdutoCarrinho(int produtoId, int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            var produtoCarrinho = await _carrinhoRepository.GetProdutoCarrinho(produtoId);
            if (produtoCarrinho == null)
                throw new ArgumentException("Produto não encontrado no carrinho.");

            var produto = await _produtoRepository.GetProduto(produtoCarrinho.ProdutoId);
            if (produto == null)
                throw new ArgumentException("Produto não encontrado.");

            produtoCarrinho.QtdProduto = quantidade;
            produtoCarrinho.ValorTotalProduto = Math.Round((produto.PrecoUnitarioProduto * quantidade), 2);

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