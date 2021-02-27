INSERT INTO termekek(tkod, nev, ear, kategoria) VALUES (1, "lapát", 2000, "K1"); /* létező kulcs */
INSERT INTO termekek(tkod, nev, ear, kategoria) VALUES (1, 5, 2000, 6); /* szöveg helyett szám érték */
INSERT INTO termekek(tkod, nev, ear, kategoria) VALUES (1, "lapát", "teszt", "K1");
INSERT INTO termekek(tkod, nev, ear, kategoria) VALUES (1, NULL, "teszt", "K1");  /* üres név mező */