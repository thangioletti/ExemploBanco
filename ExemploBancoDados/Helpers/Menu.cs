using ExemploBancoDados.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploBancoDados.Helpers
{
    public class Menu
    {

        private TipoModel _tipoModel = new TipoModel();
        private ProdutoModel _produtoModel = new ProdutoModel();
        public void MostrarMenuPrincipal()
        {
            switch (MenuPrincipal())
            {
                case 1:
                    MostrarMenuCrud(_tipoModel);
                    break;
                case 2:
                    MostrarMenuCrud(_produtoModel);
                    break;
                default:
                    Console.WriteLine("Opção invalida");
                    Console.ReadLine();
                    break;


            }

        }

        public void MostrarMenuCrud(ICrud crud)
        {

            switch (MenuCrud())
            {
                case 1:
                    crud.Read();
                    break;
                case 2:
                    crud.Create();
                    break;
                case 3:
                    crud.Update();
                    break;
                case 4:
                    crud.Delete();
                    break;
                case 0:
                    MostrarMenuPrincipal();
                    break;
                default:
                    Console.WriteLine("Opção invalida, precione uma tecla para continuar");
                    Console.ReadLine();
                    MostrarMenuCrud(crud);
                    break;


            }
            Console.WriteLine("Precione uma tecla para retornar ao menu");
            Console.ReadLine();
            MostrarMenuCrud(crud);


        }

        public int MenuPrincipal()
        {
            Console.Clear();

            Console.WriteLine("1 - Tipos");
            Console.WriteLine("2 - Produtos");
            return Convert.ToInt32(Console.ReadLine());
        }

        public int MenuCrud()
        {
            Console.Clear();

            Console.WriteLine("1 - Visualizar");
            Console.WriteLine("2 - Cadastrar");
            Console.WriteLine("3 - Editar");
            Console.WriteLine("4 - Excluir");
            Console.WriteLine("0 - Retornar");
            return Convert.ToInt32(Console.ReadLine());
        }
        
        
    }
}
