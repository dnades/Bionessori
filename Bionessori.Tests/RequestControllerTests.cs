using Bionessori.Controllers;
using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Tests {
    [TestClass]
    public class RequestControllerTests {
        [TestMethod]
        public void GetRequestsTest() {
            // Arrange
            var mock = new Mock<IRequest>();
            mock.Setup(repo => repo.GetRequests()).Returns(GetTestUsers());
            var controller = new RequestController(mock.Object);

            // Act
            var result = controller.GetRequests();
            Assert.IsTrue(result);
            Console.WriteLine();
        }

        async Task<object> GetTestUsers() {
            var users = new List<object> {
                //new Request { Number = "1" },
                //new Request { Number = "2" },
                //new Request { Number = "3" },
                //new Request { Number = "4" }
            };
            return users;
        }
    }
}
