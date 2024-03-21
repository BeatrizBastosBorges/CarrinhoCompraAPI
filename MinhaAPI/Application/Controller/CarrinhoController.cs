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
    public class CarrinhoController : ControllerBase
    {
        private readonly CarrinhoService _carrinhoService;

        public CarrinhoController(CarrinhoService carrinhoService)
        {
            _carrinhoService = carrinhoService;
        }

        [HttpGet("list-produtos-carrinho")]
        public async Task<ActionResult> ListProdutosCarrinho()
        {
            try
            {
                List<CarrinhoModel> list = await _carrinhoService.ListProdutosCarrinho();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-produto-carrinho")]
        public async Task<ActionResult> GetProdutoCarrinho([FromQuery] int produtoId)
        {
            try
            {
                CarrinhoModel produto = await _carrinhoService.GetProdutoCarrinho(produtoId);
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-produto-carrinho")]
        public async Task<ActionResult> AddProduto([FromQuery] int produtoId, [FromQuery] int quantidade)
        {
            try
            {
                await _carrinhoService.AddProduto(produtoId, quantidade);
                return Ok("Produto adicionado ao carrinho com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update-produto-carrinho")]
        public async Task<ActionResult> UpdateProdutoCarrinho([FromQuery] int compraId, [FromQuery] int produtoId, [FromQuery] int quantidade)
        {
            try
            {
                await _carrinhoService.UpdateProdutoCarrinho(compraId, produtoId, quantidade);
                return Ok("Produto alterado com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("delete-produto-carrinho")]
        public async Task<ActionResult> DeleteProdutoCarrinho([FromQuery] int produtoId)
        {
            return Ok(await _carrinhoService.DeleteProdutoCarrinho(produtoId));
        }
    }
}
