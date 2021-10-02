using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Searchs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos.Mapping.Searchs
{
    class SelectedartistMap : IEntityTypeConfiguration<Selectedartist>
    {
        public void Configure(EntityTypeBuilder<Selectedartist> builder)
        {
            builder.ToTable("selectedartists")
               .HasKey(u => u.id);
            builder.HasOne(a => a.artist)
                .WithMany(d => d.selectedartists)
                .HasForeignKey(a => a.artistid);
            builder.HasOne(a => a.selection)
                .WithMany(d => d.selectedartists)
                .HasForeignKey(a => a.selectionid);
        }
    }
}
