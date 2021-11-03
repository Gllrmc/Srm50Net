using Microsoft.EntityFrameworkCore;
using Sistema.Datos.Mapping.Artists;
using Sistema.Datos.Mapping.Checkins;
using Sistema.Datos.Mapping.Searchs;
using Sistema.Datos.Mapping.Usuarios;
using Sistema.Entidades.Artists;
using Sistema.Entidades.Checkins;
using Sistema.Entidades.Searchs;
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
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Selection> Selections { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Skillartist> Skillartists { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Checkin> Checkins { get; set; }
        public DbSet<Checkinartist> Checkinartists { get; set; }

        public DbContextSistema(DbContextOptions<DbContextSistema> options) : base(options)
        {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new SkillMap());
            modelBuilder.ApplyConfiguration(new SelectionMap());
            modelBuilder.ApplyConfiguration(new ArtistMap());
            modelBuilder.ApplyConfiguration(new SkillartistMap());
            modelBuilder.ApplyConfiguration(new ScheduleMap());
            modelBuilder.ApplyConfiguration(new RatingMap());
            modelBuilder.ApplyConfiguration(new NoteMap());
            modelBuilder.ApplyConfiguration(new PortfolioMap());
            modelBuilder.ApplyConfiguration(new CheckinMap());
            modelBuilder.ApplyConfiguration(new CheckinartistMap());
        }
    }
}
