using Microsoft.EntityFrameworkCore;
using SistemaDeEventos.Models;

namespace SistemaDeEventos.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<EventoCliente> EventoClientes { get; set; }
    }
}