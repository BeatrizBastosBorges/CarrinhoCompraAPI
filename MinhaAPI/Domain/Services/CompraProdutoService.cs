using MinhaAPI.Domain.Models;
using MinhaAPI.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaAPI.Domain.Services
{
    public class CompraProdutoService
    { 
        private readonly CompraProdutoRepository _compraProdutoRepository;

        public CompraProdutoService(CompraProdutoRepository compraProdutoRepository)
        {
            _compraProdutoRepository = compraProdutoRepository;
        }

        public async Task<List<ProdutoModel>> GetProdutosDaCompra(int compraId)
        {
            var compraProdutos = await _compraProdutoRepository.GetProdutosDaCompra(compraId);

            if (compraProdutos == null || !compraProdutos.Any())
                throw new ArgumentException("Não há produtos associados a esta compra.");

            return compraProdutos.Select(cp => cp.Produto).ToList();
        }

        public async Task<CompraProdutoModel> GetCompraProduto(int compraId, int produtoId)
        {
            CompraProdutoModel compraProduto = await _compraProdutoRepository.GetCompraProduto(compraId, produtoId);

            if (compraProduto == null)
                throw new ArgumentException("Produto não existe!");

            return compraProduto;
        }
    }
}
