﻿using MySql.Data.MySqlClient;
using SISCAN.Database;
using SISCAN.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SISCAN.Models
{
    internal class UsuarioDAO
    {
        public int count;
        public int acess;
        private static Conexao conn;
        public string mensagem;
        public bool condicao;
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
                    query.CommandText = "SELECT * FROM Usuario LEFT JOIN Funcionario ON Usuario.id_func_fk = Funcionario.id_func WHERE visivel_usu = 'Sim';";
                }
                else
                {
                    query.CommandText = $"SELECT * FROM Usuario, Funcionario WHERE (Usuario.id_func_fk = Funcionario.id_func) AND (usuario_usu LIKE '%{busca}%') AND (visivel_usu = 'Sim');";
                }

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    listCli.Add(new Usuario()
                    {
                        Id = reader.GetInt32("id_usu"),
                        UsuarioNome = DAOHelper.GetString(reader, "usuario_usu"),
                        Senha = DAOHelper.GetString(reader, "senha_usu"),
                        Acesso = reader.GetInt32("nivel_acess_usu"),
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

        public void Insert(Usuario usuario, int contador) 
        {
            try
            {
                var query = conn.Query();

                if (contador == 0)
                {
                    query.CommandText = $"CALL InsertPrimeiroUsuario(@usuario, @senha)";
                }
                else
                {
                    query.CommandText = $"CALL InsertUsuario(@usuario, @senha, @id_func, @acesso)";
                }

                query.Parameters.AddWithValue("@usuario", usuario.UsuarioNome);
                query.Parameters.AddWithValue("@senha", usuario.Senha);
                if (contador != 0)
                {
                    query.Parameters.AddWithValue("@acesso", usuario.Acesso);
                }
                query.Parameters.AddWithValue("@id_func", usuario.Funcionario.Id);

                MySqlDataReader reader = query.ExecuteReader();
                if (reader.Read())
                {
                    mensagem = reader.GetString(0); // Pega o primeiro campo, que é a string
                    condicao = reader.GetBoolean(1); // Pega o segundo campo, que é o boolean
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        public void Delete(Usuario usuario)
        {

            MessageBoxResult resultado = MessageBox.Show("Deseja deletar esse dado?", "Pergunta", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    var query = conn.Query();
                    query.CommandText = $"UPDATE Usuario SET visivel_usu = 'Nao' WHERE id_usu = @id;";

                    query.Parameters.AddWithValue("@id", usuario.Id);

                    var result = query.ExecuteNonQuery();

                    if (result == 0)
                    {
                        MessageBox.Show("Erro ao deletar os dados, verifique e tente novamente");
                    }
                    else
                    {
                        MessageBox.Show("Dados deletados com sucesso!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("Erro 3007 : Contate o suporte!");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public Usuario Login(string user, string senha)
        {
            try
            {
                Usuario usuario = new Usuario();

                var query = conn.Query();

                if (user == null && senha == null)
                {
                    query.CommandText = "SELECT * FROM Usuario, Funcionario WHERE (Funcionario.id_func = Usuario.id_func_fk) AND visivel_usu = 'Sim';";
                }
                else
                {
                    query.CommandText = $"SELECT * FROM Usuario, Funcionario WHERE (Funcionario.id_func = Usuario.id_func_fk) AND (usuario_usu = '{user}') AND (visivel_usu = 'Sim') AND (senha_usu = '{senha}');";
                }

                MySqlDataReader reader = query.ExecuteReader();

                if (reader.HasRows)
                {
                    count = 1;

                    while (reader.Read())
                    {
                        usuario.Id = reader.GetInt32("id_usu");
                        usuario.UsuarioNome = DAOHelper.GetString(reader, "usuario_usu");
                        usuario.Senha = DAOHelper.GetString(reader, "senha_usu");
                        usuario.Acesso = reader.GetInt32("nivel_acess_usu");
                        usuario.Funcionario = DAOHelper.IsNull(reader, "id_func_fk") ? null : new Funcionario() { Id = reader.GetInt32("id_func"), Nome = reader.GetString("nome_func") };
                    }
                }
                else
                {
                    count = 0;
                }

                return usuario;
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

    }
}
