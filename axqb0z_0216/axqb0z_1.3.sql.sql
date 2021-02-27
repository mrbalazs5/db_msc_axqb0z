CREATE TABLE tanfolyam (
	tkod INT PRIMARY KEY AUTO_INCREMENT,
    ar INT CHECK(ar > 0),
	tipus VARCHAR(20),
    megnevezes VARCHAR(50)
);

CREATE TABLE resztvevo (
	tajszam INT PRIMARY KEY AUTO_INCREMENT,
    lakcim VARCHAR(50),
	nev VARCHAR(50)
);

CREATE TABLE tanfolyam_resztvevo (
	tanfolyam INT,
    FOREIGN KEY(tanfolyam) REFERENCES tanfolyam(tkod),
    befizetes INT CHECK(befizetes > 0),
    resztvevo INT,
    FOREIGN KEY(resztvevo) REFERENCES resztvevo(tajszam)
);