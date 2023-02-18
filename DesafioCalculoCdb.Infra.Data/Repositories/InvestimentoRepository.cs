using DesafioCalculoCdb.Domain.Entities;
using DesafioCalculoCdb.Domain.Interfaces;
using DesafioCalculoCdb.Infra.Data.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DesafioCalculoCdb.Infra.Data.Repositories
{
    public class InvestimentoRepository : IInvestimentoRepository
    {
        private ApplicationDbContext _investimentoContext;
        public InvestimentoRepository(ApplicationDbContext context)
        {
            _investimentoContext = context;
        }
        public async Task<Investimento> GetById(int id)
        {
            var investimento = await _investimentoContext.Investimentos.FindAsync(id);
            return investimento;
        }

        public async Task<IEnumerable<Investimento>> GetInvestimentosAtivos()
        {
            return await _investimentoContext.Investimentos.Include(a => a.Ativo == true).ToListAsync();
        }
    }
}
