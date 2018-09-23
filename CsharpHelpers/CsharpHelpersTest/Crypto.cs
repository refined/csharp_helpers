using CsharpHelpers.Standard.Crypto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsharpHelpers.Test
{
    [TestClass]
    public class Crypto
    {
        private static readonly byte[] _key = { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static readonly byte[] _iv = { 1, 2, 3, 4, 5, 6, 7, 8 };

        [TestMethod]
        public void TestCrypto()
        {
            var str = "2023917";
            var encrypted = str.Encrypt(_key, _iv);

            Assert.IsTrue(str == encrypted.Decrypt(_key, _iv));
        }
    }
}
