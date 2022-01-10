using System;

namespace SistemaBancario
{
    public abstract class Transacao
    {
        protected decimal valor;
        public bool executado;
        public bool revertido;


        public bool Executado => executado;
        public bool Revertido => revertido;
        public abstract bool Sucesso { get; set; }

        public Transacao(decimal valor)
        {
            this.valor = valor;
        }

        public abstract void Imprimir();

        public virtual void Executar()
        {
            if (Executado)
            {
                throw new Exception("A transação não pode ser executada novamente!");
            }

            executado = true;
        }

        public virtual void Reverter()
        {
            if (!Executado)
            {
                throw new Exception("Nenhuma transação não foi executada previamente!");
            }

            if (Revertido)
            {
                throw new Exception("Transação já desfeita. Uma nova transação precisa ser realizada!");
            }

            revertido = true;
        }
    }
}