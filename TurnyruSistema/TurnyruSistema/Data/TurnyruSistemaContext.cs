using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TurnyruSistema.Models
{
    public class TurnyruSistemaContext : DbContext
    {
        public TurnyruSistemaContext (DbContextOptions<TurnyruSistemaContext> options)
            : base(options)
        {
        }

        public DbSet<TurnyruSistema.Models.Turnyras> Turnyras { get; set; }
        public DbSet<TurnyruSistema.Models.Komanda> Komanda { get; set; }
        public DbSet<TurnyruSistema.Models.Zaidejas> Zaidejas { get; set; }
        public DbSet<TurnyruSistema.Models.KomandaTurnyras> KomandaTurnyras { get; set; }
        public DbSet<TurnyruSistema.Models.KompiuteriuZona> KompiuteriuZona { get; set; }
        public DbSet<TurnyruSistema.Models.Raundas> Raundas { get; set; }
        public DbSet<TurnyruSistema.Models.Zaidimas> Zaidimas { get; set; }
        public DbSet<TurnyruSistema.Models.Zinute> Zinute { get; set; }
        public DbSet<TurnyruSistema.Models.Organizatorius> Organizatorius { get; set; }
        public DbSet<TurnyruSistema.Models.Naudotojas> Naudotojas { get; set; }

    }
}
