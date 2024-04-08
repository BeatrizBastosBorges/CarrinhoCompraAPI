using CarrinhoCompraAPI.Domain.Models;
using CarrinhoCompraAPI.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarrinhoCompraAPI.Domain.Services
{
    public class CompraProdutoService
    {
        private readonly CompraProdutoRepository _compraProdutoRepository;
        private readonly CompraRepository _compraRepository;

        public CompraProdutoService(CompraProdutoRepository compraProdutoRepository,
                                    CompraRepository compraRepository)
        {
            _compraProdutoRepository = compraProdutoRepository;
            _compraRepository = compraRepository;
        }

        // Lista todos o produtos vinculados a determinada compra
        public async Task<List<ProdutoModel>> ListProdutosDaCompra(int compraId)
        {
            var compra = await _compraRepository.GetCompra(compraId);
            if (compra == null)
                throw new ArgumentException("Compra não encontrada.");

            List<CompraProdutoModel> list = await _compraProdutoRepository.ListProdutosDaCompra(compraId);
            if (list == null || !list.Any())
                throw new ArgumentException("Não há produtos associados a esta compra.");

            return list.Select(cp => cp.Produto).ToList();
        }

        // Busca os detalhes da compra do produto
        public async Task<CompraProdutoModel> GetCompraProduto(int compraId, int produtoId)
        {
            var compra = await _compraRepository.GetCompra(compraId);
            if (compra == null)
                throw new ArgumentException("Compra não encontrada.");

            var produto = await _compraProdutoRepository.GetCompraProduto(compraId, produtoId);
            if (produto == null)
                throw new ArgumentException("Produto não encontrado");

            return produto;
        }
    }
}
