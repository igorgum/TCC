CREATE DATABASE TCC;

USE TCC;

CREATE TABLE Periodo(
	Cd_Periodo	CHAR(1) NOT NULL UNIQUE,
	Periodo		VARCHAR(25),

	PRIMARY KEY (Cd_Periodo)
);

CREATE TABLE Materia(
	Cd_Materia	CHAR(8) NOT NULL UNIQUE,
	Horario		TIME,
	Materia		VARCHAR(25),
	Cd_Periodo	CHAR(1) NOT NULL UNIQUE,

	PRIMARY KEY (Cd_Materia),
	FOREIGN KEY (Cd_Periodo) REFERENCES Periodo(Cd_Periodo)
);

CREATE TABLE Professor(
	Cd_Professor 	CHAR(8) NOT NULL UNIQUE,
	Nm_Professor	VARCHAR(50),
	Email		VARCHAR(50) UNIQUE,
	

	PRIMARY KEY (Cd_Professor)
);

CREATE TABLE Aluno(
	Cd_Aluno	CHAR(8) NOT NULL UNIQUE,
	Nm_Aluno	VARCHAR(50),
	RA		CHAR(10) NOT NULL UNIQUE,
	Email		VARCHAR(50),
	Turma		DATE,

	PRIMARY KEY (Cd_Aluno)
);

CREATE TABLE Funcionario(
	Cd_Funcionario	CHAR(8) NOT NULL UNIQUE,
	Login		VARCHAR(25) UNIQUE,
	Senha		VARCHAR(32),
	Nm_Funcionario	VARCHAR(50),
	Email		VARCHAR(50) UNIQUE,
	EmailConfirmado BOOLEAN,

	PRIMARY KEY (Cd_Funcionario)
);

CREATE TABLE Prof_Mat(
	Cd_Materia	CHAR(8) NOT NULL,
	Cd_Professor	CHAR(8) NOT NULL,

	PRIMARY KEY (Cd_Materia, Cd_Professor),
	FOREIGN KEY (Cd_Materia) REFERENCES Materia(Cd_Materia),
	FOREIGN KEY (Cd_Professor) REFERENCES Professor(Cd_Professor)

);

CREATE TABLE Alun_Mat(
	Cd_Materia	CHAR(8) NOT NULL,
	Cd_Aluno	CHAR(8) NOT NULL,
	Matriculado	BOOLEAN,
	Aprovado	BOOLEAN,
	Media_Final	FLOAT(10),

	PRIMARY KEY (Cd_Materia, Cd_Aluno),
	FOREIGN KEY (Cd_Materia) REFERENCES Materia(Cd_Materia),
	FOREIGN KEY (Cd_Aluno) REFERENCES Aluno(Cd_Aluno)
);

CREATE TABLE Sala(
	Numero		INTEGER NOT NULL UNIQUE,
	Andar		INTEGER,
	
	PRIMARY KEY (Numero)
);

CREATE TABLE Prof_Reserva (
    Cd_Reserva INTEGER NOT NULL UNIQUE,
    Cd_Professor CHAR(8) NOT NULL,
    Numero INTEGER NOT NULL,
    Cd_Funcionario CHAR(8) NOT NULL,
    PRIMARY KEY (Cd_Reserva),
    FOREIGN KEY (Cd_Professor)
        REFERENCES Professor (Cd_Professor),
    FOREIGN KEY (Numero)
        REFERENCES Sala (Numero),
    FOREIGN KEY (Cd_Funcionario)
        REFERENCES Funcionario (Cd_Funcionario)
);

CREATE TABLE Aluno_Reserva(
	Cd_Reserva	INTEGER NOT NULL UNIQUE,
	Cd_Aluno	CHAR(8) NOT NULL,
	Numero		INTEGER NOT NULL,
	Cd_Funcionario	CHAR(8) NOT NULL,

	PRIMARY KEY (Cd_Reserva),
	FOREIGN KEY (Cd_Aluno) REFERENCES Aluno(Cd_Aluno),
	FOREIGN KEY (Numero) REFERENCES Sala(Numero),
	FOREIGN KEY (Cd_Funcionario) REFERENCES Funcionario(Cd_Funcionario)
);




