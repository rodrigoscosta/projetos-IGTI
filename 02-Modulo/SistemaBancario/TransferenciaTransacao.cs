using System;
using System.Text;

namespace SistemaBancario
{
    public class TransferenciaTransacao : Transacao
    {
        private readonly Conta contaDe;
        private readonly Conta contaPara;
        private readonly decimal valor;
        private readonly DepositoTransacao deposito;
        private readonly SaqueTransacao saque;
        private readonly StringBuilder historicoTransacao;

        public override bool Sucesso { get; set; }

        public TransferenciaTransacao(Conta contaDe, Conta contaPara, decimal valor) : base(valor)
        {
            this.contaDe = contaDe;
            this.contaPara = contaPara;
            this.valor = valor;

            saque = new SaqueTransacao(contaDe, valor);
            deposito = new DepositoTransacao(contaPara, valor);

            historicoTransacao = new StringBuilder();
        }

        public override void Executar()
        {
            base.Executar();

            saque.Executar();

            if (saque.Sucesso)
            {
                deposito.Executar();

                if (!deposito.Sucesso)
                {
                    deposito.Reverter();
                }
            }

            Sucesso = saque.Sucesso && deposito.Sucesso;
        }

        public override void Reverter()
        {
            base.Reverter();

            saque.Reverter();

            deposito.Reverter();

            Sucesso = saque.Sucesso && deposito.Sucesso;
        }

        private void MostrarHistoricoTransacaoExecucao()
        {
            if (Executado && Sucesso)
            {
                historicoTransacao.AppendLine("Transferência executada com sucesso!");
                historicoTransacao.AppendLine($"Transferido {valor} de {contaDe.Nome} para {contaPara.Nome}.");
            }
            else
            {
                historicoTransacao.AppendLine("Erro ao executar a transferência.");
            }
        }

        private void MostrarHistoricoTransacaoReversao()
        {
            if (Executado && Revertido && Sucesso)
            {
                historicoTransacao.AppendLine("Reversão executada com sucesso!");
                historicoTransacao.AppendLine($"Devolvido {valor} de {contaPara.Nome} para {contaDe.Nome}.");
            }
            else if (Executado && Revertido && !Sucesso)
            {
                historicoTransacao.AppendLine("Erro ao executar a reversão.");
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