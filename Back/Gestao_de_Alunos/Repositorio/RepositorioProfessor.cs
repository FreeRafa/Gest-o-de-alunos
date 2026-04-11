using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Gestao_de_Alunos.Model;

namespace Gestao_de_Alunos.Repositorio
{
    public class RepositorioProfessor
    {
        private readonly string _ConnectionString;

        public RepositorioProfessor(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public void CriarProfessor(Professor professor)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = @"INSERT INTO Professor(Nome, UltimoNome) VALUES (@Nome, @UltimoNome)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@Nome", SqlDbType.NVarChar, 100).Value = professor.Nome;
                cmd.Parameters.Add("@UltimoNome", SqlDbType.NVarChar, 100).Value = professor.UltimoNome;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Professor LerProfessor(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = "SELECT * FROM Professor WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Professor
                        {
                            Id = (int)reader["Id"],
                            Nome = reader["Nome"].ToString(),
                            UltimoNome = reader["UltimoNome"].ToString()
                        };
                    }
                    return null;
                }
            }
        }

        public List<Professor> ListarTodos()
        {
            var lista = new List<Professor>();
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = "SELECT * FROM Professor ORDER BY Nome";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new Professor
                        {
                            Id = (int)reader["Id"],
                            Nome = reader["Nome"].ToString(),
                            UltimoNome = reader["UltimoNome"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public void AtualizarProfessor(Professor professor)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                // BUG CORRIGIDO: removida vírgula antes do WHERE + adicionado @Id nos parâmetros
                string query = @"UPDATE Professor 
                                 SET Nome = @Nome, 
                                     UltimoNome = @UltimoNome
                                 WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@Nome", SqlDbType.NVarChar, 100).Value = professor.Nome;
                cmd.Parameters.Add("@UltimoNome", SqlDbType.NVarChar, 100).Value = professor.UltimoNome;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = professor.Id; // BUG CORRIGIDO: parâmetro que faltava
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletarProfessor(int Id)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = "DELETE FROM Professor WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}