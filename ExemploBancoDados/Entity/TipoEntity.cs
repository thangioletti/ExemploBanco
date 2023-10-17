using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploBancoDados.Entity
{
    public class TipoEntity
    {
        public int ID { get; set; }
        public string DESCRICAO { get; set; }

        public void Mostrar()
        {
            Console.WriteLine($"{ID} - {DESCRICAO}");
        }
    }
}
