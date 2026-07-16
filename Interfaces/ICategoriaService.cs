using FluxoDeEstoque.Models;
using System.Collections;

namespace FluxoDeEstoque.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<Categoria>> ListarTodas();
        Task<Categoria?> BuscarPorId(int id);
        Task<Categoria> Criar(Categoria categoria);
        Task<bool> Atualizar(int id, Categoria categoria);
        Task<bool> Deletar(int id);
    }
}
