using ExemploBancoDados.Helpers;
using ExemploBancoDados.Model;
using MySql.Data.MySqlClient;

namespace ExemploBancoDados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try { 
                Menu menu = new Menu();
                menu.MostrarMenuPrincipal();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erro MySql");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro");
                Console.WriteLine(ex.Message);
            }
        }
    }
}