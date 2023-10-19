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
                Console.WriteLine($"Atual = {value} deseja alterar ? S/N");
                char resposta = Convert.ToChar(Console.ReadLine().ToUpper());
                if (resposta == 'S')
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
                Console.WriteLine($"Atual = {value} deseja alterar ? S/N");
                char resposta = Convert.ToChar(Console.ReadLine().ToUpper());
                if (resposta == 'S')
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
    }
}
