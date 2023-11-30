using HomeMade.Core.Entities;
using HomeMade.Core.Interfaces;
using HomeMade.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using HomeMade.Infrastructure.Data.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeMade.Infrastructure.Data.Configurations;
using Microsoft.Extensions.Options;

namespace HomeMade.Infrastructure.Repositories
{
    public class KitchenRepository : IKitchenRepository
    {
        private readonly FamomAuditContext _context;
        private readonly ServicebusConfig _servicebusConfig;
        public KitchenRepository(FamomAuditContext context, IOptions<ServicebusConfig> servicebusConfig)
        {
            _context = context;
            _servicebusConfig = servicebusConfig.Value;
        }

        public async Task<List<MenuItem>> GetMenuItems(int apartmentId)
        {
            var dateTimeNow = DateTime.Now;

            return       await _context.UserApartment
                                 .Where(x => x.ApartmentId == apartmentId)
                                 .SelectMany(x => x.ApplicationUser.Chef.Post)
                                 .Where(z => z.IsActive && (!z.AvailableTo.HasValue || DateTime.Compare(z.AvailableTo.Value, dateTimeNow) > 0))
                                 .Select(p => new MenuItem
                                 {
                                     Id = p.PostId,
                                     Title = p.Menu.Title,
                                     Description = p.Menu.Description,
                                     Price = p.Price,
                                     ImageUrl = p.PostImage.FirstOrDefault().Image.Url,
                                     Rating = p.Chef.Rating.Any() ? p.Chef.Rating.Average(x => x.Rating1) : 0,
                                     RatingsCount = p.Chef.Rating.Count(),
                                     UserName = p.Chef.ApplicationUser.UserName,
                                     UserId = p.Chef.ApplicationUserId,
                                     Quantity = p.Quantity,
                                     QuantityType = p.QuantityTypeId,
                                     BlockNumber = p.Chef.ApplicationUser.UserApartment.Select(x=>x.Block).FirstOrDefault(),
                                     FlatNumber = p.Chef.ApplicationUser.UserApartment.Select(x => x.ApartmentNumber).FirstOrDefault(),
                                     AvailableFrom = p.AvailableFrom,
                                     AvailableTo = p.AvailableTo,
                                     AvailabilityTypeId = p.AvailabilityTypeId,
                                     MinimumOrder = p.MinimumOrder,
                                     NoticeHours = p.NoticeHours,
                                     DeliveryType = p.DeliveryTypeId,
                                     DeliveryCharge = p.DeliveryCharge,
                                     Categories = p.Menu.Categories,
                                     LastUpdate = p.UpdateDateTime
                                 })
                                 .OrderByDescending(x => x.LastUpdate)
                                 .ToListAsync();
        }

        public async Task<MenuItem> GetMenuItem(int postId, int apartmentId)
        {
            var dateTimeNow = DateTime.Now;

            return await _context.Post
                                 .Where(x => x.PostId == postId)
                                 .Select(p => new MenuItem
                                 {
                                     Description = p.Menu.Description,
                                     Price = p.Price,
                                     ImageUrl = p.PostImage.FirstOrDefault().Image.Url,
                                     Quantity = p.Quantity,
                                     QuantityType = p.QuantityTypeId,
                                     AvailableFrom = p.AvailableFrom,
                                     AvailableTo = p.AvailableTo,
                                     DeliveryType = p.DeliveryTypeId,
                                     AvailabilityTypeId = p.AvailabilityTypeId,
                                     MinimumOrder = p.MinimumOrder ?? 1,
                                     NoticeHours = p.NoticeHours ?? 0,
                                     Discount = _context.ApartmentPromotion
                                                        .Where(z => z.ApartmentId == apartmentId && 
                                                                    z.Promotion.StartDate.Date <= dateTimeNow.Date &&
                                                                    z.Promotion.EndDate.Date > dateTimeNow.Date)
                                                        .Select(y => new Discount { 
                                                            DiscountPercent = y.Promotion.DiscountPercent,
                                                            PromotionId = y.PromotionId
                                                        })
                                                        .FirstOrDefault(),
                                     DeliveryCharge = p.DeliveryCharge
                                 })
                                 .FirstOrDefaultAsync();
        }

