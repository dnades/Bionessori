using Bionessori.Core.Data;
using Bionessori.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bionessori.Tests {
    /// <summary>
    /// Тесты контроллера управления закупками.
    /// </summary>
    [TestClass]
    public class ManagePurchasingTest {
        /// <summary>
        /// Метод тестирует выгрузку коммерческого предложения в файл Excel.
        /// </summary>
        [TestMethod]
        public void ExportToExcelTest() {

        }

        /// <summary>
        /// Метод тестирует получение коммерческих предложений.
        /// </summary>
        [TestMethod]
        public void GetOffersTest() {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "GetOffersTest").Options;
            var context = new ApplicationDbContext(options);

            AddTestOffers(context);
            var query = new GetDataQuery(context);
            var result = query.GetOffers();

            Assert.Equals(3, result.Count);
        }

        /// <summary>
        /// Метод добавляет тестовые коммерческие предложения.
        /// </summary>
        /// <param name="context"></param>
        void AddTestOffers(ApplicationDbContext context) {
            var offers = new[] {
                new CommerceOffer { Id = 1, Material = "Test1" },
                new CommerceOffer { Id = 2, Material = "Test2" },
                new CommerceOffer { Id = 3, Material = "Test3" }
            };
            context.CommerceOffers.AddRange(offers);
            context.SaveChanges();
        }
    }
}
