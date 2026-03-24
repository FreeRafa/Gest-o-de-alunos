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

O projeto segue uma arquitetura em camadas:

- **Model** → Representa as entidades do sistema (Aluno, Professor, Curso, Disciplina, Matricula)
- **Data** → Responsável pela conexão com a base de dados (SQL Server)
- **Repositorio** → Contém a lógica de acesso aos dados (ADO.NET)
- **Servico** → Contém as regras de negócio e validações
- **Program.cs** → Ponto de entrada da aplicação

---

## 🗄️ Base de Dados

O sistema utiliza SQL Server com as seguintes tabelas:

- Aluno
- Professor
- Curso
- Disciplina
- Matricula

Relacionamentos principais:
- Um Curso possui várias Disciplinas
- Uma Disciplina pertence a um Curso e a um Professor
- Um Aluno pode matricular-se em várias Disciplinas

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
- Consultar aluno por ID
- Atualizar aluno
- Remover aluno

### Professor
- Gestão de professores

### Curso
- Gestão de cursos disponíveis

### Disciplina
- Associada a um curso e a um professor

### Matrícula
- Alunos podem matricular-se em disciplinas
- Relacionamento entre aluno e disciplina

---

## 🚀 Como Executar

1. Configurar a connection string no ficheiro `SqlServer.cs` ou `App.config`
2. Executar o script SQL para criação da base de dados
3. Compilar e executar o projeto no Visual Studio

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

- Arquitetura em camadas
- Separação de responsabilidades
- ADO.NET para acesso a dados
- Programação orientada a objetos
- Validação de regras de negócio na camada Service
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
