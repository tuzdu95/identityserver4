// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {            
                new ApiScope("api1", "My API")
            };

        public static IEnumerable<Client> Clients =>
              new List<Client>
                {
                    new Client
                    {
                        ClientId = "client",
                        AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                        // secret for authentication
                        ClientSecrets =
                        {
                            new Secret("secret".Sha256())
                        },
                        RequireClientSecret = false,
                        AllowOfflineAccess  = true,
                        AccessTokenLifetime=30,
                        // scopes that client has access to
                        AllowedScopes = { "api1" }
                    }
                };
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
    {
        new TestUser
        {
            SubjectId = "1",
            Username = "alice",
            Password = "password",
        },
        new TestUser
        {
            SubjectId = "2",
            Username = "bob",
            Password = "password"
        }
    };
        }
    }

}