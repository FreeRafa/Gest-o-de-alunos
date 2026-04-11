using System;
using Gestao_de_Alunos.Model;
using Gestao_de_Alunos.Repositorio;

namespace Gestao_de_Alunos.Servico
{
    public class ServicoAuth
    {
        private readonly RepositorioUtilizador _repositorio;

        public ServicoAuth(RepositorioUtilizador repositorio)
        {
            _repositorio = repositorio;
        }

        public Utilizador Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("O email é obrigatório.");
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("A password é obrigatória.");

            Utilizador utilizador = _repositorio.Login(email.Trim(), password);

            if (utilizador == null)
                throw new Exception("Email ou password incorretos.");

            return utilizador;
        }

        public void RegistarAluno(Utilizador utilizador)
        {
            ValidarUtilizador(utilizador);
            utilizador.Perfil = "aluno";
            _repositorio.RegistarUtilizador(utilizador);
        }

        public void RegistarProfessor(Utilizador utilizador)
        {
            ValidarUtilizador(utilizador);
            utilizador.Perfil = "professor";
            _repositorio.RegistarUtilizador(utilizador);
        }

        public void AlterarPassword(int id, string passwordAtual, string novaPassword, string confirmarPassword)
        {
            if (string.IsNullOrWhiteSpace(novaPassword))
                throw new Exception("A nova password não pode estar vazia.");
            if (novaPassword != confirmarPassword)
                throw new Exception("As passwords não coincidem.");
            if (novaPassword.Length < 4)
                throw new Exception("A password deve ter pelo menos 4 caracteres.");

            _repositorio.AlterarPassword(id, novaPassword);
        }

        private void ValidarUtilizador(Utilizador utilizador)
        {
            if (string.IsNullOrWhiteSpace(utilizador.Email))
                throw new Exception("O email é obrigatório.");
            if (string.IsNullOrWhiteSpace(utilizador.Password))
                throw new Exception("A password é obrigatória.");
            if (utilizador.Password.Length < 4)
                throw new Exception("A password deve ter pelo menos 4 caracteres.");
            if (utilizador.IdReferencia <= 0)
                throw new Exception("Referência de utilizador inválida.");
        }
    }
}