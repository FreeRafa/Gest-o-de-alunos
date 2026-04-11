using System;
using System.Collections.Generic;
using Gestao_de_Alunos.Model;
using Gestao_de_Alunos.Repositorio;
using Gestao_de_Alunos.Servico;

namespace Gestao_de_Alunos.MenuAux
{
    public class MenuAluno
    {
        private readonly ServicoAluno _servicoAluno;
        private readonly ServicoMatricula _servicoMatricula;
        private readonly ServicoCurso _servicoCurso;
        private readonly ServicoAuth _servicoAuth;

        public MenuAluno(ServicoAluno servicoAluno, ServicoMatricula servicoMatricula,
                         ServicoCurso servicoCurso, ServicoAuth servicoAuth)
        {
            _servicoAluno = servicoAluno;
            _servicoMatricula = servicoMatricula;
            _servicoCurso = servicoCurso;
            _servicoAuth = servicoAuth;
        }

        public void Mostrar(Utilizador utilizador)
        {
            int opcao = -1;
            while (opcao != 0)
            {
                Console.WriteLine($"\n=== Menu do Aluno — {utilizador.Email} ===");
                Console.WriteLine("1. Ver cursos disponíveis");
                Console.WriteLine("2. Matricular-me numa disciplina");
                Console.WriteLine("3. Ver as minhas matrículas");
                Console.WriteLine("4. Alterar o meu cadastro");
                Console.WriteLine("5. Alterar password");
                Console.WriteLine("0. Sair / Logout");
                Console.Write("Escolha: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                { Console.WriteLine("Opção inválida!"); continue; }

                switch (opcao)
                {
                    case 1: VerCursos(); break;
                    case 2: Matricular(utilizador.IdReferencia); break;
                    case 3: VerMinhasMatriculas(utilizador.IdReferencia); break;
                    case 4: AlterarCadastro(utilizador.IdReferencia); break;
                    case 5: AlterarPassword(utilizador); break;
                    case 0: Console.WriteLine("Até logo!"); break;
                    default: Console.WriteLine("Opção inválida!"); break;
                }
            }
        }

        private void VerCursos()
        {
            Console.WriteLine("\n--- Cursos disponíveis ---");
            var cursos = _servicoCurso.ListarTodos();
            if (cursos.Count == 0) { Console.WriteLine("Nenhum curso disponível."); return; }
            Console.WriteLine($"{"ID",-5} {"Nome",-30} {"Duração",-10}");
            Console.WriteLine(new string('-', 50));
            foreach (var c in cursos)
                if (c.Status)
                    Console.WriteLine($"{c.Id,-5} {c.Nome,-30} {c.Duracao} sem.");
        }

        private void Matricular(int idAluno)
        {
            Console.WriteLine("\n--- Matricular em disciplina ---");
            Console.Write("ID da disciplina: ");
            if (!int.TryParse(Console.ReadLine(), out int idDisc))
            { Console.WriteLine("ID inválido."); return; }

            try
            {
                var matricula = new Matricula { IdAluno = idAluno, IdDisciplina = idDisc };
                _servicoMatricula.CriarMatricula(matricula);
                Console.WriteLine("Matrícula realizada com sucesso!");
            }
            catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }
        }

        private void VerMinhasMatriculas(int idAluno)
        {
            Console.WriteLine("\n--- As minhas matrículas ---");
            var lista = _servicoMatricula.ListarPorAluno(idAluno);
            if (lista.Count == 0) { Console.WriteLine("Não tens matrículas ainda."); return; }
            Console.WriteLine($"{"ID",-5} {"Disciplina",-30} {"Data",-12} {"Estado",-10}");
            Console.WriteLine(new string('-', 60));
            foreach (var m in lista)
            {
                string estado = m.Status == 1 ? "Ativa" : m.Status == 2 ? "Trancada" : "Concluída";
                Console.WriteLine($"{m.Id,-5} {m.NomeDisciplina,-30} {m.DataMatricula:dd/MM/yyyy,-12} {estado,-10}");
            }
        }

        private void AlterarCadastro(int idAluno)
        {
            Console.WriteLine("\n--- Alterar cadastro ---");
            var aluno = _servicoAluno.LerAluno(idAluno);
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

            try
            {
                _servicoAluno.AtualizarAluno(aluno);
                Console.WriteLine("Cadastro atualizado com sucesso!");
            }
            catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }
        }

        private void AlterarPassword(Utilizador utilizador)
        {
            Console.Write("\nNova password: ");
            string nova = Console.ReadLine();
            Console.Write("Confirmar password: ");
            string confirmar = Console.ReadLine();

            try
            {
                _servicoAuth.AlterarPassword(utilizador.Id, null, nova, confirmar);
                Console.WriteLine("Password alterada com sucesso!");
            }
            catch (Exception ex) { Console.WriteLine("Erro: " + ex.Message); }
        }
    }
}