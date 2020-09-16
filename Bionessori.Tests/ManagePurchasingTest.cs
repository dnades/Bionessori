using Bionessori.Core.Data;
using Bionessori.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Text;
using System.IO;
using System.Data;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using Newtonsoft.Json;

namespace Bionessori.Tests {
    /// <summary>
    /// Тесты контроллера управления закупками.
    /// </summary>
    [TestClass]
    public class ManagePurchasingTest {
        /// <summary>
        /// Метод тестирует выгрузку в файл Excel.
        /// </summary>
        [TestMethod]
        public void WriteToExcelTest() {
            List<UserDetails> persons = new List<UserDetails>() {
               new UserDetails() { Id = 1001, Name = "ABCD", City = "City1", Country = "USA" },
               new UserDetails() { Id = 1002, Name = "PQRS", City = "City2", Country = "INDIA" },
               new UserDetails() { Id = 1003, Name = "XYZZ", City = "City3", Country = "CHINA" },
               new UserDetails() { Id = 1004, Name = "LMNO", City = "City4", Country = "UK" },
          };

            TestWorkingDataExcel.WriteExcelFileTest(persons);
        }

        /// <summary>
        /// Метод тестирует чтение из Excel.
        /// </summary>
        [TestMethod]
        public void ReadFromExcelTest() {
            TestWorkingDataExcel.ReadExcelFileTest();
        }

        /// <summary>
        /// Метод тестирует получение коммерческих предложений.
        /// </summary>
        [TestMethod]
        public void GetOffersTest() {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "GetOffersTest").Options;
            var context = new ApplicationDbContext(options);

            AddTestOffers(context);
            var query = new GetTestDataQuery(context);
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
