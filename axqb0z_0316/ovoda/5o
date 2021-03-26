xquery version "3.1";

let $gy := fn:doc("ovoda.xml")//csoport[@nev="maci"]
return
update insert 
    <gyerek jel="traktor">
        <nev>Benőke</nev>
        <szuletesiev>2018</szuletesiev>
    </gyerek>
into $gy