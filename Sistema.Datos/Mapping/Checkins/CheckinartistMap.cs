using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Checkins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos.Mapping.Checkins
{
    class CheckinartistMap : IEntityTypeConfiguration<Checkinartist>
    {
        public void Configure(EntityTypeBuilder<Checkinartist> builder)
        {
            builder.ToTable("checkinartists")
               .HasKey(u => u.id);
            builder.HasOne(a => a.artist)
                .WithMany(d => d.checkinartists)
                .HasForeignKey(a => a.artistid);
            builder.HasOne(a => a.checkin)
                .WithMany(d => d.checkinartists)
                .HasForeignKey(a => a.checkinid);
        }
    }
}
