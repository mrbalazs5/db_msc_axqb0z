CREATE TABLE gyarto (
	adoszam INT PRIMARY KEY AUTO_INCREMENT,
    nev VARCHAR(50),
	irsz CHAR(4),
	varos CHAR(40),
    utca CHAR(100)
);

CREATE TABLE termek (
	tkod INT PRIMARY KEY AUTO_INCREMENT,
    nev VARCHAR(50),
	ear INT CHECK(ear > 0),
        gyarto INT,
	FOREIGN KEY(gyarto) REFERENCES gyarto(adoszam)
);

CREATE TABLE egysegek (
    aru INT,
	FOREIGN KEY(aru) REFERENCES termek(tkod),
    db INT CHECK(db > 0)
);

CREATE TABLE alkatresz (
    akod INT PRIMARY KEY AUTO_INCREMENT,
	nev VARCHAR(50)
);

CREATE TABLE komponens (
    termek INT,
	FOREIGN KEY(termek) REFERENCES termek(tkod),
	alkatresz INT,
	FOREIGN KEY(alkatresz) REFERENCES alkatresz(akod)
);