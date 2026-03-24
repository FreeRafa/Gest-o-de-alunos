INSERT INTO Professor (Nome, UltimoNome)
VALUES 
('Carlos', 'Silva'),
('Ana', 'Souza'),
('Marcos', 'Oliveira');

INSERT INTO Curso (Nome, Duracao, Descricao, [Status])
VALUES 
('Engenharia Informática', 36, 'Curso focado em programação e sistemas', 1),
('Gestão', 24, 'Curso de administração e negócios', 1);

INSERT INTO Disciplina (IdCurso, IdProfessor, Nome, CargaHoraria, Semestre)
VALUES
(1, 1, 'Programação C#', 60, 1),
(1, 2, 'Banco de Dados', 80, 1),
(1, 3, 'Algoritmos', 70, 1),
(2, 2, 'Gestão de Projetos', 50, 1);