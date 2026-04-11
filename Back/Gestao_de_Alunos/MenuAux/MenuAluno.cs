using System;
using Gestao_de_Alunos.Model;
using Gestao_de_Alunos.Servico;

namespace Gestao_de_Alunos.MenuAux
{
    public class MenuAluno
    {
        private readonly ServicoAluno _servicoAluno;

        public MenuAluno(ServicoAluno servicoAluno)
        {
            _servicoAluno = servicoAluno;
        }

        public void Mostrar()
        {
            int opcao = -1;
            while (opcao != 0)
            {
                Console.WriteLine("\n=== Menu de Alunos ===");
                Console.WriteLine("1. Criar Aluno");
                Console.WriteLine("2. Consultar Aluno por ID");
                Console.WriteLine("3. Listar Todos os Alunos");
                Console.WriteLine("4. Atualizar Aluno");
                Console.WriteLine("5. Remover Aluno");
                Console.WriteLine("0. Voltar");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Opção inválida!");
                    continue;
                }

                switch (opcao)
                {
                    case 1: CriarAluno(); break;
                    case 2: ConsultarAluno(); break;
                    case 3: ListarAlunos(); break;
                    case 4: AtualizarAluno(); break;
                    case 5: RemoverAluno(); break;
                    case 0: break;
                    default: Console.WriteLine("Opção inválida!"); break;
                }
            }
        }

        private void CriarAluno()
        {
            Console.WriteLine("\n--- Criar Aluno ---");
            var aluno = new Aluno();
            Console.Write("Nome: ");
            aluno.Nome = Console.ReadLine();
            Console.Write("Último nome: ");
            aluno.UltimoNome = Console.ReadLine();
            Console.Write("Data de nascimento (dd/mm/aaaa): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime data))
            {
                Console.WriteLine("Data inválida.");
                return;
            }
            aluno.DataNascimento = data;
            Console.Write("Telefone: ");
            aluno.Fone = Console.ReadLine();
            Console.Write("Email: ");
            aluno.Email = Console.ReadLine();

            try
            {
                _servicoAluno.CriarAluno(aluno);
                Console.WriteLine("Aluno criado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        private void ConsultarAluno()
        {
            Console.WriteLine("\n--- Consultar Aluno ---");
            Console.Write("ID do aluno: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            var aluno = _servicoAluno.LerAluno(id);
            if (aluno == null)
                Console.WriteLine("Aluno não encontrado.");
            else
                MostrarAluno(aluno);
        }

        private void ListarAlunos()
        {
            Console.WriteLine("\n--- Lista de Alunos ---");
            var alunos = _servicoAluno.ListarTodos();
            if (alunos.Count == 0)
            {
                Console.WriteLine("Nenhum aluno registado.");
                return;
            }
            Console.WriteLine($"{"ID",-5} {"Nome",-20} {"Último Nome",-20} {"Email",-30}");
            Console.WriteLine(new string('-', 75));
            foreach (var a in alunos)
                Console.WriteLine($"{a.Id,-5} {a.Nome,-20} {a.UltimoNome,-20} {a.Email,-30}");
        }

        private void AtualizarAluno()
        {
            Console.WriteLine("\n--- Atualizar Aluno ---");
            Console.Write("ID do aluno a atualizar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            var aluno = _servicoAluno.LerAluno(id);
            if (aluno == null)
            {
                Console.WriteLine("Aluno não encontrado.");
                return;
            }
            Console.WriteLine("Dados atuais:");
            MostrarAluno(aluno);

            Console.Write($"Novo nome [{aluno.Nome}]: ");
            string nome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nome)) aluno.Nome = nome;

            Console.Write($"Novo último nome [{aluno.UltimoNome}]: ");
            string ultimoNome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(ultimoNome)) aluno.UltimoNome = ultimoNome;

            Console.Write($"Nova data de nascimento [{aluno.DataNascimento:dd/MM/yyyy}]: ");
            string dataStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(dataStr) && DateTime.TryParse(dataStr, out DateTime novaData))
                aluno.DataNascimento = novaData;

            Console.Write($"Novo telefone [{aluno.Fone}]: ");
            string fone = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(fone)) aluno.Fone = fone;

            Console.Write($"Novo email [{aluno.Email}]: ");
            string email = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(email)) aluno.Email = email;

            try
            {
                _servicoAluno.AtualizarAluno(aluno);
                Console.WriteLine("Aluno atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        private void RemoverAluno()
        {
            Console.WriteLine("\n--- Remover Aluno ---");
            Console.Write("ID do aluno a remover: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }
            Console.Write("Tem a certeza? (s/n): ");
            if (Console.ReadLine()?.ToLower() != "s") return;

            try
            {
                _servicoAluno.DeletarAluno(id);
                Console.WriteLine("Aluno removido com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
        }

        private void MostrarAluno(Aluno a)
        {
            Console.WriteLine($"  ID: {a.Id}");
            Console.WriteLine($"  Nome: {a.Nome} {a.UltimoNome}");
            Console.WriteLine($"  Nascimento: {a.DataNascimento:dd/MM/yyyy}");
            Console.WriteLine($"  Telefone: {a.Fone}");
            Console.WriteLine($"  Email: {a.Email}");
        }
    }
}