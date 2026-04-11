using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestao_de_Alunos.Model
{
    public class Disciplina
    {
        public int Id { get; set; }
        public int IdCurso { get; set; }
        public int IdProfessor { get; set; }
        public string Nome { get; set; }
        public int CargaHoraria { get; set; }
        public int Semestre { get; set; }

        // Para exibição
        public string NomeCurso { get; set; }
        public string NomeProfessor { get; set; }
    }
}
