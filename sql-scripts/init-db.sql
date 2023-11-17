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

CREATE TABLE obligatoriodb.Funcionarios (
	Ci INT NOT NULL,
	Email VARCHAR(30) NOT NULL,
	PRIMARY KEY (Ci)
);

CREATE TABLE obligatoriodb.Carnet_Salud (
	Ci INT NOT NULL,
	FOREIGN KEY (Ci) REFERENCES Funcionarios(Ci)
);

CREATE TABLE obligatoriodb.Periodos_Actualizacion(
	Año YEAR NOT NULL,
	Semestre INT NOT NULL,
	Fch_Inicio DATE NOT NULL,
	Fch_Fin DATE NOT NULL
);