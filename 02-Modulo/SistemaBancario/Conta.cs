using System;

namespace SistemaBancario
{
    public class Conta
    {
        private decimal saldo;
        private string nome;

        public decimal Saldo => saldo;
        public string Nome => nome;

        public Conta(decimal saldo, string nome)
        {
            this.saldo = saldo;
            this.nome = nome;
        }

        public bool Deposito(decimal valor)
        {
            if (valor > 0)
            {
                saldo+= valor;

                return true;
            }

            return false;
        }

        public bool Saque(decimal valor)
        {
            if (saldo >= valor && valor > 0)
            {
                saldo-= valor;

                return true;
            }

            return false;
        }

        public void Imprimir()
        {
            Console.WriteLine($"Olá {nome}, seu saldo atual é de {saldo}");
        }
    }
}