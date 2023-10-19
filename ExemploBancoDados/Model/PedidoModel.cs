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
            pedido.TICKET = ConsoleHelper.AskString("Digite o numero do cartao");
            pedido = CriarProdutosPedido(pedido);
            return pedido;
        }

        private PedidoEntity  CriarProdutosPedido(PedidoEntity pedido)
        {
            pedido.Produtos = new List<ProdutoPedidoEntity>();

            char repetir = 'S';
            while (repetir == 'S')
            {
                ProdutoPedidoEntity produtoPedido = new ProdutoPedidoEntity();
                produtoPedido.Produto = GetProduto();
                produtoPedido.PRODUTO_ID = produtoPedido.Produto.ID;
                produtoPedido.QUANTIDADE = ConsoleHelper.AskDouble("Qual a quantidade?");
                produtoPedido.VALOR_PAGO = produtoPedido.QUANTIDADE * produtoPedido.Produto.PRECO;
                pedido.Produtos.Add(produtoPedido);
                repetir = ConsoleHelper.PerguntarSimNao("Deseja adicionar mais um produto");
            }
            return pedido;
        }

        private int InsertDBPedido(PedidoEntity pedido)
        {
            string sql = "INSERT INTO PEDIDO VALUE (NULL, @TICKET)";
            this.Execute(sql, pedido);

            sql = "SELECT * FROM PEDIDO ORDER BY ID DESC LIMIT 1";
            return this.GetConnection().QueryFirst<PedidoEntity>(sql).ID;
        }

        public void Create()
        {
            PedidoEntity pedido = CreateOrder();
            pedido.ID = InsertDBPedido(pedido);

            Console.WriteLine($"{pedido.ID} Ticket {pedido.TICKET}");
            foreach (ProdutoPedidoEntity produtoPedido in pedido.Produtos)
            {
                produtoPedido.PEDIDO_ID = pedido.ID;
                InsertDBProdutoPedido(produtoPedido);
                Console.WriteLine($"{produtoPedido.Produto.DESCRICAO} - Qtd = {produtoPedido.QUANTIDADE} - Valor R${produtoPedido.VALOR_PAGO}");
            }
        }

        private void InsertDBProdutoPedido(ProdutoPedidoEntity produtoPedido)
        {
            string sql = "INSERT INTO PRODUTO_PEDIDO VALUE (@PRODUTO_ID, @PEDIDO_ID, @QUANTIDADE, @VALOR_PAGO)";
            this.Execute(sql, produtoPedido);
        }

        private IEnumerable<PedidoEntity> GetPedidos()
        {
            string sql = "SELECT * FROM PEDIDO";
            return this.GetConnection().Query<PedidoEntity>(sql);
        }

        private PedidoEntity GetPedidoById(int id)
        {
            string sql = "SELECT * FROM PEDIDO WHERE ID = @ID";
            var parameters = new { ID = id };
            return this.GetConnection().QueryFirst<PedidoEntity>(sql, parameters);
        }

        private IEnumerable<ProdutoPedidoEntity> GetProdutoPedido(int pedidoId)
        {
            string sql = @"SELECT * 
                             FROM PRODUTO_PEDIDO 
                             JOIN PRODUTO 
                               ON PRODUTO.ID = PRODUTO_PEDIDO.PRODUTO_ID 
                             JOIN TIPO 
                               ON TIPO.ID = PRODUTO.TIPO_ID
                            WHERE PRODUTO_PEDIDO.PEDIDO_ID = @ID";
            var parameters = new { ID = pedidoId };
            return this.GetConnection().Query<ProdutoPedidoEntity, ProdutoEntity, TipoEntity, ProdutoPedidoEntity>(
                sql,
                (produtoPedido, produto, tipo) =>
                {
                    produto.TIPO = tipo;
                    produtoPedido.Produto = produto;
                    return produtoPedido;
                },
                parameters
            );
        }

        public void Read()
        {
            //1 - 15 - 12
            IEnumerable<PedidoEntity> pedidos = GetPedidos();
            foreach (PedidoEntity pedido  in pedidos)
            {
                pedido.Produtos = GetProdutoPedido(pedido.ID).ToList();
                Console.WriteLine($"{pedido.ID} - {pedido.TICKET} - {pedido.Produtos.Count}");
            }

            PedidoEntity pedidoDetalhado = GetPedidoById(ConsoleHelper.AskInt("Digite o id do pedido a detalhar"));
            pedidoDetalhado.Produtos = GetProdutoPedido(pedidoDetalhado.ID).ToList();
            Console.WriteLine("Pedido detalhado");
            double total = 0;
            Console.WriteLine($"{pedidoDetalhado.ID} - {pedidoDetalhado.TICKET} - {pedidoDetalhado.Produtos.Count}");
            foreach (ProdutoPedidoEntity produtoPedido in pedidoDetalhado.Produtos)
            {
                Console.WriteLine($"{produtoPedido.Produto.ID} - {produtoPedido.Produto.DESCRICAO} - {produtoPedido.Produto.TIPO.DESCRICAO} - Qtd = {produtoPedido.QUANTIDADE} - Valor Pago = {produtoPedido.VALOR_PAGO}");
                total += produtoPedido.VALOR_PAGO;
            }
            Console.WriteLine($"Total do pedido = {total}");


        }
    }
}
