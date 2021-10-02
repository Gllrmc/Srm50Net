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
    public class MainroleartistMap : IEntityTypeConfiguration<Mainroleartist>
    {
        public void Configure(EntityTypeBuilder<Mainroleartist> builder)
        {
            builder.ToTable("mainroleartists")
               .HasKey(u => u.id);
            builder.HasOne(a => a.artist)
                .WithMany(d => d.mainroleartists)
                .HasForeignKey(a => a.artistid);
            builder.HasOne(a => a.skill)
                .WithMany(d => d.mainroleartists)
                .HasForeignKey(a => a.skillid);
        }
    }
}
