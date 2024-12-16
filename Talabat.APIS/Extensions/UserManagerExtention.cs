using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIS.Extensions
{
    public static class UserManagerExtention
    {
        public  static async Task<AppUser> FindUserWhithAddressByEmailAsync(this UserManager<AppUser> userManager,ClaimsPrincipal User)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = userManager.Users.Include(u =>u.Address).FirstOrDefault(u => u.NormalizedEmail == email.ToUpper());

            return user;
        }
    }
}
