using FluxoDeEstoque.Data;
using FluxoDeEstoque.Models;
using FluxoDeEstoque.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeEstoque.Controllers
{
    [Route("api/categorias")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _service;


        public CategoriaController(ICategoriaService service) 
        {
            _service = service;
        }

        [HttpGet("/api/categorias")]
        public async Task<IActionResult> GetTodasCategorias()
        {
            var categoria = await _service.ListarTodas();
            return Ok(categoria);
        }

        [HttpGet("/api/categorias/{id}")]
        public async Task<IActionResult> GetCategoria(int id) 
        {
            var categoria = await _service.BuscarPorId(id);
            return categoria == null ? NotFound("Categoria não encontrada") : Ok(categoria);
        }

        [HttpPost("/api/categorias")]
        public async Task<IActionResult> CriarCategoria(Categoria categoria)
        {
            var criada = await _service.Criar(categoria);

            return CreatedAtAction(nameof(GetCategoria), new { id = criada.Id }, criada);
        }
    
        [HttpPut("/api/categorias/{id}")]
        public async Task<IActionResult> AtualizarCategoria(int id, Categoria categoria)
        {
            var upd = await _service.Atualizar(id, categoria);
            return upd ? NoContent() : BadRequest("Não foi possível atualizar a categoria");
        }
        
        [HttpDelete("/api/categorias/{id}")]
        public async Task<ActionResult> DeletarCategoria(int id) 
        {
            var del = await _service.Deletar(id);
            
            if(!del) { return BadRequest(); }

            return del ? NoContent() : NotFound("Categoria não encontrada");
        }
        
    }
}
