using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Gestao_de_Alunos.Model;

namespace Gestao_de_Alunos.Repositorio
{
    public class RepositorioAluno
    {
        private readonly string _ConnectionString;

        public RepositorioAluno(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public void CriarAluno(Aluno aluno)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = @"INSERT INTO Aluno(Nome, UltimoNome, DataNascimento, Fone, Email) 
                                 VALUES (@Nome, @UltimoNome, @DataNascimento, @Fone, @Email)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@Nome", SqlDbType.NVarChar, 100).Value = aluno.Nome;
                cmd.Parameters.Add("@UltimoNome", SqlDbType.NVarChar, 100).Value = aluno.UltimoNome;
                cmd.Parameters.Add("@DataNascimento", SqlDbType.Date).Value = aluno.DataNascimento;
                cmd.Parameters.Add("@Fone", SqlDbType.NVarChar, 50).Value = aluno.Fone;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = aluno.Email;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Aluno LerAluno(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = "SELECT * FROM Aluno WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Aluno
                        {
                            Id = (int)reader["Id"],
                            Nome = reader["Nome"].ToString(),
                            UltimoNome = reader["UltimoNome"].ToString(),
                            DataNascimento = Convert.ToDateTime(reader["DataNascimento"]),
                            Fone = reader["Fone"].ToString(),
                            Email = reader["Email"].ToString()
                        };
                    }
                    return null;
                }
            }
        }

        public List<Aluno> ListarTodos()
        {
            var lista = new List<Aluno>();
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = "SELECT * FROM Aluno ORDER BY Nome";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Aluno
                        {
                            Id = (int)reader["Id"],
                            Nome = reader["Nome"].ToString(),
                            UltimoNome = reader["UltimoNome"].ToString(),
                            DataNascimento = Convert.ToDateTime(reader["DataNascimento"]),
                            Fone = reader["Fone"].ToString(),
                            Email = reader["Email"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public void AtualizarAluno(Aluno aluno)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                // BUG CORRIGIDO: removida vírgula antes do WHERE
                string query = @"UPDATE Aluno 
                                 SET Nome = @Nome, 
                                     UltimoNome = @UltimoNome,
                                     DataNascimento = @DataNascimento,
                                     Fone = @Fone,
                                     Email = @Email
                                 WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@Nome", SqlDbType.NVarChar, 100).Value = aluno.Nome;
                cmd.Parameters.Add("@UltimoNome", SqlDbType.NVarChar, 100).Value = aluno.UltimoNome;
                cmd.Parameters.Add("@DataNascimento", SqlDbType.Date).Value = aluno.DataNascimento;
                cmd.Parameters.Add("@Fone", SqlDbType.NVarChar, 50).Value = aluno.Fone;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = aluno.Email;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = aluno.Id;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletarAluno(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = "DELETE FROM Aluno WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}