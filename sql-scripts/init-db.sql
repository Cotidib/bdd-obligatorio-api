CREATE TABLE obligatoriodb.Logins (
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
	Logid VARCHAR(30) NOT NULL,
	PRIMARY KEY (Ci),
	FOREIGN KEY (Logid) REFERENCES Logins(Logid)
);

CREATE TABLE obligatoriodb.Agenda (
	Nro INT AUTO_INCREMENT,
	Ci INT NOT NULL,
	Fch_Agenda DATETIME NOT NULL,
	PRIMARY KEY (Nro),
	FOREIGN KEY (Ci) REFERENCES Funcionarios(Ci)
);

CREATE TABLE obligatoriodb.Comprobante (
	Cid INT NOT NULL,
	Nombre VARCHAR(30) NOT NULL,
	Contenido LONGBLOB NOT NULL,
	Tipo VARCHAR(10),
	PRIMARY KEY (Cid)
);

CREATE TABLE obligatoriodb.Carnet_Salud (
	Ci INT NOT NULL,
	Fch_Emision DATE NOT NULL,
	Fch_Vencimiento DATE NOT NULL,
	Comprobante INT,
	FOREIGN KEY (Comprobante) REFERENCES Comprobante(Cid),
	FOREIGN KEY (Ci) REFERENCES Funcionarios(Ci)
);

CREATE TABLE obligatoriodb.Periodos_Actualizacion (
	Anio INT NOT NULL,
	Semestre INT NOT NULL,
	Fch_Inicio DATE NOT NULL,
	Fch_Fin DATE NOT NULL
);

INSERT INTO Periodos_Actualizacion (Anio, Semestre, Fch_Inicio, Fch_Fin)
VALUES
(2023, 2, '2023-11-01', '2023-11-15');