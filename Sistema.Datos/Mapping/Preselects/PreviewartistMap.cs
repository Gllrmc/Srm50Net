using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Preselects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos.Mapping.Preselects
{
    class PreselectartistMap : IEntityTypeConfiguration<Preselectartist>
    {
        public void Configure(EntityTypeBuilder<Preselectartist> builder)
        {
            builder.ToTable("preselectartists")
               .HasKey(u => u.id);
            builder.HasOne(a => a.artist)
                .WithMany(d => d.preselectartists)
                .HasForeignKey(a => a.artistid);
            builder.HasOne(a => a.preselect)
                .WithMany(d => d.preselectartists)
                .HasForeignKey(a => a.preselectid);
        }
    }
}
