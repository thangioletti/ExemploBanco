using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploBancoDados.Helpers
{
    public class ConsoleHelper
    {
        public static string ChangeValue(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (PerguntarSimNao($"Atual = {value} deseja alterar") == 'S')
                {
                    Console.WriteLine("Digite o novo valor");
                    value = Console.ReadLine();
                }
            }
            else
            {
                value = Console.ReadLine();
            }
            return value;
        }
        public static double ChangeValue(double value)
        {
            if (value > 0)
            {                      
                if (PerguntarSimNao($"Atual = {value} deseja alterar") == 'S')
                {
                    Console.WriteLine("Digite o novo valor");
                    value = Convert.ToDouble(Console.ReadLine());
                }
            }
            else
            {
                value = Convert.ToDouble(Console.ReadLine());
            }
            return value;
        }

        public static char PerguntarSimNao(string mensagem = "Deseja continuar")
        {
            Console.WriteLine($"{mensagem}? S/N");
            return Convert.ToChar(Console.ReadLine().ToUpper());
        }

        public static double AskDouble(string pergunta)
        {
            Console.WriteLine(pergunta);
            return Convert.ToDouble(Console.ReadLine());
        }

        public static int AskInt(string pergunta)
        {
            Console.WriteLine(pergunta);
            return Convert.ToInt32(Console.ReadLine());
        }

        public static string AskString(string pergunta)
        {
            Console.WriteLine(pergunta);
            return Console.ReadLine();
        }


    }
}
