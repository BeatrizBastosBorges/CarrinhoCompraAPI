using MinhaAPI.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MinhaAPI.Infrastructure.Data.Repositories;

namespace MinhaAPI.Domain.Services
{
    public class ProdutoService
    {
        private readonly ProdutoRepository _produtoRepository;
        private readonly CompraProdutoRepository _compraProdutoRepository;

        public ProdutoService(ProdutoRepository produtoRepository,
                              CompraProdutoRepository compraProduto)
        {
            _produtoRepository = produtoRepository;
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
                throw new InvalidOperationException("Não é possível atualizar um produto que já esteja vinculado a uma compra.");

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

            await _produtoRepository.DeleteProduto(produtoId);

            return true;
        }
    }
}