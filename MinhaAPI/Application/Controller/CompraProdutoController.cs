using Microsoft.AspNetCore.Mvc;
using MinhaAPI.Domain.Models;
using MinhaAPI.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<ActionResult> GetProdutosDaCompra([FromQuery] int compraId)
        {
            try
            {
                var produtosDaCompra = await _compraProdutoService.GetProdutosDaCompra(compraId);
                return Ok(produtosDaCompra);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-compra-produto")]
        public async Task<ActionResult> GetCompraProduto([FromQuery] int compraId, int produtoId)
        {
            try
            {
                CompraProdutoModel compraProduto = await _compraProdutoService.GetCompraProduto(compraId, produtoId);
                return Ok(compraProduto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
