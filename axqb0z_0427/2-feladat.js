//2. feladat

//a)
db.artists.insert({
    "name": "The Butterfield Blues Band",
    "country": "USA"
})

db.artists.insertMany(
    [
        {
            "name": "The Doors",
            "country": "USA"
        },
        {
            "name": "Black Sabbath",
            "country": "UK"
        },
        {
            "name": "Iron Butterfly",
            "country": "USA"
        },
        {
            "name": "Iron Maiden",
            "country": "UK"
        },
        {
            "name": "Soul Media",
            "country": "Japan"
        }
    ]
)

//b) Írjon egy olyan szerveroldali függvényt, amely elvégzi a következő módosításokat az albumok kollekcióján:
//- az előadó neve helyére az albumhoz tartozó előadó _id-jét redeli.
//- az ország mezőket törli, hiszen az előadó adatai között már szerepel az.
db.system.js.save(
    {
        _id: "hozzaRendel",
        value: function () {
            db.artists.find().forEach(function (x) {
                db.albums.updateMany({ artist: x.name }, {
                    $unset: { country: "" },
                    $set: { artist: x._id }
                }
                )
            });
        }
    }
)
db.loadServerScripts()
hozzaRendel()


//c)Írjon egy olyan szerveroldali függvényt, amely segítségével egy kiválaszott műfaj százalékos árcsökkentését elvégezhetjük.
//Az eredményt egy új kollekcióba mentse, ne írja felül az eredeti adatokat!
db.system.js.save(
    {
        _id: "akciozas",
        value: function (genre, percentage) {
            var query = db.albums.find({ "genre": genre });
            while (query.hasNext()) {
                var album = query.next();
                album.price = album.price * (1 - percentage / 100);
                db.akcio.insert(album);
            }
        }
    }
)
db.loadServerScripts()
akciozas("Rock", 20)


//d) Írjon egy olyan függvényt, amely lekérdezi az átlagárnál drágább albumok címeit
db.system.js.save(
    {
        _id: "atlagar",
        value: function () {
            var atlag = db.albums.aggregate([
                { $group: { _id: "null", aprice: { $avg: "$price" } } }
            ])._batch[0].aprice;

            var query = db.albums.find({ price: { $gt: atlag } });

            while (query.hasNext()) {
                print(query.next())
            }
        }
    }
)
db.loadServerScripts()
atlagar()


//e) Hajtson végre aggregációs műveleteket

//Számítsa ki az egyes műfajokhoz tartozó átlagárat forintban (1 USD 300 magyar forintnak felel meg)
db.albums.aggregate(
    [
        {
            $group: { _id: "$genre", "avgPrice(HUF)": { $avg: { $multiply: ["$price", 300] } } }
        }
    ]
)

//Számolja meg, hogy az egyes műfajokban hány 20$-nál drágább album van és rendezze darabszám szerinti csökkenő sorrendbe
db.albums.aggregate(
    [
        {
            $match: { price: { $gt: 20 } }
        },
        {
            $group: { _id: "$genre", count: { $sum: 1 } }
        },
        {
            $sort: { "count": -1 }
        }
    ]
)

//Kérdezze le a legolcsóbb albumot az előadó adataival kiegészítve
db.albums.aggregate([
    {
        $sort: { price: 1 }
    },
    {
        $limit: 1
    },
    {
        $lookup:
        {
            from: "artists",
            localField: "artist",
            foreignField: "_id",
            as: "artist"
        }
    }
])

//Keresse meg, hogy mely előadóhoz tartozik a legtöbb album, majd írassa ki az előadó nevét és a hozzá tartozó album címek listáját, valamint a legolcsóbb lemezének az árát.
db.artists.aggregate([
    {
        $lookup:
        {
            from: "albums",
            localField: "_id",
            foreignField: "artist",
            as: "albums"
        }
    },
    {
        $project: {
            _id: 0,
            name: 1,
            albums: {
                title: 1
            },
            cheapestPrice: { $min: "$albums.price" },
            numberOfTitles: { $size: "$albums" }
        }
    },
    {
        $sort: { numberOfTitles: -1 }
    },
    {
        $limit: 1
    }
])
