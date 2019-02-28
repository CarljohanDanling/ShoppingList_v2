using System;
using Xunit;
using ShoppingListWebApi.Controllers;
using ShoppingListWebApi.Database;
using ShoppingListWebApiTests.Mocks;
using System.Collections.Generic;
using ShoppingListWebApi.Models;

namespace ShoppingListWebApiTests
{
    public class ShoppingListControllerTests
    {
        [Fact]
        public void GetAllShoppingListsFromDatabase_NoShoppingLists_EmptyListOfShoppingLists()
        {
            //Arrange
            var repoMock = new RespositoryMock()
            {
                MockResult = new List<ShoppingList>()
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
            var repoMock = new RespositoryMock()
            {
                MockResult = new List<ShoppingList>()
                {
                    new ShoppingList()
                    {
                        ID = 1,
                        BudgetSum = 100,
                        ShoppingListName ="Test list1"
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
            var repoMock = new RespositoryMock()
            {
                MockResult = new List<ShoppingList>()
                {
                    new ShoppingList()
                    {
                        ID = 1,
                        BudgetSum = 100,
                        ShoppingListName ="Test list1"
                    },
                    new ShoppingList()
                    {
                        ID = 2,
                        BudgetSum = 120,
                        ShoppingListName ="Test list2"
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
        public void InsertShoppingListToDatabase_NoShoppingList_NoneAddedShoppingList()
        {
            //Arrange
            var repoMock = new RespositoryMock()
            {
                MockResult = new List<ShoppingList>()
            };
            var sut = new ShoppingListController(repoMock);

            //Act
            var result = sut.GetAllShoppingListsFromDatabase();

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void InsertShoppingListToDatabase_OneShoppingList_OneShoppingListAdded()
        {
            //Arrange
            List<int> listOfShoppingListID = new List<int>();

            var repoMock = new RespositoryMock()
            {
                MockResultShoppingList = new ShoppingList()
                {
                    ID = 21,
                    BudgetSum= 520,
                    ShoppingListName = "Test list"
                }
            };
            var sut = new ShoppingListController(repoMock);

            //Act
            var result = sut.InsertShoppingListToDatabase(repoMock.MockResultShoppingList);
            listOfShoppingListID.Add(result);

            //Assert
            Assert.Single(listOfShoppingListID);
        }
    }
}
