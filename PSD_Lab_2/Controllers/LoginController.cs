using Microsoft.AspNetCore.Mvc;
using PSD_Lab_2.Models;
using PSD_Lab_2.Services;
using System.Net;

namespace PSD_Lab_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        /// <summary>
        /// Returns a encrypted server's open key
        /// </summary>
        /// <param name="clientOpenKey">Client's open key</param>
        [HttpGet]
        [ProducesResponseType(typeof(byte[]), (int)HttpStatusCode.OK)]
        public IActionResult GetForm()
        {
            var encryptedServerOpenKey = _loginService.GetForm();
            return Ok(encryptedServerOpenKey);
        }

        /// <summary>
        /// Check login data
        /// </summary>
        /// <param name="encryptedData">Encrypted login data</param>
        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public IActionResult CheckData([FromBody] EncryptedData encryptedData)
        {
            var result = _loginService.CheckData(encryptedData.Data);

            return Ok(result);
        }
    }
}
