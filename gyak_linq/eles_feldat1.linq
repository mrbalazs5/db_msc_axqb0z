<Query Kind="Statements">
  <Connection>
    <ID>5b2023dc-3da1-49ed-aa21-b6cb9c181c68</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.MySql</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAiXUPYEs5bUSxj/wEQfzHggAAAAACAAAAAAAQZgAAAAEAACAAAADjECAIHEt63J7nG3Qzpy7pf7LYDuqi5yTGZZCjesrDpwAAAAAOgAAAAAIAACAAAADuptpIz2jeJtJrDMxYHLorqS5QbZei0cOKED9zqVUeiFAAAACcyfCmUsm1kMyXPaDOGbuBKy7pveFuRZztSox1uUAFbCR7tY3O9Z6Gvr1XABgjXkmxt88ngOwqPwhHAJURQzDEXL9Gji3CzD1+Gj14NWQE00AAAACpNVtpvg9PlN6F4S8IOIncWhaAjYeNzqls3H5r2sOU1TUUnEtmqRJq8HuP/JY7N6KXZsUZGsXJTw9UfjDjCUeQ</CustomCxString>
    <Server>localhost</Server>
    <Database>db_msc_gyak</Database>
    <UserName>root</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAiXUPYEs5bUSxj/wEQfzHggAAAAACAAAAAAAQZgAAAAEAACAAAAAAO4EFJWFophhrkxnuI3jNVBgCef3Mqs1VELTDYYs4KQAAAAAOgAAAAAIAACAAAABwBk4dAPnVGGjJyLfteyyPFPMutppB/7vuExeoBs5G6BAAAACNduQYUYsbXkzgb/6QIIsaQAAAALuDMqndnMJHI0wMyyGRoyjK+BMTyNgN1fbfxcaI4YbncwARmNlIYJ96kB01JMjWYzTS5ZPXQQLXHafO/JAU2ao=</Password>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <DriverData>
      <StripUnderscores>false</StripUnderscores>
      <QuietenAllCaps>false</QuietenAllCaps>
      <ExtraCxOptions>charset=utf8;</ExtraCxOptions>
    </DriverData>
  </Connection>
</Query>

//1. feladat
var q1 = Customers.Select(c => new { name = c.FullName, age = c.Age }).OrderBy(c => c.age);
q1.Dump();

//2. feladat
var q2 = Customers.Where(c => c.Age > 28).OrderByDescending(c => c.FullName);
q2.Dump();

//3. feladat
var q3 = Products.Where(p => p.Category.Contains("ház"));
q3.Dump();

//4. feladat
var maxPrice = Products.Max(p => p.Price);
var q4 = Products
			.Where(p => p.Price == maxPrice)
			.Select(p => new { productName = p.ProductName, price = p.Price });
q4.Dump();

//5. feladat
var avgPrice = Math.Round((double) Products.Average(p => p.Price));
avgPrice.Dump();
var q5 = Products.Where(p => p.Price < avgPrice);
q5.Dump();

//6. feladat
var q6 = Products.Where(p => p.Category == "divat").Count();
q6.Dump();

//7. feladat
var q7 = Orders
			.Join(Orderproducts, o => o.Id, op => op.OrderId, (o, op) => new {o, op})
			.Join(Products, o_op => o_op.op.ProductId, p => p.Id, (o_op, p) => new {o_op, p})
			.GroupBy(result => result.o_op.o.Id)
			.Select(
			groups => new {
				orderId = groups.Key,
				sum = groups.Select(g => g.p.Price).Aggregate((p1, p2) => (p1 + p2))
			});
q7.Dump();

//8. feladat
var q8 = Customers
			.Join(Orders, c => c.Id, o => o.Customer, (c, o) => new {c, o})
			.Join(Orderproducts, co => co.o.Id, op => op.OrderId, (co, op) => new {co, op})
			.Join(Products, coop => coop.op.ProductId, p => p.Id, (coop, p) => new {coop, p})
			.GroupBy(result => result.coop.co.c.FullName)
			.Select(groups => new { fullName = groups.Key, productCount = groups.Count()});
q8.Dump();
