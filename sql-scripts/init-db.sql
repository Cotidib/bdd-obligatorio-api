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