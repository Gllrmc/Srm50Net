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
    class PreselectMap : IEntityTypeConfiguration<Preselect>
    {
        public void Configure(EntityTypeBuilder<Preselect> builder)
        {
            builder.ToTable("preselects")
               .HasKey(u => u.id);
        }
    }
}
