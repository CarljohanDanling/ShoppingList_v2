using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingListWebApi.Controllers;
using ShoppingListWebApi.Model;
using ShoppingListWebApi.Service;
using Xunit;

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
        public async void Should_NotReturnAnyShoppingLists()
        {
            var options = ReturnDbContextOptions("Should_NotReturnAnyShoppingLists");

            // Arrange
            using (var context = new ShoppingListContext(options))
            {
                var sut = new ShoppingListController(new Repository(context));

                // Act
                var result = await sut.GetAllShoppingListsAsync();

                // Assert
                Assert.Empty(result.Value);
            }
        }

        [Fact]
        public async void Should_ReturnOneShoppingList()
        {
            var options = ReturnDbContextOptions("Should_ReturnOneShoppingList");

            // Arrange
            using (var context = new ShoppingListContext(options))
            {
                await context.ShoppingList.AddAsync(new ShoppingList
                {
                    Name = "Test",
                    BudgetSum = 20
                });

                await context.SaveChangesAsync();
            }

            using (var context = new ShoppingListContext(options))
            {
                var sut = new ShoppingListController(new Repository(context));

                // Act
                var result = await sut.GetAllShoppingListsAsync();

                // Assert
                Assert.Single(result.Value);
            }
        }

        [Fact]
        public async void Should_ReturnMoreThanOneShoppingList()
        {
            var options = ReturnDbContextOptions("Should_ReturnMoreThanOneShoppingList");

            // Arrange
            using (var context = new ShoppingListContext(options))
            {
                await context.ShoppingList.AddRangeAsync(
                    new ShoppingList { Name = "Test1", BudgetSum = 10 },
                    new ShoppingList { Name = "Test2", BudgetSum = 15 });
                
                await context.SaveChangesAsync();
            }

            using (var context = new ShoppingListContext(options))
            {
                var sut = new ShoppingListController(new Repository(context));

                // Act
                var result = await sut.GetAllShoppingListsAsync();

                // Assert
                Assert.True(result.Value.Count > 1);
                Assert.Equal(2, result.Value.Count);
            }
        }

        [Fact]
        public async void Should_ReturnStatusCode500_When_InsertingShoppingListWithNull()
        {
            var options = ReturnDbContextOptions("Should_ReturnStatusCode500_When_InsertingShoppingListWithNull");

            // Arrange
            using (var context = new ShoppingListContext(options))
            {
                var sut = new ShoppingListController(new Repository(context));

                // Act
                var result = await sut.InsertShoppingListAsync(null);

                // Assert
                var objectResult = result as ObjectResult;
                Assert.Equal(500, objectResult.StatusCode);
            }
        }

        [Fact]
        public async void Should_ReturnStatusCode200_When_InsertingOneShoppingList()
        {
            var options = ReturnDbContextOptions("Should_ReturnStatusCode200_When_InsertingOneShoppingList");

            // Arrange
            using (var context = new ShoppingListContext(options))
            {
                var sut = new ShoppingListController(new Repository(context));

                // Act
                var result = await sut.InsertShoppingListAsync(
                    new ShoppingList { Name = "Test", BudgetSum = 50 });

                // Assert
                var objectResult = result as ObjectResult;
                Assert.IsType<OkObjectResult>(result);
            }
        }

        [Fact]
        public async void Should_ReturnNullForSpecificShoppingList()
        {
            var options = ReturnDbContextOptions("Should_ReturnNullForSpecificShoppingList");

            // Arrange
            using (var context = new ShoppingListContext(options))
            {
                var sut = new ShoppingListController(new Repository(context));

                // Act
                var result = await sut.GetShoppingListWithRelatedItemsAsync(5);

                // Assert
                Assert.Null(result.Value);
            }

        }
    }
}
