using System;
using Xunit;
using ShoppingListWebApi.Controllers;
using ShoppingListWebApiTests.Mocks;
using System.Collections.Generic;
using ShoppingListWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace ShoppingListWebApiTests
{
    public class ShoppingListControllerTests
    {
        [Fact]
        public void GetAllShoppingLists_NoShoppingLists_EmptyListOfShoppingLists()
        {
            //Arrange
            var repoMock = new RepositoryMock()
            {
                MockShoppingListResult = new List<ShoppingList>()
            };
            var sut = new ShoppingListController(repoMock);

            //Act
            var result = sut.GetAllShoppingLists();

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetAllShoppingLists_OneShoppingListInRepo_ListContainingOneShoppingList()
        {
            //Arrange
            var repoMock = new RepositoryMock()
            {
                MockShoppingListResult = new List<ShoppingList>()
                {
                    new ShoppingList()
                    {
                        ShoppingListId = 1,
                        BudgetSum = 100,
                        Name ="Test list1"
                    }
                }
            };
            var sut = new ShoppingListController(repoMock);

            //Act
            var result = sut.GetAllShoppingLists();

            //Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetAllShoppingLists_ManyShoppingListsInRepo_ListContainingManyShoppingLists()
        {
            //Arrange
            var repoMock = new RepositoryMock()
            {
                MockShoppingListResult = new List<ShoppingList>()
                {
                    new ShoppingList()
                    {
                        ShoppingListId = 1,
                        BudgetSum = 100,
                        Name ="Test list1"
                    },
                    new ShoppingList()
                    {
                        ShoppingListId = 2,
                        BudgetSum = 120,
                        Name ="Test list2"
                    }
                }
            };
            var sut = new ShoppingListController(repoMock);

            //Act
            var result = sut.GetAllShoppingLists();

            //Assert
            Assert.True(result.Count > 1);
        }

        [Fact]
        public void InsertShoppingList_InputShoppingListIsNull_StatusCode500IsReturned()
        {
            //Arrange
            var repoMock = new RepositoryMock()
            {
                MockShoppingListResult = new List<ShoppingList>()
            };
            var sut = new ShoppingListController(repoMock);

            //Act 
            var result = sut.InsertShoppingList(null);

            //Assert
            var objectResult = result as ObjectResult;
            Assert.Equal(500, objectResult.StatusCode);
        }

        [Fact]
        public void InsertShoppingList_OneShoppingList_StatusCode200IsReturned()
        {
            //Arrange
            var repoMock = new RepositoryMock()
            {
                MockShoppingListResult = new List<ShoppingList>()
                {
                    new ShoppingList()
                    {
                        ShoppingListId = 21,
                        Name = "TestShoppingList",
                        BudgetSum = 280
                    }
                }
            };
            var sut = new ShoppingListController(repoMock);

            //Act
            var result = sut.InsertShoppingList(repoMock.MockShoppingListResult[0]);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetDetailedInformationOfSpecificShoppingList_NoShoppingList_EmptyListOfShoppingList()
        {
            //Arrange
            var repoMock = new RepositoryMock()
            {
                MockShoppingListResult = new List<ShoppingList>()
            };
            var sut = new ShoppingListController(repoMock);

            //Act
            var result = sut.GetDetailedInformationOfSpecificShoppingList(5);

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetDetailedInformationOfSpecificShoppingList_OneShoppingList_ListContainingOneShoppingList()
        {
            //Arrange
            var repoMock = new RepositoryMock()
            {
                MockShoppingListResult = new List<ShoppingList>()
                {
                    new ShoppingList()
                    {
                        ShoppingListId = 5,
                        BudgetSum = 100,
                        Name ="Test list1"
                    }
                }
            };
            var sut = new ShoppingListController(repoMock);

            //Act
            var result = sut.GetDetailedInformationOfSpecificShoppingList(repoMock.MockShoppingListResult[0].ShoppingListId);

            //Assert
            Assert.Single(result);
        }
    }
}
