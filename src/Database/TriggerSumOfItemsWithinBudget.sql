CREATE TRIGGER CheckSumOfItemsWithinBudget
   ON Item
   AFTER INSERT, UPDATE
AS 
BEGIN
    -- Prevents unnecessary "(x rows affected)" output from queries in trigger.
    SET NOCOUNT ON;
	
	-- Integrity check
	IF EXISTS (
		SELECT BudgetSum, SUM(Price * Quantity)
        FROM Item
        JOIN ShoppingList ON Item.ShoppingListId = ShoppingList.ShoppingListId
		GROUP BY Item.ShoppingListId, BudgetSum
		HAVING SUM(Price * Quantity) > BudgetSum
    )
    BEGIN
        -- Throwing an exception with an error number, message, and state.
        -- For error number and state, we just use 50000 and 1 by default.
        THROW 50000, 'Budget of shopping list must be > the items price * quantity', 1;
    END
END