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

        public PasswordController(ILogger<PasswordController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get(string password)
        {
            var result = new List<string>();


            PasswordHasher<User> oldHasher = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(
                new PasswordHasherOptions()
                {
                    CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2
                })
            );

            PasswordHasher<User> newHasher = new PasswordHasher<User>(new OptionsWrapper<PasswordHasherOptions>(
                new PasswordHasherOptions()
                {
                    CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV3
                })
            );

            result.Add($"Password: {password}");
            result.Add($"IdentityV2 hash: {oldHasher.HashPassword(new User(), password)}");
            result.Add($"IdentityV3 hash: {newHasher.HashPassword(new User(), password)}");

            return result;
        }
    }
}