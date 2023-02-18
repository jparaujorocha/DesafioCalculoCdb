using DesafioCalculoCdb.Domain.Entities;
using System.Collections.Generic;

namespace DesafioCalculoCdb.Domain.Interfaces
{
    public interface IImpostoRepository
    {
        IEnumerable<Imposto> GetImpostosAtivosByIdImposto(IEnumerable<ImpostoInvestimento> listImpostoInvestimento);
    }
}
