using Dapper;
using ExemploBancoDados.Entity;
using ExemploBancoDados.Helpers;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploBancoDados.Model
{
    public class ProdutoModel : Database, ICrud
    {

       

        private int ChangeTipo(ProdutoEntity produto)
        {
            TipoModel tipoModel = new TipoModel();

            if (produto.TIPO_ID > 0)
            {
                Console.WriteLine($"Atual = {produto.TIPO.DESCRICAO}  deseja alterar ? S/N");
                char resposta = Convert.ToChar(Console.ReadLine().ToUpper());
                if (resposta == 'S')
                {
                    tipoModel.Read();
                    Console.WriteLine("Digite o id do tipo do produto");
                    produto.TIPO_ID = Convert.ToInt32(Console.ReadLine());
                }
            } else {
                tipoModel.Read();
                Console.WriteLine("Digite o id do tipo do produto");
                produto.TIPO_ID = Convert.ToInt32(Console.ReadLine());
            }
            return produto.TIPO_ID;
        }
        private ProdutoEntity Popular(ProdutoEntity produto)
        {
            Console.WriteLine("Digite o nome do produto");
            produto.DESCRICAO = ConsoleHelper.ChangeValue(produto.DESCRICAO);
            Console.WriteLine("Digite o preço do produto");
            produto.PRECO = ConsoleHelper.ChangeValue(produto.PRECO);

            produto.TIPO_ID = ChangeTipo(produto);
            return produto;
        }
        public void Create()
        {
            ProdutoEntity produto = new ProdutoEntity();
            produto = Popular(produto);
            string sql = "INSERT INTO PRODUTO VALUE (NULL, @DESCRICAO, @PRECO, @TIPO_ID)";
            int linhas = this.Execute(sql, produto);
            Console.WriteLine($"Produto inserido - {linhas} linhas afetadas");

        }

        public void Delete()
        {           
           var parameters = new { Id = GetIndex() };
           string sql = "DELETE FROM PRODUTO WHERE ID = @ID";
           this.Execute(sql, parameters);
           Console.WriteLine("Produto excluido com sucesso");            
        }

        private IEnumerable<ProdutoEntity> ListProdutoEntity()
        {
            string sql = "SELECT * FROM PRODUTO P JOIN TIPO T ON T.ID = P.TIPO_ID";
            return this.GetConnection().Query<ProdutoEntity, TipoEntity, ProdutoEntity>(
                sql,
                (produto, tipo) =>
                {
                    produto.TIPO = tipo;
                    return produto;
                }
            );
        }
        public void Read()
        {            
            foreach (var produto in ListProdutoEntity())
            {
                Console.WriteLine($"{produto.ID} - {produto.DESCRICAO} - Preço: {produto.PRECO} - Tipo: {produto.TIPO.DESCRICAO}");
            }
        }

        public ProdutoEntity GetById(int id = 0)
        {
            if (id == 0)
            {
                id = GetIndex();
            }
            return ListProdutoEntity().Where(o => o.ID == id).ToList()[0];
        }

        private int GetIndex()
        {
            Read();
            Console.WriteLine("Digite o id para continuar");
            return Convert.ToInt32(Console.ReadLine());
        }
        public void Update()
        {
            ProdutoEntity produto = Popular(GetById());
            string sql = "UPDATE PRODUTO SET DESCRICAO = @DESCRICAO, PRECO = @PRECO, TIPO_ID = @TIPO_ID WHERE ID = @ID";
            int linhas = this.Execute(sql, produto);
            Console.WriteLine($"Produto atualizado - {linhas} linhas afetadas");
        }
    }
}
