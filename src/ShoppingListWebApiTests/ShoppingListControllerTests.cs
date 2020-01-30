using Microsoft.EntityFrameworkCore;
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
                var sut = new Repository(context);

                // Act
                var result = await sut.GetAllShoppingListsAsync();

                // Assert
                Assert.Empty(result);
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
                var sut = new Repository(context);

                // Act
                var result = await sut.GetAllShoppingListsAsync();

                // Assert
                Assert.Single(result);
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
                var sut = new Repository(context);

                // Act
                var result = await sut.GetAllShoppingListsAsync();

                // Assert
                Assert.True(result.Count > 1);
            }
        }
    }
}
