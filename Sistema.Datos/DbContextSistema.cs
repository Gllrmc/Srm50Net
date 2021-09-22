using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Mapping.Usuarios;
using Sistema.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema.Datos
{
    public class DbContextSistema : DbContext
    {
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbContextSistema(DbContextOptions<DbContextSistema> options) : base(options)
        {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new RolMap());
        modelBuilder.ApplyConfiguration(new UsuarioMap());
    }
}
}
