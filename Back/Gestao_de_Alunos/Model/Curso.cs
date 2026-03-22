using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestao_de_Alunos.Model
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Duracao { get; set; }
        public string Descricao { get; set; }
        public bool status { get; set; } // 1 Ativo 0 Inativo

    }
}
