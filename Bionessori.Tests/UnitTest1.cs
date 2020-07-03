using Bionessori.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bionessori.Tests {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void CheckGenerateHash() {
            string testPassword = "12345!";
            string testHash = "2cbe7f341eb6aca638a32b77ddedfd4c";
            var result = HashMD5Service.HashPassword(testPassword).Result;

            Assert.IsTrue(result.Equals(testHash));
        }
    }
}
