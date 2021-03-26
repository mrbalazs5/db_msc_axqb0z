xquery version "3.1";

for $f in fn:doc("mozi.xml")//film
for $r in fn:doc("rendezok.xml")//rendezo
where $r/@id = $f/rendezo/text()
return
    element {"rendezte"} {
        attribute {"cim"} {$f/cim},
        text {$r/nev/text()}
    }