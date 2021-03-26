xquery version "3.1";

declare namespace fv ="f1";
declare function fv:kor ($s) as xs:integer{
    let $e := fn:year-from-date(fn:current-date()) - $s cast as xs:integer
    return $e
};

<eredmeny>
{
    let $doc := fn:doc("mozi.xml")
    for $f in $doc//film
    return
        element {"film"} {
        attribute {"cím"} {$f/cim},
        attribute {"megjelenés"} {$f/ev},
        text {fv:kor(number($f/ev))}
    }
}
</eredmeny>