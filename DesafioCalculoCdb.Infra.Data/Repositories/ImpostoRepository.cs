using DesafioCalculoCdb.Domain.Entities;
using DesafioCalculoCdb.Domain.Interfaces;
using DesafioCalculoCdb.Infra.Data.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DesafioCalculoCdb.Infra.Data.Repositories
{
    public class ImpostoRepository : IImpostoRepository
    {
        private ApplicationDbContext _impostoContext;
        public ImpostoRepository(ApplicationDbContext context)
        {
            _impostoContext = context;
        }

        public IEnumerable<Imposto> GetImpostosAtivosByIdImposto(IEnumerable<ImpostoInvestimento> listImpostoInvestimento)
        {
            return _impostoContext.Impostos.Include(a => a.Ativo == true && listImpostoInvestimento.Any(b => a.Id == b.IdImposto)).ToList();
        }
    }
}
