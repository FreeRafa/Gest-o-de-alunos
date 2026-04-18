using System;
using Gestao_de_Alunos.Model;
using Gestao_de_Alunos.Servico;

namespace Gestao_de_Alunos.MenuAux
{
    public class MenuProfessor
    {
        private readonly ServicoProfessor _servicoProfessor;
        private readonly ServicoAluno _servicoAluno;
        private readonly ServicoCurso _servicoCurso;
        private readonly ServicoDisciplina _servicoDisciplina;
        private readonly ServicoMatricula _servicoMatricula;
        private readonly ServicoAuth _servicoAuth;
        private readonly MenuCurso _menuCurso;
        private readonly MenuDisciplina _menuDisciplina;

        // Construtor atualizado — recebe os menus prontos
        public MenuProfessor(ServicoProfessor servicoProfessor, ServicoAluno servicoAluno,
                             ServicoCurso servicoCurso, ServicoDisciplina servicoDisciplina,
                             ServicoMatricula servicoMatricula, ServicoAuth servicoAuth,
                             MenuCurso menuCurso, MenuDisciplina menuDisciplina)
        {
            _servicoProfessor = servicoProfessor;
            _servicoAluno = servicoAluno;
            _servicoCurso = servicoCurso;
            _servicoDisciplina = servicoDisciplina;
            _servicoMatricula = servicoMatricula;
            _servicoAuth = servicoAuth;
            _menuCurso = menuCurso;
            _menuDisciplina = menuDisciplina;
        }

        public void Mostrar(Utilizador utilizador)
        {
            int opcao = -1;
            while (opcao != 0)
            {
                Console.WriteLine($"\n=== Menu do Professor — {utilizador.Email} ===");
                Console.WriteLine("--- Gestao de Alunos ---");
                Console.WriteLine("1. Cadastrar aluno");
                Console.WriteLine("2. Listar alunos");
                Console.WriteLine("3. Atualizar cadastro de aluno");
                Console.WriteLine("4. Apagar aluno");
                Console.WriteLine("--- Gestao Academica ---");
                Console.WriteLine("5. Gerir cursos");
                Console.WriteLine("6. Gerir disciplinas");
                Console.WriteLine("7. Ver todas as matrículas");
                Console.WriteLine("8. Ver as minhas disciplinas");//aqui
                Console.WriteLine("9. Ver alunos de uma disciplina");//aqui
                Console.WriteLine("--- Conta ---");
                Console.WriteLine("10. Alterar password");
                Console.WriteLine("0. Sair / Logout");
                Console.Write("Escolha: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                { Console.WriteLine("Opção inválida!"); continue; }

                switch (opcao)
                {
                    case 1: CadastrarAluno(); break;
                    case 2: ListarAlunos(); break;
                    case 3: AtualizarAluno(); break;
                    case 4: ApagarAluno(); break;
                    case 5: GerirCursos(); break;
                    case 6: GerirDisciplinas(); break;
                    case 7: VerMatriculas(); break;
                    case 8: VerMinhasDisciplinas(utilizador.IdReferencia); break;   
                    case 9: VerAlunosDaDisciplina(utilizador.IdReferencia); break;
                    case 10: AlterarPassword(utilizador); break;
                    case 0: Console.WriteLine("Até logo!"); break;
                    default: Console.WriteLine("Opção inválida!"); break;
                }
            }
        }

        private void CadastrarAluno()
        {
            Console.WriteLine("\n--- Cadastrar aluno ---");
            var aluno = new Aluno();
            Console.Write("Nome: "); aluno.Nome = Console.ReadLine();
            Console.Write("Último nome: "); aluno.UltimoNome = Console.ReadLine();
            Console.Write("Data de nascimento (dd/mm/aaaa): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime data))
            { Console.WriteLine("Data inválida."); return; }
            aluno.DataNascimento = data;
            Console.Write("Telefone: "); aluno.Fone = Console.ReadLine();
            Console.Write("Email: "); aluno.Email = Console.ReadLine();

            try
            {
                _servicoAluno.CriarAluno(aluno);

                // Criar utilizador automaticamente
                Console.Write("Password para o aluno: ");
                string password = Console.ReadLine();
                var novoAluno = _servicoAluno.ListarTodos()
                    .Find(a => a.Email == aluno.Email);

                if (novoAluno != null)
                {
                    _servicoAuth.RegistarAluno(new Utilizador
                    {
                        Email = aluno.Email,
                        Password = password,
                        Perfil = "aluno",
                        IdReferencia = novoAluno.Id
                    });
                }

                Console.WriteLine("Aluno cadastrado com sucesso!");
            }
            catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }
        }

        private void ListarAlunos()
        {
            Console.WriteLine("\n--- Lista de alunos ---");
            var lista = _servicoAluno.ListarTodos();
            if (lista.Count == 0) { Console.WriteLine("Nenhum aluno registado."); return; }
            Console.WriteLine($"{"ID",-5} {"Nome",-20} {"Último Nome",-20} {"Email",-30}");
            Console.WriteLine(new string('-', 78));
            foreach (var a in lista)
                Console.WriteLine($"{a.Id,-5} {a.Nome,-20} {a.UltimoNome,-20} {a.Email,-30}");
        }

        private void AtualizarAluno()
        {
            Console.Write("\nID do aluno: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("ID inválido."); return; }
            var aluno = _servicoAluno.LerAluno(id);
            if (aluno == null) { Console.WriteLine("Aluno não encontrado."); return; }

            Console.Write($"Nome [{aluno.Nome}]: ");
            string nome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nome)) aluno.Nome = nome;

            Console.Write($"Último nome [{aluno.UltimoNome}]: ");
            string ultimoNome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(ultimoNome)) aluno.UltimoNome = ultimoNome;

