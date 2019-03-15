CREATE TABLE ShoppingList (
	ShoppingListId int NOT NULL PRIMARY KEY IDENTITY (1,1),
	[Name] nvarchar(50) NOT NULL,
	BudgetSum smallint NOT NULL,
	CONSTRAINT BudgetSumMustBeGreaterThan0 CHECK (BudgetSum > 0)
	);

CREATE TABLE Item (
	ItemId int NOT NULL PRIMARY KEY IDENTITY (1,1),
	[Name] nvarchar(50) NOT NULL,
	Price decimal(6,0) NOT NULL,
	Quantity decimal(6,0) NOT NULL,
	ShoppingListId int NOT NULL FOREIGN KEY REFERENCES ShoppingList(ShoppingListId),
	CONSTRAINT CheckPriceGreaterThan0 CHECK (Price > 0),
	CONSTRAINT CheckQuantityGreaterThan0 CHECK (Quantity > 0)
);