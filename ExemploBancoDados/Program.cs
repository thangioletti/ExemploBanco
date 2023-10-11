using ExemploBancoDados.Model;

namespace ExemploBancoDados
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TipoModel tipoModel = new TipoModel();
            tipoModel.Read();
            tipoModel.Create();
            tipoModel.Read();
        }
    }
}