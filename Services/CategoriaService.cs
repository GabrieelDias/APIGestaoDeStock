using FluxoDeEstoque.Models;
using FluxoDeEstoque.Repository;

namespace FluxoDeEstoque.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repo;

        public CategoriaService(ICategoriaRepository repo) 
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Categoria>> ListarTodas() 
        {
            return await _repo.GetTodasCategorias();
        }

        public async Task<Categoria?> BuscarPorId(int id) 
        {
            return await _repo.GetCategoria(id);
        }

        public async Task<Categoria> Criar(Categoria categoria) 
        {
            await _repo.CriarCategoria(categoria);
            await _repo.SalvarAsync();
            return categoria;
        }

        public async Task<bool> Atualizar(int id, Categoria categoria) 
        {
            var existe = await _repo.GetCategoria(id);
            if (existe == null) return false;

            existe.Nome = categoria.Nome;
            existe.Descricao = categoria.Descricao;

            await _repo.AtualizarCategoria(existe);
            return await _repo.SalvarAsync();
        }

        public async Task<bool> Deletar(int id) 
        {
            var existe = await _repo.GetCategoria(id);
            if (existe == null) return false;
            await _repo.DeletarCategoria(existe);
            return await _repo.SalvarAsync();

        }
    }
}
