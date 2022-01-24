using BookStore.Data.Contexto;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BookStore.Data.Gerador
{
    public class GeradorDados
    {
        public static void Inicializar(IServiceProvider serviceProvider)
        {
            using (var contexto = new LivrosDbContext(serviceProvider.GetRequiredService<DbContextOptions<LivrosDbContext>>()))
            {
                if (contexto.Livros.Any())
                {
                    return;
                }

                contexto.Livros.AddRange(
                    new Livro 
                    { 
                        Id = 1,
                        Autor = "MICROSOFT",
                        Edicao = 12,
                        Editora = "MICROSOFT",
                        ISBN = Guid.NewGuid().ToString(),
                        Titulo = "ASP.NET CORE WEB API, COSMOS DB E SIGNALR"
                    });

                contexto.Usuarios.Add(new Usuario { Id = 1, Name = "Renato", Password = "Admin@123", Role = "admin" });
                contexto.Usuarios.Add(new Usuario { Id = 2, Name = "Pedro", Password = "User@123", Role = "user" });

                contexto.SaveChanges();
            }
        }
    }
}