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
    public class RatingsController : ControllerBase
    {
        private readonly IRatingsRepository _ratingsRepository;
        public RatingsController(IRatingsRepository ratingsRepository)
        {
            _ratingsRepository = ratingsRepository;
        }

        [HttpPost]
        [Route("Submit")]
        public async Task<IActionResult> Submit(RatingsModel ratings)
        {
            await _ratingsRepository.Submit(ratings);
            return Ok();
        }

        [HttpGet]
        [Route("GetReviews")]
        public async Task<IActionResult> GetReviews(int chefApplicationUserId)
        {
            var reviews =  await _ratingsRepository.GetReviews(chefApplicationUserId);
            return Ok(reviews);
        }
    }
}
