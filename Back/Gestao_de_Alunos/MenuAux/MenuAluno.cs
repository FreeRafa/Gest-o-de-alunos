using Gestao_de_Alunos.Model;
using Gestao_de_Alunos.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestao_de_Alunos.MenuAux
{
    public class MenuAluno
    {
        private ServicoAluno servicoAluno;

        public MenuAluno(ServicoAluno _servicoAluno)
        {
            servicoAluno = _servicoAluno;
        }

        public void MatriculaAluno() 
        {
            int opcao = -1;

            while (opcao != 0)
            {
                Console.WriteLine("=== Menu de Aluno ===");
                Console.WriteLine("1 .Login");
                Console.WriteLine("2 .Cadastrar");
              
                Console.Write("Escolha uma opção: ");

                opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:


                        break;

                    case 2:

                        Aluno aluno = new Aluno();

                        Console.WriteLine("Cadastro:");

                        

                        Console.WriteLine("Aluno cadastrado com sucesso!");

                        break;

                    case 0:
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
        }
    }
}
