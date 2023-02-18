using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using DesafioCalculoCdb.Domain.Entities;

namespace DesafioCalculoCdb.Infra.Data.EntitiesConfiguration
{
    public class InvestimentoConfiguration : IEntityTypeConfiguration<Investimento>
    {
        public void Configure(EntityTypeBuilder<Investimento> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();
            builder.Property(p => p.ValorTaxaInvestimento).IsRequired();
            builder.Property(p => p.ValorTaxaBanco).IsRequired();
            builder.Property(p => p.DataDeCriacao).IsRequired();
            builder.Property(p => p.Ativo).IsRequired();

            builder.HasData(
              new Investimento("CDB", DateTime.Now, null, 0.9M, 108, true, 1),
              new Investimento("OUTROS", DateTime.Now, null, 0.1M, 112, false, 2)
            );
        }
    }
}
