using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Interfaces
{
    public interface IUsersService
    {
        Task<IdentityResult> AddUserAsync(Domain.Entities.User user, string password);
        Task<Domain.Entities.User> GetUserByIdAsync(int id);
        Task<Domain.Entities.User> GetUserByEmailAsync(string email);
        Task<bool> ValidatePasswordAsync(Domain.Entities.User user, string password);
    }
}