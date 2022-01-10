using System;
using System.Text;

namespace SistemaBancario
{
    public class DepositoTransacao : Transacao
    {
        private readonly Conta conta;
        private readonly decimal valor;
        private readonly StringBuilder historicoTransacao;

        public override bool Sucesso { get; set; }

        public DepositoTransacao(Conta conta, decimal valor) : base(valor)
        {
            this.conta = conta;
            this.valor = valor;

            historicoTransacao = new StringBuilder();
        }

        public override void Executar()
        {
            base.Executar();

            Sucesso = conta.Deposito(valor);
        }

        public override void Reverter()
        {
            base.Reverter();

            Sucesso = conta.Deposito(valor);
        }

        private void MostrarHistoricoTransacaoExecucao()
        {
            if (Executado && Sucesso)
            {
                historicoTransacao.AppendLine("Depósito realizado com sucesso!");
                historicoTransacao.AppendLine($"Cliente: {conta.Nome}");
                historicoTransacao.AppendLine($"Saldo atual: {conta.Saldo}");
            }
            else
            {
                historicoTransacao.AppendLine("Erro ao realizar o saque!");
            }
        }

        private void MostrarHistoricoTransacaoReversao()
        {
            if (Executado && Revertido && Sucesso)
            {
                historicoTransacao.AppendLine("Operação desfeita com sucesso!");
                historicoTransacao.AppendLine($"Cliente: {conta.Nome}");
                historicoTransacao.AppendLine($"Saldo atual: {conta.Saldo}");
            }
            else if (Executado && Revertido && !Sucesso)
            {
                historicoTransacao.AppendLine("Falha na operação!");
            }
        }

        public override void Imprimir()
        {
            MostrarHistoricoTransacaoExecucao();

            MostrarHistoricoTransacaoReversao();

            Console.Write(Environment.NewLine);
            Console.Write(historicoTransacao.ToString());

            Limpar();
        }

        private void Limpar()
        {
            historicoTransacao.Clear();
        }
    }
}