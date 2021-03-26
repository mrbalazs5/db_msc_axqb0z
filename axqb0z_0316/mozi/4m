xquery version "3.1";

let $doc := fn:doc("mozi.xml")
for $k in distinct-values($doc//film/kategoria)
return
    element {"eredmény"} {
        attribute {"kategória"} {$k},
        text {count($doc//film[kategoria eq $k])}
    }