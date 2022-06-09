using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public interface IUserStore
    {
        Task<bool> ValidateCredentials(string username, string password);
        Task<User> FindBySubjectId(string subjectId);
        Task<User> FindByUsername(string username);
        Task<User> FindByExternalProvider(string provider, string subjectId);
        Task<User> AutoProvisionUser(string provider, string subjectId, List<Claim> claims);
        Task<bool> SaveAppUser(User user, string newPasswordToHash = null);
    }
}
