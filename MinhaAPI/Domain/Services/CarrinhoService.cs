using MinhaAPI.Domain.Models;
using MinhaAPI.Infrastructure.Data.Repositories;
using System.Collections.Generic;
using System;
using System.Reflection.Metadata;
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

        public async Task<List<CarrinhoModel>> ListProdutosCarrinho()
        {
            List<CarrinhoModel> list = await _carrinhoRepository.ListProdutosCarrinho();
            return list;
        }

        public async Task<CarrinhoModel> GetProdutoCarrinho(int produtoId)
        {
            CarrinhoModel produto = await _carrinhoRepository.GetProdutoCarrinho(produtoId);

            if (produtoId == null)
                throw new ArgumentException("Produto não existe!");

            return produto;
        }

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
                ValorTotalProduto = produto.precoUnitarioProduto * quantidade
            };

            await _carrinhoRepository.AddProduto(produtoCarrinho);
        }

        public async Task UpdateProdutoCarrinho(int carrinhoId, int produtoId, int quantidade)
        {
            if (quantidade <= 0)
                throw new ArgumentException("A quantidade deve ser maior que zero.");

            var produtoCarrinho = await _carrinhoRepository.GetProdutoCarrinho(carrinhoId);

            if (produtoCarrinho == null)
            {
                throw new ArgumentException("Compra não encontrada.");
            }

            var produto = await _produtoRepository.GetProduto(produtoId);

            if (produto == null)
            {
                throw new ArgumentException("Produto não encontrado.");
            }

            produtoCarrinho.ProdutoId = produtoId;
            produtoCarrinho.QtdProduto = quantidade;
            produtoCarrinho.ValorTotalProduto = produto.precoUnitarioProduto * quantidade;

            await _carrinhoRepository.UpdateProdutoCarrinho(produtoCarrinho);
        }

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