using FluxoDeEstoque.DTO;
using FluxoDeEstoque.Interfaces;
using FluxoDeEstoque.Services;
using Microsoft.AspNetCore.Mvc;

namespace FluxoDeEstoque.Controllers
{
    [Route("api/carrinho")]
    [ApiController]
    public class CarrinhoController : ControllerBase
    {
        private readonly VendaService _vendaService;
        private readonly ICarrinhoRepository _carrinhoRepo;

        public CarrinhoController(VendaService vendaService, ICarrinhoRepository carrinhoRepo)
        {
            _vendaService = vendaService;
            _carrinhoRepo = carrinhoRepo;
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> GetCarrinho(string usuarioId)
        {
            var carrinho = await _vendaService.RespostaCart(usuarioId);
            if (carrinho == null) return NotFound("Carrinho vazio ou não encontrado");

            return Ok(carrinho);
        }

        [HttpPost("{usuarioId}/itens")]
        public async Task<IActionResult> AdicionarItem(string usuarioId, [FromBody] AddItemDTO dto)
        {
            var sucesso = await _vendaService.AdicionarAoCarrinho(usuarioId, dto);
            if (!sucesso) return BadRequest("Estoque insuficiente ou produto inválido");

            return Ok("Item atualizado com sucesso no carrinho");
        }
    }
}

