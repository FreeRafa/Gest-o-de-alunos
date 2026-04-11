using System;
using Gestao_de_Alunos.Model;
using Gestao_de_Alunos.Servico;

namespace Gestao_de_Alunos.MenuAux
{
    public class MenuCurso
    {
        private readonly ServicoCurso _servico;

        public MenuCurso(ServicoCurso servico) { _servico = servico; }

        public void Mostrar()
        {
            int opcao = -1;
            while (opcao != 0)
            {
                Console.WriteLine("\n=== Menu de Cursos ===");
                Console.WriteLine("1. Criar Curso");
                Console.WriteLine("2. Consultar Curso por ID");
                Console.WriteLine("3. Listar Todos os Cursos");
                Console.WriteLine("4. Atualizar Curso");
                Console.WriteLine("5. Remover Curso");
                Console.WriteLine("0. Voltar");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao)) { Console.WriteLine("Opção inválida!"); continue; }

                switch (opcao)
                {
                    case 1: CriarCurso(); break;
                    case 2: ConsultarCurso(); break;
                    case 3: ListarCursos(); break;
                    case 4: AtualizarCurso(); break;
                    case 5: RemoverCurso(); break;
                    case 0: break;
                    default: Console.WriteLine("Opção inválida!"); break;
                }
            }
        }

        private void CriarCurso()
        {
            Console.WriteLine("\n--- Criar Curso ---");
            var c = new Curso();
            Console.Write("Nome: "); c.Nome = Console.ReadLine();
            Console.Write("Duração (semestres): ");
            int.TryParse(Console.ReadLine(), out int dur); c.Duracao = dur;
            Console.Write("Descrição: "); c.Descricao = Console.ReadLine();
            Console.Write("Ativo? (s/n): ");
            c.Status = Console.ReadLine()?.ToLower() == "s";

            try { _servico.CriarCurso(c); Console.WriteLine("Curso criado com sucesso!"); }
            catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }
        }

        private void ConsultarCurso()
        {
            Console.Write("\nID do curso: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("ID inválido."); return; }
            var c = _servico.LerCurso(id);
            if (c == null) Console.WriteLine("Curso não encontrado.");
            else MostrarCurso(c);
        }

        private void ListarCursos()
        {
            Console.WriteLine("\n--- Lista de Cursos ---");
            var lista = _servico.ListarTodos();
            if (lista.Count == 0) { Console.WriteLine("Nenhum curso registado."); return; }
            Console.WriteLine($"{"ID",-5} {"Nome",-25} {"Duração",-10} {"Ativo",-6}");
            Console.WriteLine(new string('-', 50));
            foreach (var c in lista)
                Console.WriteLine($"{c.Id,-5} {c.Nome,-25} {c.Duracao,-10} {(c.Status ? "Sim" : "Não"),-6}");
        }

        private void AtualizarCurso()
        {
            Console.Write("\nID do curso a atualizar: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("ID inválido."); return; }
            var c = _servico.LerCurso(id);
            if (c == null) { Console.WriteLine("Curso não encontrado."); return; }

            Console.Write($"Novo nome [{c.Nome}]: ");
            string nome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nome)) c.Nome = nome;

            Console.Write($"Nova duração [{c.Duracao}]: ");
            string durStr = Console.ReadLine();
            if (int.TryParse(durStr, out int dur)) c.Duracao = dur;

            Console.Write($"Nova descrição [{c.Descricao}]: ");
            string desc = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(desc)) c.Descricao = desc;

            Console.Write($"Ativo? (s/n) [{(c.Status ? "s" : "n")}]: ");
            string st = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(st)) c.Status = st.ToLower() == "s";

            try { _servico.AtualizarCurso(c); Console.WriteLine("Curso atualizado com sucesso!"); }
            catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }
        }

        private void RemoverCurso()
        {
            Console.Write("\nID do curso a remover: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("ID inválido."); return; }
            Console.Write("Tem a certeza? (s/n): ");
            if (Console.ReadLine()?.ToLower() != "s") return;
            try { _servico.DeletarCurso(id); Console.WriteLine("Curso removido com sucesso!"); }
            catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }
        }

        private void MostrarCurso(Curso c)
        {
            Console.WriteLine($"  ID: {c.Id} | Nome: {c.Nome} | Duração: {c.Duracao} sem. | Ativo: {(c.Status ? "Sim" : "Não")}");
            Console.WriteLine($"  Descrição: {c.Descricao}");
        }
    }
}