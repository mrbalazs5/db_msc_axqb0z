CREATE TABLE gyarto (
	adoszam INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    nev VARCHAR(50),
	telephely VARCHAR(50)
);

CREATE TABLE termek (
	tkod INT PRIMARY KEY NOT NULL AUTO_INCREMENT,
    nev VARCHAR(50),
	ear NUMERIC CHECK(ear > 0),
    gyarto INT,
    FOREIGN KEY(gyarto) REFERENCES gyarto(adoszam)
);