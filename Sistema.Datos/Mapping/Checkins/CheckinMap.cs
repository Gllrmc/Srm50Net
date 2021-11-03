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
    class CheckinMap : IEntityTypeConfiguration<Checkin>
    {
        public void Configure(EntityTypeBuilder<Checkin> builder)
        {
            builder.ToTable("checkins")
               .HasKey(u => u.id);
        }
    }
}
