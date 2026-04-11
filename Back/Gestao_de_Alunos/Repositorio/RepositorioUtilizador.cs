using System.Data;
using System.Data.SqlClient;
using Gestao_de_Alunos.Model;

namespace Gestao_de_Alunos.Repositorio
{
    public class RepositorioUtilizador
    {
        private readonly string _connectionString;

        public RepositorioUtilizador(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Utilizador Login(string email, string password)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM Utilizador 
                                 WHERE Email = @Email AND Password = @Password";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = email;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar, 100).Value = password;

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Utilizador
                        {
                            Id = (int)reader["Id"],
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString(),
                            Perfil = reader["Perfil"].ToString(),
                            IdReferencia = (int)reader["IdReferencia"]
                        };
                    }
                    return null;
                }
            }
        }

        public void RegistarUtilizador(Utilizador utilizador)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO Utilizador(Email, Password, Perfil, IdReferencia)
                                 VALUES (@Email, @Password, @Perfil, @IdReferencia)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = utilizador.Email;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar, 100).Value = utilizador.Password;
                cmd.Parameters.Add("@Perfil", SqlDbType.VarChar, 20).Value = utilizador.Perfil;
                cmd.Parameters.Add("@IdReferencia", SqlDbType.Int).Value = utilizador.IdReferencia;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AlterarPassword(int id, string novaPassword)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Utilizador SET Password = @Password WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@Password", SqlDbType.VarChar, 100).Value = novaPassword;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}