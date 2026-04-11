using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Gestao_de_Alunos.Model;

namespace Gestao_de_Alunos.Repositorio
{
    public class RepositorioCurso
    {
        private readonly string _ConnectionString;

        public RepositorioCurso(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public void CriarCurso(Curso curso)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = @"INSERT INTO Curso(Nome, Duracao, Descricao, Status) 
                                 VALUES (@Nome, @Duracao, @Descricao, @Status)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@Nome", SqlDbType.VarChar, 100).Value = curso.Nome;
                cmd.Parameters.Add("@Duracao", SqlDbType.Int).Value = curso.Duracao;
                cmd.Parameters.Add("@Descricao", SqlDbType.VarChar, 200).Value = curso.Descricao ?? "";
                cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = curso.Status;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Curso LerCurso(int id)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = "SELECT * FROM Curso WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return MapearCurso(reader);
                    return null;
                }
            }
        }

        public List<Curso> ListarTodos()
        {
            var lista = new List<Curso>();
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = "SELECT * FROM Curso ORDER BY Nome";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                        lista.Add(MapearCurso(reader));
            }
            return lista;
        }

        public void AtualizarCurso(Curso curso)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = @"UPDATE Curso 
                                 SET Nome = @Nome, Duracao = @Duracao, 
                                     Descricao = @Descricao, Status = @Status
                                 WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@Nome", SqlDbType.VarChar, 100).Value = curso.Nome;
                cmd.Parameters.Add("@Duracao", SqlDbType.Int).Value = curso.Duracao;
                cmd.Parameters.Add("@Descricao", SqlDbType.VarChar, 200).Value = curso.Descricao ?? "";
                cmd.Parameters.Add("@Status", SqlDbType.Bit).Value = curso.Status;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = curso.Id;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletarCurso(int id)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = "DELETE FROM Curso WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private Curso MapearCurso(SqlDataReader reader)
        {
            return new Curso
            {
                Id = (int)reader["Id"],
                Nome = reader["Nome"].ToString(),
                Duracao = (int)reader["Duracao"],
                Descricao = reader["Descricao"].ToString(),
                Status = (bool)reader["Status"]
            };
        }
    }
}