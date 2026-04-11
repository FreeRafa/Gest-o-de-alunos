# 🎓 Gestão de Alunos

Sistema de gestão académica desenvolvido em C# com arquitetura em camadas, utilizando SQL Server como base de dados. O sistema possui autenticação com perfis distintos (Aluno e Professor), CRUD completo de todas as entidades e gestão de matrículas.

---

## 📌 Objetivo

O sistema permite gerir:
- Utilizadores com login e perfis (Aluno / Professor)
- Alunos
- Professores
- Cursos
- Disciplinas
- Matrículas

Com foco em boas práticas de programação, separação de responsabilidades e persistência de dados.

---

## 🏗️ Arquitetura do Projeto

O projeto segue uma arquitetura em camadas:

- **Model** → Representa as entidades do sistema (Aluno, Professor, Curso, Disciplina, Matricula, Utilizador)
- **Data** → Script SQL para criação da base de dados
- **Repositorio** → Lógica de acesso aos dados via ADO.NET
- **Servico** → Regras de negócio e validações
- **MenuAux** → Menus de consola para cada entidade
- **Program.cs** → Ponto de entrada da aplicação

---

## 🗄️ Base de Dados

O sistema utiliza SQL Server com as seguintes tabelas:

| Tabela       | Descrição                                      |
|--------------|------------------------------------------------|
| Aluno        | Dados pessoais dos alunos                      |
| Professor    | Dados dos professores                          |
| Curso        | Cursos disponíveis no sistema                  |
| Disciplina   | Associada a um Curso e a um Professor          |
| Matricula    | Registo de matrículas de Alunos em Disciplinas |
| Utilizador   | Credenciais de acesso e perfil do utilizador   |

### Relacionamentos principais:
- Um **Curso** possui várias **Disciplinas**
- Uma **Disciplina** pertence a um **Curso** e a um **Professor**
- Um **Aluno** pode matricular-se em várias **Disciplinas**
- Um **Utilizador** está associado a um **Aluno** ou **Professor**

---

## 🔐 Sistema de Autenticação

O sistema possui login com dois perfis distintos:

### Aluno
- Ver cursos disponíveis
- Matricular-se em disciplinas
- Ver as suas matrículas
- Alterar o seu cadastro
- Alterar password

### Professor (Administrador)
- Cadastrar, listar, atualizar e apagar alunos
- Gerir cursos (criar, consultar, atualizar, remover)
- Gerir disciplinas (criar, consultar, atualizar, remover)
- Ver todas as matrículas do sistema
- Alterar password

### Sem login
- Visualizar a lista de cursos disponíveis

---

## ⚙️ Tecnologias Utilizadas

- C# / .NET
- SQL Server
- ADO.NET
- Programação Orientada a Objetos (POO)
- Arquitetura em Camadas

---

## 🔑 Funcionalidades por Entidade

### Aluno
- Criar, consultar, listar, atualizar e remover

### Professor
- Criar, consultar, listar, atualizar e remover

### Curso
- Criar, consultar, listar, atualizar e remover
- Campo de status (ativo/inativo)

### Disciplina
- Criar, consultar, listar, atualizar e remover
- Associada a um Curso e a um Professor

### Matrícula
- Criar matrícula (Aluno + Disciplina)
- Listar matrículas por aluno
- Listar todas as matrículas
- Alterar estado: `1=Ativa` | `2=Trancada` | `3=Concluída`
- Cancelar matrícula

### Utilizador
- Login com email e password
- Redirecionamento automático por perfil
- Alteração de password

---

## 🚀 Como Executar

1. Clonar o repositório
2. Abrir o projeto no **Visual Studio**
3. Executar o script SQL da pasta `Data/` no **SQL Server Management Studio**
4. Configurar a connection string no `Program.cs`:
```csharp
string cs = "Server=SEU_SERVIDOR;Database=Gestao_de_Alunos;User Id=SEU_USER;Password=SUA_PASSWORD;";
```
5. Compilar e executar o projeto (F5)

### Utilizadores de teste
| Email               | Password | Perfil    |
|---------------------|----------|-----------|
| carlos@escola.com   | 1234     | Professor |
| ana@escola.com      | 1234     | Professor |
| pedro@escola.com    | 1234     | Professor |
| joao@escola.com     | 1234     | Aluno     |
| maria@escola.com    | 1234     | Aluno     |
| rafael@escola.com   | 1234     | Aluno     |

---

## 📂 Estrutura do Projeto