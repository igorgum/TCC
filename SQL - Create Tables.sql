CREATE DATABASE TCC;

CREATE TABLE Periodo(
	Cd_Periodo	CHAR(1) NOT NULL UNIQUE,
	Periodo		VARCHAR(25),

	PRIMARY KEY (Cd_Periodo)
);

CREATE TABLE Materia(
	Cd_Materia	CHAR(4) NOT NULL UNIQUE,
	Horario		TIME,
	Materia		VARCHAR(25),
	Cd_Periodo	CHAR(1) NOT NULL UNIQUE,

	PRIMARY KEY (Cd_Materia),
	FOREIGN KEY (Cd_Periodo) REFERENCES Periodo
);

CREATE TABLE Professor(
	Cd_Professor 	CHAR(4) NOT NULL UNIQUE,
	Nm_Professor	VARCHAR(50),
	Email		VARCHAR(50) UNIQUE,

	PRIMARY KEY (Cd_Professor)
);

CREATE TABLE Aluno(
	Cd_Aluno	CHAR(4) NOT NULL UNIQUE,
	Nm_Aluno	VARCHAR(50),
	RA		CHAR(10) NOT NULL UNIQUE,
	Email		VARCHAR(50),
	Turma		DATE,

	PRIMARY KEY (Cd_Aluno)
);

CREATE TABLE Funcionario(
	Cd_Funcionario	CHAR(4) NOT NULL UNIQUE,
	Login		VARCHAR(25) UNIQUE,
	Senha		VARCHAR(25),
	Nm_Funcionario	VARCHAR(50),
	Email		VARCHAR(50) UNIQUE,

	PRIMARY KEY (Cd_Funcionario)
);

CREATE TABLE Prof_Mat(
	Cd_Materia	CHAR(4) NOT NULL,
	Cd_Professor	CHAR(4) NOT NULL,

	PRIMARY KEY (Cd_Materia, Cd_Professor),
	FOREIGN KEY (Cd_Materia) REFERENCES Materia,
	FOREIGN KEY (Cd_Professor) REFERENCES Professor,

);

CREATE TABLE Alun_Mat(
	Cd_Materia	CHAR(4) NOT NULL,
	Cd_Aluno	CHAR(4) NOT NULL,
	Matriculado	BOOLEAN,
	Aprovado	BOOLEAN,
	Media_Final	FLOAT(10),

	PRIMARY KEY (Cd_Materia, Cd_Aluno),
	FOREIGN KEY (Cd_Materia) REFERENCES Materia,
	FOREIGN KEY (Cd_Aluno) REFERENCES Aluno
);

CREATE TABLE Sala(
	Numero		INTEGER NOT NULL UNIQUE,
	Andar		INTEGER,
	
	PRIMARY KEY (Numero)
);

CREATE TABLE Prof_Reserva(
	Cd_Reserva	INTEGER NOT NULL UNIQUE,
	Cd_Professor	CHAR(4) NOT NULL,
	Numero		INTEGER NOT NULL,
	Cd_Funcionario	CHAR(4) NOT NULL,

	PRIMARY KEY (Cd_Reserva),
	FOREIGN KEY (Cd_Professor) REFERENCES Professor,
	FOREIGN KEY (Numero) REFERENCES Sala,
	FOREIGN KEY (Cd_Funcionario) REFERENCES Funcionario,
);

CREATE TABLE Aluno_Reserva(
	Cd_Reserva	INTEGER NOT NULL UNIQUE,
	Cd_Aluno	CHAR(4) NOT NULL,
	Numero		INTEGER NOT NULL,
	Cd_Funcionario	CHAR(4) NOT NULL,

	PRIMARY KEY (Cd_Reserva),
	FOREIGN KEY (Cd_Aluno) REFERENCES Aluno,
	FOREIGN KEY (Numero) REFERENCES Sala,
	FOREIGN KEY (Cd_Funcionario) REFERENCES Funcionario,
);

CREATE TABLE Presenca_Al(
	Alu_Data	INTEGER NOT NULL UNIQUE,
	Cd_Aluno	CHAR(4) NOT NULL,
	Numero		INTEGER NOT NULL,
	Cd_Funcionario	CHAR(4) NOT NULL,
	
	PRIMARY KEY (AluData),
	FOREIGN KEY (Cd_Aluno) REFERENCES Aluno,
	FOREIGN KEY (Numero) REFERENCES Sala,
	FOREIGN KEY (Cd_Funcionario) REFERENCES Funcionario
);

CREATE TABLE Presenca_Prof(
	Prof_TimeStamp	TIMESTAMP NOT NULL UNIQUE,
	Cd_Professor	CHAR(4) NOT NULL,
	Numero		INTEGER NOT NULL,

	PRIMARY KEY (Prof_TimeStamp),
	FOREIGN KEY (Cd_Professor) REFERENCES Professor,
	FOREIGN KEY (Numero) REFERENCES Sala
);

CREATE TABLE Funcao(
	Cd_Funcao	CHAR(4) NOT NULL UNIQUE,
	Funcao		CHAR(50),
	
	PRIMARY KEY (Cd_Funcao)
);

CREATE TABLE Func_Func(
	Cd_Funcionario	CHAR(4) NOT NULL UNIQUE,
	Cd_Funcao	CHAR(4) NOT NULL,

	PRIMARY KEY (Cd_Funcionario, Cd_Funcao),
	FOREIGN KEY (Cd_Funcionario) REFERENCES Funcionario,
	FOREIGN KEY (Cd_Funcao) REFERENCES Funcao
);
