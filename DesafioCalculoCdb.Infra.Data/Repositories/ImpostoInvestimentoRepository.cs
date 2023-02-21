﻿using DesafioCalculoCdb.Domain.Entities;
using DesafioCalculoCdb.Domain.Interfaces;
using DesafioCalculoCdb.Infra.Data.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioCalculoCdb.Infra.Data.Repositories
{
    public class ImpostoInvestimentoRepository : IImpostoInvestimentoRepository
    {
        private ApplicationDbContext _impostoInvestimentoContext;
        public ImpostoInvestimentoRepository(ApplicationDbContext context)
        {
            _impostoInvestimentoContext = context;
        }

        public ImpostoInvestimentoRepository()
        {

        }

        public async Task<ImpostoInvestimento> GetById(int id)
        {
            var impostoInvestimento = await _impostoInvestimentoContext.ImpostoInvestimentos.FindAsync(id);
            return impostoInvestimento;
        }

        public IEnumerable<ImpostoInvestimento> GetByIdInvestimento(int idInvestimento)
        {
            return _impostoInvestimentoContext.ImpostoInvestimentos.Where(a => a.IdInvestimento == idInvestimento).
                                                                    Include(b => b.Investimento).Include(b => b.Imposto).ToList();
        }

        public async Task<IEnumerable<ImpostoInvestimento>> GetImpostosInvestimentosAtivos()
        {
            return await _impostoInvestimentoContext.ImpostoInvestimentos.Where(a => a.Ativo).Include(b => b.Investimento).Include(b => b.Imposto).ToListAsync();
        }
    }
}
