using Dapper;
using ExemploBancoDados.Entity;
using ExemploBancoDados.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploBancoDados.Model
{
    public class PedidoModel : Database
    {
        private ProdutoModel _produtoModel = new ProdutoModel();

        private ProdutoEntity GetProduto()
        {
            Console.WriteLine("Escolha o produto");
            _produtoModel.Read();
            Console.WriteLine("Digite o ID do produto");
            int id = Convert.ToInt32(Console.ReadLine());
            return _produtoModel.GetById(id);
        }

        private PedidoEntity CreateOrder()
        {
            PedidoEntity pedido = new PedidoEntity();
            Console.WriteLine("Digite o numero do cartao");
            pedido.TICKET = Console.ReadLine();
            pedido.Produtos = new List<ProdutoPedidoEntity>();
            char repetir = 'S';
            while (repetir == 'S')
            {
                ProdutoPedidoEntity produtoPedido = new ProdutoPedidoEntity();
                produtoPedido.Produto = GetProduto();
                Console.WriteLine("Qual a quantidade?");
                produtoPedido.PRODUTO_ID = produtoPedido.Produto.ID;
                produtoPedido.QUANTIDADE = Convert.ToDouble(Console.ReadLine());
                produtoPedido.VALOR_PAGO = produtoPedido.QUANTIDADE * produtoPedido.Produto.PRECO;
                pedido.Produtos.Add(produtoPedido);
                Console.WriteLine("Deseja adicionar mais um produto? S/N");
                repetir = Convert.ToChar(Console.ReadLine().ToUpper());
            }
            return pedido;
        }
        public void Create()
        {
            PedidoEntity pedido = CreateOrder();

            string sql = "INSERT INTO PEDIDO VALUE (NULL, @TICKET)";
            this.Execute(sql, pedido);

            sql = "SELECT * FROM PEDIDO ORDER BY ID DESC LIMIT 1";
            pedido.ID = this.GetConnection().QueryFirst<PedidoEntity>(sql).ID;


            Console.WriteLine($"{pedido.ID} Ticket {pedido.TICKET}");
            foreach (ProdutoPedidoEntity produtoPedido in pedido.Produtos)
            {
                produtoPedido.PEDIDO_ID = pedido.ID;
                sql = "INSERT INTO PRODUTO_PEDIDO VALUE (@PRODUTO_ID, @PEDIDO_ID, @QUANTIDADE, @VALOR_PAGO)";
                this.Execute(sql, produtoPedido);
                Console.WriteLine($"{produtoPedido.Produto.DESCRICAO} - Qtd = {produtoPedido.QUANTIDADE} - Valor R${produtoPedido.VALOR_PAGO}");
            }

        }
    }
}
