using FluxoDeEstoque.Data;
using FluxoDeEstoque.Interfaces;
using FluxoDeEstoque.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeEstoque.Controllers
{
    [ApiController]
    [Route("api/cupons")]
    public class CupomController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ICupomRepository _repo;
        public CupomController(AppDbContext appDbContext, ICupomRepository repo) 
        {
            _context = appDbContext;
            _repo = repo;
        }

        [HttpPost("criar")]
        public async Task<IActionResult> CriarCupom([FromBody] Cupom novoCupom)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            novoCupom.Codigo = novoCupom.Codigo.ToUpper();

            _context.Cupons.Add(novoCupom); 
            await _context.SaveChangesAsync();

            return Ok("Cupom criado com sucesso");
        }
    }
}
