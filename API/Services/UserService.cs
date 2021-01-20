using API.Contracts;
using API.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly IOptions<ResourceSettings> _config;
        private readonly ILogger<UserService> _logger;

        public UserService(IOptions<ResourceSettings> config,
            ILogger<UserService> logger)
        {
            _config = config;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public User GetUser()
        {
            if (_config?.Value?.UserName == null || _config?.Value?.Token == null)
                throw new ApplicationException("Configuration parameter value is Null");
            return new User { Name = _config.Value.UserName, Token = _config.Value.Token };
        }
    }
}
