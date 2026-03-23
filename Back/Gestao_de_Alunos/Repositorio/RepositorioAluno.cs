using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestao_de_Alunos.Repositorio
{
    public class RepositorioAluno
    {
        private readonly string _ConnectionString;

        public RepositorioAluno(string connectionString)
        {
            _ConnectionString = connectionString;
        }
    }
}
