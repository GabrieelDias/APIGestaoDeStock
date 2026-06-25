using FluxoDeEstoque.DTO;
using FluxoDeEstoque.Models;
using System.Collections;

namespace FluxoDeEstoque.Services
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> ListarTodos();
        Task<Produto?> BuscarPorId(int id);
        Task<IEnumerable<Produto>> FiltrarNome(string nome);
        Task<Produto?> CriarProduto(CriarProdutoDTO dto);
        Task<bool> Atualizar(int id, CriarProdutoDTO dto);
        Task<bool> Delete(int id);
        Task<bool> AjustarEstoque(int id, AjustarQuantidadeDTO dto);
        Task<IEnumerable<ProdutoDTO>> ListarStockBaixo();
    }
}
