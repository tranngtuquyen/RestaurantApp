using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantApp;
using RestaurantUI.Controllers;
using RestaurantUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantTest
{
    [TestClass]
    public class MenusControllerTest
    {
        [TestMethod]
        public void TestIndexWithNoLogin()
        {
            //Arrange
            var dbContext = new RestaurantContext();
            var menuManager = new MenuManager(dbContext);
            var menusController = new MenusController(menuManager);

            //Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => menusController.Index());
        }

        [TestMethod] 
        public void TestIndexWithEmptyLogin()
        {
            //Arrange
            var dbContext = new RestaurantContext();
            var menuManager = new MenuManager(dbContext);
            var menusController = new MenusController(menuManager);
            menusController.UserID = string.Empty;

            //Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => menusController.Index());
        }

        [TestMethod]
        public void TestIndexWithLogin()
        {
            //Arrange
            var dbContext = new RestaurantContext();
            var menuManager = new MenuManager(dbContext);
            var menusController = new MenusController(menuManager);
            menusController.UserID = "8774f57e-f0c7-450a-b17e-09e30668eddc";

                  //Act
            var result = menusController.Index() as ViewResult;
            var menus = (IEnumerable<Menu>)result.ViewData.Model;
            var count = 0;
            foreach (var item in menus)
            {
                count++;
            }

            //Assert
            Assert.AreEqual<int>(2, count);
        }
        
        [TestMethod] 
        public void TestDetailsWithMenu()
        {
            //Arrange
            var dbContext = new RestaurantContext();
            var menuManager = new MenuManager(dbContext);
            var menusController = new MenusController(menuManager);
            var menuID = 1;

            //Act
            var result = menusController.Details(menuID) as ViewResult;
            var viewModel = (MenuDetailsViewModel)result.ViewData.Model;
            
            var categories = viewModel.Categories;
            var categoriesSameMenu = categories.All(c => c.MenuID == menuID);
            var countCategory = categories.Count();
            
            var menuItems = viewModel.MenuItems;
            var countMenuItems = menuItems.Count();
            var itemsSameMenu = menuItems.All(i => i.MenuID == menuID);

            //Assert
            Assert.AreEqual<int>(1, countCategory);
            Assert.AreEqual<int>(1, countMenuItems);
            Assert.IsTrue(categoriesSameMenu);
            Assert.IsTrue(itemsSameMenu);
        }

        [TestMethod]
        public void TestDetailsWithNullMenuID()
        {
            //Arrange
            var dbContext = new RestaurantContext();
            var menuManager = new MenuManager(dbContext);
            var menusController = new MenusController(menuManager);
            int? menuID = null;

            //Act
            var result = menusController.Details(menuID);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TestDetailsWithNullMenu()
        {
            //Arrange
            var dbContext = new RestaurantContext();
            var menuManager = new MenuManager(dbContext);
            var menusController = new MenusController(menuManager);
            var menuID = -1;

            //Act
            var result = menusController.Details(menuID);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void TestCreateWithFullData()
        {
            //Arrange
            var dbContext = new RestaurantContext();
            var menuManager = new MenuManager(dbContext);
            var menusController = new MenusController(menuManager);
            menusController.UserID = "8774f57e-f0c7-450a-b17e-09e30668eddc";
            var menu = new Menu { Name = Guid.NewGuid().ToString(), UserID = "8774f57e-f0c7-450a-b17e-09e30668eddc" };

            //Act
            menusController.Create(menu);
            var menus = menuManager.GetAllMenus(menu.UserID);
            var newMenu = menus.SingleOrDefault(m => m.Name == menu.Name && m.UserID == menu.UserID);

            //Assert
            Assert.IsNotNull(newMenu);
            dbContext.Remove(newMenu);
            dbContext.SaveChanges();
        }

        [TestMethod]
        public void TestCreateWithDuplicateName()
        {
            //Arrange
            var dbContext = new RestaurantContext();
            var menuManager = new MenuManager(dbContext);
            var menusController = new MenusController(menuManager);
            var userID = "8774f57e-f0c7-450a-b17e-09e30668eddc";
            menusController.UserID = userID;
            var dupMenu = new Menu { Name = "Dinner Menu", UserID = userID };

            //Act
            menusController.Create(dupMenu);
            var allMenus = menuManager.GetAllMenus(userID);
            var selectedMenus = allMenus.Where(m => m.Name == dupMenu.Name);
            var countMenu = selectedMenus.Count();

            //Assert
            Assert.AreEqual(1, countMenu);
        }

        [TestMethod] 
        public void TestCreateWithEmptyLogin()
        {
            //Arrange
            var dbContext = new RestaurantContext();
            var menuManager = new MenuManager(dbContext);
            var menusController = new MenusController(menuManager);
            menusController.UserID = string.Empty;
            var menu = new Menu { Name = Guid.NewGuid().ToString(), UserID = menusController.UserID };

            //Act
            var result = menusController.Create(menu);

            //Assert
            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public void TestEditWithFullData()
        {
            //Arrange
            var dbContext = new RestaurantContext();
            var menuManager = new MenuManager(dbContext);
            var menusController = new MenusController(menuManager);
            var userID = "8774f57e-f0c7-450a-b17e-09e30668eddc";
            menusController.UserID = userID;
            var newMenu = menuManager.CreateMenu(Guid.NewGuid().ToString(), userID);
            var menu = new Menu { Name = Guid.NewGuid().ToString(), UserID = userID };
            menu.ID = newMenu.ID;

            //Act
            menusController.Edit(newMenu.ID, menu);
            var editedMenu = menuManager.GetMenuByID(menu.ID);

            //Assert
            Assert.AreEqual(menu.Name, editedMenu.Name);
            dbContext.Remove(editedMenu);
            dbContext.SaveChanges();
        }

        [TestMethod]
        public void TestEditWithWrongID()
        {
            //Arrange
            var dbContext = new RestaurantContext();
            var menuManager = new MenuManager(dbContext);
            var menusController = new MenusController(menuManager);
            var userID = "8774f57e-f0c7-450a-b17e-09e30668eddc";
            menusController.UserID = userID;
            var newMenu = menuManager.CreateMenu(Guid.NewGuid().ToString(), userID);
            var menu = new Menu { Name = Guid.NewGuid().ToString(), UserID = userID };
            menu.ID = -1;

            //Act
            var result = menusController.Edit(newMenu.ID, menu);

            //Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            dbContext.Remove(newMenu);
            dbContext.SaveChanges();
        }
    }
}
