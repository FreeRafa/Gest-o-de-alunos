using Gestao_de_Alunos.Model;
using Gestao_de_Alunos.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (string.IsNullOrEmpty(aluno.Nome))
                throw new Exception("O nome é obtogatório");

            if (string.IsNullOrEmpty(aluno.UltimoNome))
                throw new Exception("O ultimo nome é obtogatório");

            //Verificar DATANASCIMENTO

            _alunoRepositorio.CriarAluno(aluno);

        }

        public Aluno LerAluno(int Id) 
        {
           return _alunoRepositorio.LerAluno(Id);

        }

        public void AtualizarAluno(Aluno aluno) 
        {
            if (aluno.Id <= 0)
                throw new ArgumentException("Id invalido");

            if (string.IsNullOrWhiteSpace(aluno.Nome))
                throw new Exception("O nome é obrigatorio");

            if(string.IsNullOrWhiteSpace(aluno.UltimoNome))
                throw new Exception("O ultimo nome é obrigatorio");

            _alunoRepositorio.AtualizarAluno(aluno);

        }

        public void DeletarAluno(int Id) 
        {
            if (Id <= 0)
                throw new ArgumentException("Id invalido");

            _alunoRepositorio.DeletarAluno(Id);
        }
    }
}
