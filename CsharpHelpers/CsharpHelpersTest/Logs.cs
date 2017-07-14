using System;
using System.Threading.Tasks;
using CsharpHelpers.Logging;
using CsharpHelpers.MessageProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CsharpHelpersTest
{
    [TestClass]
    public class Logs
    {
        [TestMethod]
        public async Task TestThrottleLog()
        {
            Logger.ThrottleTime = TimeSpan.FromSeconds(1);

            for (int i = 0; i < 11; i++)
            {
                Logger.AddThrottle("test");
                await Task.Delay(200);
            }

            var provider = Logger.GetProvider<ConsoleProvider>();
            
            Assert.IsTrue(provider.Messages.Count == 3);
        }
    }
}
