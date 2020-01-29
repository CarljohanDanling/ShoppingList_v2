using System;
using ShoppingListWebApi.Controllers;
using ShoppingListWebApiTests.Mocks;
using System.Collections.Generic;
using ShoppingListWebApi.Model;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace ShoppingListWebApiTests
{
    public class ShoppingListControllerTests
    {
        private DbContextOptions<ShoppingListContext> ReturnDbContextOptions(string databaseString)
        {
            return new DbContextOptionsBuilder<ShoppingListContext>()
                .UseInMemoryDatabase(databaseName: databaseString)
                .Options;
        }

        [Fact]
        public void GetAllShoppingLists_NoShoppingLists_EmptyListOfShoppingLists()
        {
            var options = ReturnDbContextOptions("GetAllShoppingLists_NoShoppingLists_EmptyListOfShoppingLists");

            // Arrange
            using (var context = new ShoppingListContext(options))
            {
                var sut = new ShoppingListController(context);

                // Act
                var result = sut.GetAllShoppingLists();

                // Assert
                Assert.Empty(result);
            }
        }

        [Fact]
        public void GetAllShoppingLists_OneShoppingList_ListContainingOneShoppingList()
        {
            var options = ReturnDbContextOptions("GetAllShoppingLists_OneShoppingList_ListContainingOneShoppingList");

            // Arrange
            using (var context = new ShoppingListContext(options))
            {
                context.ShoppingList.Add(new ShoppingList { Name = "Test", BudgetSum = 20 });
                context.SaveChanges();
            }

            using (var context = new ShoppingListContext(options))
            {
                var sut = new ShoppingListController(context);

                // Act
                var result = sut.GetAllShoppingLists();

                // Assert 
                Assert.Single(result);
            }
        }

        [Fact]
        public void GetAllShoppingLists_ManyShoppingLists_ListContainingManyShoppingLists()
        {
            var options = ReturnDbContextOptions("GetAllShoppingLists_ManyShoppingLists_ListContainingManyShoppingLists");

            // Arrange
            using (var context = new ShoppingListContext(options))
            {
                context.ShoppingList.Add(new ShoppingList { Name = "Test", BudgetSum = 20 });
                context.ShoppingList.Add(new ShoppingList { Name = "Test2", BudgetSum = 50 });
                context.SaveChanges();
            }

            using (var context = new ShoppingListContext(options))
            {
                var sut = new ShoppingListController(context);

                // Act
                var result = sut.GetAllShoppingLists();

                // Assert 
                Assert.True(result.Count > 1);
            }
        }

        [Fact]
        public void InsertShoppingList_InputShoppingListIsNull_StatusCode500IsReturned()
        {
            var options = ReturnDbContextOptions("InsertShoppingList_InputShoppingListIsNull_StatusCode500IsReturned");

            // Arrange
            using (var context = new ShoppingListContext(options))
            {
                var sut = new ShoppingListController(context);

                // Act
                var result = sut.InsertShoppingList(null);

                //Assert
                var objectResult = result as ObjectResult;
                Assert.Equal(500, objectResult.StatusCode);
            }
        }

        [Fact]
        public void InsertShoppingList_OneShoppingList_StatusCode200IsReturned()
        {
            var options = ReturnDbContextOptions("InsertShoppingList_OneShoppingList_StatusCode200IsReturned");

            // Arrange
            using (var context = new ShoppingListContext(options))
            {
                var sut = new ShoppingListController(context);

                // Act
                var result = sut.InsertShoppingList(new ShoppingList { Name = "test", BudgetSum = 50 });

                //Assert
                var objectResult = result as ObjectResult;
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public void GetDetailedInformationOfSpecificShoppingList_NoShoppingList_EmptyListOfShoppingList()
        {
            var options = ReturnDbContextOptions("GetDetailedInformationOfSpecificShoppingList_NoShoppingList_EmptyListOfShoppingList");

            // Arrange
            using (var context = new ShoppingListContext(options))
            {
                var sut = new ShoppingListController(context);

                // Act
                var result = sut.GetShoppingListWithRelatedItems(5);

                // Assert
                Assert.Empty(result);
            }
        }

        [Fact]
        public void GetDetailedInformationOfSpecificShoppingList_OneShoppingList_ListContainingOneShoppingList()
        {
            var options = ReturnDbContextOptions("GetDetailedInformationOfSpecificShoppingList_OneShoppingList_ListContainingOneShoppingList");

            // Arrange
            using (var context = new ShoppingListContext(options))
            {
                context.ShoppingList.Add(new ShoppingList { Name = "Test", BudgetSum = 20, ShoppingListId = 5});
                context.Item.Add(new Item {
                    Name = "föremål",
                    Price = 20,
                    Quantity = 2,
                    ShoppingListId = 5 });
                context.SaveChanges();
            }

            using (var context = new ShoppingListContext(options))
            {
                var sut = new ShoppingListController(context);

                // Act
                var result = sut.GetShoppingListWithRelatedItems(5);

                // Assert
                Assert.Single(result);
            }
        }

        [Fact]
        public void UpdateShoppingList_NoUpdatedShoppingList_ReturnShoppingListsAreEqual()
        {
            var options = ReturnDbContextOptions("UpdateShoppingList_NoUpdatedShoppingList_ReturnShoppingListsAreEqual");

            // Arrange
            var shoppingList = new ShoppingList()
            {
                Name = "Test",
                BudgetSum = 20,
                ShoppingListId = 5
            };

            using (var context = new ShoppingListContext(options))
            {
                context.ShoppingList.Add(shoppingList);
                context.SaveChanges();
            }

            using (var context = new ShoppingListContext(options))
            {
                context.Update(shoppingList);
                context.SaveChanges();
            }

            using (var context = new ShoppingListContext(options))
            {
                var sut = new ShoppingListController(context);

                // Act
                var result = sut.UpdateShoppingList(5, new ShoppingList { Name = "Test", BudgetSum = 20, ShoppingListId = 5 });

                // Assert
                Assert.Equals(shoppingList, result);
            }
        }
    }
}
