using Microsoft.AspNetCore.Mvc;
using HomeMade.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HomeMade.Core.ViewModels;
using HomeMade.Core.Entities;
using HomeMade.Api.Models;
using Microsoft.Extensions.Options;
using HomeMade.Api.Utility;
using System.IO;
using HomeMade.Api.Filters;
using Microsoft.Extensions.Logging;

namespace HomeMade.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(UsageAttribute))]
    public class KitchenController : ControllerBase
    {
        private readonly JwtAuthenticationConfig _authenticationConfig = null;
        private readonly AzureStorageConfig _storageConfig = null;
        private readonly IKitchenRepository _kitchenRepository;
        private ILogger<KitchenController> _logger;
        public KitchenController(IKitchenRepository kitchenRepository, IOptions<AzureStorageConfig> storageConfig, IOptions<JwtAuthenticationConfig> authenticationConfig, ILogger<KitchenController> logger)
        {
            _kitchenRepository = kitchenRepository;
            _authenticationConfig = authenticationConfig.Value;
            _storageConfig = storageConfig.Value;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetMenuItems")]
        public async Task<IActionResult> GetMenuItems()
        {
            var userInfo = (UserModel)HttpContext.Items["User"];
            var menuItems = await _kitchenRepository.GetMenuItems(userInfo.ApartmentId);
            
            return Ok(menuItems);
        }

        [HttpGet]
        [Route("GetMenuItem")]
        public async Task<IActionResult> GetMenuItem(int postId)
        {
            var userInfo = (UserModel)HttpContext.Items["User"];
            var menuItems = await _kitchenRepository.GetMenuItem(postId, userInfo.ApartmentId);

            return Ok(menuItems);
        }


        //TODO: refactor this code.
        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> PostAsync([FromForm]PostModel postModel)
        {
            _logger.LogInformation($"User posted {postModel}");
            var userInfo = (UserModel)HttpContext.Items["User"];
            var postImages = new List<PostImage>();
            if (postModel.Image !=null)
            {
                string fileUrl = await FileUpload.UploadToStorage(postModel.Image, _storageConfig);

                if (string.IsNullOrEmpty(fileUrl))
                {
                    return BadRequest("Look like the image couldnt upload to the storage");
                }

                postImages.Add(new PostImage()
                {
                    Image = new Image()
                    {
                        FileName = postModel.Image.Name,
                        Url = fileUrl,
                    },
                });
            }

            var post = new Post()
            {
                Menu = new Menu()
                {
                    Title = postModel.Title,
                    Description = postModel.Description,
                    Categories = postModel.Categories,
                },

                PostImage = postImages,
                Quantity = postModel.Quantity,
                QuantityTypeId = postModel.QuantityTypeId,
                DeliveryTypeId = postModel.DeliveryTypeId,
                DeliveryCharge = postModel.DeliveryCharge,
                Price = postModel.Price,
                MinimumOrder = postModel.MinimumOrder,
                NoticeHours = postModel.NoticeHours,
                AvailabilityTypeId = postModel.AvailabilityTypeId,
                IsActive = postModel.AvailabilityTypeId != (int)Core.Enums.AvailabilityType.NONE,
                AvailableFrom = getAvailDate(postModel.FromDateTime),
                AvailableTo = getAvailDate(postModel.ToDateTime),
            };

            //TODO: Refactor this code.
            if (postModel.ChefId == 0)
            {
                var chefId = await _kitchenRepository.GetChefId(postModel.ApplicationUserId);
                if (chefId == 0)
                {
                    post.Chef = new Chef()
                    {
                        ApplicationUserId = postModel.ApplicationUserId,
                    };
                }

                else
                {
                    post.ChefId = chefId;
                }

            }
            else
            {
                post.ChefId = postModel.ChefId;
            }

            await _kitchenRepository.PostMenu(post, userInfo.UserName, userInfo.ApartmentId);
            //var token = Common.GenerateToken(appUser, _authenticationConfig.SecretKey, _authenticationConfig.Issuer, _authenticationConfig.Audience);
            return Ok();
        }

        private DateTime? getAvailDate(string availDateTime)
        {
            DateTime availDate;
            if (DateTime.TryParse(availDateTime, out availDate))
            {
                return availDate;
            }
            return null;
        }

        [HttpGet]
        [Route("GetPostHistory")]
        public async Task<IActionResult> GetPosts()
        {
            var userInfo = (UserModel)HttpContext.Items["User"];
            var result = await _kitchenRepository.GetPosts(userInfo.ApplicationUserId);
            var postHistory = new List<PostHistoryModel>();
            result.ForEach(x =>
            {
                postHistory.Add(new PostHistoryModel
                {
                    PostId = x.PostId,
                    FromDate = x.AvailableFrom,
                    ToDate = x.AvailableTo,
                    QuantityLeft = x.Quantity,
                    QuantityType = x.QuantityTypeId,
                    DeliveryCharge = x.DeliveryCharge,
                    DeliveryTypeId = x.DeliveryTypeId,
                    Description = x.Menu.Description,
                    Orders = x.SubOrder.Count(y => y.StatusId != (int)Core.Enums.OrderStatus.CANCELED),
                    PostDate = x.CreateDateTime.Value,
                    Price = x.Price,
                    Title = x.Menu.Title,
                    Categories = x.Menu.Categories,
                    ImageUrl = x.PostImage.FirstOrDefault()?.Image.Url,
                    MinimumOrder = x.MinimumOrder,
                    NoticeHours = x.NoticeHours,
                    AvailabilityTypeId = x.AvailabilityTypeId,
                    Status = x.IsActive && (!x.AvailableTo.HasValue || DateTime.Compare(x.AvailableTo.Value, DateTime.Now) > 0) ? "Active" : "Inactive"
                });
            });
                return Ok(postHistory);
        }

        [HttpPost]
        [Route("RemovePost")]
        public async Task<IActionResult> RemovePost(PostHistoryModel post)
        {
            await _kitchenRepository.RemovePost(post.PostId);
            
            return Ok();
        }

        [HttpPost]
        [Route("UpdateDish")]
        public async Task<IActionResult> UpdateDish([FromForm]PostModel post)
        {
            var userInfo = (UserModel)HttpContext.Items["User"];
            string imageUrl = string.Empty;
            if (post.Image != null)
            {
                imageUrl = await FileUpload.UploadToStorage(post.Image, _storageConfig);

                if (string.IsNullOrEmpty(imageUrl))
                {
                    return BadRequest("Look like the image couldnt upload to the storage");
                }
            }
            
            await _kitchenRepository.UpdateDish(post, imageUrl, userInfo.UserName, userInfo.ApartmentId);

            return Ok();
        }
    }
}
