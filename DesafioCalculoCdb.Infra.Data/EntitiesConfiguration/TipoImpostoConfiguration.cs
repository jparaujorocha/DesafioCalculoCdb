using DesafioCalculoCdb.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DesafioCalculoCdb.Infra.Data.EntitiesConfiguration
{
    public class TipoImpostoConfiguration : IEntityTypeConfiguration<TipoImposto>
    {
        public void Configure(EntityTypeBuilder<TipoImposto> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();

            builder.HasData(
              new TipoImposto("PERCENTUAL", 1),
              new TipoImposto("VALOR FIXO",2)
            );
        }
    }
}
