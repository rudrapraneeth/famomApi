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
    public class LookupController : ControllerBase
    {
        private readonly ILookupRepository _lookupRepository;
        public LookupController(ILookupRepository lookupRepository)
        {
            _lookupRepository = lookupRepository;
        }

        [HttpGet]
        [Route("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories =  await _lookupRepository.GetCategories();
            return Ok(categories);
        }
    }
}
