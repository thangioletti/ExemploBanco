using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploBancoDados.Entity
{
    public class ProdutoPedidoEntity
    {
        public int PRODUTO_ID { get; set; }
        public int PEDIDO_ID { get; set; }
        public double QUANTIDADE { get; set; }
        public double VALOR_PAGO { get; set; }

        public ProdutoEntity Produto { get; set; }

    }
}
