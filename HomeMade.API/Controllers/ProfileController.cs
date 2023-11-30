using System.Collections.Generic;
using System.Threading.Tasks;
using HomeMade.Api.Filters;
using HomeMade.Core.Interfaces;
using HomeMade.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HomeMade.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(UsageAttribute))]
    public class ProfileController : ControllerBase
    {
        private readonly IRatingsRepository _ratingsRepository;
        public ProfileController(IRatingsRepository ratingsRepository)
        {
            _ratingsRepository = ratingsRepository;
        }

        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save(RatingsModel ratings)
        {
            await _ratingsRepository.Submit(ratings);
            return Ok();
        }
    }
}
