using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestao_de_Alunos.Model
{
    public class Matricula
    {
        public int Id { get; set; }
        public DateTime DataMatricula { get; set; }
        public StatusMatricula status { get; set; }

        public Aluno aluno { get; set; }
        public int Id_Aluno { get; set; }

        public Disciplina Disciplina { get; set; }
        public int Id_Disciplina { get; set; }
    }

    public enum StatusMatricula
    {
        Ativa = 1,
        Trancada = 2,
        Concluida = 3
    }
}
