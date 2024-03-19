using MinhaAPI.Domain.Models;
using MinhaAPI.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinhaAPI.Domain.Services
{
    public class AbateService
    {
        private readonly AbateRepository _abateRepository;
        private readonly CompraRepository _compraRepository;

        public AbateService(AbateRepository abateRepository,
                            CompraRepository compraRepository)
        {
            _abateRepository = abateRepository;
            _compraRepository = compraRepository;
        }

        public async Task<List<AbateModel>> ListComprasAbatidas()
        {
            List<AbateModel> list = await _abateRepository.ListComprasAbatidas();
            return list;
        }

        public async Task<AbateModel> GetCompraAbatida(int compraAbatidaId)
        {
            AbateModel compra = await _abateRepository.GetCompraAbatida(compraAbatidaId);

            if (compra == null)
                throw new ArgumentException("Compra não existe!");

            return compra;
        }

        public async Task<AbateModel> CreateCompraAbatida(int compraId, double valorAbatido)
        {
            CompraModel compra = await _compraRepository.GetCompra(compraId);

            if (compra == null)
                throw new ArgumentException("Compra não encontrada.");

            double valorTotalCompra = compra.ValorTotalCompra - valorAbatido;
            double valorParcelas = Math.Round((valorTotalCompra / compra.QtdParcelas), 2);

            if (valorParcelas < 40)
                throw new ArgumentException("O valor minimo da paracela é R$ 40.00");

            var resto = Math.Round((valorTotalCompra - valorParcelas * compra.QtdParcelas), 2);
            double valorParcelaAuxiliar;

            if (resto == 0)
                valorParcelaAuxiliar = 0;
            else
                valorParcelaAuxiliar = Math.Round((valorParcelas + resto), 2);

            var compraAbatida = new AbateModel
            {
                ValorAbate = valorAbatido,
                NovoValorTotalCompra = valorTotalCompra,
                NovoValorParcelas = valorParcelas,
                NovoValorParcelaAuxiliar = valorParcelaAuxiliar,
                CompraId = compraId
            };

            return await _abateRepository.CreateCompraAbatida(compraAbatida);
        }

        public async Task<int> UpdateCompraAbatida(int compraAbatidaId, double valorAbate)
        {
            AbateModel compraAbatida = await _abateRepository.GetCompraAbatida(compraAbatidaId);

            if (compraAbatida == null)
                throw new ArgumentException("Compra não encontrada.");

            CompraModel compra = await _compraRepository.GetCompra(compraAbatida.CompraId);

            double valorTotalCompra = compra.ValorTotalCompra - valorAbate;
            double valorParcelas = Math.Round((valorTotalCompra / compra.QtdParcelas), 2);

            if (valorParcelas < 40)
                throw new ArgumentException("O valor minimo da paracela é R$ 40.00");

            var resto = Math.Round((valorTotalCompra - valorParcelas * compra.QtdParcelas), 2);

            if (resto == 0)
                compraAbatida.NovoValorParcelaAuxiliar = 0;
            else
                compraAbatida.NovoValorParcelaAuxiliar = Math.Round((valorParcelas + resto), 2);

            compraAbatida.ValorAbate = valorAbate;
            compraAbatida.NovoValorTotalCompra = valorTotalCompra;
            compraAbatida.NovoValorParcelas = valorParcelas;

            return await _abateRepository.UpdateCompraAbatida(compraAbatida);
        }

        public async Task<bool> DeleteCompraAbatida(int compraAbatidaId)
        {
            AbateModel findCompraAbatida = await _abateRepository.GetCompraAbatida(compraAbatidaId);

            if (findCompraAbatida == null)
                throw new ArgumentException("Compra não existe!");

            await _abateRepository.DeleteCompraAbatida(compraAbatidaId);

            return true;
        }
    }
}
