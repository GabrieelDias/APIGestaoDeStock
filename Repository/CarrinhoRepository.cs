using FluxoDeEstoque.Data;
using FluxoDeEstoque.Interfaces;
using FluxoDeEstoque.Models;
using Microsoft.EntityFrameworkCore;

public class CarrinhoRepository : ICarrinhoRepository
{
    private readonly AppDbContext _appDbContext;
    public CarrinhoRepository(AppDbContext context) => _appDbContext = context;

    public async Task<Carrinho?> GetCarrinhoPorUsuarioAsync(string usuarioId) =>
        await _appDbContext.Carrinhos.Include(c => c.Itens).
        ThenInclude(i => i.Produto).
        FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);
    public async Task CriarCarrinhoAsync(Carrinho carrinho) => await _appDbContext.Carrinhos.AddAsync(carrinho);
    public void RemoverCarrinho(Carrinho carrinho) => _appDbContext.Carrinhos.Remove(carrinho);
    public async Task<bool> SalvarAsync() => (await _appDbContext.SaveChangesAsync()) > 0;
}