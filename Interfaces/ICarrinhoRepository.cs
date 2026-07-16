using FluxoDeEstoque.Models;

namespace FluxoDeEstoque.Interfaces
{
    public interface ICarrinhoRepository
    {
        Task<Carrinho?> GetCarrinhoPorUsuarioAsync(string usuarioId);
        Task CriarCarrinhoAsync(Carrinho carrinho);
        void RemoverCarrinho(Carrinho carrinho);
        Task<bool> SalvarAsync();

    }
}