            Console.Write($"Telefone [{aluno.Fone}]: ");
            string fone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(fone)) aluno.Fone = fone;

            try { _servicoAluno.AtualizarAluno(aluno); Console.WriteLine("Aluno atualizado!"); }
            catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }
        }

        private void ApagarAluno()
        {
            Console.Write("\nID do aluno a apagar: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("ID inválido."); return; }
            Console.Write("Tem a certeza? (s/n): ");
            if (Console.ReadLine()?.ToLower() != "s") return;
            try { _servicoAluno.DeletarAluno(id); Console.WriteLine("Aluno removido."); }
            catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }
        }

        private void GerirCursos()
        {
            _menuCurso.Mostrar();
        }

        private void GerirDisciplinas()
        {
            _menuDisciplina.Mostrar();
        }

        private void VerMatriculas()
        {
            Console.WriteLine("\n--- Todas as matrículas ---");
            var lista = _servicoMatricula.ListarTodos();
            if (lista.Count == 0) { Console.WriteLine("Nenhuma matrícula."); return; }
            Console.WriteLine($"{"ID",-5} {"Aluno",-25} {"Disciplina",-25} {"Estado",-10}");
            Console.WriteLine(new string('-', 68));
            foreach (var m in lista)
            {
                string estado = m.Status == 1 ? "Ativa" : m.Status == 2 ? "Trancada" : "Concluída";
                Console.WriteLine($"{m.Id,-5} {m.NomeAluno,-25} {m.NomeDisciplina,-25} {estado,-10}");
            }
        }

        private void AlterarPassword(Utilizador utilizador)
        {
            Console.Write("\nNova password: ");
            string nova = Console.ReadLine();
            Console.Write("Confirmar: ");
            string confirmar = Console.ReadLine();
            try
            {
                _servicoAuth.AlterarPassword(utilizador.Id, null, nova, confirmar);
                Console.WriteLine("Password alterada!");
            }
            catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }
        }

        private void VerMinhasDisciplinas(int idProfessor)
        {
            Console.WriteLine("\n--- As minhas disciplinas ---");
            var lista = _servicoDisciplina.ListarPorProfessor(idProfessor);

            if (lista.Count == 0)
            { Console.WriteLine("Não tens disciplinas atribuídas."); return; }

            Console.WriteLine($"{"ID",-5} {"Disciplina",-30} {"Curso",-25}");
            Console.WriteLine(new string('-', 62));
            foreach (var d in lista)
                Console.WriteLine($"{d.Id,-5} {d.Nome,-30} {d.NomeCurso,-25}");
        }

        private void VerAlunosDaDisciplina(int idProfessor)
        {
            Console.WriteLine("\n--- Alunos por disciplina ---");

            var disciplinas = _servicoDisciplina.ListarPorProfessor(idProfessor);
            if (disciplinas.Count == 0)
            { Console.WriteLine("Não tens disciplinas atribuídas."); return; }

            Console.WriteLine($"{"ID",-5} {"Disciplina",-30} {"Curso",-25}");
            Console.WriteLine(new string('-', 62));
            foreach (var d in disciplinas)
                Console.WriteLine($"{d.Id,-5} {d.Nome,-30} {d.NomeCurso,-25}");

            Console.Write("\nID da disciplina (0 para voltar): ");
            if (!int.TryParse(Console.ReadLine(), out int idDisc) || idDisc == 0) return;

            // Garante que a disciplina pertence a este professor
            if (!disciplinas.Exists(d => d.Id == idDisc))
            { Console.WriteLine("Disciplina não encontrada ou não te pertence."); return; }

            var alunos = _servicoMatricula.ListarPorDisciplina(idDisc);
            var nomeDisciplina = disciplinas.Find(d => d.Id == idDisc)?.Nome;

            if (alunos.Count == 0)
            { Console.WriteLine($"Nenhum aluno inscrito em {nomeDisciplina}."); return; }

            Console.WriteLine($"\n--- Alunos inscritos em: {nomeDisciplina} ---");
            Console.WriteLine($"{"ID Mat.",-8} {"Aluno",-30} {"Data Insc.",-12} {"Status",-10}");
            Console.WriteLine(new string('-', 62));

            foreach (var m in alunos)
            {
                string status = m.Status == 1 ? "Ativa"
                              : m.Status == 2 ? "Trancada"
                              : "Concluída";
                Console.WriteLine($"{m.Id,-8} {m.NomeAluno,-30} {m.DataMatricula:dd/MM/yyyy,-12} {status,-10}");
            }

            Console.WriteLine($"\nTotal: {alunos.Count} aluno(s) inscrito(s)");
        }
    }
}