using System;
using System.Collections.Generic;
using Gestao_de_Alunos.Model;
using Gestao_de_Alunos.Repositorio;

namespace Gestao_de_Alunos.Servico
{
    public class ServicoCurso
    {
        public string ConnectionString => _repositorio.ConnectionString;
        private readonly RepositorioCurso _repositorio;

        public ServicoCurso(RepositorioCurso repositorio)
        {
            _repositorio = repositorio;
        }

        public void CriarCurso(Curso curso)
        {
            if (string.IsNullOrWhiteSpace(curso.Nome))
                throw new Exception("O nome do curso é obrigatório.");
            if (curso.Duracao <= 0)
                throw new Exception("A duração deve ser maior que zero.");
            _repositorio.CriarCurso(curso);
        }

        public Curso LerCurso(int id) => _repositorio.LerCurso(id);

        public List<Curso> ListarTodos() => _repositorio.ListarTodos();

        public void AtualizarCurso(Curso curso)
        {
            if (curso.Id <= 0) throw new ArgumentException("Id inválido.");
            if (string.IsNullOrWhiteSpace(curso.Nome))
                throw new Exception("O nome do curso é obrigatório.");
            if (curso.Duracao <= 0)
                throw new Exception("A duração deve ser maior que zero.");
            _repositorio.AtualizarCurso(curso);
        }

        public void DeletarCurso(int id)
        {
            if (id <= 0) throw new ArgumentException("Id inválido.");
            _repositorio.DeletarCurso(id);
        }
    }
}