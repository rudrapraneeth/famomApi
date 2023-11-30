using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMade.Core.Interfaces;
using HomeMade.Core.ViewModels;
using HomeMade.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HomeMade.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;
        public AddressController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        [HttpGet]
        [Route("GetApartments")]
        public async Task<IActionResult> GetApartments()
        {
            var apartments = await _addressRepository.GetApartments();

            var apartmentModels = new List<ApartmentModel>();
            apartments.ForEach(x => apartmentModels.Add(new ApartmentModel()
            {
                Name = x.Name + ", " + x.Address.AddressLine2 + ", " + x.Address.City,
                Id = x.ApartmentId
            }));
            
            return Ok(apartmentModels);
        }
    }
}
