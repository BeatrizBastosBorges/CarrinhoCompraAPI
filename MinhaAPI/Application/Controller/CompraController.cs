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
    public class CompraController : ControllerBase
    {
        private readonly CompraService _compraService;

        public CompraController(CompraService compraService)
        {
            _compraService = compraService;
        }

        [HttpGet("list-compras")]
        public async Task<ActionResult> ListCompras()
        {
            try
            {
                List<CompraModel> list = await _compraService.ListCompras();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-compra")]
        public async Task<ActionResult> GetCompra([FromQuery] int compraId)
        {
            try
            {
                CompraModel compra = await _compraService.GetCompra(compraId);
                return Ok(compra);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create-compra")]
        public async Task<ActionResult> CreateCompra([FromQuery] int quantidadeParcelas)
        {
            try
            {
                CompraModel compra = await _compraService.CreateCompra(quantidadeParcelas);
                return Ok(compra);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update-compra")]
        public async Task<ActionResult> UpdateCompra([FromQuery] int compraId, [FromQuery] int quantidadeParcelas)
        {
            try
            {
                var compra = await _compraService.UpdateCompra(compraId, quantidadeParcelas);
                return Ok(compra);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("delete-compra")]
        public async Task<ActionResult> DeleteCompra([FromQuery] int compraId)
        {
            return Ok(await _compraService.DeleteCompra(compraId));
        }
    }
}
