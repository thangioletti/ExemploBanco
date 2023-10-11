using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploBancoDados.Entity
{
    public class ProdutoEntity
    {
        public int ID { get; set; }
        public string DESCRICAO { get; set; }
        public double PRECO { get; set; }
        public int TIPO_ID { get; set; }
    }
}