CREATE TABLE Presenca_Al(
	Alu_Data	TIMESTAMP NOT NULL UNIQUE,
	Cd_Aluno	CHAR(8) NOT NULL,
	Numero		INTEGER NOT NULL,
	Cd_Funcionario	CHAR(8) NOT NULL,
	
	PRIMARY KEY (Alu_Data),
	FOREIGN KEY (Cd_Aluno) REFERENCES Aluno(Cd_Aluno),
	FOREIGN KEY (Numero) REFERENCES Sala(Numero),
	FOREIGN KEY (Cd_Funcionario) REFERENCES Funcionario(Cd_Funcionario)
);

CREATE TABLE Presenca_Prof(
	Prof_TimeStamp	TIMESTAMP NOT NULL UNIQUE,
	Cd_Professor	CHAR(8) NOT NULL,
	Numero		INTEGER NOT NULL,

	PRIMARY KEY (Prof_TimeStamp),
	FOREIGN KEY (Cd_Professor) REFERENCES Professor(Cd_Professor),
	FOREIGN KEY (Numero) REFERENCES Sala(Numero)
);

CREATE TABLE Funcao(
	Cd_Funcao	CHAR(8) NOT NULL UNIQUE,
	Funcao		CHAR(50),
	
	PRIMARY KEY (Cd_Funcao)
);

CREATE TABLE Func_Func(
	Cd_Funcionario	CHAR(8) NOT NULL UNIQUE,
	Cd_Funcao	CHAR(8) NOT NULL,

	PRIMARY KEY (Cd_Funcionario, Cd_Funcao),
	FOREIGN KEY (Cd_Funcionario) REFERENCES Funcionario(Cd_Funcionario),
	FOREIGN KEY (Cd_Funcao) REFERENCES Funcao(Cd_Funcao)
);


INSERT INTO `tcc`.`periodo`
(`Cd_Periodo`,
`Periodo`)
VALUES
(1,
'Matutino');

INSERT INTO `tcc`.`periodo`
(`Cd_Periodo`,
`Periodo`)
VALUES
(2,
'Vespertino');

INSERT INTO `tcc`.`periodo`
(`Cd_Periodo`,
`Periodo`)
VALUES
(3,
'Noturno');



INSERT INTO `tcc`.`materia`
(`Cd_Materia`,
`Horario`,
`Materia`,
`Cd_Periodo`)
VALUES
(1,
'07:30',
'Banco de Dados',
1);


INSERT INTO `tcc`.`materia`
(`Cd_Materia`,
`Horario`,
`Materia`,
`Cd_Periodo`)
VALUES
(2,
'14:30',
'COBOL',
2);



INSERT INTO `tcc`.`materia`
(`Cd_Materia`,
`Horario`,
`Materia`,
`Cd_Periodo`)
VALUES
(3,
'19:30',
'Engenharia de Software',
3);


INSERT INTO `tcc`.`funcao`
(`Cd_Funcao`,
`Funcao`)
VALUES
(1,
'Limpeza');

INSERT INTO `tcc`.`funcao`
(`Cd_Funcao`,
`Funcao`)
VALUES
(2,
'Manutencao');

INSERT INTO `tcc`.`funcao`
(`Cd_Funcao`,
`Funcao`)
VALUES
(3,
'Administracao');


INSERT INTO `tcc`.`funcionario`
(`Cd_Funcionario`,
`Login`,
`Senha`,
`Nm_Funcionario`,
`Email`)
VALUES
(1,
'igor',
123,
'Igor',
'algumacoisa@email.com');

