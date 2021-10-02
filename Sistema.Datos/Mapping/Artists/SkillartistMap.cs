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
    public class SkillartistMap : IEntityTypeConfiguration<Skillartist>
    {
        public void Configure(EntityTypeBuilder<Skillartist> builder)
        {
            builder.ToTable("skillartists")
               .HasKey(u => u.id);
            builder.HasOne(a => a.artist)
                .WithMany(d => d.skillartists)
                .HasForeignKey(a => a.artistid);
            builder.HasOne(a => a.skill)
                .WithMany(d => d.skillartists)
                .HasForeignKey(a => a.skillid);
        }
    }
}
