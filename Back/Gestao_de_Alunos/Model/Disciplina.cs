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
        public string Nome { get; set; }
        public int CargaHoraria { get; set; }
        public int Semestre { get; set; }

        public Curso curso { get; set; }
        public int Id_Curso { get; set; }

        public Professor professor { get; set; }
        public int Id_Professor { get; set; }
    }
}
