using Bionessori.Controllers;
using Bionessori.Core;
using Bionessori.Core.Data;
using Bionessori.Models;
using Bionessori.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Tests {
    [TestClass]
    public class RequestControllerTests {
        [TestMethod]
        public void GetRequestsTest() {
            var mock = new Mock<ApplicationDbContext>();
            //mock.Setup(r => r.Database);  
            var controller = new RequestController(null);
            var action = controller.GetRequests().Result;
            Console.WriteLine();
        }
    }
}