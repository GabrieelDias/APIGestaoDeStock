using FluxoDeEstoque.Models;

namespace FluxoDeEstoque.Interfaces
{
    public interface ICupomRepository
    {
        Task<Cupom?> GetPorCodigoAsync(string codigo);
        void CriarCupom(Cupom cupom);
        Task<bool> Salvar();
    }
}
