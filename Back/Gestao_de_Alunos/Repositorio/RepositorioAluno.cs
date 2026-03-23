using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                string query = @"INSERT INTO Aluno(Id, Nome, UltimoNOme, DataNascimento, Fone, Email) VALUES (@Id, @Nome, @UltimoNome, @DataNascimento, @Fone, @Email)";

                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = aluno.Id;
                cmd.Parameters.Add("@Nome", SqlDbType.NVarChar, 100).Value = aluno.Nome;
                cmd.Parameters.Add("@UltimoNome", SqlDbType.NVarChar, 100).Value = aluno.UltimoNome;
                cmd.Parameters.Add("@DataNascimento", SqlDbType.Date).Value = aluno.DataNascimento;
                cmd.Parameters.Add("@Fone", SqlDbType.NChar, 50).Value = aluno.Fone;
                cmd.Parameters.Add("@Email", SqlDbType.NChar, 100).Value = aluno.Email;

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
                        Aluno aluno = new Aluno
                        {
                            Id = (int)reader["Id"],
                            Nome = reader["Nome"].ToString(),
                            UltimoNome = reader["UltimoNome"].ToString(),
                            DataNascimento = Convert.ToDateTime(reader["DataNascimento"]),
                            Fone = reader["Fone"].ToString(),
                            Email = reader["email"].ToString(),
                        };
                        return aluno;
                    }
                    else 
                    {
                        return null;
                    }
                }
            }
        }

        public void AtualizarAluno(Aluno aluno) 
        {
        }

        public void DeletarAluno(int Id) 
        {
        }
    }
}
