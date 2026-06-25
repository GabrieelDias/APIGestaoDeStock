using FluxoDeEstoque.Models;
using System.Collections;

namespace FluxoDeEstoque.Repository
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAllProdutos();
        Task<Produto?> GetProduto(int id);
        Task<IEnumerable<Produto>> GetNomeProd(string nome);
        Task AdicionarProduto(Produto produto);
        Task AtualizarProduto(Produto produto);
        Task DeletarProduto(Produto produto);
        Task<bool> SalvarAsync();
        Task<IEnumerable<Produto>> ObterStockBaixo();
    }
}
