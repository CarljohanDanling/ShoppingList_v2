using System;
using System.Collections.Generic;
using ShoppingListWebApiTests.Mocks;
using ShoppingListWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using ShoppingListWebApi.Controllers;
using Xunit;

namespace ShoppingListWebApiTests
{
    public class ItemControllerTests
    {
        [Fact]
        public void InsertItemToDatabase_InputItemIsNull_StatusCod500IsReturned()
        {
            //Arrange
            var repoMock = new RepositoryMock()
            {
                MockItemResult = new List<Item>()
            };
            var sut = new ItemController(repoMock);

            //Act
            var result = sut.InsertItem(5, null);

            //Assert
            var objectResult = result as ObjectResult;
            Assert.Equal(500, objectResult.StatusCode);
        }

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
            var result = sut.InsertItem(repoMock.MockItemResult[0].ShoppingListId, repoMock.MockItemResult[0]);

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
            int shoppingListId = 2;
            var result = sut.GetAllItemsRelatedToSpecificShoppingList(shoppingListId);

            //Assert
            Assert.Empty(result);
        }
    }
}
