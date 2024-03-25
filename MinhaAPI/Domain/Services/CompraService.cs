using MinhaAPI.Domain.Models;
using MinhaAPI.Infrastructure.Data.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace MinhaAPI.Domain.Services
{
    public class CompraService
    {
        private readonly CompraRepository _compraRepository;
        private readonly CarrinhoRepository _carrinhoRepository;
        private readonly CompraProdutoRepository _compraProdutoRepository;

        public CompraService(CompraRepository compraRepository,
                             CarrinhoRepository carrinhoRepository,
                             CompraProdutoRepository compraProdutoRepository)
        {
            _compraRepository = compraRepository;
            _carrinhoRepository = carrinhoRepository;
            _compraProdutoRepository = compraProdutoRepository;
        }

        public async Task<List<CompraModel>> ListCompras()
        {
            List<CompraModel> list = await _compraRepository.ListCompras();
            return list;
        }

        public async Task<CompraModel> GetCompra(int compraId)
        {
            CompraModel compra = await _compraRepository.GetCompra(compraId);

            if (compra == null)
                throw new ArgumentException("Compra não existe!");

            return compra;
        }

        public async Task<CompraModel> CreateCompra(int quantidadeParcelas)
        {
            if (quantidadeParcelas <= 0)
                throw new ArgumentException("A quantidade de parcelas deve ser maior que zero.");

            var produtosCarrinho = await _carrinhoRepository.ListProdutosCarrinho();
            if (produtosCarrinho.Count == 0)
                throw new InvalidOperationException("Não é possível criar uma compra sem produtos no carrinho.");

            double valorTotalCompra = Math.Round((produtosCarrinho.Sum(item => item.ValorTotalProduto)), 2);

            double valorParcela = Math.Round((valorTotalCompra / quantidadeParcelas), 2);

            if (valorParcela < 40)
                throw new ArgumentException("O valor minimo da paracela é R$ 40.00");

            var resto = Math.Round((valorTotalCompra - valorParcela * quantidadeParcelas), 2);
            double valorParcelaAuxiliar = Math.Round((valorParcela + resto), 2);

            if (resto == 0)
                valorParcelaAuxiliar = 0;
            else
                valorParcelaAuxiliar = Math.Round((valorParcela + resto), 2);

            var createCompra = new CompraModel
            {
                ValorTotalCompra = valorTotalCompra,
                QtdParcelas = quantidadeParcelas,
                ValorParcelas = valorParcela,
                ValorParcelaAuxiliar = valorParcelaAuxiliar,
                QtdParcelasAtual = quantidadeParcelas,
                ValorAbatido = 0,
                ValorRestante = valorTotalCompra
            };
            
            var compra = await _compraRepository.CreateCompra(createCompra);

            foreach (var produto in produtosCarrinho)
            {
                var compraProduto = new CompraProdutoModel
                {
                    CompraId = compra.Id,
                    ProdutoId = produto.ProdutoId,
                    QtdProduto = produto.QtdProduto
                };

                await _compraProdutoRepository.AddCompraProduto(compraProduto);

                await _carrinhoRepository.DeleteProdutoCarrinho(produto.Id);
            }

            return compra;
        }

        public async Task<int> AbaterValorCompra(int compraId, double valorAbate)
        {
            if (valorAbate <= 0)
                throw new ArgumentException("O valor deve ser maior que zero.");

            var compra = await _compraRepository.GetCompra(compraId);
            if (compra == null)
                throw new ArgumentException("Compra não encontrada.");

            if (valorAbate > compra.ValorRestante)
                throw new ArgumentException("O valor de abate excede o valor restante da compra.");

            compra.ValorRestante -= valorAbate;

            int parcelasRestantes = (int)Math.Ceiling(compra.ValorRestante / compra.ValorParcelas);
            compra.QtdParcelasAtual = parcelasRestantes;

            if (compra.QtdParcelasAtual < 0)
                compra.QtdParcelasAtual = 0;

            double somaParcelas = compra.ValorParcelas * parcelasRestantes; ;

            if (somaParcelas > compra.ValorRestante)
            {
                double diferenca = compra.ValorRestante - ((parcelasRestantes - 1) * compra.ValorParcelas);
                compra.ValorParcelaAuxiliar = compra.ValorParcelas + diferenca;
                compra.QtdParcelasAtual--;
            }
            else if (valorAbate == compra.ValorParcelaAuxiliar)
            {
                compra.ValorParcelaAuxiliar = 0;
            }
            else
            {
                compra.ValorParcelaAuxiliar = 0;
            }

            compra.ValorAbatido += valorAbate;

            return await _compraRepository.UpdateCompra(compra);
        }

        public async Task<bool> DeleteCompra(int compraId)
        {
            CompraModel findCompra = await _compraRepository.GetCompra(compraId);

            if (findCompra == null)
                throw new ArgumentException("Compra não existe!");

            await _compraProdutoRepository.DeleteProdutosDaCompra(compraId);

            await _compraRepository.DeleteCompra(compraId);

            return true;
        }
    }
}