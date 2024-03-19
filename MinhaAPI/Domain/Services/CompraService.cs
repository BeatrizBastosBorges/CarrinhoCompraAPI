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

        public CompraService(CompraRepository compraRepository,
                             CarrinhoRepository carrinhoRepository)
        {
            _compraRepository = compraRepository;
            _carrinhoRepository = carrinhoRepository;
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

            var compra = new CompraModel
            {
                ValorTotalCompra = valorTotalCompra,
                QtdParcelas = quantidadeParcelas,
                ValorParcelas = valorParcela,
                ValorParcelaAuxiliar = valorParcelaAuxiliar
            };

            return await _compraRepository.CreateCompra(compra);
        }

        public async Task<int> UpdateCompra(int compraId, int quantidadeParcelas)
        {
            if (quantidadeParcelas <= 0)
                throw new ArgumentException("A quantidade de parcelas deve ser maior que zero.");

            CompraModel compra = await _compraRepository.GetCompra(compraId);

            if (compra == null)
                throw new ArgumentException("Compra não existe!");

            double valorParcela = Math.Round((compra.ValorTotalCompra / quantidadeParcelas), 2);

            if (valorParcela < 40)
                throw new ArgumentException("O valor mínimo da paracela é R$ 40.00");

            var resto = Math.Round((compra.ValorTotalCompra - compra.ValorParcelas * compra.QtdParcelas), 2);

            if (resto == 0)
                compra.ValorParcelaAuxiliar = 0;
            else
                compra.ValorParcelaAuxiliar = Math.Round((compra.ValorParcelas + resto), 2);

            compra.QtdParcelas = quantidadeParcelas;
            compra.ValorParcelas = valorParcela;

            return await _compraRepository.UpdateCompra(compra);
        }

        public async Task<bool> DeleteCompra(int compraId)
        {
            CompraModel findCompra = await _compraRepository.GetCompra(compraId);

            if (findCompra == null)
                throw new ArgumentException("Compra não existe!");

            await _compraRepository.DeleteCompra(compraId);

            return true;
        }
    }
}