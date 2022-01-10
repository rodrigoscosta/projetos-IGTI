using System;

namespace SistemaBancario
{
    class Program
    {
        public enum Opcoes
        {
            NovaConta = 0,
            Saque = 1,
            Deposito = 2,
            Transferencia = 3,
            Imprimir = 4,
            Sair = 5,
        }
        static void Main(string[] args)
        {
            Banco banco = new Banco();
            Opcoes opcaoUsuario;

            do
            {
                opcaoUsuario = LerOpcaoUsuario();

                switch (opcaoUsuario)
                {
                    case Opcoes.NovaConta:
                        CriarConta(banco);
                        break;
                    case Opcoes.Saque:
                        ExecutarSaque(banco);
                        break;
                    case Opcoes.Deposito:
                        ExecutarDeposito(banco);
                        break;
                    case Opcoes.Transferencia:
                        ExecutarTransferencia(banco);
                        break;
                    case Opcoes.Imprimir:
                        ExecutarImpressao(banco);
                        break;
                    case Opcoes.Sair:
                        Console.WriteLine("Até logo!!!");
                        break;
                    default:
                    break;
                }
            } while (opcaoUsuario != Opcoes.Sair);
        }

        private static Opcoes LerOpcaoUsuario()
        {
            int opcao;

            do
            {
                Console.WriteLine("======================= Banco =======================");
                Console.WriteLine("*                                                   *");
                Console.WriteLine("1 - Nova conta                                       ");
                Console.WriteLine("2 - Saque                                            ");
                Console.WriteLine("3 - Depósito                                         ");
                Console.WriteLine("4 - Transferência                                    ");
                Console.WriteLine("5 - Imprimir                                         ");
                Console.WriteLine("6 - Sair                                             ");
                Console.WriteLine("*                                                   *");
                Console.WriteLine("=====================================================");

                Console.WriteLine("Digite a opção desejada: ");

                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch
                {
                    opcao = -1;
                    Console.WriteLine("Opção inválida");
                }
            } while (opcao < 1 || opcao > 7);

            return (Opcoes)(opcao - 1);
        }

        private static Conta RecuperarConta(Banco banco, string rotuloAdicional = null)
        {
            Console.Write(Environment.NewLine);
            Console.WriteLine($"Digite o nome do usuário {rotuloAdicional}");
            string nome = Console.ReadLine();
            
            Conta contaAtual = banco.RecuperarConta(nome);

            if (contaAtual == null)
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine($"Nenhuma conta encontrada com o nome {nome}");
            }
            return contaAtual;
        }

        private static void CriarConta(Banco banco)
        {
            string nome = null;
            decimal saldo;
            bool sairDaOperacao;

            do
            {
                try
                {
                    if (string.IsNullOrEmpty(nome))
                    {
                        Console.Write("Digite o nome: ");
                        nome = Console.ReadLine();

                        if (string.IsNullOrEmpty(nome))
                        {
                            throw new Exception("O nome é obrigatório!");
                        }
                    }

                    Console.Write("Insira o valor do saldo R$ ");
                    saldo = decimal.Parse(Console.ReadLine());

                    if (saldo < 0)
                    {
                        throw new Exception("O valor não pode ser igual ou inferior a R$ 0.00!");
                    }

                    Conta conta = new Conta(saldo, nome);

                    banco.AdicionarConta(conta);

                    sairDaOperacao = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(Environment.NewLine + e.Message);   
                    sairDaOperacao = RefazerOperacao() == 0;
                }
            } while (!sairDaOperacao);
        }

        private static int RefazerOperacao()
        {
            int opcao = 0;

            do
            {
                Console.Write(Environment.NewLine + " Se deseja refazer a operação digite 1 ou 0 para sair: ");

                try
                {
                    opcao = int.Parse(Console.ReadLine());
                }
                catch (System.Exception)
                {
                    continue;
                }
            } while (opcao != 0 && opcao != 1);

            return opcao;
        }

