using System;

namespace GerenciadorBancario
{
    public class Cliente
    {
        //Atributos
        public string nome;
        public string rg;
        public string cpf;
        public string endereco;

        //Construtor
        public Cliente(string nome, string rg, string cpf, string endereco)
        {
            this.nome = nome;
            this.rg = rg;
            this.cpf = cpf;
            this.endereco = endereco;
        }

        //Métodos
        public void SimulaInvestimento(decimal valorInvestido)
        {
            for (int i = 1; i <= 12; i++)
            {
                valorInvestido = valorInvestido * 1.01m;
            }
            
            Console.WriteLine($"O valor investido após um ano será de {valorInvestido}");
        }
    }
}