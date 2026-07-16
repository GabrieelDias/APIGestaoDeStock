using FluxoDeEstoque.DTO;
using FluxoDeEstoque.Interfaces;
using FluxoDeEstoque.Models;
using FluxoDeEstoque.Services;
using Microsoft.AspNetCore.Mvc;

namespace FluxoDeEstoque.Controllers
{
    [Route("api/pedidos")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly VendaService _vendaService;
        private readonly IPedidoRepository _repo;

        public PedidosController(VendaService vendaService, IPedidoRepository repo) 
        {
            _vendaService = vendaService;
            _repo = repo; 
        }

        [HttpPost("{id}/checkout")]
        public async Task<IActionResult> Checkout([FromBody] FecharPedidoDTO dto)
        {
            var pedidoObj = await _vendaService.FecharPedido(dto);
            if (pedidoObj == null) return BadRequest("Não foi possível finalizar o pedido. Verifique o estoque ou carrinho.");

            return CreatedAtAction("GetPedido", new { id = pedidoObj.Id }, pedidoObj);
        }

        [HttpGet("{id}/pedidos")]
        public async Task<IActionResult> GetPedido(int id) 
        {
            var p = await _vendaService.RespostaPedido(id);
            if (p == null) return NotFound("Pedido não encontrado.");

            return Ok(p);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> AtualizarStatus(int id, [FromBody] StatusPedido status) 
        {
            var sucesso = await _vendaService.UpdateStatus(id, status);
            if(!sucesso) return BadRequest("Não foi possivel atualizar o status. Verifique os dados");
            return NoContent();
        }
    }
}

