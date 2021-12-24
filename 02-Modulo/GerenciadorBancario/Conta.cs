namespace GerenciadorBancario
{
    public class Conta
    {
        //Atributos
        public int numeroDaAgencia;
        public int numeroDaConta;
        public Cliente nomeDoTitular;
        public decimal saldoDaConta;

        public decimal Saldo => saldoDaConta;

        //Construtor
        public Conta(int numeroDaAgencia, int numeroDaConta, Cliente nomeDoTitular, decimal saldoDaConta)
        {
            this.numeroDaAgencia = numeroDaAgencia;
            this.numeroDaConta = numeroDaConta;
            this.nomeDoTitular = nomeDoTitular;
            this.saldoDaConta = saldoDaConta;
        }

        //Métodos
        public bool Sacar(decimal valorSaque) 
        { 
            if(saldoDaConta >= valorSaque && valorSaque > 0)
            {
                saldoDaConta-= valorSaque;
                
                return true;
            }
            
            return false;
        }

        public bool Depositar(decimal valorDeposito)
        {
            if(valorDeposito > 0)
            {
                saldoDaConta += valorDeposito;

                return true;
            }
            return false;
        }

        public bool Transferir(decimal valorTransferencia, Conta contaDestino)
        {
            if(saldoDaConta >= valorTransferencia && valorTransferencia > 0)
            {
                saldoDaConta -= valorTransferencia;

                contaDestino.Depositar(valorTransferencia);

                return true;
            }
            return false;
        }
    }
}