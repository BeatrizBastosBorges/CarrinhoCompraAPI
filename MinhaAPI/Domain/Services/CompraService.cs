using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using CarrinhoCompraAPI.Domain.Models;
using CarrinhoCompraAPI.Infrastructure.Data.Repositories;

namespace CarrinhoCompraAPI.Domain.Services
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

        // Lista todas as compras existentes
        public async Task<List<CompraModel>> ListCompras()
        {
            List<CompraModel> list = await _compraRepository.ListCompras();
            return list;
        }

        // Busca uma compra específica
        public async Task<CompraModel> GetCompra(int compraId)
        {
            CompraModel compra = await _compraRepository.GetCompra(compraId);

            if (compra == null)
                throw new ArgumentException("Compra não existe!");

            return compra;
        }

        // Cria uma compra
        public async Task<CompraModel> CreateCompra(int quantidadeParcelas)
        {
            if (quantidadeParcelas <= 0)
                throw new ArgumentException("A quantidade de parcelas deve ser maior que zero.");

            var produtosCarrinho = await _carrinhoRepository.ListProdutosCarrinho();
            if (produtosCarrinho.Count == 0)
                throw new InvalidOperationException("Não é possível criar uma compra sem produtos no carrinho.");

            // Calcula o valor total da compra
            decimal valorTotalCompra = Math.Round(produtosCarrinho.Sum(item => item.ValorTotalProduto), 2);

            // Calcula o valor da parcela
            decimal valorParcela = Math.Round((produtosCarrinho.Sum(item => Math.Round((item.ValorTotalProduto / quantidadeParcelas), 2))), 2);

            if (valorParcela < 40)
                throw new ArgumentException("O valor minimo da paracela é R$ 40.00");

            // Checa se uma das parcelas terá um valor diferente
            var resto = Math.Round(valorTotalCompra - valorParcela * quantidadeParcelas, 2);
            decimal valorParcelaAuxiliar = Math.Round(valorParcela + resto, 2);

            if (resto == 0)
                valorParcelaAuxiliar = 0;
            else
                valorParcelaAuxiliar = Math.Round(valorParcela + resto, 2);

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

            // Faz a relação de quais produtos e suas quantidades estão na compra criada
            foreach (var produto in produtosCarrinho)
            {
                decimal parcelaProduto = Math.Round((produto.ValorTotalProduto / quantidadeParcelas), 2);
                decimal restoProduto = Math.Round(produto.ValorTotalProduto - parcelaProduto * quantidadeParcelas, 2);
                decimal parcelaAuxiliarProduto = 0;

                if (restoProduto != 0)
                {
                    parcelaAuxiliarProduto = Math.Round(parcelaProduto + restoProduto, 2);
                }

                var compraProduto = new CompraProdutoModel
                {
                    CompraId = compra.Id,
                    ProdutoId = produto.ProdutoId,
                    QtdProduto = produto.QtdProduto,
                    ValorTotalProduto =  produto.ValorTotalProduto,
                    QtdParcelas = quantidadeParcelas,
                    ValorParcelasProduto = parcelaProduto,
                    ValorParcelaAuxiliarProduto = parcelaAuxiliarProduto,
                    ValorAbatidoProduto = 0,
                    QtdParcelasAtual = quantidadeParcelas,
                    ValorRestanteProduto = produto.ValorTotalProduto
                };

                await _compraProdutoRepository.AddCompraProduto(compraProduto);

                // Apaga todos os produtos presentes no carrinho
                await _carrinhoRepository.DeleteProdutoCarrinho(produto.Id);
            }

            return compra;
        }

        // Abate um determinado valor da compra
        public async Task<int> AbaterValorCompra(int compraId, decimal valorAbate)
        {
            if (valorAbate <= 0)
                throw new ArgumentException("O valor deve ser maior que zero.");

            var compra = await _compraRepository.GetCompra(compraId);
            if (compra == null)
                throw new ArgumentException("Compra não encontrada.");

            if (valorAbate > compra.ValorRestante)
                throw new ArgumentException("O valor de abate excede o valor restante da compra.");

            if (compra.QtdParcelas == 1 && valorAbate != compra.ValorRestante)
                throw new ArgumentException("O ultimo valor a ser abatido deve ser igual ao valor restante da compra");

            // Atualiza o valor que deverá ser pago futuramente
            compra.ValorRestante -= valorAbate;

            // Calcula quantas parcelas faltam para que o pagamento seja concluido
            int parcelasRestantes = (int)Math.Ceiling(compra.ValorRestante / compra.ValorParcelas);
            compra.QtdParcelasAtual = parcelasRestantes;

            if (compra.QtdParcelasAtual < 0)
                compra.QtdParcelasAtual = 0;

            // Checa se a soma das parcelas ultrapassar o valor restante e faz as devidas adaptações para cada caso
            if (valorAbate == compra.ValorParcelaAuxiliar)
            {
                compra.ValorParcelaAuxiliar = 0;
            }
            else if ((compra.ValorParcelas * parcelasRestantes) > compra.ValorRestante)
            {
                decimal diferenca = Math.Round((compra.ValorRestante - (parcelasRestantes - 1) * compra.ValorParcelas), 2);
                compra.ValorParcelaAuxiliar = compra.ValorParcelas + diferenca;
                compra.QtdParcelasAtual--;
            }
            else
            {
                compra.ValorParcelaAuxiliar = 0;
            }

            // Incrementa o valor pago até o momento
            compra.ValorAbatido += valorAbate;

            // Faz a listagem dos produtos da compra
            var produtosCompra = await _compraProdutoRepository.ListProdutosDaCompra(compra.Id);
            decimal valorTotalAbatidoProduto = 0;

            // Atualiza o valor abatido de todos os produtos ligados a compra
            foreach (var produto in produtosCompra)
            {
                // Calcula quanto cada parcel do produto vale na parcela da compra
                decimal porcentagemParcela = Math.Round((produto.ValorParcelasProduto * 100 / compra.ValorParcelas), 2);
                decimal valorAbateProduto = Math.Round((valorAbate * porcentagemParcela / 100), 2);

                produto.ValorRestanteProduto -= valorAbateProduto;
                produto.ValorAbatidoProduto += valorAbateProduto;
                produto.QtdParcelasAtual = compra.QtdParcelasAtual;

                decimal somaParcelas = Math.Round((produto.ValorParcelasProduto * produto.QtdParcelasAtual), 2);

                // Se soma de todas as parecals do produto resultarem em um valor diferente do valor restante do mesmo
                // a parcela auxiliar será ajustada para que isso nção ocorra
                if (somaParcelas != produto.ValorRestanteProduto)
                {
                    produto.ValorParcelaAuxiliarProduto = Math.Round(produto.ValorRestanteProduto - (produto.ValorParcelasProduto * (produto.QtdParcelasAtual - 1)), 2);
                }

                valorTotalAbatidoProduto += produto.ValorAbatidoProduto;

                // Se no ultimo produto for detectado que o total abatido em todos ios produtos for diferente do valor abatido
                // da compra a parcela auxiliar vai ser ajustada d acordo
                if (produto == produtosCompra[produtosCompra.Count - 1])
                {
                    if (valorTotalAbatidoProduto > compra.ValorAbatido)
                    {
                        decimal diferenca = valorTotalAbatidoProduto - compra.ValorAbatido;
                        produto.ValorAbatidoProduto -= diferenca;
                        produto.ValorRestanteProduto += diferenca;
                        if (produto.ValorParcelaAuxiliarProduto != 0)
                        {
                            produto.ValorParcelaAuxiliarProduto += diferenca;
                        }
                        else
                        {
                            produto.ValorParcelaAuxiliarProduto = produto.ValorParcelasProduto + diferenca;
                        }
                    }
                    else if (valorTotalAbatidoProduto < compra.ValorAbatido)
                    {
                        decimal diferenca = compra.ValorAbatido - valorTotalAbatidoProduto;
                        produto.ValorAbatidoProduto += diferenca;
                        produto.ValorRestanteProduto -= diferenca;
                        if (produto.ValorParcelaAuxiliarProduto != 0)
                        {
                            produto.ValorParcelaAuxiliarProduto -= diferenca;
                        }
                        else
                        {
                            produto.ValorParcelaAuxiliarProduto = produto.ValorParcelasProduto += -diferenca;
                        }
                    }
                }

                await _compraProdutoRepository.UpdateProdutoCompra(produto);
            }

            return await _compraRepository.UpdateCompra(compra);
        }

        // Apaga uma compra
        public async Task<bool> DeleteCompra(int compraId)
        {
            CompraModel findCompra = await _compraRepository.GetCompra(compraId);

            if (findCompra == null)
                throw new ArgumentException("Compra não existe!");

            // Apaga os produtos relacionados a determinada compra
            await _compraProdutoRepository.DeleteProdutosDaCompra(compraId);

            await _compraRepository.DeleteCompra(compraId);

            return true;
        }
    }
}