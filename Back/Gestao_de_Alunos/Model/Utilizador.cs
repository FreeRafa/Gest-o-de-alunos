using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestao_de_Alunos.Model
{
    public class Utilizador
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Perfil { get; set; }   // "aluno" ou "professor"
        public int IdReferencia { get; set; } // Id do Aluno ou Professor

        public bool EhAluno => Perfil == "aluno";
        public bool EhProfessor => Perfil == "professor";
    }
}
