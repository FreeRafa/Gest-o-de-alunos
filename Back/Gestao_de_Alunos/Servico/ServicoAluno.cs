using System;
using Gestao_de_Alunos.Model;
using Gestao_de_Alunos.Repositorio;

namespace Gestao_de_Alunos.Servico
{
    public class ServicoAluno
    {
        private readonly RepositorioAluno _alunoRepositorio;

        public ServicoAluno(RepositorioAluno repositorioAluno)
        {
            _alunoRepositorio = repositorioAluno;
        }

        public void CriarAluno(Aluno aluno)
        {
            if (string.IsNullOrWhiteSpace(aluno.Nome))
                throw new Exception("O nome é obrigatório.");
            if (string.IsNullOrWhiteSpace(aluno.UltimoNome))
                throw new Exception("O último nome é obrigatório.");
            if (aluno.DataNascimento == default || aluno.DataNascimento > DateTime.Today)
                throw new Exception("Data de nascimento inválida.");
            if (aluno.DataNascimento.Year < 1900)
                throw new Exception("Data de nascimento inválida.");

            _alunoRepositorio.CriarAluno(aluno);
        }

        public Aluno LerAluno(int Id)
        {
            return _alunoRepositorio.LerAluno(Id);
        }

        public System.Collections.Generic.List<Aluno> ListarTodos()
        {
            return _alunoRepositorio.ListarTodos();
        }

        public void AtualizarAluno(Aluno aluno)
        {
            if (aluno.Id <= 0)
                throw new ArgumentException("Id inválido.");
            if (string.IsNullOrWhiteSpace(aluno.Nome))
                throw new Exception("O nome é obrigatório.");
            if (string.IsNullOrWhiteSpace(aluno.UltimoNome))
                throw new Exception("O último nome é obrigatório.");
            if (aluno.DataNascimento == default || aluno.DataNascimento > DateTime.Today)
                throw new Exception("Data de nascimento inválida.");

            _alunoRepositorio.AtualizarAluno(aluno);
        }

        public void DeletarAluno(int Id)
        {
            if (Id <= 0)
                throw new ArgumentException("Id inválido.");
            _alunoRepositorio.DeletarAluno(Id);
        }
    }
}