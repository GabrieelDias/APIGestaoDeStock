using FluxoDeEstoque.Data;
using FluxoDeEstoque.Interfaces;
using FluxoDeEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeEstoque.Repository
{
    public class CupomRepository : ICupomRepository
    {
        private readonly AppDbContext _context;
        public CupomRepository(AppDbContext context) => _context = context;

        public async Task<Cupom?> GetPorCodigoAsync(string codigo) =>
            await _context.Cupons.FirstOrDefaultAsync(c => c.Codigo.ToUpper() == codigo.ToUpper() && c.Ativo);

        public void CriarCupom(Cupom cupom) =>
                _context.Cupons.Add(cupom);

        public async Task<bool> Salvar() => await _context.SaveChangesAsync() > 0;
    } 
}
