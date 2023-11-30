using HomeMade.Core.Entities;
using HomeMade.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HomeMade.Core.Interfaces
{
    public interface IKitchenRepository
    {
        Task<List<MenuItem>> GetMenuItems(int apartmentId);
        Task<MenuItem> GetMenuItem(int postId, int apartmentId);
        Task<Post> PostMenu(Post menu, string chefName, int apartmentId);
        Task<int> GetChefId(int applicationUserId);
        Task<List<Post>> GetPosts(int applicationUserId);
        Task RemovePost(int postId);
        Task UpdateDish(PostModel post, string imageUrl, string chefName, int apartmentId);
    }
}
