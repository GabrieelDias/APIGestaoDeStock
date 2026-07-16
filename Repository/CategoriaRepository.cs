using FluxoDeEstoque.Data;
using FluxoDeEstoque.Interfaces;
using FluxoDeEstoque.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeEstoque.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoriaRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Categoria>> GetTodasCategorias() 
        {
            return await _appDbContext.Categorias.ToListAsync();
        }

        public async Task<Categoria?> GetCategoria(int id) 
        {
           return await _appDbContext.Categorias.FindAsync(id);
        }

        public async Task CriarCategoria(Categoria categoria) 
        {
            await _appDbContext.Categorias.AddAsync(categoria);
        }

        public async Task AtualizarCategoria(Categoria categoria) 
        {
            _appDbContext.Categorias.Update(categoria);
        } 
        
        public async Task DeletarCategoria(Categoria categoria) 
        {
            _appDbContext.Categorias.Remove(categoria);
        }
        public async Task<bool> ExisteAsync(int id) 
        {
            return await _appDbContext.Categorias.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> SalvarAsync() 
        {
            var resultado = await _appDbContext.SaveChangesAsync();
            return resultado > 0;
        }
    }   
}
