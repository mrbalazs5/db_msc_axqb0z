<Query Kind="Program">
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

void Main()
{
	while(true) {
		Console.WriteLine();
		Console.WriteLine("--Menu--");
		Console.WriteLine("Add new customer (1)");
		Console.WriteLine("Add new product (2)");
		Console.WriteLine("Create new order (3)");
		Console.WriteLine("Delete customer (4)");
		Console.WriteLine("Delete product (5)");
		Console.WriteLine("List customer (6)");
		Console.WriteLine("List products (7)");
		Console.WriteLine("List orders (8)");
		Console.WriteLine("Search in products (9)");
		Console.WriteLine("Find products by customer (10)");
		Console.WriteLine("Quit (11)");
		Console.WriteLine();
		
		string menuInput = Util.ReadLine("What would you like to do?");
		
		switch(menuInput) {
			case "1":
				addNewCustomer();
				break;
			case "2":
				addNewProduct();
				break;
			case "3":
				createOrder();
				break;
			case "11":
				Console.WriteLine("Bye!");
				return;
			default:
				Console.WriteLine("Invalid command!");
				break;
		}
	}
}

void addNewCustomer() {
	try {
		Console.WriteLine("Add new customer");
		
		int id = int.Parse(Util.ReadLine("CustomerId: "));
		string fullName = Util.ReadLine("FullName: ");
		int age = int.Parse(Util.ReadLine("Age: "));
		
		Customers customer = new Customers { Id = id, FullName = fullName, Age = age };
		
		Customers.InsertOnSubmit(customer);
		SubmitChanges();
		
		Console.WriteLine("Customer created successfully!");
	} catch(Exception e) {
		Console.WriteLine("Failed to create customer: " + e.Message);
	}
}

void addNewProduct() {
	try {
		Console.WriteLine("Add new product");
		
		int productId = int.Parse(Util.ReadLine("ProductId: "));
		string productName = Util.ReadLine("ProductName: ");
		string category = Util.ReadLine("Category: ");
		int price = int.Parse(Util.ReadLine("Price: "));
		
		Products product = new Products { 
			Id = productId,
			ProductName = productName,
			Category = category,
			Price = price
		};
		
		Products.InsertOnSubmit(product);
		SubmitChanges();
		
		Console.WriteLine("Product created successfully!");
	} catch(Exception e) {
		Console.WriteLine("Failed to create product: " + e.Message);
	}
}

void createOrder() {
	try {
		Console.WriteLine("Create order");
		
		int orderId = int.Parse(Util.ReadLine("OrderId: "));
		int customerId = int.Parse(Util.ReadLine("CustomerId: "));
		string productIdString = Util.ReadLine("Product ids(separated by comma): ");
		string[] separatedProductIdString = productIdString.Split(',');
		int[] productIds = Array.ConvertAll(separatedProductIdString, pIdS => int.Parse(pIdS));
		
		Orders order = new Orders { Id = orderId , Customer = customerId };
		Orders.InsertOnSubmit(order);
		
		SubmitChanges();
		
		foreach(int productId in productIds) {
			Orderproducts orderProduct = new Orderproducts {OrderId = orderId, ProductId = productId};
			Orderproducts.InsertOnSubmit(orderProduct);
		}
		
		SubmitChanges();
		
		Console.WriteLine("Order created successfully!");
	} catch(Exception e) {
		Console.WriteLine("Failed to create order: " + e.Message);
	}
}
