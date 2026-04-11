using Gestao_de_Alunos.MenuAux;
using Gestao_de_Alunos.Repositorio;
using Gestao_de_Alunos.Servico;
using System;
using System.Data.SqlClient;

namespace Gestao_de_Alunos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=DESKTOP-42RL6N1;Database=Gestao_de_Alunos;User Id=sa;Password=135113rr;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                { conn.Open(); Console.WriteLine("Conexão bem-sucedida!\n"); }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro de ligação: " + ex.Message);
                Console.ReadKey(); return;
            }

            // Alunos
            var menuAluno = new MenuAluno(new ServicoAluno(new RepositorioAluno(connectionString)));
            // Professores
            var menuProfessor = new MenuProfessor(new ServicoProfessor(new RepositorioProfessor(connectionString)));
            // Cursos
            var menuCurso = new MenuCurso(new ServicoCurso(new RepositorioCurso(connectionString)));
            // Disciplinas
            var menuDisciplina = new MenuDisciplina(new ServicoDisciplina(new RepositorioDisciplina(connectionString)));
            // Matrículas
            var menuMatricula = new MenuMatricula(new ServicoMatricula(new RepositorioMatricula(connectionString)));

            int opcao = -1;
            while (opcao != 0)
            {
                Console.WriteLine("\n╔══════════════════════════════╗");
                Console.WriteLine("║   SISTEMA DE GESTÃO ESCOLAR  ║");
                Console.WriteLine("╠══════════════════════════════╣");
                Console.WriteLine("║  1. Gerir Alunos             ║");
                Console.WriteLine("║  2. Gerir Professores        ║");
                Console.WriteLine("║  3. Gerir Cursos             ║");
                Console.WriteLine("║  4. Gerir Disciplinas        ║");
                Console.WriteLine("║  5. Gerir Matrículas         ║");
                Console.WriteLine("║  0. Sair                     ║");
                Console.WriteLine("╚══════════════════════════════╝");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao)) { Console.WriteLine("Opção inválida!"); continue; }

                switch (opcao)
                {
                    case 1: menuAluno.Mostrar(); break;
                    case 2: menuProfessor.Mostrar(); break;
                    case 3: menuCurso.Mostrar(); break;
                    case 4: menuDisciplina.Mostrar(); break;
                    case 5: menuMatricula.Mostrar(); break;
                    case 0: Console.WriteLine("Até logo!"); break;
                    default: Console.WriteLine("Opção inválida!"); break;
                }
            }
        }
    }
}