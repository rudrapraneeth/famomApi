using HomeMade.Core.Interfaces;
using HomeMade.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMade.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpoController : ControllerBase
    {
        private readonly IExpoRepository _expoRepository;
        private ILogger<LoginController> _logger;

        public ExpoController(IExpoRepository expoRepository, ILogger<LoginController> logger)
        {
            _expoRepository = expoRepository;
            _logger = logger;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Post(UserModel userModel)
        {
            var userInfo = (UserModel)HttpContext.Items["User"];
            await _expoRepository.UpdateExpoToken(userInfo.ApplicationUserId, userModel.PushToken);
            return Ok();
        }
    }
}
