using System;
using System.Collections.Generic;
using Gestao_de_Alunos.Model;
using Gestao_de_Alunos.Repositorio;

namespace Gestao_de_Alunos.Servico
{
    public class ServicoMatricula
    {
        private readonly RepositorioMatricula _repositorio;

        public ServicoMatricula(RepositorioMatricula repositorio)
        {
            _repositorio = repositorio;
        }

        public void CriarMatricula(Matricula matricula)
        {
            if (matricula.IdAluno <= 0) throw new Exception("Aluno inválido.");
            if (matricula.IdDisciplina <= 0) throw new Exception("Disciplina inválida.");
            matricula.DataMatricula = DateTime.Today;
            matricula.Status = 1; // Ativa por defeito
            _repositorio.CriarMatricula(matricula);
        }

        public Matricula LerMatricula(int id) => _repositorio.LerMatricula(id);

        public List<Matricula> ListarTodos() => _repositorio.ListarTodos();

        public List<Matricula> ListarPorAluno(int idAluno) => _repositorio.ListarPorAluno(idAluno);

        public void AlterarStatus(int id, int novoStatus)
        {
            if (id <= 0) throw new ArgumentException("Id inválido.");
            if (novoStatus < 1 || novoStatus > 3)
                throw new Exception("Status inválido. Use 1=Ativa, 2=Trancada, 3=Concluída.");
            _repositorio.AtualizarStatus(id, novoStatus);
        }

        public void DeletarMatricula(int id)
        {
            if (id <= 0) throw new ArgumentException("Id inválido.");
            _repositorio.DeletarMatricula(id);
        }
    }
}