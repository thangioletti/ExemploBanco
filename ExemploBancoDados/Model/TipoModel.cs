using MySql.Data.MySqlClient;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExemploBancoDados.Entity;

namespace ExemploBancoDados.Model
{
    public class TipoModel : ICrud
    {
        public string conectionString = "Server=localhost;Database=cerveja;User=root;Password=root;";

        public void Create()
        {
            TipoEntity tipo = new TipoEntity();
            Console.WriteLine("Digite a descricao");
            tipo.Descricao = Console.ReadLine();

            using (MySqlConnection connection = new MySqlConnection(conectionString))
            {
                string sql = "INSERT INTO TIPO VALUE (NULL, @Descricao)";
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
                string sql = "DELETE FROM TIPO WHERE ID = @Id";
                connection.Execute(sql, parameters);
                Console.WriteLine("Tipo excluido com sucesso");
            }
        }



        public void Read()
        {
            using (MySqlConnection con = new MySqlConnection(conectionString))
            {
                IEnumerable<TipoEntity> tipos = con.Query<TipoEntity>("SELECT ID as Id, DESCRICAO as Descricao FROM TIPO");
                foreach(TipoEntity tipo in tipos)
                {
                    tipo.Mostrar();
                }
            }
        }

        public void ReadModoRuim()
        {

                string conectionString = "Server=localhost;Database=cerveja;User=root;Password=root;";
                MySqlConnection connection = new MySqlConnection(conectionString);
                connection.Open();                
                string sql = "SELECT * FROM TIPO";
                MySqlCommand command = new MySqlCommand(sql, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["ID"]} - {reader["DESCRICAO"]}");
                    }
                }
                
            
            
        }

        private TipoEntity getById(int id)
        {
            using (MySqlConnection con = new MySqlConnection(conectionString))
            {
                string sql = "SELECT ID as Id, DESCRICAO as Descricao FROM TIPO WHERE ID = @Id";
                var parameters = new { Id = id };
                return con.QueryFirst<TipoEntity>(sql, parameters);
            }
        }

        private TipoEntity getTipoEntity()
        {
            Console.WriteLine("Digite o id para editar");
            int id = Convert.ToInt32(Console.ReadLine());
            return getById(id);
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(conectionString);
        }
        private TipoEntity updateTipoDescricao(TipoEntity tipo)
        {
            Console.WriteLine($"Digite a nova descrição para o tipo {tipo.Descricao}");
            tipo.Descricao = Console.ReadLine();
            return tipo;
        }

        private int Execute(string sql, object obj)
        {
            using (MySqlConnection con = GetConnection())
            {
                return con.Execute(sql, obj);
            }
        }

        public void Update()
        {
            Read();
            TipoEntity tipo = getTipoEntity();
            tipo = updateTipoDescricao(tipo);

            string sql = "UPDATE TIPO SET DESCRICAO = @Descricao WHERE ID = @Id";
            Execute(sql, tipo);
            Console.WriteLine("Tipo alterado com sucesso!");
        }
    }
}
