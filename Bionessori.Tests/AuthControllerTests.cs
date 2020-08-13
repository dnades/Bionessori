using Bionessori.Controllers;
using Bionessori.Core.Interfaces;
using Bionessori.Models;
using Bionessori.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bionessori.Tests {
    [TestClass]
    public class AuthControllerTests {
        /// <summary>
        /// Тест хэширования пароля.
        /// </summary>
        [TestMethod]
        public void GenerateHashStringTest() {
            string testStr = "12345!";
            var hashMethod = HashMD5Service.HashPassword(testStr).Result;

            Assert.IsNotNull(hashMethod);
        }
    }
}
