using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploBancoDados.Entity
{
    public class TipoEntity
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public void Mostrar()
        {
            Console.WriteLine($"{Id} - {Descricao}");
        }
    }
}
