using MySql.Data.MySqlClient;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExemploBancoDados.Entity;
using ExemploBancoDados.Helpers;

namespace ExemploBancoDados.Model
{
    public class TipoModel : Database, ICrud
    {

        public void Create()
        {
            TipoEntity tipo = new TipoEntity();
            Console.WriteLine("Digite a descricao");
            tipo.DESCRICAO = Console.ReadLine();

            using (MySqlConnection connection = new MySqlConnection(conectionString))
            {
                string sql = "INSERT INTO TIPO VALUE (NULL, @DESCRICAO)";
                int linhas = connection.Execute(sql, tipo);
                Console.WriteLine($"Tipo inserido - {linhas} linhas afetadas");
            }
        }

        public void Delete()
        {
            Read();
            Console.WriteLine("Digite o id para excluir");
            int id = Convert.ToInt32(Console.ReadLine());

            using (MySqlConnection connection = new MySqlConnection(conectionString))
            {
                var parameters = new { Id = id };
                string sql = "DELETE FROM TIPO WHERE ID = @ID";
                connection.Execute(sql, parameters);
                Console.WriteLine("Tipo excluido com sucesso");
            }
        }



        public void Read()
        {
            using (MySqlConnection con = new MySqlConnection(conectionString))
            {
                IEnumerable<TipoEntity> tipos = con.Query<TipoEntity>("SELECT ID, DESCRICAO FROM TIPO");
                foreach(TipoEntity tipo in tipos)
                {
                    tipo.Mostrar();
                }
            }
        }

        private TipoEntity getById(int id)
        {
            using (MySqlConnection con = new MySqlConnection(conectionString))
            {
                string sql = "SELECT ID, DESCRICAO FROM TIPO WHERE ID = @ID";
                var parameters = new { ID = id };
                return con.QueryFirst<TipoEntity>(sql, parameters);
            }
        }

        private TipoEntity getTipoEntity()
        {
            Console.WriteLine("Digite o id para editar");
            int id = Convert.ToInt32(Console.ReadLine());
            return getById(id);
        }

      
        private TipoEntity updateTipoDescricao(TipoEntity tipo)
        {
            Console.WriteLine($"Digite a nova descrição para o tipo {tipo.DESCRICAO}");
            tipo.DESCRICAO = Console.ReadLine();
            return tipo;
        }

       

        public void Update()
        {
            Read();
            TipoEntity tipo = getTipoEntity();
            tipo = updateTipoDescricao(tipo);

            string sql = "UPDATE TIPO SET DESCRICAO = @DESCRICAO WHERE ID = @ID";
            Execute(sql, tipo);
            Console.WriteLine("Tipo alterado com sucesso!");
        }
    }
}
