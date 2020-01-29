using System;
using System.Collections.Generic;
using ShoppingListWebApiTests.Mocks;
using ShoppingListWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using ShoppingListWebApi.Controllers;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace ShoppingListWebApiTests
{
    public class ItemControllerTests
    {
        private DbContextOptions<ShoppingListContext> ReturnDbContextOptions(string databaseString)
        {
            return new DbContextOptionsBuilder<ShoppingListContext>()
                .UseInMemoryDatabase(databaseName: databaseString)
                .Options;
        }
        
        [Fact]
        public void InsertItemToDatabase_InputItemIsNull_StatusCode500IsReturned()
        {
            var options = ReturnDbContextOptions("InsertItemToDatabase_InputItemIsNull_StatusCode500IsReturned");

            //Arrange
            using (var context = new ShoppingListContext(options))
            {
                var sut = new ItemController(context);


            }

            //Act
            //var result = sut.InsertItem(5, null);

            //Assert
            var objectResult = result as ObjectResult;
            Assert.Equal(500, objectResult.StatusCode);
        }

        /*

        [Fact]
        public void InsertItemToDatabase_OneItem_OneItemAdded()
        {
            //Arrange
            var repoMock = new RepositoryMock()
            {
                MockItemResult = new List<Item>()
                {
                    new Item()
                    {
                        Name = "Bröd",
                        Price = 20,
                        Quantity = 2,
                        ShoppingListId = 12
                    }
                }
            };
            var sut = new ItemController(repoMock);

            //Act
            var result = sut.InsertItem(1, repoMock.MockItemResult[0]);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetAllItemsRelatedToSpecificShoppingList_NoItems_EpmtyListOfItems()
        {
            //Arrange
            var repoMock = new RepositoryMock()
            {
                MockItemResult = new List<Item>()
            };
            var sut = new ItemController(repoMock);

            //Act
            var result = sut.GetAllItemsRelatedToSpecificShoppingList(2);

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllItemsRelatedToSpecificShoppingList_OneItem_ListOfOneItem()
        {
            //Arrange
            var repoMock = new RepositoryMock()
            {
                MockItemResult = new List<Item>()
                {
                    new Item()
                    {
                        Name = "Bröd",
                        Price = 10,
                        Quantity = 1,
                    }
                }
            };
            var sut = new ItemController(repoMock);

            //Act
            var result = sut.GetAllItemsRelatedToSpecificShoppingList(1);

            //Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetAllItemsRelatedToSpecificShoppingList_ManyItems_ListOfManyItems()
        {
            //Arrange
            var repoMock = new RepositoryMock()
            {
                MockItemResult = new List<Item>()
                {
                    new Item()
                    {
                        Name = "Bröd",
                        Price = 10,
                        Quantity = 1,
                    },
                    new Item()
                    {
                        Name = "Vatten",
                        Price = 5,
                        Quantity = 2
                    }
                }
            };
            var sut = new ItemController(repoMock);

            //Act
            var result = sut.GetAllItemsRelatedToSpecificShoppingList(1);

            //Assert
            Assert.True(result.Count > 1);
        }

        
        [Fact]
        public void UpdateItem_NoChangesInItem_StatusCode204IsReturned()
        {
            // Arrange
            
            var sut = new ItemController(repoMock);

            // Behöver två olika items att jämföra mot varandra?
            // Hur gör jag bäst?

            // Act
            var result = sut.UpdateItem(5, repoMock.MockOneItemResult) as StatusCodeResult;

            // Assert
            // Assert.IsType<NoContentResult>
        }
        */
    }
}
