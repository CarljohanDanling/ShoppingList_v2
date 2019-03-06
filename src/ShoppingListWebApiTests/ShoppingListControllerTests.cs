using System;
using Xunit;
using ShoppingListWebApi.Controllers;
using ShoppingListWebApi.Database;
using ShoppingListWebApiTests.Mocks;
using System.Collections.Generic;
using ShoppingListWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingListWebApiTests
{
    public class ShoppingListControllerTests
    {
        [Fact]
        public void GetAllShoppingListsFromDatabase_NoShoppingLists_EmptyListOfShoppingLists()
        {
            //Arrange
            var repoMock = new RepositoryMock()
            {
                MockShoppingListResult = new List<ShoppingList>()
            };
            var sut = new ShoppingListController(repoMock);

            //Act
            var result = sut.GetAllShoppingListsFromDatabase();

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllShoppingListsFromDatabase_OneShoppingList_ListContainingOneShoppingList()
        {
            //Arrange
            var repoMock = new RepositoryMock()
            {
                MockShoppingListResult = new List<ShoppingList>()
                {
                    new ShoppingList()
                    {
                        ID = 1,
                        BudgetSum = 100,
                        Name ="Test list1"
                    }
                }
            };
            var sut = new ShoppingListController(repoMock);

            //Act
            var result = sut.GetAllShoppingListsFromDatabase();

            //Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetAllShoppingListsFromDatabase_ManyShoppingLists_ListContainingManyShoppingLists()
        {
            //Arrange
            var repoMock = new RepositoryMock()
            {
                MockShoppingListResult = new List<ShoppingList>()
                {
                    new ShoppingList()
                    {
                        ID = 1,
                        BudgetSum = 100,
                        Name ="Test list1"
                    },
                    new ShoppingList()
                    {
                        ID = 2,
                        BudgetSum = 120,
                        Name ="Test list2"
                    }
                }
            };
            var sut = new ShoppingListController(repoMock);

            //Act
            var result = sut.GetAllShoppingListsFromDatabase();

            //Assert
            Assert.True(result.Count > 1);
        }

        [Fact]
        public void InsertShoppingListToDatabase_NoShoppingList_ExceptionArgumentOutOfRange()
        {
            //Arrange
            var repoMock = new RepositoryMock()
            {
                MockShoppingListResult = new List<ShoppingList>()
            };
            var sut = new ShoppingListController(repoMock);

            //Act
            Action act = () => sut.InsertShoppingListToDatabase(repoMock.MockShoppingListResult[0]);

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public void InsertShoppingListToDatabase_OneShoppingList_OneShoppingListAdded()
        {
            //Arrange
            var repoMock = new RepositoryMock()
            {
                MockShoppingListResult = new List<ShoppingList>()
                {
                    new ShoppingList()
                    {
                        ID = 21,
                        Name = "TestShoppingList",
                        BudgetSum = 280
                    }
                }
            };
            var sut = new ShoppingListController(repoMock);

            //Act
            var result = sut.InsertShoppingListToDatabase(repoMock.MockShoppingListResult[0]);
            var okResult = result as OkObjectResult;

            //Assert
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
