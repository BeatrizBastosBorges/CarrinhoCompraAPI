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
        private readonly ProdutoRepository _produtoRepository;

        public CompraProdutoService(CompraProdutoRepository compraProdutoRepository,
                                    CompraRepository compraRepository,
                                    ProdutoRepository produtoRepository)
        {
            _compraProdutoRepository = compraProdutoRepository;
            _compraRepository = compraRepository;
            _produtoRepository = produtoRepository;
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

        // Gera o quanto um produto vale em cada parcela da compra, lvando em conta a quantidade total desse produto
        public async Task<string> GerarParcelaProduto(int compraId, int produtoId)
        {
            CompraProdutoModel compraProduto = await _compraProdutoRepository.GetCompraProduto(compraId, produtoId);
            if (compraProduto == null)
                throw new ArgumentException("Dados inválidos.");

            CompraModel compra = await _compraRepository.GetCompra(compraId);
            if (compra == null)
                throw new ArgumentException("Compra não existe!");

            ProdutoModel produto = await _produtoRepository.GetProduto(produtoId);
            if (produto == null)
                throw new ArgumentException("Produto não existe!");

            // Calcula a parcela
            double parcelaProduto = Math.Round(produto.PrecoUnitarioProduto * compraProduto.QtdProduto / compra.QtdParcelas, 2);

            // Checa se uma das parcelas terá um valor diferente
            var resto = Math.Round(produto.PrecoUnitarioProduto * compraProduto.QtdProduto - parcelaProduto * compra.QtdParcelas, 2);

            if (resto == 0)
                return $"O produto está parcelado em {compra.QtdParcelas} vezes de R$ {parcelaProduto};";
            else
            {
                double parcelaAuxiliarProduto = Math.Round(parcelaProduto + resto, 2);
                return $"O produto está parcelado em {compra.QtdParcelas - 1} vezes de R$ {parcelaProduto} e uma parcela de R$ {parcelaAuxiliarProduto};";
            }
        }

        // Gera o quanto um produto vale em cada parcela da compra, lvando em conta a quantidade total desse produto
        public async Task<string> GerarParcelaUnidadeProduto(int compraId, int produtoId)
        {
            CompraProdutoModel compraProduto = await _compraProdutoRepository.GetCompraProduto(compraId, produtoId);
            if (compraProduto == null)
                throw new ArgumentException("Dados inválidos.");

            CompraModel compra = await _compraRepository.GetCompra(compraId);
            if (compra == null)
                throw new ArgumentException("Compra não existe!");

            ProdutoModel produto = await _produtoRepository.GetProduto(produtoId);
            if (produto == null)
                throw new ArgumentException("Produto não existe!");

            // Calcula a parcela
            double parcelaProduto = Math.Round(produto.PrecoUnitarioProduto / compra.QtdParcelas, 2);

            // Checa se uma das parcelas terá um valor diferente
            var resto = Math.Round(produto.PrecoUnitarioProduto - parcelaProduto * compra.QtdParcelas, 2);

            if (resto == 0)
                return $"A unidade do produto está parcelada em {compra.QtdParcelas} vezes de R$ {parcelaProduto};";
            else
            {
                double parcelaAuxiliarProduto = Math.Round(parcelaProduto + resto, 2);
                return $"A unidade do produto está parcelada em {compra.QtdParcelas - 1} vezes de R$ {parcelaProduto} e uma parcela de R$ {parcelaAuxiliarProduto};";
            }
        }
    }
}
