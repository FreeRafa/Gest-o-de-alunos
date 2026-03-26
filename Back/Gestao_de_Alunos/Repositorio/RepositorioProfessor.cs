using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                string query = @"INSERT INTO Professor(Nome, UltimoNOme) VALUES (@Nome, @UltimoNome)";

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
                        Professor professor = new Professor
                        {
                            Id = (int)reader["Id"],
                            Nome = reader["Nome"].ToString(),
                            UltimoNome = reader["UltimoNome"].ToString(),
                        };
                        return professor;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public void AtualizarProfessor(Professor professor)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                string query = @"UPDATE Professor 
                                SET Nome = @Nome, 
                                    UltimoNome = @UltimoNome,
                                WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, connection);


                cmd.Parameters.Add("@Nome", SqlDbType.NVarChar, 100).Value = professor.Nome;
                cmd.Parameters.Add("@UltimoNome", SqlDbType.NVarChar, 100).Value = professor.UltimoNome;
               
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
