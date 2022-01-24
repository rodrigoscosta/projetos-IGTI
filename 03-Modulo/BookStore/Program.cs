using BookStore.Data.Contexto;
using BookStore.Data.Gerador;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //1 - RECUPERA O IHOST NO QUAL SUPORTARA A APLICACAO.
            var host = CreateHostBuilder(args).Build();

            //2 - ENCONTRA A CAMADA DE SERVICO DENTRO DO ESCOPO
            using (var escopo = host.Services.CreateScope())
            {
                //3 - RECUPERA A INSTANCIA DE LIVROSDBCONTEXT EM NOSSA CAMADA DE SERVICO
                var servicos = escopo.ServiceProvider;

                var contexto = servicos.GetRequiredService<LivrosDbContext>();


                //4 - CHAMA O GERADOR DE DADOS PARA CRIAR OS DADOS EXEMPLO
                GeradorDados.Inicializar(servicos);
            }
            
            //CONTINUA EXECUTAR A APLICACAO
             host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}