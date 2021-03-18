using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace HasherDlaKuby.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PasswordController : ControllerBase
    {
        private readonly ILogger<PasswordController> _logger;
        private readonly PasswordHasher<User> _oldHasher;
        private readonly PasswordHasher<User> _newHasher;
        private readonly User _user = new User();

        public PasswordController(ILogger<PasswordController> logger)
        {
            _logger = logger;

            _oldHasher = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(
                new PasswordHasherOptions()
                {
                    CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2
                })
            );

            _newHasher = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(
                new PasswordHasherOptions()
                {
                    CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3
                })
            );
        }

        [HttpGet]
        public IEnumerable<string> Get(string password)
        {
            var result = new List<string>();
            result.Add($"Password: {password}");
            result.Add($"IdentityV2 hash: {_oldHasher.HashPassword(_user, password)}");
            result.Add($"IdentityV3 hash: {_newHasher.HashPassword(_user, password)}");

            return result;
        }
    }
}