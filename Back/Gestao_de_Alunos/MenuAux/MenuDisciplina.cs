using System;
using Gestao_de_Alunos.Model;
using Gestao_de_Alunos.Servico;

namespace Gestao_de_Alunos.MenuAux
{
    public class MenuDisciplina
    {
        private readonly ServicoDisciplina _servico;

        public MenuDisciplina(ServicoDisciplina servico) { _servico = servico; }

        public void Mostrar()
        {
            int opcao = -1;
            while (opcao != 0)
            {
                Console.WriteLine("\n=== Menu de Disciplinas ===");
                Console.WriteLine("1. Criar Disciplina");
                Console.WriteLine("2. Consultar Disciplina por ID");
                Console.WriteLine("3. Listar Todas as Disciplinas");
                Console.WriteLine("4. Atualizar Disciplina");
                Console.WriteLine("5. Remover Disciplina");
                Console.WriteLine("0. Voltar");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Opção inválida!");
                    continue;
                }

                switch (opcao)
                {
                    case 1: CriarDisciplina(); break;
                    case 2: ConsultarDisciplina(); break;
                    case 3: ListarDisciplinas(); break;
                    case 4: AtualizarDisciplina(); break;
                    case 5: RemoverDisciplina(); break;
                    case 0: break;
                    default: Console.WriteLine("Opção inválida!"); break;
                }
            }
        }

        private void CriarDisciplina()
        {
            Console.WriteLine("\n--- Criar Disciplina ---");
            var d = new Disciplina();

            Console.Write("Nome: ");
            d.Nome = Console.ReadLine();

            Console.Write("ID do Curso: ");
            if (!int.TryParse(Console.ReadLine(), out int idCurso))
            { Console.WriteLine("ID inválido."); return; }
            d.IdCurso = idCurso;

            Console.Write("ID do Professor: ");
            if (!int.TryParse(Console.ReadLine(), out int idProf))
            { Console.WriteLine("ID inválido."); return; }
            d.IdProfessor = idProf;

            Console.Write("Carga Horária (horas): ");
            if (!int.TryParse(Console.ReadLine(), out int carga))
            { Console.WriteLine("Valor inválido."); return; }
            d.CargaHoraria = carga;

            Console.Write("Semestre: ");
            if (!int.TryParse(Console.ReadLine(), out int semestre))
            { Console.WriteLine("Valor inválido."); return; }
            d.Semestre = semestre;

            try
            {
                _servico.CriarDisciplina(d);
                Console.WriteLine("Disciplina criada com sucesso!");
            }
            catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }
        }

        private void ConsultarDisciplina()
        {
            Console.Write("\nID da disciplina: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            { Console.WriteLine("ID inválido."); return; }

            var d = _servico.LerDisciplina(id);
            if (d == null) Console.WriteLine("Disciplina não encontrada.");
            else MostrarDisciplina(d);
        }

        private void ListarDisciplinas()
        {
            Console.WriteLine("\n--- Lista de Disciplinas ---");
            var lista = _servico.ListarTodos();
            if (lista.Count == 0) { Console.WriteLine("Nenhuma disciplina registada."); return; }

            Console.WriteLine($"{"ID",-5} {"Nome",-25} {"Curso",-20} {"Professor",-25} {"C.H.",-6} {"Sem.",-5}");
            Console.WriteLine(new string('-', 90));
            foreach (var d in lista)
                Console.WriteLine($"{d.Id,-5} {d.Nome,-25} {d.NomeCurso,-20} {d.NomeProfessor,-25} {d.CargaHoraria,-6} {d.Semestre,-5}");
        }

        private void AtualizarDisciplina()
        {
            Console.Write("\nID da disciplina a atualizar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            { Console.WriteLine("ID inválido."); return; }

            var d = _servico.LerDisciplina(id);
            if (d == null) { Console.WriteLine("Disciplina não encontrada."); return; }

            Console.Write($"Novo nome [{d.Nome}]: ");
            string nome = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nome)) d.Nome = nome;

            Console.Write($"Novo ID do Curso [{d.IdCurso}]: ");
            string cursoStr = Console.ReadLine();
            if (int.TryParse(cursoStr, out int idCurso)) d.IdCurso = idCurso;

            Console.Write($"Novo ID do Professor [{d.IdProfessor}]: ");
            string profStr = Console.ReadLine();
            if (int.TryParse(profStr, out int idProf)) d.IdProfessor = idProf;

            Console.Write($"Nova Carga Horária [{d.CargaHoraria}]: ");
            string chStr = Console.ReadLine();
            if (int.TryParse(chStr, out int carga)) d.CargaHoraria = carga;

            Console.Write($"Novo Semestre [{d.Semestre}]: ");
            string semStr = Console.ReadLine();
            if (int.TryParse(semStr, out int sem)) d.Semestre = sem;

            try
            {
                _servico.AtualizarDisciplina(d);
                Console.WriteLine("Disciplina atualizada com sucesso!");
            }
            catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }
        }

        private void RemoverDisciplina()
        {
            Console.Write("\nID da disciplina a remover: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            { Console.WriteLine("ID inválido."); return; }

            Console.Write("Tem a certeza? (s/n): ");
            if (Console.ReadLine()?.ToLower() != "s") return;

            try
            {
                _servico.DeletarDisciplina(id);
                Console.WriteLine("Disciplina removida com sucesso!");
            }
            catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }
        }

        private void MostrarDisciplina(Disciplina d)
        {
            Console.WriteLine($"  ID: {d.Id} | Nome: {d.Nome}");
            Console.WriteLine($"  Curso: {d.NomeCurso} | Professor: {d.NomeProfessor}");
            Console.WriteLine($"  Carga Horária: {d.CargaHoraria}h | Semestre: {d.Semestre}");
        }
    }
}