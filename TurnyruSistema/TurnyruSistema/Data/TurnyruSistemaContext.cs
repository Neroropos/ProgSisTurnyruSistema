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
    }
}
