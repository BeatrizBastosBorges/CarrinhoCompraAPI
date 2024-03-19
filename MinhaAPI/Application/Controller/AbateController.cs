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
    public class AbateController : ControllerBase
    {
        private readonly AbateService _abateService;

        public AbateController(AbateService abateService)
        {
            _abateService = abateService;
        }

        [HttpGet("list-compras-abatidas")]
        public async Task<ActionResult> ListComprasAbatidas()
        {
            try
            {
                List<AbateModel> list = await _abateService.ListComprasAbatidas();
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-compra-abatida")]
        public async Task<ActionResult> GetCompraAbatida([FromQuery] int compraAbatidaId)
        {
            try
            {
                AbateModel compra = await _abateService.GetCompraAbatida(compraAbatidaId);
                return Ok(compra);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create-compra-abatida")]
        public async Task<ActionResult> CreateCompraAbatida([FromQuery] int compraId, double valorAbatido)
        {
            try
            {
                AbateModel compra = await _abateService.CreateCompraAbatida(compraId, valorAbatido);
                return Ok(compra);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update-compra-abatida")]
        public async Task<ActionResult> UpdateCompraAbatida([FromQuery] int compraAbatidaId, double valorAbate)
        {
            try
            {
                var compra = await _abateService.UpdateCompraAbatida(compraAbatidaId, valorAbate);
                return Ok(compra);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("delete-compra-abatida")]
        public async Task<ActionResult> DeleteCompraAbatida([FromQuery] int compraAbatidaId)
        {
            return Ok(await _abateService.DeleteCompraAbatida(compraAbatidaId));
        }
    }
}
