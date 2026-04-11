using Gestao_de_Alunos.MenuAux;
using Gestao_de_Alunos.Model;
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
            string cs = "Server=DESKTOP-42RL6N1;Database=Gestao_de_Alunos;User Id=sa;Password=135113rr;";

            // Testar ligação
            try
            {
                using (SqlConnection conn = new SqlConnection(cs))
                { conn.Open(); Console.WriteLine("Ligacao bem-sucedida!\n"); }
            }
            catch (Exception ex)
            { Console.WriteLine("Erro: " + ex.Message); Console.ReadKey(); return; }

            // Repositórios
            var repoAluno = new RepositorioAluno(cs);
            var repoProfessor = new RepositorioProfessor(cs);
            var repoCurso = new RepositorioCurso(cs);
            var repoDisc = new RepositorioDisciplina(cs);
            var repoMatricula = new RepositorioMatricula(cs);
            var repoUtil = new RepositorioUtilizador(cs);

            // Serviços
            var servicoAluno = new ServicoAluno(repoAluno);
            var servicoProfessor = new ServicoProfessor(repoProfessor);
            var servicoCurso = new ServicoCurso(repoCurso);
            var servicoDisc = new ServicoDisciplina(repoDisc);
            var servicoMatricula = new ServicoMatricula(repoMatricula);
            var servicoAuth = new ServicoAuth(repoUtil);

            // Menus auxiliares
            var menuCurso = new MenuCurso(servicoCurso);
            var menuDisciplina = new MenuDisciplina(servicoDisc);

            // Menu principal
            int opcao = -1;
            while (opcao != 0)
            {
                Console.WriteLine("\n╔══════════════════════════════╗");
                Console.WriteLine("║   SISTEMA DE GESTAO ESCOLAR  ║");
                Console.WriteLine("╠══════════════════════════════╣");
                Console.WriteLine("║  1. Ver cursos               ║");
                Console.WriteLine("║  2. Login                    ║");
                Console.WriteLine("║  0. Sair                     ║");
                Console.WriteLine("╚══════════════════════════════╝");
                Console.Write("Escolha: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                { Console.WriteLine("Opcao invalida!"); continue; }

                switch (opcao)
                {
                    case 1:
                        var cursos = servicoCurso.ListarTodos();
                        Console.WriteLine("\n--- Cursos disponiveis ---");
                        foreach (var c in cursos)
                            if (c.Status)
                                Console.WriteLine($"  [{c.Id}] {c.Nome} — {c.Duracao} semestres");
                        break;

                    case 2:
                        Utilizador utilizador = null;
                        try
                        {
                            Console.Write("\nEmail: ");
                            string email = Console.ReadLine();
                            Console.Write("Password: ");
                            string password = Console.ReadLine();
                            utilizador = servicoAuth.Login(email, password);
                            Console.WriteLine($"\nBem-vindo, {utilizador.Email}! Perfil: {utilizador.Perfil}");
                        }
                        catch (Exception ex)
                        { Console.WriteLine("Erro: " + ex.Message); break; }

                        if (utilizador.EhAluno)
                        {
                            var menuAluno = new MenuAluno(servicoAluno, servicoMatricula,
                                                          servicoCurso, servicoAuth);
                            menuAluno.Mostrar(utilizador);
                        }
                        else if (utilizador.EhProfessor)
                        {
                            var menuProf = new MenuProfessor(servicoProfessor, servicoAluno,
                                                             servicoCurso, servicoDisc,
                                                             servicoMatricula, servicoAuth,
                                                             menuCurso, menuDisciplina);
                            menuProf.Mostrar(utilizador);
                        }
                        break;

                    case 0:
                        Console.WriteLine("Ate logo!");
                        break;

                    default:
                        Console.WriteLine("Opcao invalida!");
                        break;
                }
            }
        }
    }
}