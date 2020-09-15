using Bionessori.Controllers;
using Bionessori.Core;
using Bionessori.Core.Constants;
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
    // Тесты для контроллера заявок.
    [TestClass]
    public class RequestControllerTests {
        // Тест на получение заявок.
        [TestMethod]
        public void GetRequestsTest() {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "GetRequestsTestAsync").Options;
            var context = new ApplicationDbContext(options);

            AddTestRequests(context);

            var query = new GetDataQuery(context);
            var result = query.GetRequests();

            Assert.Equals(3, result.Count);
        }

        // Добавляет тестовые заявки.
        void AddTestRequests(ApplicationDbContext context) {
            var requests = new[] {
                new Request { Id = 1, Material = "Test1" },
                new Request { Id = 2, Material = "Test2" },
                new Request { Id = 3, Material = "Test3" }
            };
            context.Requests.AddRange(requests);
            context.SaveChanges();
        }

        // Добавляет тестовые заявки со статусом "Новая".
        void AddTestRequestWithStatuses(ApplicationDbContext context) {
            var requests = new[] {
                new Request { Id = 1, Material = "Test1", Status = RequestStatus.REQ_STATUS_NEW, Number = 8508 },
                new Request { Id = 2, Material = "Test2", Status = RequestStatus.REQ_STATUS_NEW, Number = 1111 },
                new Request { Id = 3, Material = "Test3", Status = RequestStatus.REQ_STATUS_NEW, Number = 1112 }
            };
            context.Requests.AddRange(requests);
            context.SaveChanges();
        }

        // Метод изменяет статус заявки, которая имеет статус "Новая" на "В работе" по ее номеру.
        [TestMethod]
        public async Task ChangeRequestStatusOnInWork() {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "ChangeRequestStatusOnInWork").Options;
            var context = new ApplicationDbContext(options);

            AddTestRequestWithStatuses(context);

            var query = new GetDataQuery(context);

            // Изменяет статус на "В работе".
            query.GetRequests().Where(s => s.Status.Equals(RequestStatus.REQ_STATUS_NEW)).Where(r => r.Number == 8508).ToList().ForEach(r => r.Status = RequestStatus.REQ_STATUS_IN_WORK);
            context.UpdateRange(query.GetRequests());
            await context.SaveChangesAsync();

            Assert.IsTrue(context.Requests.ToList().Any(e => e.Status.Equals(RequestStatus.REQ_STATUS_IN_WORK)));
        }
    }   
}