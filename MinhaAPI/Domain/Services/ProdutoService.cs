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

        public ProdutoService(ProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<List<ProdutoModel>> ListProdutos()
        {
            List<ProdutoModel> list = await _produtoRepository.ListProdutos();
            return list;
        }

        public async Task<ProdutoModel> GetProduto(int produtoId)
        {
            ProdutoModel produto = await _produtoRepository.GetProduto(produtoId);

            if (produto == null)
                throw new ArgumentException("Produto não existe!");

            return produto;
        }

        public async Task<ProdutoModel> CreateProduto(ProdutoModel produto)
        {
            produto = await _produtoRepository.CreateProduto(produto);

            return produto;
        }

        public async Task<int> UpdateProduto(ProdutoModel produto)
        {
            return await _produtoRepository.UpdateProduto(produto);
        }

        public async Task<bool> DeleteProduto(int produtoId)
        {
            ProdutoModel findProduto = await _produtoRepository.GetProduto(produtoId);

            if (findProduto == null)
                throw new ArgumentException("Produto não existe!");

            await _produtoRepository.DeleteProduto(produtoId);

            return true;
        }
    }
}