namespace GerenciadorBancario
{
    public class ContaInvestimento : Conta
    {
        public ContaInvestimento(int numeroDaAgencia, int numeroDaConta, Cliente nomeDoTitular, decimal saldoDaConta)
            : base(numeroDaAgencia, numeroDaConta, nomeDoTitular, saldoDaConta)
        {
            
        }

        public decimal CalcularTributo()
        {
            return Saldo * 0.08m;
        }
    }
}