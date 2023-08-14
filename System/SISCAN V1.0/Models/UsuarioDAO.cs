using MySql.Data.MySqlClient;
using SISCAN.Database;
using SISCAN.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SISCAN.Models
{
    internal class UsuarioDAO
    {
        private static Conexao conn;
        public UsuarioDAO()
        {
            conn = new Conexao();
        }

        public List<Usuario> List(string busca)
        {
            try
            {
                List<Usuario> listCli = new List<Usuario>();

                var query = conn.Query();

                if (busca == null)
                {
                    query.CommandText = "SELECT * FROM Usuario LEFT JOIN Funcionario ON Usuario.id_func_fk = Funcionario.id_func;";
                }
                else
                {
                    query.CommandText = $"SELECT * FROM Usuario, Funcionario WHERE (Usuario.id_func_fk = Funcionario.id_func) AND (usuario_usu LIKE '%{busca}%');";
                }

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    listCli.Add(new Usuario()
                    {
                        Id = reader.GetInt32("id_usu"),
                        UsuarioNome = DAOHelper.GetString(reader, "usuario_usu"),
                        Senha = DAOHelper.GetString(reader, "senha_usu"),
                        Funcionario = DAOHelper.IsNull(reader, "id_func_fk") ? null : new Funcionario() { Id = reader.GetInt32("id_func"), Nome = reader.GetString("nome_func") }
                    });
                }

                return listCli;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Insert(Usuario usuario) 
        {
            try
            {
                var query = conn.Query();
                query.CommandText = $"INSERT INTO Usuario (usuario_usu, senha_usu, id_func_fk)" +
                    $"VALUES (@usuario, @senha, @id_func)";

                query.Parameters.AddWithValue("@usuario", usuario.UsuarioNome);
                query.Parameters.AddWithValue("@senha", usuario.Senha);
                query.Parameters.AddWithValue("@id_func", usuario.Funcionario.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente");
                }
                else
                {
                    MessageBox.Show("Dados salvos com sucesso!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro 3007 : Contate o suporte!");
            }
            finally
            {
                conn.Close();
            }
        }

        public void Update(Usuario usuario)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "UPDATE Usuario SET usuario_usu = @usuario, senha_usu = @senha, id_func_fk = @id_func WHERE id_usu = @id";

                query.Parameters.AddWithValue("@id", usuario.Id);
                query.Parameters.AddWithValue("@usuario", usuario.UsuarioNome);
                query.Parameters.AddWithValue("@senha", usuario.Senha);
                query.Parameters.AddWithValue("@id_func", usuario.Funcionario.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    MessageBox.Show("Erro ao inserir os dados, verifique e tente novamente");
                }
                else
                {
                    MessageBox.Show("Dados atualizados com sucesso!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro 3008 : Contate o suporte!");
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
