using System;
using System.Collections.Generic;

namespace SistemaBancario
{
    public class Banco
    {
        private readonly List<Conta> contas;
        private readonly List<Transacao> transacoes;

        public Banco()
        {
            contas = new List<Conta>();
            transacoes = new List<Transacao>();
        }

        public void AdicionarConta(Conta conta)
        {
            if (conta == null)
            {
                throw new Exception("Conta inválida!");
            }

            contas.Add(conta);
            Console.WriteLine();
            Console.WriteLine("Conta criada com sucesso!");
        }

        public Conta RecuperarConta(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new Exception("Conta inválida!");
            }

            foreach (var conta in contas)
            {
                if (conta.Nome.ToLower() == nome.ToLower())
                {
                    return conta;
                }
            }

            return null;
        }

        public void ExecutarTransacao(Transacao transacao)
        {
            transacoes.Add(transacao);
            transacao.Executar();
        }
    }
}