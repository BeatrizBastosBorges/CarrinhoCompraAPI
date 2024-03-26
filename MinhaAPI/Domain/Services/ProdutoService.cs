using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using CarrinhoCompraAPI.Domain.Models;
using CarrinhoCompraAPI.Infrastructure.Data.Repositories;

namespace CarrinhoCompraAPI.Domain.Services
{
    public class ProdutoService
    {
        private readonly ProdutoRepository _produtoRepository;
        private readonly CarrinhoRepository _carrinhoRepository;
        private readonly CompraProdutoRepository _compraProdutoRepository;

        public ProdutoService(ProdutoRepository produtoRepository,
                              CarrinhoRepository carrinhoRepository,
                              CompraProdutoRepository compraProduto)
        {
            _produtoRepository = produtoRepository;
            _carrinhoRepository = carrinhoRepository;
            _compraProdutoRepository = compraProduto;
        }

        // Lista todos os produtos existentes
        public async Task<List<ProdutoModel>> ListProdutos()
        {
            List<ProdutoModel> list = await _produtoRepository.ListProdutos();
            return list;
        }

        // Busca um determinado produto
        public async Task<ProdutoModel> GetProduto(int produtoId)
        {
            ProdutoModel produto = await _produtoRepository.GetProduto(produtoId);

            if (produto == null)
                throw new ArgumentException("Produto não existe!");

            return produto;
        }

        // Cria um produto
        public async Task<ProdutoModel> CreateProduto(ProdutoModel produto)
        {
            produto = await _produtoRepository.CreateProduto(produto);

            return produto;
        }

        // Atualiza um produto
        public async Task<int> UpdateProduto(ProdutoModel produto)
        {
            bool produtoEstaVinculadoACompra = await _compraProdutoRepository.ProdutoVinculadoACompra(produto.Id);
            if (produtoEstaVinculadoACompra)
                throw new ArgumentException("Não é possível atualizar um produto que já esteja vinculado a uma compra.");

            var produtoCarrinho = await _carrinhoRepository.GetProdutoCarrinho(produto.Id);
            if (produtoCarrinho == null)
                throw new ArgumentException("Não é possível atualizar um produto que já esteja vinculado a um carrinho");

            return await _produtoRepository.UpdateProduto(produto);
        }

        // Apaga um determinado produto
        public async Task<bool> DeleteProduto(int produtoId)
        {
            ProdutoModel findProduto = await _produtoRepository.GetProduto(produtoId);
            if (findProduto == null)
                throw new ArgumentException("Produto não existe!");

            bool produtoEstaVinculadoACompra = await _compraProdutoRepository.ProdutoVinculadoACompra(produtoId);
            if (produtoEstaVinculadoACompra)
                throw new ArgumentException("Não é possível apagar um produto que já esteja vinculado a uma compra.");

            var produtoCarrinho = await _carrinhoRepository.GetProdutoCarrinho(produtoId);
            if (produtoCarrinho == null)
                throw new ArgumentException("Não é possível atualizar um produto que já esteja vinculado a um carrinho");

            await _produtoRepository.DeleteProduto(produtoId);

            return true;
        }
    }
}