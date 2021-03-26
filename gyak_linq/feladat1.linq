<Query Kind="Statements">
  <Connection>
    <ID>f07ee4ca-0d48-4b37-9859-54e5d9c7cac1</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.MySql</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAiXUPYEs5bUSxj/wEQfzHggAAAAACAAAAAAAQZgAAAAEAACAAAADEOyyaKXcO9cp3AgX+JujiuxNVcBZW9exESl6MuAh3ZAAAAAAOgAAAAAIAACAAAABnyFW0pNowoEpvYrKeSKqYBpndO5CI0TNIaycb5qnpqlAAAACEBSItwZSQLnPm02NlGMFzDE6H6WNEA59nEhsrZeBn0i5pVWG86txbBUmecpqb6Pp5dsaS+k1tcXHzJpz2EKg6/PkZk6Q25pq+LYhGrfLTfUAAAAD0TCVkwJLGfa7NJ4H6QOoHHrDtqv7l85PaDT0rmB6E2VyJZD7N0NKfC1naCXZpHvX/tQ1wFtXvG40ZRGOiDiFM</CustomCxString>
    <Server>localhost</Server>
    <Database>db_msc_gyak</Database>
    <UserName>root</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAiXUPYEs5bUSxj/wEQfzHggAAAAACAAAAAAAQZgAAAAEAACAAAADuixV68AhOlG8uEk9FINl+PEhvlLuiwgh8KO4XqOtdTAAAAAAOgAAAAAIAACAAAADvHivHlcyt2obr7YcNmwQ1Emo6KQOvaaXNPNOjIOExfxAAAAB1SofhOG5+om5PieCyOGUEQAAAAPWKUmVVQQMVINyE5qM9hjN8pniUCXvY5EvQRqWWt5t5S9yzOj0gPRDIiMgSVZD7ajAsoXlzpYnlX8tlwyzbNTE=</Password>
    <DisplayName>db_msc</DisplayName>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <DriverData>
      <StripUnderscores>false</StripUnderscores>
      <QuietenAllCaps>false</QuietenAllCaps>
      <ExtraCxOptions>charset=utf8;</ExtraCxOptions>
    </DriverData>
  </Connection>
</Query>

//1. feladat Írassuk ki az összes vevő nevét és életkorát életkor szerinti növekvő sorrendben!
var q1 = Customers.Select(c => new {name = c.FullName, age = c.Age}).OrderBy(c => c.age);
q1.Dump();

//2. feladat Írassuk ki a 28 évnél idősebb vevőket név szerint csökkenő sorrendben!
var q2 = Customers.Where(c => c.Age > 28).OrderByDescending(c => c.FullName);
q2.Dump();

//3.feladat Írassuk ki azokat a termékeket, melyek kategóriája tartalmazza a "ház" szót!
var q3 = Products.Where(p => p.Category.Contains("ház"));
q3.Dump();

//4. feladat Keressük meg a legdrágább termékeket és írassuk ki a termékek nevét és árát!
var maxValue = Products.Max(p => p.Price);
var q4 = Products
			.Where(p => p.Price == maxValue)
			.Select( p => new {
				productName = p.ProductName, price = p.Price
			});
q4.Dump();

//5. feladat Mely termékek ára alacsonyabb a termékek átlag áránál?
var avgPrice = Math.Round((double) Products.Average(p => p.Price));
avgPrice.Dump();

var q5 = Products
			.Where(p => p.Price < avgPrice)
			.Select( p => new {
				productName = p.ProductName, price = p.Price
			});
q5.Dump();

//6. feladat Keressük meg hány "divat" kategóriájú termék van!
var q6 = Products.Where(p => p.Category == "divat").Count();
q6.Dump();

//7. feladat Írassuk ki minden rendeléshez a hozzá tartozó termékek árának összegét!
var q7 = Orders
			.Join(Orderproducts, o => o.Id, op => op.OrderId, (o, op) => new {o, op})
			.Join(Products, oop => oop.op.ProductId, p => p.Id, (oop, p) => new {oop, p})
			.GroupBy(result => result.oop.o.Id)
			.Select(
				groups => new {
					orderId = groups.Key,
					count = groups.Select(g => g.p.Price).Aggregate((p1, p2) => (p1 + p2))
				}
			);
q7.Dump();

//8. feladat Írassuk ki, hogy ki hány darab terméket rendelt!
var q8 = Customers
			.Join(Orders, c => c.Id, o => o.Customer, (c, o) => new {c, o})
			.Join(Orderproducts, oc => oc.o.Id, op => op.OrderId, (oc, op) => new {oc, op})
			.Join(Products, ocop => ocop.op.ProductId, p => p.Id, (ocop, p) => new {ocop, p})
			.GroupBy(result => result.ocop.oc.c.FullName)
			.Select(groups => new {fullName = groups.Key, count = groups.Count()});
q8.Dump();