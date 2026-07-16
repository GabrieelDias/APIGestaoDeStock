using FluxoDeEstoque.Models;
using Microsoft.AspNetCore.Mvc;

namespace FluxoDeEstoque.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<Categoria>> GetTodasCategorias();
        Task<Categoria?> GetCategoria(int id);
        Task CriarCategoria(Categoria categoria);
        Task AtualizarCategoria(Categoria categoria);
        Task DeletarCategoria(Categoria categoria);
        Task<bool> ExisteAsync(int id);
        Task<bool> SalvarAsync();
    }
}
