using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrimerParcial.Models;

namespace PrimerParcial.Data
{
    public class PrimerParcialContext : DbContext
    {
        public PrimerParcialContext (DbContextOptions<PrimerParcialContext> options)
            : base(options)
        {
        }

        public DbSet<PrimerParcial.Models.Cliente> Clientes { get; set; } = default!;

        public DbSet<PrimerParcial.Models.Ciudad> Ciudades { get; set; }
    }
}
