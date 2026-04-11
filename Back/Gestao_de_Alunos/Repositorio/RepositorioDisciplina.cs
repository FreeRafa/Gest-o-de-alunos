using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Gestao_de_Alunos.Model;

namespace Gestao_de_Alunos.Repositorio
{
    public class RepositorioDisciplina
    {
        private readonly string _ConnectionString;

        public RepositorioDisciplina(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public void CriarDisciplina(Disciplina disciplina)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = @"INSERT INTO Disciplina(IdCurso, IdProfessor, Nome, CargaHoraria, Semestre)
                                 VALUES (@IdCurso, @IdProfessor, @Nome, @CargaHoraria, @Semestre)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@IdCurso", SqlDbType.Int).Value = disciplina.IdCurso;
                cmd.Parameters.Add("@IdProfessor", SqlDbType.Int).Value = disciplina.IdProfessor;
                cmd.Parameters.Add("@Nome", SqlDbType.VarChar, 100).Value = disciplina.Nome;
                cmd.Parameters.Add("@CargaHoraria", SqlDbType.Int).Value = disciplina.CargaHoraria;
                cmd.Parameters.Add("@Semestre", SqlDbType.Int).Value = disciplina.Semestre;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Disciplina LerDisciplina(int id)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = @"SELECT d.*, c.Nome AS NomeCurso, 
                                        p.Nome + ' ' + p.UltimoNome AS NomeProfessor
                                 FROM Disciplina d
                                 JOIN Curso c ON d.IdCurso = c.Id
                                 JOIN Professor p ON d.IdProfessor = p.Id
                                 WHERE d.Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) return MapearDisciplina(reader);
                    return null;
                }
            }
        }

        public List<Disciplina> ListarTodos()
        {
            var lista = new List<Disciplina>();
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = @"SELECT d.*, c.Nome AS NomeCurso,
                                        p.Nome + ' ' + p.UltimoNome AS NomeProfessor
                                 FROM Disciplina d
                                 JOIN Curso c ON d.IdCurso = c.Id
                                 JOIN Professor p ON d.IdProfessor = p.Id
                                 ORDER BY d.Nome";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                    while (reader.Read())
                        lista.Add(MapearDisciplina(reader));
            }
            return lista;
        }

        public void AtualizarDisciplina(Disciplina disciplina)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = @"UPDATE Disciplina
                                 SET IdCurso = @IdCurso, IdProfessor = @IdProfessor,
                                     Nome = @Nome, CargaHoraria = @CargaHoraria, Semestre = @Semestre
                                 WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@IdCurso", SqlDbType.Int).Value = disciplina.IdCurso;
                cmd.Parameters.Add("@IdProfessor", SqlDbType.Int).Value = disciplina.IdProfessor;
                cmd.Parameters.Add("@Nome", SqlDbType.VarChar, 100).Value = disciplina.Nome;
                cmd.Parameters.Add("@CargaHoraria", SqlDbType.Int).Value = disciplina.CargaHoraria;
                cmd.Parameters.Add("@Semestre", SqlDbType.Int).Value = disciplina.Semestre;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = disciplina.Id;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletarDisciplina(int id)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = "DELETE FROM Disciplina WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private Disciplina MapearDisciplina(SqlDataReader reader)
        {
            return new Disciplina
            {
                Id = (int)reader["Id"],
                IdCurso = (int)reader["IdCurso"],
                IdProfessor = (int)reader["IdProfessor"],
                Nome = reader["Nome"].ToString(),
                CargaHoraria = (int)reader["CargaHoraria"],
                Semestre = (int)reader["Semestre"],
                NomeCurso = reader["NomeCurso"].ToString(),
                NomeProfessor = reader["NomeProfessor"].ToString()
            };
        }
    }
}