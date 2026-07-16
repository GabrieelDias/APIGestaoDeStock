using FluxoDeEstoque.Models;

namespace FluxoDeEstoque.Interfaces
{
    public interface IPedidoRepository
    {
        Task<Pedido?> GetPedidoPorIdAsync(int id);
        Task<IEnumerable<Pedido>> GetPedidosPorUsuarioAsync(string usuarioId);
        Task AdicionarPedidoAsync(Pedido pedido);
        Task<bool> SalvarAsync();
    }
}
