CREATE DATABASE Gestao_de_Alunos;
GO

USE Gestao_de_Alunos;
GO

-- =========================
-- Tabela Aluno
-- =========================
CREATE TABLE Aluno
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    UltimoNome VARCHAR(100) NOT NULL,
    DataNascimento DATE NOT NULL,
    Fone VARCHAR(50),
    Email VARCHAR(100) UNIQUE
);

-- =========================
-- Tabela Professor
-- =========================
CREATE TABLE Professor
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    UltimoNome VARCHAR(100) NOT NULL
);

-- =========================
-- Tabela Curso
-- =========================
CREATE TABLE Curso
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Duracao INT NOT NULL,
    Descricao VARCHAR(200),
    [Status] BIT NOT NULL
);

-- =========================
-- Tabela Disciplina
-- =========================
CREATE TABLE Disciplina
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    IdCurso INT NOT NULL,
    IdProfessor INT NOT NULL,
    Nome VARCHAR(100) NOT NULL,
    CargaHoraria INT NOT NULL,
    Semestre INT NOT NULL,

    CONSTRAINT FK_Disciplina_Curso FOREIGN KEY (IdCurso)
        REFERENCES Curso (Id),

    CONSTRAINT FK_Disciplina_Professor FOREIGN KEY (IdProfessor)
        REFERENCES Professor (Id)
);

-- =========================
-- Tabela Matricula
-- =========================
CREATE TABLE Matricula
(
    Id INT IDENTITY (1,1) PRIMARY KEY,
    IdAluno INT NOT NULL,
    IdDisciplina INT NOT NULL,
    DataMatricula DATE NOT NULL,
    [Status] INT NOT NULL, -- 1=Ativa | 2=Trancada | 3=Concluída

    CONSTRAINT FK_Matricula_Aluno FOREIGN KEY (IdAluno)
        REFERENCES Aluno (Id),

    CONSTRAINT FK_Matricula_Disciplina FOREIGN KEY (IdDisciplina)
        REFERENCES Disciplina (Id),

    CONSTRAINT UQ_Aluno_Disciplina UNIQUE (IdAluno, IdDisciplina)
);

