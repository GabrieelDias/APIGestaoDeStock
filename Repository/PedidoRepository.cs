using FluxoDeEstoque.Data;
using FluxoDeEstoque.Interfaces;
using FluxoDeEstoque.Models;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeEstoque.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;
        public PedidoRepository(AppDbContext context) => _context = context;

        public async Task<Pedido?> GetPedidoPorIdAsync(int id) =>
            await _context.Pedidos.Include(p => p.Cupom).Include(p => p.Itens).ThenInclude(i => i.Produto).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Pedido>> GetPedidosPorUsuarioAsync(string usuarioId) =>
            await _context.Pedidos.Include(p => p.Itens).Where(p => p.UsuarioId == usuarioId).ToListAsync();

        public async Task AdicionarPedidoAsync(Pedido pedido) => await _context.Pedidos.AddAsync(pedido);
        
        public async Task<bool> SalvarAsync() => (await _context.SaveChangesAsync()) > 0;
    }
}
