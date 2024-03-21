using Microsoft.AspNetCore.Mvc;
using MinhaAPI.Domain.Services;
using System;
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
