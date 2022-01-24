using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data.Contexto
{
    public class LivrosDbContext : DbContext
    {
        public LivrosDbContext(DbContextOptions<LivrosDbContext> options)
            : base(options) { }

        public DbSet<Livro> Livros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}