using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Gestao_de_Alunos.Model;

namespace Gestao_de_Alunos.Repositorio
{
    public class RepositorioMatricula
    {
        private readonly string _ConnectionString;

        public RepositorioMatricula(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public void CriarMatricula(Matricula matricula)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = @"INSERT INTO Matricula(IdAluno, IdDisciplina, DataMatricula, Status)
                                 VALUES (@IdAluno, @IdDisciplina, @DataMatricula, @Status)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@IdAluno", SqlDbType.Int).Value = matricula.IdAluno;
                cmd.Parameters.Add("@IdDisciplina", SqlDbType.Int).Value = matricula.IdDisciplina;
                cmd.Parameters.Add("@DataMatricula", SqlDbType.Date).Value = matricula.DataMatricula;
                cmd.Parameters.Add("@Status", SqlDbType.Int).Value = matricula.Status;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Matricula LerMatricula(int id)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = @"SELECT m.*, 
                                        a.Nome + ' ' + a.UltimoNome AS NomeAluno,
                                        d.Nome AS NomeDisciplina
                                 FROM Matricula m
                                 JOIN Aluno a ON m.IdAluno = a.Id
                                 JOIN Disciplina d ON m.IdDisciplina = d.Id
                                 WHERE m.Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) return MapearMatricula(reader);
                    return null;
                }
            }
        }

        public List<Matricula> ListarPorAluno(int idAluno)
        {
            var lista = new List<Matricula>();
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = @"SELECT m.*,
                                        a.Nome + ' ' + a.UltimoNome AS NomeAluno,
                                        d.Nome AS NomeDisciplina
                                 FROM Matricula m
                                 JOIN Aluno a ON m.IdAluno = a.Id
                                 JOIN Disciplina d ON m.IdDisciplina = d.Id
                                 WHERE m.IdAluno = @IdAluno";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@IdAluno", SqlDbType.Int).Value = idAluno;
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                        lista.Add(MapearMatricula(reader));
            }
            return lista;
        }

        public List<Matricula> ListarTodos()
        {
            var lista = new List<Matricula>();
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = @"SELECT m.*,
                                        a.Nome + ' ' + a.UltimoNome AS NomeAluno,
                                        d.Nome AS NomeDisciplina
                                 FROM Matricula m
                                 JOIN Aluno a ON m.IdAluno = a.Id
                                 JOIN Disciplina d ON m.IdDisciplina = d.Id
                                 ORDER BY a.Nome";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                        lista.Add(MapearMatricula(reader));
            }
            return lista;
        }

        public void AtualizarStatus(int id, int novoStatus)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = "UPDATE Matricula SET Status = @Status WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@Status", SqlDbType.Int).Value = novoStatus;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletarMatricula(int id)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = "DELETE FROM Matricula WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private Matricula MapearMatricula(SqlDataReader reader)
        {
            return new Matricula
            {
                Id = (int)reader["Id"],
                IdAluno = (int)reader["IdAluno"],
                IdDisciplina = (int)reader["IdDisciplina"],
                DataMatricula = Convert.ToDateTime(reader["DataMatricula"]),
                Status = (int)reader["Status"],
                NomeAluno = reader["NomeAluno"].ToString(),
                NomeDisciplina = reader["NomeDisciplina"].ToString()
            };
        }
    }
}