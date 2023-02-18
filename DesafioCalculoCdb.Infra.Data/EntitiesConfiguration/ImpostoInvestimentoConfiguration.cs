using DesafioCalculoCdb.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;

namespace DesafioCalculoCdb.Infra.Data.EntitiesConfiguration
{
    public class ImpostoInvestimentoConfiguration : IEntityTypeConfiguration<ImpostoInvestimento>
    {
        public void Configure(EntityTypeBuilder<ImpostoInvestimento> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.IdImposto).IsRequired();
            builder.Property(p => p.IdInvestimento).IsRequired();
            builder.Property(p => p.DataInicio).IsRequired();
            builder.Property(p => p.Ativo).IsRequired();

            builder.HasOne(e => e.Investimento).WithMany(e => e.ImpostoInvestimentos)
                .HasForeignKey(e => e.IdInvestimento);
            builder.HasOne(e => e.Imposto).WithMany(e => e.ImpostoInvestimentos)
                .HasForeignKey(e => e.IdImposto);
            

            builder.HasData(
              new ImpostoInvestimento(1, 1, DateTime.Now, null, true, 1),
              new ImpostoInvestimento(2, 1, DateTime.Now, null, true, 2),
              new ImpostoInvestimento(3, 1, DateTime.Now, null, true, 3),
              new ImpostoInvestimento(4, 1, DateTime.Now, null, true, 4)
            );
        }
    }
}
