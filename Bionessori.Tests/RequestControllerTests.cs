using Bionessori.Controllers;
using Bionessori.Core;
using Bionessori.Core.Data;
using Bionessori.Models;
using Bionessori.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Tests {
    /// <summary>
    /// Тесты для контроллера заявок.
    /// </summary>
    [TestClass]
    public class RequestControllerTests {
        /// <summary>
        /// Тест на получение заявок.
        /// </summary>
        [TestMethod]
        public void GetRequestsTest() {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "GetRequestsTestAsync")
            .Options;
            var context = new ApplicationDbContext(options);

            AddTestRequests(context);

            var query = new GetRequestsQuery(context);
            var result = query.GetRequests();

            Assert.Equals(3, result.Count);
        }

        void AddTestRequests(ApplicationDbContext context) {
            var requests = new[] {
                new Request { Id = 1, Material = "Test1" },
                new Request { Id = 2, Material = "Test2" },
                new Request { Id = 3, Material = "Test3" }
            };
            context.Requests.AddRange(requests);
            context.SaveChanges();
        }
    }   
}