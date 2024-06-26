﻿using CarrinhoCompraAPI.Domain.Models;
using CarrinhoCompraAPI.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarrinhoCompraAPI.Application.Controller
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
                if (ex.InnerException != null)
                    return BadRequest(ex.InnerException.Message);

                else
                    return BadRequest(ex.Message);
            }
        }

        [HttpPost("abater-valor-compra")]
        public async Task<ActionResult> AbaterValorCompra([FromQuery] int compraId, [FromQuery] decimal valorAbate)
        {
            try
            {
                var compra = await _compraService.AbaterValorCompra(compraId, valorAbate);
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
