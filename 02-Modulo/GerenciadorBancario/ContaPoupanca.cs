namespace GerenciadorBancario
{
    public class ContaPoupanca : Conta
    {
        public ContaPoupanca(int numeroDaAgencia, int numeroDaConta, Cliente nomeDoTitular, decimal saldoDaConta)
            : base(numeroDaAgencia, numeroDaConta, nomeDoTitular, saldoDaConta)
        {
            
        }

        public decimal CalcularTributo()
        {
            return Saldo * 0.06m;
        }
    }
}