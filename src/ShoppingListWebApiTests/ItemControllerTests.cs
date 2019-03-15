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
        public void InsertItemToDatabase_NoItem_ExceptionArgumentOutOfRange()
        {
            //Arrange
            var repoMock = new RepositoryMock()
            {
                MockItemResult = new List<Item>()
            };
            var sut = new ItemController(repoMock);

            //Act
            Action act = () => sut.InsertItem(repoMock.MockItemResult[0].ShoppingListId, repoMock.MockItemResult[0]);

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
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
            var okResult = result as OkObjectResult;

            //Assert
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
