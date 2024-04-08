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

        [HttpGet("list-produtos-da-compra")]
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

        [HttpGet("get-produto-da-compra")]
        public async Task<ActionResult> GetCompraProduto([FromQuery] int compraId, [FromQuery] int produtoId)
        {
            try
            {
                CompraProdutoModel produto = await _compraProdutoService.GetCompraProduto(compraId, produtoId);
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}