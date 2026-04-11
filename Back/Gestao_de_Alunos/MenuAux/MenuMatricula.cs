using System;
using Gestao_de_Alunos.Model;
using Gestao_de_Alunos.Servico;

namespace Gestao_de_Alunos.MenuAux
{
    public class MenuMatricula
    {
        private readonly ServicoMatricula _servico;

        public MenuMatricula(ServicoMatricula servico) { _servico = servico; }

        public void Mostrar()
        {
            int opcao = -1;
            while (opcao != 0)
            {
                Console.WriteLine("\n=== Menu de Matrículas ===");
                Console.WriteLine("1. Nova Matrícula");
                Console.WriteLine("2. Consultar Matrícula por ID");
                Console.WriteLine("3. Listar Todas as Matrículas");
                Console.WriteLine("4. Ver Matrículas de um Aluno");
                Console.WriteLine("5. Alterar Status da Matrícula");
                Console.WriteLine("6. Cancelar Matrícula");
                Console.WriteLine("0. Voltar");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao)) { Console.WriteLine("Opção inválida!"); continue; }

                switch (opcao)
                {
                    case 1: CriarMatricula(); break;
                    case 2: ConsultarMatricula(); break;
                    case 3: ListarMatriculas(); break;
                    case 4: ListarPorAluno(); break;
                    case 5: AlterarStatus(); break;
                    case 6: CancelarMatricula(); break;
                    case 0: break;
                    default: Console.WriteLine("Opção inválida!"); break;
                }
            }
        }

        private void CriarMatricula()
        {
            Console.WriteLine("\n--- Nova Matrícula ---");
            var m = new Matricula();
            Console.Write("ID do Aluno: ");
            if (!int.TryParse(Console.ReadLine(), out int idAluno)) { Console.WriteLine("ID inválido."); return; }
            m.IdAluno = idAluno;

            Console.Write("ID da Disciplina: ");
            if (!int.TryParse(Console.ReadLine(), out int idDisc)) { Console.WriteLine("ID inválido."); return; }
            m.IdDisciplina = idDisc;

            try
            {
                _servico.CriarMatricula(m);
                Console.WriteLine("Matrícula criada com sucesso! (Data: hoje | Status: Ativa)");
            }
            catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }
        }

        private void ConsultarMatricula()
        {
            Console.Write("\nID da matrícula: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("ID inválido."); return; }
            var m = _servico.LerMatricula(id);
            if (m == null) Console.WriteLine("Matrícula não encontrada.");
            else MostrarMatricula(m);
        }

        private void ListarMatriculas()
        {
            Console.WriteLine("\n--- Lista de Matrículas ---");
            var lista = _servico.ListarTodos();
            if (lista.Count == 0) { Console.WriteLine("Nenhuma matrícula registada."); return; }
            ImprimirTabela(lista);
        }

        private void ListarPorAluno()
        {
            Console.Write("\nID do Aluno: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("ID inválido."); return; }
            var lista = _servico.ListarPorAluno(id);
            if (lista.Count == 0) Console.WriteLine("Nenhuma matrícula encontrada para este aluno.");
            else ImprimirTabela(lista);
        }

        private void AlterarStatus()
        {
            Console.Write("\nID da matrícula: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("ID inválido."); return; }
            Console.WriteLine("Novo status: 1=Ativa | 2=Trancada | 3=Concluída");
            Console.Write("Escolha: ");
            if (!int.TryParse(Console.ReadLine(), out int status)) { Console.WriteLine("Opção inválida."); return; }

            try { _servico.AlterarStatus(id, status); Console.WriteLine("Status atualizado!"); }
            catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }
        }

        private void CancelarMatricula()
        {
            Console.Write("\nID da matrícula a cancelar: ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("ID inválido."); return; }
            Console.Write("Tem a certeza? (s/n): ");
            if (Console.ReadLine()?.ToLower() != "s") return;
            try { _servico.DeletarMatricula(id); Console.WriteLine("Matrícula cancelada."); }
            catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }
        }

        private void ImprimirTabela(System.Collections.Generic.List<Matricula> lista)
        {
            Console.WriteLine($"{"ID",-5} {"Aluno",-25} {"Disciplina",-25} {"Data",-12} {"Status",-10}");
            Console.WriteLine(new string('-', 80));
            foreach (var m in lista)
            {
                string status = m.Status == 1 ? "Ativa" : m.Status == 2 ? "Trancada" : "Concluída";
                Console.WriteLine($"{m.Id,-5} {m.NomeAluno,-25} {m.NomeDisciplina,-25} {m.DataMatricula:dd/MM/yyyy,-12} {status,-10}");
            }
        }

        private void MostrarMatricula(Matricula m)
        {
            string status = m.Status == 1 ? "Ativa" : m.Status == 2 ? "Trancada" : "Concluída";
            Console.WriteLine($"  ID: {m.Id} | Aluno: {m.NomeAluno} | Disciplina: {m.NomeDisciplina}");
            Console.WriteLine($"  Data: {m.DataMatricula:dd/MM/yyyy} | Status: {status}");
        }
    }
}