CREATE TABLE obligatoriodb.Weather (
	wid INT NOT NULL,
	name VARCHAR(30) NOT NULL,
	PRIMARY KEY (wid)
);

INSERT INTO obligatoriodb.Weather (wid, name) VALUES
	(1, 'Freezing'),
	(2, 'Bracing'),
	(3, 'Chilly'),
	(4, 'Cool'),
	(5, 'Mild'),
	(6, 'Warm'),
	(7, 'Balmy'),
	(8, 'Hot'),
	(9, 'Sweltering'),
	(10, 'Scorching');

CREATE TABLE obligatoriodb.Logins(
	Logid VARCHAR(30) NOT NULL,
	Pwd VARCHAR(100) NOT NULL,
	PRIMARY KEY (Logid)
);

CREATE TABLE obligatoriodb.Funcionarios (
	Ci INT NOT NULL,
	Nombre VARCHAR(30), 
	Apellido VARCHAR(30),
	Email VARCHAR(60) NOT NULL,
	Fch_Nacimiento DATE,
	Direccion VARCHAR(60),
	Telefono INT,
	Logid INT NOT NULL,
	PRIMARY KEY (Ci),
	FOREIGN KEY (Logid) REFERENCES Logins(Logid)
);

CREATE TABLE obligatoriodb.Agenda (
	Nro INT NOT NULL,
	Ci INT NOT NULL,
	Fch_Agenda DATE TIME NOT NULL,
	PRIMARY KEY (Nro),
	FOREIGN KEY (Ci) REFERENCES Funcionarios(Ci)
);

CREATE TABLE obligatoriodb.Carnet_Salud (
	Ci INT NOT NULL,
	Fch_Emision DATE NOT NULL,
	Fch_Vencimiento DATE NOT NULL,
	Comprobante VARCHAR(100),
	FOREIGN KEY (Ci) REFERENCES Funcionarios(Ci)
);

CREATE TABLE obligatoriodb.Periodos_Actualizacion(
	Año YEAR NOT NULL,
	Semestre INT NOT NULL,
	Fch_Inicio DATE NOT NULL,
	Fch_Fin DATE NOT NULL
);
