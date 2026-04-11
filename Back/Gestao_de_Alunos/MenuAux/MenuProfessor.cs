using System;
using Gestao_de_Alunos.Model;
using Gestao_de_Alunos.Servico;

namespace Gestao_de_Alunos.MenuAux
{
    public class MenuProfessor
    {
        private readonly ServicoProfessor _servicoProfessor;

        public MenuProfessor(ServicoProfessor servicoProfessor)
        {
            _servicoProfessor = servicoProfessor;
        }

        public void Mostrar()
        {
            int opcao = -1;
            while (opcao != 0)
            {
                Console.WriteLine("\n=== Menu de Professores ===");
                Console.WriteLine("1. Criar Professor");
                Console.WriteLine("2. Consultar Professor por ID");
                Console.WriteLine("3. Listar Todos os Professores");
                Console.WriteLine("4. Atualizar Professor");
                Console.WriteLine("5. Remover Professor");
                Console.WriteLine("0. Voltar");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Opção inválida!");
                    continue;
                }

                switch (opcao)
                {
                    case 1: CriarProfessor(); break;
                    case 2: ConsultarProfessor(); break;
                    case 3: ListarProfessores(); break;
                    case 4: AtualizarProfessor(); break;
                    case 5: RemoverProfessor(); break;
                    case 0: break;
                    default: Console.WriteLine("Opção inválida!"); break;
                }
            }
        }

        private void CriarProfessor()
        {
            Console.WriteLine("\n--- Criar Professor ---");
            var p = new Professor();
            Console.Write("Nome: ");
            p.Nome = Console.ReadLine();
            Console.Write("Último nome: ");
            p.UltimoNome = Console.ReadLine();

            try
            {
                _servicoProfessor.CriarProfessor(p);
                Console.WriteLine("Professor criado com sucesso!");
            }
            catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }
        }

        private void ConsultarProfessor()
        {
            Console.Write("\nID do professor: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("ID inválido."); return; }
            var p = _servicoProfessor.LerProfessor(id);
            if (p == null) Console.WriteLine("Professor não encontrado.");
            else Console.WriteLine($"  ID: {p.Id} | Nome: {p.Nome} {p.UltimoNome}");
        }

        private void ListarProfessores()
        {
            Console.WriteLine("\n--- Lista de Professores ---");
            var lista = _servicoProfessor.ListarTodos();
            if (lista.Count == 0) { Console.WriteLine("Nenhum professor registado."); return; }
            Console.WriteLine($"{"ID",-5} {"Nome",-20} {"Último Nome",-20}");
            Console.WriteLine(new string('-', 45));
            foreach (var p in lista)
                Console.WriteLine($"{p.Id,-5} {p.Nome,-20} {p.UltimoNome,-20}");
        }

        private void AtualizarProfessor()
        {
            Console.Write("\nID do professor a atualizar: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("ID inválido."); return; }
            var p = _servicoProfessor.LerProfessor(id);
            if (p == null) { Console.WriteLine("Professor não encontrado."); return; }

            Console.Write($"Novo nome [{p.Nome}]: ");
            string nome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nome)) p.Nome = nome;

            Console.Write($"Novo último nome [{p.UltimoNome}]: ");
            string ultimoNome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(ultimoNome)) p.UltimoNome = ultimoNome;

            try
            {
                _servicoProfessor.AtualizarProfessor(p);
                Console.WriteLine("Professor atualizado com sucesso!");
            }
            catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }
        }

        private void RemoverProfessor()
        {
            Console.Write("\nID do professor a remover: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("ID inválido."); return; }
            Console.Write("Tem a certeza? (s/n): ");
            if (Console.ReadLine()?.ToLower() != "s") return;
            try
            {
                _servicoProfessor.DeletarProfessor(id);
                Console.WriteLine("Professor removido com sucesso!");
            }
            catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }
        }
    }
}