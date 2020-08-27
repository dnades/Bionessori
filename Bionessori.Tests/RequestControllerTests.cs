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
            //var mock = new Mock<BaseRequest>();
            //mock.Setup(r => r.GetRequests()).Returns(GetRequests());
            //var action = new RequestService();
            //var result = action.GetRequests().Result;
            Console.WriteLine();
        }

        Task<IEnumerable> GetRequests() {
            return null;
        }
    }
}