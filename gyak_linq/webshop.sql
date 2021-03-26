CREATE TABLE customers (
	id INT PRIMARY KEY NOT NULL,
    fullName VARCHAR(50),
	age INT CHECK(age > 0)
);

CREATE TABLE orders (
	id INT PRIMARY KEY NOT NULL,
    customer INT,
    FOREIGN KEY (customer) REFERENCES customers(id) ON DELETE CASCADE
);

CREATE TABLE products (
	id INT PRIMARY KEY NOT NULL,
    productName VARCHAR(50),
    category VARCHAR(50),
    price INT CHECK(price > 0)
);

CREATE TABLE orderProducts (
	orderId INT,
    FOREIGN KEY (orderId) REFERENCES orders(id) ON DELETE CASCADE,
	productId INT,
    FOREIGN KEY (productId) REFERENCES products(id) ON DELETE CASCADE
);

-- CUSTOMERS
INSERT INTO customers (id, fullName, age)
VALUES (1, 'Kiss István', 23);
INSERT INTO customers (id, fullName, age)
VALUES (2, 'Nagy Károly', 45);
INSERT INTO customers (id, fullName, age)
VALUES (3, 'Teszt Eszter', 26);
INSERT INTO customers (id, fullName, age)
VALUES (4, 'Varga Zoltán', 50);
INSERT INTO customers (id, fullName, age)
VALUES (5, 'Balogh Vivien', 30);

-- ORDERS
INSERT INTO orders (id, customer)
VALUES (1, 2);
INSERT INTO orders (id, customer)
VALUES (2, 3);
INSERT INTO orders (id, customer)
VALUES (3, 1);
INSERT INTO orders (id, customer)
VALUES (4, 5);
INSERT INTO orders (id, customer)
VALUES (5, 3);

-- PRODUCTS
INSERT INTO products (id, productName, category, price)
VALUES (1, "mosógép", "háztartási eszköz", 30000);
INSERT INTO products (id, productName, category, price)
VALUES (2, "fülhallgató", "elektronika", 4000);
INSERT INTO products (id, productName, category, price)
VALUES (3, "kenyérsütő", "háztartási eszköz", 20000);
INSERT INTO products (id, productName, category, price)
VALUES (4, "hűtő", "háztartási eszköz", 150000);
INSERT INTO products (id, productName, category, price)
VALUES (5, "sapka", "divat", 1500);
INSERT INTO products (id, productName, category, price)
VALUES (6, "sál", "divat", 2300);

-- ORDER-PRODUCTS
INSERT INTO orderProducts (orderId, productId)
VALUES (1, 2);
INSERT INTO orderProducts (orderId, productId)
VALUES (1, 3);
INSERT INTO orderProducts (orderId, productId)
VALUES (2, 1);
INSERT INTO orderProducts (orderId, productId)
VALUES (3, 5);
INSERT INTO orderProducts (orderId, productId)
VALUES (3, 2);
INSERT INTO orderProducts (orderId, productId)
VALUES (3, 6);
INSERT INTO orderProducts (orderId, productId)
VALUES (4, 4);
INSERT INTO orderProducts (orderId, productId)
VALUES (5, 1);
INSERT INTO orderProducts (orderId, productId)
VALUES (5, 2);
INSERT INTO orderProducts (orderId, productId)
VALUES (5, 3);
INSERT INTO orderProducts (orderId, productId)
VALUES (5, 5);
