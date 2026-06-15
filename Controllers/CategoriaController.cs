using FluxoDeEstoque.Data;
using FluxoDeEstoque.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeEstoque.Controllers
{
    [Route("api/categorias")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public CategoriaController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("/api/categorias")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetTodasCategorias()
        {
            return await _appDbContext.Categorias.ToListAsync();
        }

        [HttpGet("/api/categorias/{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id) 
        {
            var categoria = await _appDbContext.Categorias.FindAsync(id);
            if(categoria == null)
            {
                return NotFound("Categoria não encontrada.");
            }
            return Ok(categoria);
        }

        [HttpPost("/api/categorias")]
        public async Task<ActionResult<Categoria>> CriarCategoria(Categoria categoria)
        {
            _appDbContext.Categorias.Add(categoria);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategoria), new { id = categoria.Id }, categoria);
        }
    
        [HttpPut("/api/categorias/{id}")]
        public async Task<ActionResult> AtualizarCategoria(int id, Categoria categoriaDto)
        {
            var categoria = await _appDbContext.Categorias.FindAsync(id);

            if (categoria == null)
                return NotFound("Categoria não encontrada");

            if (id != categoriaDto.Id) { return NotFound("Dados inválidos"); }

            categoria.Nome = categoriaDto.Nome;
            categoria.Descricao = categoriaDto.Descricao;

            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpDelete("/api/categorias/{id}")]
        public async Task<ActionResult> DeletarCategoria(int id) 
        {
            var categoria = await _appDbContext.Categorias.
            Include(c => c.Produtos).FirstOrDefaultAsync(c => c.Id == id );
            
            if (categoria == null) { return NotFound("Categoria não encontrada."); }

            if (categoria.Produtos.Any())
            {
                return Conflict();
            }

            _appDbContext.Categorias.Remove(categoria);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
        
    }
}
