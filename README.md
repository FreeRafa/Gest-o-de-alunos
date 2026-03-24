# 🎓 Gestao de Alunos

Sistema de gestão académica desenvolvido em C# com base em arquitetura em camadas, utilizando SQL Server como base de dados.

## 📌 Objetivo

O sistema permite gerir:

- Alunos
- Professores
- Cursos
- Disciplinas
- Matrículas

Com foco em boas práticas de programação, separação de responsabilidades e persistência de dados.

---

## 🏗️ Arquitetura do Projeto

O projeto está dividido em camadas:

- **Model** → Representa as entidades (Aluno, Professor, Curso, etc.)
- **Repositorio** → Responsável pelo acesso à base de dados (SQL Server)
- **Servico** → Contém regras de negócio e validações
- **(Futuro) Controller / API ou UI** → Interface de interação com o utilizador

---

## 🗄️ Base de Dados

A base de dados inclui as seguintes tabelas:

- Aluno
- Professor
- Curso
- Disciplina
- Matricula

### Relações principais:

- Um Curso possui várias Disciplinas
- Uma Disciplina está associada a um Professor e a um Curso
- Um Aluno pode matricular-se em várias Disciplinas (via Matricula)

---

## ⚙️ Tecnologias Utilizadas

- C#
- .NET
- SQL Server
- ADO.NET
- Programação Orientada a Objetos (POO)

---

## 🔑 Funcionalidades

### Aluno
- Criar aluno
- Listar aluno por ID
- Atualizar aluno
- Remover aluno

### Professor
- CRUD completo

### Curso
- CRUD completo

### Disciplina
- CRUD com ligação a Curso e Professor

### Matrícula
- Criar matrícula
- Listar matrículas
- Cancelar matrícula

---

## 🚀 Como Executar

1. Criar a base de dados executando o script SQL
2. Configurar a connection string no projeto
3. Executar o projeto em Visual Studio
4. Testar as funcionalidades através da camada de serviço/repositório

---

## 📂 Estrutura do Projeto
Gestao_de_Alunos/
│
├── Model/
├── Repositorio/
├── Servico/
├── Program.cs (ou entry point)


---

## 🧠 Conceitos Aplicados

- Separação de responsabilidades (Repository / Service)
- Injeção de dependência
- Validação de dados na camada de serviço
- Acesso a dados com ADO.NET
- Modelagem relacional de base de dados

---

## 📈 Próximos Passos

- Implementação de interface (API ou UI)
- Uso de Entity Framework (opcional)
- Implementação de autenticação
- Melhorias na gestão de matrículas (fluxos automáticos)
- Testes unitários

---

## 👨‍💻 Autor

Projeto desenvolvido como parte de estudo prático de C# e SQL Server.
