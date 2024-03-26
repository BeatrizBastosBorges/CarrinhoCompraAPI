using CarrinhoCompraAPI.Domain.Models;
using CarrinhoCompraAPI.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarrinhoCompraAPI.Application.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompraProdutoController : ControllerBase
    {
        private readonly CompraProdutoService _compraProdutoService;

        public CompraProdutoController(CompraProdutoService compraProdutoService)
        {
            _compraProdutoService = compraProdutoService;
        }

        [HttpGet("get-produtos-da-compra")]
        public async Task<ActionResult> ListProdutosDaCompra([FromQuery] int compraId)
        {
            try
            {
                List<ProdutoModel> list = await _compraProdutoService.ListProdutosDaCompra(compraId);
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("produto-parcela")]
        public async Task<ActionResult> GerarParcelaProduto([FromQuery] int compraId, [FromQuery] int produtoId)
        {
            try
            {
                var parcelaProduto = await _compraProdutoService.GerarParcelaProduto(compraId, produtoId);
                return Ok(parcelaProduto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("unidade-produto-parcela")]
        public async Task<ActionResult> GerarParcelaUnidadeProduto([FromQuery] int compraId, [FromQuery] int produtoId)
        {
            try
            {
                var parcelaUnidadeProduto = await _compraProdutoService.GerarParcelaUnidadeProduto(compraId, produtoId);
                return Ok(parcelaUnidadeProduto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
