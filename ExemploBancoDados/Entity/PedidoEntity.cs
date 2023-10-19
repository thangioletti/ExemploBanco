using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploBancoDados.Entity
{
    public class PedidoEntity
    {
        public int ID { get; set; }
        public string TICKET { get; set; }

        public List<ProdutoPedidoEntity> Produtos { get; set; }
    }
}
