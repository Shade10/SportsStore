using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.HtmlHelpers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void Can_Paginate() {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "p1"},
                new Product {ProductID = 2, Name = "p2"},
                new Product {ProductID = 3, Name = "p3"},
                new Product {ProductID = 4, Name = "p4"},
                new Product {ProductID = 5, Name = "p5"},
            });

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //IEnumerable<Product> result = (IEnumerable<Product>) controller.List(2).Model;
            ProductListViewModel result = (ProductListViewModel) controller.List(null, 2).Model;

            Product[] prodArray = result.Products.ToArray();
            Assert.IsTrue(prodArray.Length == 2);
            Assert.AreEqual(prodArray[0].Name, "p4");
            Assert.AreEqual(prodArray[1].Name, "p5");

        }

        [TestMethod]
        public void Can_Generate_Page_Links() {
            HtmlHelper myHelper = null;

            PagingInfo pagingInfo = new PagingInfo {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Strona" + i;

            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Strona1"">1</a>"
                            + @"<a class=""btn btn-default btn-primary selected"" href=""Strona2"">2</a>"
                            + @"<a class=""btn btn-default"" href=""Strona3"">3</a>"
                , result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model() {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "p1"},
                new Product {ProductID = 2, Name = "p2"},
                new Product {ProductID = 3, Name = "p3"},
                new Product {ProductID = 4, Name = "p4"},
                new Product {ProductID = 5, Name = "p5"},
            });

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            ProductListViewModel result = (ProductListViewModel) controller.List(null, 2).Model;

            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Products() {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "p1", Category = "cat1"},
                new Product {ProductID = 2, Name = "p2", Category = "cat2"},
                new Product {ProductID = 3, Name = "p3", Category = "cat1"},
                new Product {ProductID = 4, Name = "p4", Category = "cat2"},
                new Product {ProductID = 5, Name = "p5", Category = "cat3"},
            });

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            Product[] result = ((ProductListViewModel) controller.List("cat2", 1).Model).Products.ToArray();

            Assert.AreEqual(result.Length, 2);
            Assert.IsTrue(result[0].Name == "p2" && result[0].Category == "cat2");
            Assert.IsTrue(result[1].Name == "p4" && result[1].Category == "cat2");
        }

        [TestMethod]
        public void Can_Create_Categories() {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "p1", Category = "jabłka"},
                new Product {ProductID = 2, Name = "p2", Category = "jabłka"},
                new Product {ProductID = 3, Name = "p3", Category = "śliwki"},
                new Product {ProductID = 4, Name = "p4", Category = "pomarańcze"},
            });

            NavController target = new NavController(mock.Object);

            string[] result = ((IEnumerable<string>) target.Menu().Model).ToArray();

            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual(result[0], "jabłka");
            Assert.AreEqual(result[1], "pomarańcze");
            Assert.AreEqual(result[2], "śliwki");
        }

    }
}