        public async Task<List<Post>> GetPosts(int applicationUserId)
        {
            var result = await _context.ApplicationUser
                                       .Include(x => x.Chef)
                                       .ThenInclude(x => x.Post)
                                       .ThenInclude(x => x.SubOrder)
                                       .Include(x=>x.Chef.Post)
                                       .ThenInclude(x=>x.Menu)
                                       .Include(x => x.Chef.Post)
                                       .ThenInclude(x=>x.PostImage)
                                       .ThenInclude(x=>x.Image)
                                       .Where(x => x.ApplicationUserId == applicationUserId)
                                       .Select(x => x.Chef.Post)
                                       .FirstOrDefaultAsync();
                                       
            return result.OrderByDescending(x=>x.UpdateDateTime).ToList();
        }

        public async Task<Post> PostMenu(Post menu, string chefName, int apartmentId)
        {
            _context.Post.Add(menu);
            await _context.SaveChangesAsync();
            if (menu.AvailabilityTypeId != (int)Core.Enums.AvailabilityType.NONE)
            {
                await Data.ServiceBus.Queue.SendPostMenuBroadcastMessageAsync(_servicebusConfig, new PostMenuQueueMessage()
                { 
                    AvailabilityTypeId = menu.AvailabilityTypeId, 
                    ChefName = chefName, 
                    MenuTitle = menu.Menu.Title, 
                    Quantity = menu.Quantity ?? 0, 
                    QuantityType = Enum.GetName(typeof(Core.Enums.QuantityType), 
                    menu.QuantityTypeId), 
                    ApartmentId = apartmentId,
                    PostId = menu.PostId
                });
            }
            
            return menu;
        }

        public async Task<int> GetChefId(int applicationUserId)
        {
            return await _context.Chef.Where(x=>x.ApplicationUserId == applicationUserId).Select(x=>x.ChefId).FirstOrDefaultAsync();
        }

        public async Task RemovePost(int postId)
        {
            var post = await _context.Post.FindAsync(postId);
            post.IsActive = false;
            post.InactiveDateTime = DateTime.Now;
            await _context.SaveChangesAsync();
        }
        
        public async Task UpdateDish(PostModel post, string imageUrl, string chefName, int apartmentId)
        {
            var result = await _context.Post
                                       .Include(x => x.PostImage)
                                       .Include(x=>x.Menu)
                                       .Where(x => x.PostId == post.PostId)
                                       .FirstOrDefaultAsync();

            result.AvailableFrom = getAvailDate(post.FromDateTime);
            result.AvailableTo = getAvailDate(post.ToDateTime);
            result.Quantity = post.Quantity;
            result.QuantityTypeId = post.QuantityTypeId;
            result.Price = post.Price;
            result.DeliveryTypeId = post.DeliveryTypeId;
            result.Menu.Description = post.Description;
            result.DeliveryCharge = post.DeliveryCharge;
            result.AvailabilityTypeId = post.AvailabilityTypeId;
            result.MinimumOrder = post.MinimumOrder;
            result.NoticeHours = post.NoticeHours;
            result.IsActive = post.AvailabilityTypeId != (int)Core.Enums.AvailabilityType.NONE;
            result.Menu.Categories = post.Categories;

            if (post.Image != null)
            {
                var image = new Image()
                {
                    FileName = post.Image.Name,
                    Url = imageUrl,
                    CreateDateTime = DateTime.Now
                };

                if (!result.PostImage.Any())
                {
                    var postImage = new PostImage { Image = image, CreateDateTime = DateTime.Now, PostId = result.PostId };
                    _context.PostImage.Add(postImage);
                }
                else
                {
                    result.PostImage.FirstOrDefault().Image = image;
                }
            }

            _context.SaveChanges();

            if (result.AvailabilityTypeId == (int)Core.Enums.AvailabilityType.NOW && result.AvailabilityTypeId == (int)Core.Enums.AvailabilityType.SCHPREORDER)
            {
                await Data.ServiceBus.Queue.SendPostMenuBroadcastMessageAsync(_servicebusConfig, new PostMenuQueueMessage()
                {
                    AvailabilityTypeId = result.AvailabilityTypeId,
                    ChefName = chefName,
                    MenuTitle = result.Menu.Title,
                    Quantity = result.Quantity ?? 0,
                    QuantityType = Enum.GetName(typeof(Core.Enums.QuantityType), result.QuantityTypeId),
                    ApartmentId = apartmentId,
                    PostId = result.PostId
                });
            }
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
    }
}