INSERT INTO `tcc`.`funcionario`
(`Cd_Funcionario`,
`Login`,
`Senha`,
`Nm_Funcionario`,
`Email`)
VALUES
('XUPA',
'Jeff',
321,
'Jeff',
'Jeff@email.com');

INSERT INTO `tcc`.`func_func`
(`Cd_Funcionario`,
`Cd_Funcao`)
VALUES
(1,
3);

INSERT INTO `tcc`.`func_func`
(`Cd_Funcionario`,
`Cd_Funcao`)
VALUES
('XUPA',
3);


INSERT INTO `tcc`.`aluno`
(`Cd_Aluno`,
`Nm_Aluno`,
`RA`,
`Email`,
`Turma`)
VALUES
(1,
'Jeff',
12345,
null,
null);

INSERT INTO `tcc`.`aluno`
(`Cd_Aluno`,
`Nm_Aluno`,
`RA`,
`Email`,
`Turma`)
VALUES
(2,
'Carlinhos',
12346,
null,
null);

INSERT INTO `tcc`.`aluno`
(`Cd_Aluno`,
`Nm_Aluno`,
`RA`,
`Email`,
`Turma`)
VALUES
(3,
'Jorge',
12347,
null,
null);

INSERT INTO `tcc`.`alun_mat`
(`Cd_Materia`,
`Cd_Aluno`,
`Matriculado`,
`Aprovado`,
`Media_Final`)
VALUES
(1,
1,
true,
false,
0);

INSERT INTO `tcc`.`alun_mat`
(`Cd_Materia`,
`Cd_Aluno`,
`Matriculado`,
`Aprovado`,
`Media_Final`)
VALUES
(2,
2,
true,
true,
10);

INSERT INTO `tcc`.`alun_mat`
(`Cd_Materia`,
`Cd_Aluno`,
`Matriculado`,
`Aprovado`,
`Media_Final`)
VALUES
(3,
3,
true,
true,
7);



INSERT INTO `tcc`.`professor`
(`Cd_Professor`,
`Nm_Professor`,
`Email`)
VALUES
(1,
'Chiara',
null);

INSERT INTO `tcc`.`professor`
(`Cd_Professor`,
`Nm_Professor`,
`Email`)
VALUES
(2,
'Gerson',
null);

INSERT INTO `tcc`.`professor`
(`Cd_Professor`,
`Nm_Professor`,
`Email`)
VALUES
(3,
'Anesio',
null);






INSERT INTO `tcc`.`sala`
(`Numero`,
`Andar`)
VALUES
(1,
1);

INSERT INTO `tcc`.`sala`
(`Numero`,
`Andar`)
VALUES
(2,
1);

INSERT INTO `tcc`.`sala`
(`Numero`,
`Andar`)
VALUES
(3,
1);





SELECT `materia`.`Cd_Materia`,
    `materia`.`Horario`,
    `materia`.`Materia`,
    `periodo`.`Periodo`
FROM `tcc`.`materia`
INNER JOIN `periodo` ON `materia`.`Cd_Periodo` = `periodo`.`Cd_Periodo`;



SELECT funcionario.Cd_Funcionario, funcionario.Login, funcionario.Senha, funcionario.Nm_Funcionario, funcionario.Email, funcao.Funcao
from funcionario
INNER JOIN func_func ON funcionario.Cd_Funcionario = func_func.Cd_Funcionario INNER JOIN funcao ON
func_func.Cd_Funcao = funcao.Cd_Funcao
where funcionario.Cd_Funcionario = 1;



SELECT aluno.Nm_Aluno, materia.Materia, alun_mat.Aprovado, alun_mat.Media_Final
from aluno
INNER JOIN alun_mat ON aluno.Cd_Aluno = alun_mat.Cd_Aluno INNER JOIN materia ON
materia.Cd_Materia = alun_mat.Cd_Materia;

SELECT * FROM `funcionario` WHERE 1

