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
        public int IdAluno { get; set; }
        public int IdDisciplina { get; set; }
        public DateTime DataMatricula { get; set; }
        public int Status { get; set; } // 1=Ativa | 2=Trancada | 3=Concluída

        // Para exibição
        public string NomeAluno { get; set; }
        public string NomeDisciplina { get; set; }
    }
}
