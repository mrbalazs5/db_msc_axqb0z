<Query Kind="Program">
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

void Main()
{
	while(true) {
		Console.WriteLine();
		Console.WriteLine("--Menu---");
		Console.WriteLine("Add new customer (1)");
		Console.WriteLine("Add new product (2)");
		Console.WriteLine("Create new order (3)");
		Console.WriteLine("Delete customer (4)");
		Console.WriteLine("Delete product (5)");
		Console.WriteLine("List customers (6)");
		Console.WriteLine("List products (7)");
		Console.WriteLine("List orders (8)");
		Console.WriteLine("Search in products (9)");
		Console.WriteLine("Find products by customer (10)");
		Console.WriteLine("Quit (11)");
		Console.WriteLine();
		
		string menuInput = Util.ReadLine("What would you like to do?");
		
		switch(menuInput) {
			case "1" :
				addNewCustomer();
				break;
			case "2":
				addNewProduct();
				break;
			case "3":
				addNewOrder();
				break;
			case "4":
				deleteCustomer();
				break;
			case "5":
				deleteProduct();
				break;
			case "6":
				listCustomers();
				break;
			case "7":
				listProducts();
				break;
			case "8":
				listOrders();
				break;
			case "9":
				searchInProducts();
				break;
			case "10":
				findProductsByCustomer();
				break;
			case "11":
				Console.WriteLine("Bye.");
				return;
			default:
				Console.WriteLine("Invalid command.");
				break;
		}
	}
}

void addNewCustomer() {
	try {
		Console.WriteLine("Add new customer.");
		
		int id = int.Parse(Util.ReadLine("Id:"));
		string fullName = Util.ReadLine("FullName:");
		int age = int.Parse(Util.ReadLine("Age:"));
		
		Customers customer = new Customers { Id = id, FullName = fullName, Age = age };
	
		Customers.InsertOnSubmit(customer);
		SubmitChanges();
		
		Console.WriteLine("Customer successfully created.");
	} catch(Exception e) {
		Console.WriteLine("Failed to create customer: " + e.Message);
	}
}

void addNewProduct() {
	try {
		Console.WriteLine("Add new product.");
	
		int id = int.Parse(Util.ReadLine("Id:"));
		string productName = Util.ReadLine("ProductName:");
		string category = Util.ReadLine("Category:");
		int price = int.Parse(Util.ReadLine("Price:"));
		
		Products product = new Products {
			Id = id,
			ProductName = productName,
			Category = category,
			Price = price 
		};
		
		Products.InsertOnSubmit(product);
		SubmitChanges();
		
		Console.WriteLine("Product successfully created.");
	} catch(Exception e) {
		Console.WriteLine("Failed to create product: " + e.Message);
	}

}

void addNewOrder() {
	try {
		Console.WriteLine("Create new order.");
	
		int orderId = int.Parse(Util.ReadLine("OrderId:"));
		int customerId = int.Parse(Util.ReadLine("CustomerId:"));
		string productIdsString = Util.ReadLine("ProductIds(separated by comma):");
		string[] separatedProductIdsString = productIdsString.Split(',');
		int[] productIds = Array.ConvertAll(separatedProductIdsString, s => int.Parse(s));
		
		Orders order = new Orders { Id = orderId, Customer = customerId };
		Orders.InsertOnSubmit(order);
		
		foreach (int productId in productIds) {
			Orderproducts orderProduct = new Orderproducts {OrderId = orderId, ProductId = productId};
			Orderproducts.InsertOnSubmit(orderProduct);
		}
		
		SubmitChanges();
		
		Console.WriteLine("Order successfully created.");
	} catch(Exception e) {
		Console.WriteLine("Failed to create order: " + e.Message);
	}
}

void deleteCustomer() {
	int customerId = int.Parse(Util.ReadLine("CustomerId:"));
	
	var customer = Customers.Where(c => c.Id == customerId).First();
	
	Customers.DeleteOnSubmit(customer);
	
	SubmitChanges();
	
	Console.WriteLine("Customer successfully removed.");
}

void deleteProduct() {
	int productId = int.Parse(Util.ReadLine("ProductId:"));
	
	var product = Products.Where(p => p.Id == productId).First();
	
	Products.DeleteOnSubmit(product);
	
	SubmitChanges();
	
	Console.WriteLine("Product successfully removed.");
}

void listCustomers() {
	var queryResults = Customers.Select(c => new {fullName = c.FullName, age = c.Age});
	
	Console.WriteLine("Customers:");
	
	queryResults.Dump();
}

void listProducts() {
	var queryResults = Products.Select(p => new {
		productName = p.ProductName,
		category = p.Category,
		price = p.Price
	});
	
	Console.WriteLine("Products:");
	
	queryResults.Dump();
}

void listOrders() {
	var queryResults = 
		Orders
			.Join(Customers, o => o.Customer, c => c.Id, (o, c) => new {o, c})
			.Join(Orderproducts, oc => oc.o.Id, op => op.OrderId, (oc, op) => new {oc, op})
			.Join(
				Products,
				ocop => ocop.op.ProductId,
				p => p.Id,
				(ocop, p) => new {ocop, p}
			)
			.GroupBy(result => result.ocop.oc.o.Id)
			.Select(
				groups => new {
					orderId = groups.Key,
					customer = groups.Select(g => g.ocop.oc.c.FullName).First(),
					products = groups.Select(g => g.p.ProductName)
				}
			);
	
	Console.WriteLine("Orders:");
	
	foreach(var queryResult in queryResults) {
		Console.WriteLine($"orderId: {queryResult.orderId}");
		
		Console.WriteLine($"customer: {queryResult.customer}");
		
		Console.WriteLine("products:");
		
		foreach(string product in queryResult.products) {
			Console.WriteLine(product);
		}
		
		Console.WriteLine();
	}
}

void searchInProducts() {
	string searchString = Util.ReadLine("Search in products:");
	
	var queryResults = Products.Where(
		p => p.Category.Contains(searchString) || p.ProductName.Contains(searchString)
	);
	
	queryResults.Dump();
}

void findProductsByCustomer() {
	int customerId = int.Parse(Util.ReadLine("CustomerId:"));
	
	var queryResults = Customers
			.Where(c => c.Id == customerId)
			.Join(Orders, c => c.Id, o => o.Customer, (c, o) => new {c, o})
			.Join(Orderproducts, oc => oc.o.Id, op => op.OrderId, (oc, op) => new {oc, op})
			.Join(Products, ocop => ocop.op.ProductId, p => p.Id, (ocop, p) => new {ocop, p})
			.GroupBy(result => result.ocop.oc.c.FullName)
			.Select(groups => new {
				fullName = groups.Key,
				products = groups.Select(g => new {
					orderId = g.ocop.oc.o.Id,
					productId = g.p.Id,
					productName = g.p.ProductName
				})
			});
			
	queryResults.Dump();
}