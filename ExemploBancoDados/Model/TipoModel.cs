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
            throw new NotImplementedException();
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
            try
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
                
            } catch(MySqlException ex)
            {
                Console.WriteLine("Erro MySql");
                Console.WriteLine(ex.Message);
            } catch (Exception ex)
            {
                Console.WriteLine("Erro");
                Console.WriteLine(ex.Message);
            }
            
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
