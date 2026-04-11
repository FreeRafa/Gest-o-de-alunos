using Gestao_de_Alunos.Model;
using Gestao_de_Alunos.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestao_de_Alunos.Servico
{
    public class ServicoProfessor
    {
        private readonly RepositorioProfessor _professorRepositorio;

        public ServicoProfessor(RepositorioProfessor repositorioProfessor)
        {
            _professorRepositorio = repositorioProfessor;
        }

        public void CriarProfessor(Professor professor)
        {
            if (string.IsNullOrEmpty(professor.Nome))
                throw new Exception("O nome é obtogatório");

            if (string.IsNullOrEmpty(professor.UltimoNome))
                throw new Exception("O ultimo nome é obtogatório");

            //Verificar DATANASCIMENTO

            _professorRepositorio.CriarProfessor(professor);
        }

        public Professor LerProfessor(int Id)
        {
            return _professorRepositorio.LerProfessor(Id);
        }

        public void AtualizarProfessor(Professor professor)
        {
            if (professor.Id <= 0)
                throw new ArgumentException("Id invalido");

            if (string.IsNullOrWhiteSpace(professor.Nome))
                throw new Exception("O nome é obrigatorio");

            if (string.IsNullOrWhiteSpace(professor.UltimoNome))
                throw new Exception("O ultimo nome é obrigatorio");

            _professorRepositorio.AtualizarProfessor(professor);
        }

        public void DeletarProfessor(int Id)
        {
            if (Id <= 0)
                throw new ArgumentException("Id invalido");

            _professorRepositorio.DeletarProfessor(Id);
        }

        public List<Professor> ListarTodos()
        {
            return _professorRepositorio.ListarTodos();
        }
    }
}