        private static void ExecutarDeposito(Banco banco)
        {
            bool sairDaOperacao;
            DepositoTransacao depositoTransacao;

            do
            {
                decimal valor;

                Conta contaAtual = RecuperarConta(banco);

                if (contaAtual == null)
                {
                    return;
                }
                Console.Write(Environment.NewLine);
                Console.WriteLine("Você selecionou a opção de Depósito!");
                Console.Write(Environment.NewLine);
                Console.WriteLine("Digite o valor a ser depositado: ");

                try
                {
                    valor = decimal.Parse(Console.ReadLine());

                    depositoTransacao = new DepositoTransacao(contaAtual, valor);
                    banco.ExecutarTransacao(depositoTransacao);

                    depositoTransacao.Imprimir();
                    sairDaOperacao = depositoTransacao.Sucesso;

                    if (!sairDaOperacao)
                    {
                        Console.Write(Environment.NewLine);
                        Console.WriteLine("Operação inválida!");
                        sairDaOperacao = RefazerOperacao() == 0;
                    }
                }
                catch (Exception e) when (e.Message.Contains("Input string was not in a correct format!"))
                {
                    Console.Write(Environment.NewLine);
                    Console.WriteLine("Valor inválido!");
                    sairDaOperacao = RefazerOperacao() == 0;
                }
                catch (Exception e)
                {
                    Console.Write(Environment.NewLine);
                    Console.WriteLine($"{e.Message}");
                    sairDaOperacao = RefazerOperacao() == 0;
                }
            } while (!sairDaOperacao);
        }

        private static void ExecutarSaque(Banco banco)
        {
            bool sairDaOperacao;
            SaqueTransacao saqueTransacao;

            do
            {
                decimal valor;

                Conta contaAtual = RecuperarConta(banco);

                if (contaAtual == null)
                {
                    return;
                }
                Console.Write(Environment.NewLine);
                Console.WriteLine("Você selecionou a opção de Saque!");
                Console.Write(Environment.NewLine);
                Console.WriteLine("Digite o valor a ser sacado: ");

                try
                {
                    valor = decimal.Parse(Console.ReadLine());

                    saqueTransacao = new SaqueTransacao(contaAtual, valor);
                    banco.ExecutarTransacao(saqueTransacao);

                    saqueTransacao.Imprimir();
                    sairDaOperacao = saqueTransacao.Sucesso;

                    if (!sairDaOperacao)
                    {
                        Console.Write(Environment.NewLine);
                        Console.WriteLine("Operação inválida!");
                        sairDaOperacao = RefazerOperacao() == 0;
                    }
                }
                catch (Exception e) when (e.Message.Contains("Input string was not in a correct format!"))
                {
                    Console.Write(Environment.NewLine);
                    Console.WriteLine("Valor inválido!");
                    sairDaOperacao = RefazerOperacao() == 0;
                }
                catch (Exception e)
                {
                    Console.Write(Environment.NewLine);
                    Console.WriteLine($"{e.Message}");
                    sairDaOperacao = RefazerOperacao() == 0;
                }
            } while (!sairDaOperacao);
        }

        private static void ExecutarTransferencia(Banco banco)
        {
            bool sairDaOperacao;
            TransferenciaTransacao transferenciaTransacao;

            do
            {
                decimal valor;

                Conta contaDe = RecuperarConta(banco, "(conta origem) ");

                if (contaDe == null)
                {
                    return;
                }

                Conta contaPara = RecuperarConta(banco, "(conta destino) ");

                if (contaPara == null)
                {
                    return;
                }

                Console.Write(Environment.NewLine);
                Console.WriteLine("Você selecionou a opção de Transferência!");
                Console.Write(Environment.NewLine);
                Console.WriteLine("Digite o valor a ser transferido: ");

                try
                {
                    valor = decimal.Parse(Console.ReadLine());

                    transferenciaTransacao = new TransferenciaTransacao(contaDe, contaPara, valor);
                    banco.ExecutarTransacao(transferenciaTransacao);

                    transferenciaTransacao.Imprimir();
                    sairDaOperacao = transferenciaTransacao.Sucesso;

                    if (!sairDaOperacao)
                    {
                        Console.Write(Environment.NewLine);
                        Console.WriteLine("Operação inválida!");
                        sairDaOperacao = RefazerOperacao() == 0;
                    }
                }
                catch (Exception e) when (e.Message.Contains("Input string was not in a correct format!"))
                {
                    Console.Write(Environment.NewLine);
                    Console.WriteLine("Valor inválido!");
                    sairDaOperacao = RefazerOperacao() == 0;
                }
                catch (Exception e)
                {
                    Console.Write(Environment.NewLine);
                    Console.WriteLine($"{e.Message}");
                    sairDaOperacao = RefazerOperacao() == 0;
                }
            } while (!sairDaOperacao);
        }

        private static void ExecutarImpressao(Banco banco)
        {
            bool sairDaOperacao;

            do
            {
                Console.Write(Environment.NewLine);

                Conta conta = RecuperarConta(banco);

                if (conta == null)
                {
                    return;
                }

                Console.WriteLine(Environment.NewLine + $" {conta.Nome} seu saldo é de: R$ {conta.Saldo}");

                sairDaOperacao = RefazerOperacao() == 0;
            } while (!sairDaOperacao);
        }
    }
}