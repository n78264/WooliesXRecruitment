using API.Contracts;
using API.Models;
using API.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;

namespace Tests
{
    [TestClass]
    public class UserServiceTest
    {
        private IOptions<ResourceSettings> config;
        private ILogger<UserService> logger;

        [TestInitialize()]
        public void Initialize() {

            var configuration = new ConfigurationBuilder()
                             .AddJsonFile("appsettings.json", false).Build();
            config = Options.Create(configuration.GetSection("WooliesXAPISettings").
                             Get<ResourceSettings>());
            var services = new ServiceCollection();
            //services.AddTransient<IUserService, UserService>();
            var serviceProvider = services.BuildServiceProvider();
            var logFactory = LoggerFactory.Create(builder => builder.AddConsole());
            logger = logFactory.CreateLogger<UserService>();
        }

        [TestCleanup()]
        public void Cleanup() { }


        [TestMethod]
        public void TestGetUser()
        {
            var user = new UserService(config,logger).GetUser();
            Assert.IsTrue(user.Name.Length > 0);
            Assert.AreEqual(user.Name, "WooliesXClient");
            Assert.IsTrue(user.Token.Length > 0);
            Assert.AreEqual(user.Token, "fbbb619a-3aba-4cc2-8c72-18fdb015b615");
        }
    }
}
