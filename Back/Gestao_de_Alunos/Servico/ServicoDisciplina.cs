using System;
using System.Collections.Generic;
using Gestao_de_Alunos.Model;
using Gestao_de_Alunos.Repositorio;

namespace Gestao_de_Alunos.Servico
{
    public class ServicoDisciplina
    {
        private readonly RepositorioDisciplina _repositorio;

        public ServicoDisciplina(RepositorioDisciplina repositorio)
        {
            _repositorio = repositorio;
        }

        public void CriarDisciplina(Disciplina disciplina)
        {
            if (string.IsNullOrWhiteSpace(disciplina.Nome))
                throw new Exception("O nome da disciplina é obrigatório.");
            if (disciplina.IdCurso <= 0)
                throw new Exception("Curso inválido.");
            if (disciplina.IdProfessor <= 0)
                throw new Exception("Professor inválido.");
            if (disciplina.CargaHoraria <= 0)
                throw new Exception("Carga horária deve ser maior que zero.");
            if (disciplina.Semestre <= 0)
                throw new Exception("Semestre deve ser maior que zero.");
            _repositorio.CriarDisciplina(disciplina);
        }

        public Disciplina LerDisciplina(int id) => _repositorio.LerDisciplina(id);

        public List<Disciplina> ListarTodos() => _repositorio.ListarTodos();

        public void AtualizarDisciplina(Disciplina disciplina)
        {
            if (disciplina.Id <= 0) throw new ArgumentException("Id inválido.");
            if (string.IsNullOrWhiteSpace(disciplina.Nome))
                throw new Exception("O nome da disciplina é obrigatório.");
            _repositorio.AtualizarDisciplina(disciplina);
        }

        public void DeletarDisciplina(int id)
        {
            if (id <= 0) throw new ArgumentException("Id inválido.");
            _repositorio.DeletarDisciplina(id);
        }
    }
}