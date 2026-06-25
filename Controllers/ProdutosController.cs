using FluxoDeEstoque.Data;
using FluxoDeEstoque.DTO;
using FluxoDeEstoque.Models;
using FluxoDeEstoque.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace FluxoDeEstoque.Controllers
{
    [Route("api/produtos")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _service;

        public ProdutosController(IProdutoService service)
        {
            _service = service;
        }

        [HttpGet("/api/produtos")]
        public async Task<IActionResult> GetAllProdutos()
        {
           var prod = await _service.ListarTodos();
           return Ok(prod); 
        }

        [HttpGet("/api/produtos/{id}")]
        public async Task<IActionResult> GetProduto(int id)
        {
            var prod = await _service.BuscarPorId(id);
            return prod == null ? NotFound("Produto não encontrado") : Ok(prod); 
        }

        [HttpGet("/api/produtos/pesquisa")] 
        public async Task<IActionResult> SearchProducts ([FromQuery] string nome) 
        {
            var prod = await _service.FiltrarNome(nome);
            return Ok(prod);
        }

        [HttpGet("/api/produtos/stock-baixo")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetStockBaixo()
        {
            var prod = await _service.ListarStockBaixo();
            return Ok(prod);
        }

        [HttpPost("/api/produtos")]
        public async Task<IActionResult> PostProduto( [FromBody] CriarProdutoDTO dto) 
        {
            var prod = await _service.CriarProduto(dto);
            if (prod == null) return BadRequest("Não foi possível cadastrar o produto.");

            return CreatedAtAction(nameof(GetProduto), new { id = prod.Id }, prod);
        }

        [HttpPut("/api/produtos/{id}")]
        public async Task<IActionResult> PutProduto(int id, [FromBody] CriarProdutoDTO produtoDTO) 
        {
            var prod = await _service.Atualizar(id, produtoDTO);
            return prod ? NoContent() : NotFound("Produto não encontrado");
        }

        [HttpDelete("/api/produtos/{id}")]
        public async Task<IActionResult> DeletarProduto(int id)
        {
            var prod = await _service.Delete(id);
            return prod ? NoContent() : NotFound("Produto não encontrado");
        }

        [HttpPatch("api/produtos/{id}/quantidade")]
        public async Task<IActionResult> AjustarQuantidade(int id, AjustarQuantidadeDTO dto)
        {
            var prod = await _service.AjustarEstoque(id, dto);
            if(!prod) { return BadRequest("Valores negativos não são aceitos");}
            return NoContent();
        }

    }
}
