using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Artists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos.Mapping.Artists
{
    public class PortfolioMap : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.ToTable("portfolios")
               .HasKey(u => u.id);
            builder.HasOne(a => a.artist)
                .WithMany(d => d.portfolios)
                .HasForeignKey(a => a.artistid);
        }
    }
}
