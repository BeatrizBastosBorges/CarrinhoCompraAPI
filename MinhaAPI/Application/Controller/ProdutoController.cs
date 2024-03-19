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
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _produtoService;

        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet("list-produtos")]
        public async Task<ActionResult> ListProdutos()
        {
            try
            {
                List<ProdutoModel> list = await _produtoService.ListProdutos();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-produto")]
        public async Task<ActionResult> GetProduto([FromQuery] int produtoId)
        {
            try
            {
                ProdutoModel produto = await _produtoService.GetProduto(produtoId);
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create-produto")]
        public async Task<ActionResult> CreateProduto([FromBody] ProdutoModel produto)
        {
            try
            {
                produto = await _produtoService.CreateProduto(produto);
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update-produto")]
        public async Task<ActionResult> UpdateProduto([FromBody] ProdutoModel produto)
        {
            try
            {
                return Ok(await _produtoService.UpdateProduto(produto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("delete-produto")]
        public async Task<ActionResult> DeleteProduto([FromBody] int produtoId)
        {
            return Ok(await _produtoService.DeleteProduto(produtoId));
        }
    }
}