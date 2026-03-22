# Gest-o-de-alunos
Sistema de gestão escolar desenvolvido em C# e SQL Server para controle de alunos, professores, cursos e matrículas, aplicando arquitetura em camadas e CRUD completo.

# 🎓 Gestão de Alunos

## 📚 Descrição

Sistema de gestão escolar desenvolvido em **C#** com **SQL Server**, com o objetivo de controlar alunos, professores, cursos, disciplinas e matrículas.

Este projeto simula um ambiente real de uma instituição de ensino, aplicando conceitos de **modelagem de banco de dados**, **CRUD completo** e **arquitetura em camadas**.

---

## 🛠️ Tecnologias utilizadas

* C#
* ADO.NET
* SQL Server

---

## ⚙️ Funcionalidades

### 👨‍🎓 Alunos

* Cadastro de alunos
* Listagem de alunos

### 👨‍🏫 Professores

* Cadastro de professores

### 📚 Cursos

* Criação de cursos
* Associação de cursos com disciplinas

### 📖 Disciplinas

* Cadastro de disciplinas
* Associação com cursos e professores

### 📝 Matrículas

* Matrícula de alunos em disciplinas
* Controle de status da matrícula

---

## 🧠 Conceitos aplicados

* CRUD completo (Create, Read, Update, Delete)
* Relacionamentos entre tabelas (1:N e N:N)
* Uso de FOREIGN KEY para integridade de dados
* Constraint UNIQUE para evitar duplicidade
* Separação de camadas:

  * Model
  * Repositório
  * Serviço

---

## 🗄️ Estrutura do banco de dados

O sistema é composto pelas seguintes tabelas:

* Alunos
* Professores
* Curso
* Disciplina
* Matricula

### 🔗 Relacionamentos

* Um curso possui várias disciplinas
* Um professor pode lecionar várias disciplinas
* Um aluno pode estar matriculado em várias disciplinas

---

## 📂 Estrutura do projeto

```
/Model
/Repositorio
/Servico
/SQL
Program.cs
```

---

## 🚀 Como executar o projeto

1. Clonar o repositório:

```
git clone https://github.com/FreeRafa/Gestao_de_Alunos.git
```

2. Criar o banco de dados no SQL Server usando os scripts da pasta `/SQL`

3. Configurar a connection string no projeto

4. Executar o projeto no Visual Studio

---

## ⚠️ Observações

* Este projeto é voltado para fins de estudo
* A connection string deve ser ajustada conforme o ambiente local
* Não contém dados sensíveis

---

## 🎯 Objetivo

Praticar desenvolvimento **back-end com C# e SQL Server**, com foco em:

* Modelagem de dados
* Organização de código
* Boas práticas de desenvolvimento

---

## 📈 Próximas melhorias

* Implementar validações mais robustas
* Adicionar filtros e buscas avançadas
* Criar interface gráfica (futuramente)
* Implementar async/await
