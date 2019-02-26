using System;
using Xunit;
using ShoppingListWebApi.Controllers;

namespace ShoppingListWebApiTests
{
    public class ShoppingListControllerTests
    {
        [Fact]
        public void GetAllShoppingListsFromDatabase_NoShoppingLists_EmptyListOfShoppingLists()
        {
            //Arrange
            var sut = new ShoppingListController();

            //Act
            var result = sut.GetAllShoppingListsFromDatabase();

            //Assert
            Assert.Empty(result);

        }

        [Fact]
        public void GetAllShoppingListsFromDatabase_OneShoppingList_ListContainingOneShoppingList()
        {
            //Arrange
            var sut = new ShoppingListController();

            //Act
            var result = sut.GetAllShoppingListsFromDatabase();

            //Assert
            Assert.Equal(1, result.Count);

        }

    }
}
