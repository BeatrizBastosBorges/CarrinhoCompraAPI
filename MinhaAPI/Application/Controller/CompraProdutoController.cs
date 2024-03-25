using Microsoft.AspNetCore.Mvc;
using MinhaAPI.Domain.Models;
using MinhaAPI.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinhaAPI.Application.Controller
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
    }
}
