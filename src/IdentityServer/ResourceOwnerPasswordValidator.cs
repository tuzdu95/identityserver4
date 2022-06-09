using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            context.Result = new GrantValidationResult(
                         subject:"Hi",
                         authenticationMethod: "custom",
                         claims: GetUserClaims(new User()));
        }
        public static List<Claim> GetUserClaims(User user)
        {
            return new List<Claim>
            {

                new Claim(type:JwtClaimTypes.Role,"hêlo")
            };
        }
    }
}
