Drop Table OrderDetails
Drop Table Orders
Drop Table Inventory
Drop Table Products
Drop Table Customers
Drop Table Stores

Create Table Stores (
	LocationID INT NOT NULL Identity Primary Key,
	City NVARCHAR(60) NOT NULL,
	State NVARCHAR(60) NOT NULL,
	)

Create Table Customers (
	CustomerID INT NOT NULL Identity Primary Key,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL
)

Create Table Products (
	ProductID INT NOT NULL Identity Primary Key,
	Title NVARCHAR(80) NOT NULL,	
	Rating NVARCHAR(6) NOT NULL,
	Details NVARCHAR(200) NOT NULL,
	Price Money NOT NULL,
)

Create Table Inventory (
	InventoryID INT NOT NULL Identity Primary Key,
	ProductID INT NOT NULL Foreign Key References Products(ProductID),
	LocationID INT NOT NULL Foreign Key References Stores(LocationID),
	InventoryAmount INT NOT NULL
)

Create Table Orders (
	OrderID INT NOT NULL Identity Primary Key,
	CustomerID INT NOT NULL Foreign Key References Customers(CustomerID),
	LocationID INT NOT NULL Foreign Key References Stores(LocationID),
	Date Datetime2 NOT NULL
)

Create Table OrderDetails (
	OrderDetailID INT NOT NULL Identity Primary Key,
	OrderID INT NOT NULL Foreign Key References Orders(OrderID),
	InventoryID INT NOT NULL Foreign Key References Inventory(InventoryID) ON Delete Cascade
)

Insert Into Stores (City, State) Values ('Arlington', 'Texas')

Insert Into Stores (City, State) Values ('Dallas', 'Texas')

Insert Into Stores (City, State) Values ('Houston', 'Texas')


Insert Into Products (Title, Rating, Details, Price) Values 
	('The Dark Knight', 'R', 'The 2nd film in the Nolan''s Batman trilogy', $20)

Insert Into Products (Title, Rating, Details, Price) Values 
	('Grand Theft Auto', 'M', 'Explore the city and complete missions', $40)

Insert Into Products (Title, Rating, Details, Price) Values 
	('Crash Bandicoot', 'T', 'Curse you Bandicoot', $10)

Insert Into Inventory (ProductID, LocationID, InventoryAmount) Values 
	(1,1,20)

Insert Into Inventory (ProductID, LocationID, InventoryAmount) Values 
	(2,1,10)

Insert Into Inventory (ProductID, LocationID, InventoryAmount) Values 
	(3,1,15)

select o.OrderID, c.FirstName, c.LastName, p.Title, p.Price, o.Date from Customers as c
	Inner Join Orders as o ON c.CustomerID = o.CustomerID
	Inner Join OrderDetails as od ON o.OrderID = od.OrderID
	Inner Join Inventory as i ON od.InventoryID = i.InventoryID
	Inner Join Products as p ON i.ProductID = p.ProductID
	where c.FirstName = 'Jack'

select s.City, s.State, o.OrderID, c.FirstName, c.LastName, p.Title, p.Price, i.InventoryAmount, o.Date from Stores as s
	Inner Join Orders as o ON s.LocationID = o.LocationID
	inner Join Customers as c ON o.CustomerID = c.CustomerID
	Inner Join OrderDetails as od ON o.OrderID = od.OrderID
	Inner Join Inventory as i ON od.InventoryID = i.InventoryID
	Inner Join Products as p ON i.ProductID = p.ProductID


select * from Stores