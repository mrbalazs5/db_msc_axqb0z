xquery version "3.1";

for $f in fn:doc("mozi.xml")//film
where $f/imbd = max(//imbd)
return $f