using DesafioCalculoCdb.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DesafioCalculoCdb.Shared.Enums;

namespace DesafioCalculoCdb.Infra.Data.EntitiesConfiguration
{
    public class ImpostoConfiguration : IEntityTypeConfiguration<Imposto>
    {
        public void Configure(EntityTypeBuilder<Imposto> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();
            builder.Property(p => p.IdTipoImposto).IsRequired();
            builder.Property(p => p.Valor).IsRequired();
            builder.Property(p => p.Ativo).IsRequired();

            builder.HasOne(e => e.TipoImposto).WithMany(e => e.Impostos)
                .HasForeignKey(e => e.IdTipoImposto);

            builder.HasData(
              new Imposto("CDB6", (int)EnumTipoImposto.PERCENTUAL, 22.5M, true, 1),
              new Imposto("CDB12", (int)EnumTipoImposto.PERCENTUAL, 20, true, 2),
              new Imposto("CDB24", (int)EnumTipoImposto.PERCENTUAL, 17.5M, true, 3),
              new Imposto("CDB24PLUS", (int)EnumTipoImposto.PERCENTUAL, 15, true, 4)
            );
        }
    }
}
