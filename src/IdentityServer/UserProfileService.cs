using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class UserProfileService : IProfileService
    {
        protected readonly IUserStore userstore;

        public UserProfileService(IUserStore injectedUserStore)
        {
            userstore = injectedUserStore;
        }

        public virtual async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            if (context.RequestedClaimTypes.Any())
            {
                var user = await userstore.FindByUsername(context.Subject.GetSubjectId());
                if (user != null)
                {
                    context.AddRequestedClaims(user.Claims);
                }
            }
        }

        public virtual async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await userstore.FindByUsername(context.Subject.GetSubjectId());
            context.IsActive = !(user is null); // TODO check indicators like account status
        }
    }
}
