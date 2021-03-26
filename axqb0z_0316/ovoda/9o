xquery version "3.1";

declare namespace fv ="f1";
declare function fv:eletkor ($s) as xs:integer{
    let $e := fn:year-from-date(fn:current-date()) - $s cast as xs:integer
    return $e
};

<eredmeny>
{
    let $doc := fn:doc("ovoda.xml")
    for $gy in $doc//gyerek
    return
        element {"gyerek"} {
        attribute {"jel"} {$gy/@jel},
        attribute {"név"} {$gy/nev},
        text {fv:eletkor(number($gy/szuletesiev))}
    }
}
</eredmeny>