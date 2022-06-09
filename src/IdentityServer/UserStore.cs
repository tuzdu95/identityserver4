using IdentityModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Npgsql;

namespace IdentityServer
{
    public class UserStore : IUserStore
    {
        private string connectionString;
        public UserStore(IConfiguration configuration)
        {
            connectionString = "User ID=root;Password=E9Qhp9WI,72~qa6?bkM,;Host=localhost;Port=9998;Database=Identity;";
        }
        public async Task<bool> ValidateCredentials(string username, string password)
        {
            string hash = null;
            string salt = null;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                //await conn.OpenAsync();
                //DataTable table = new DataTable();
                //using (var cmd = new NpgsqlCommand("SELECT [PasswordSalt], [PasswordHash] FROM [User] WHERE [Username] = @username;", conn))
                //{
                //    cmd.Parameters.Add(new SqlParameter("@username", username));
                //    var reader = await cmd.ExecuteReaderAsync();
                //    table.Load(reader);
                //    reader.Close();
                //}
                //if (table.Rows.Count > 0)
                //{
                //    salt = (string)(table.Rows[0]["PasswordSalt"]);
                //    hash = (string)(table.Rows[0]["PasswordHash"]);
                //}
            }
            return true;
        }

        public async Task<User> FindBySubjectId(string subjectId)
        {
            User user = null;
            using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM \"User\" WHERE \"Name\" = @username;"))
            {
                cmd.Parameters.AddWithValue("@username", "Duong");
                user = await ExecuteFindCommand(cmd);
            }
            return user;
        }

        public async Task<User> FindByUsername(string username)
        {
            User user = null;
            using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM \"User\" WHERE \"Name\" = @username;"))
            {
                cmd.Parameters.AddWithValue("@username", "Duong");
                user = await ExecuteFindCommand(cmd);
            }
            return user;
        }

        public async Task<User> FindByExternalProvider(string provider, string subjectId)
        {
            User user = null;
            using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM [User] WHERE [ProviderName] = @pname AND [ProviderSubjectId] = @psub;"))
            {
                cmd.Parameters.AddWithValue("@pname", provider);
                cmd.Parameters.AddWithValue("@psub", subjectId);
                user = await ExecuteFindCommand(cmd);
            }
            return user;
        }

        private async Task<User> ExecuteFindCommand(NpgsqlCommand cmd)
        {
            User user = null;
            using (var conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync();
                cmd.Connection = conn;
                var reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    DataTable table = new DataTable();
                    table.Load(reader);
                    reader.Close();
                    var userRow = table.Rows[0];
                    user = new User()
                    {
                        Id = (Guid)userRow["Id"],
                        Name = (string)userRow["Name"],
                        PasswordSalt = (string)userRow["PasswordSalt"],
                        PasswordHash = (string)userRow["PasswordHash"],
                    };
                    user.Claims.Add(new Claim(
                                   type: "a",
                                   value: "b",
                                   valueType: "c",
                                   issuer: "d",
                                   originalIssuer: "e"));
                }
                cmd.Connection = null;
            }
            return user;
        }

        public async Task<User> AutoProvisionUser(string provider, string subjectId, List<Claim> claims)
        {
            // create a list of claims that we want to transfer into our store
            //var filtered = new List<Claim>();

            //foreach (var claim in claims)
            //{
            //    // if the external system sends a display name - translate that to the standard OIDC name claim
            //    if (claim.Type == ClaimTypes.Name)
            //    {
            //        filtered.Add(new Claim(JwtClaimTypes.Name, claim.Value));
            //    }
            //    // if the JWT handler has an outbound mapping to an OIDC claim use that
            //    else if (JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.ContainsKey(claim.Type))
            //    {
            //        filtered.Add(new Claim(JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap[claim.Type], claim.Value));
            //    }
            //    // copy the claim as-is
            //    else
            //    {
            //        filtered.Add(claim);
            //    }
            //}

            //// if no display name was provided, try to construct by first and/or last name
            //if (!filtered.Any(x => x.Type == JwtClaimTypes.Name))
            //{
            //    var first = filtered.FirstOrDefault(x => x.Type == JwtClaimTypes.GivenName)?.Value;
            //    var last = filtered.FirstOrDefault(x => x.Type == JwtClaimTypes.FamilyName)?.Value;
            //    if (first != null && last != null)
            //    {
            //        filtered.Add(new Claim(JwtClaimTypes.Name, first + " " + last));
            //    }
            //    else if (first != null)
            //    {
            //        filtered.Add(new Claim(JwtClaimTypes.Name, first));
            //    }
            //    else if (last != null)
            //    {
            //        filtered.Add(new Claim(JwtClaimTypes.Name, last));
            //    }
            //}

            //// create a new unique subject id
            //var sub = CryptoRandom.CreateUniqueId();

            //// check if a display name is available, otherwise fallback to subject id
            //var name = filtered.FirstOrDefault(c => c.Type == JwtClaimTypes.Name)?.Value ?? sub;

            //// create new user
            //var user = new User
            //{
            //    SubjectId = sub,
            //    Username = name,
            //    ProviderName = provider,
            //    ProviderSubjectId = subjectId,
            //    Claims = filtered
            //};

            //// store it and give it back
            //await SaveUser(user);
            return null;
        }

        public async Task<bool> SaveAppUser(User user, string newPasswordToHash = null)
        {
            bool success = true;
            //if (!String.IsNullOrEmpty(newPasswordToHash))
            //{
            //    user.PasswordSalt = User.PasswordSaltInBase64();
            //    user.PasswordHash = User.PasswordToHashBase64(newPasswordToHash, user.PasswordSalt);
            //}
            //try
            //{
            //    using (var conn = new NpgsqlConnection(connectionString))
            //    {
            //        await conn.OpenAsync();
            //        string upsert =
            //            $"MERGE [User] WITH (ROWLOCK) AS [T] " +
            //            $"USING (SELECT {user.id} AS [id]) AS [S] " +
            //            $"ON [T].[id] = [S].[id] " +
            //            $"WHEN MATCHED THEN UPDATE SET [SubjectId]='{user.SubjectId}', [Username]='{user.Username}', [PasswordHash]='{user.PasswordHash}', [PasswordSalt]='{user.PasswordSalt}', [ProviderName]='{user.ProviderName}', [ProviderSubjectId]='{user.ProviderSubjectId}' " +
            //            $"WHEN NOT MATCHED THEN INSERT ([SubjectId],[Username],[PasswordHash],[PasswordSalt],[ProviderName],[ProviderSubjectId]) " +
            //            $"VALUES ('{user.SubjectId}','{user.Username}','{user.PasswordHash}','{user.PasswordSalt}','{user.ProviderName}','{user.ProviderSubjectId}'); " +
            //            $"SELECT SCOPE_IDENTITY();";
            //        object result = null;
            //        using (var cmd = new NpgsqlCommand(upsert, conn))
            //        {
            //            result = await cmd.ExecuteScalarAsync();
            //        }
            //        int newId = (result is null || result is DBNull) ? 0 : Convert.ToInt32(result); // SCOPE_IDENTITY returns a SQL numeric(38,0) type
            //        if (newId > 0) user.id = newId;
            //        if (user.id > 0 && user.Claims.Count > 0)
            //        {
            //            foreach (Claim c in user.Claims)
            //            {
            //                string insertIfNew =
            //                    $"MERGE [Claim] AS [T] " +
            //                    $"USING (SELECT {user.id} AS [uid], '{c.Subject}' AS [sub], '{c.Type}' AS [type], '{c.Value}' as [val]) AS [S] " +
            //                    $"ON [T].[User_id]=[S].[uid] AND [T].[Subject]=[S].[sub] AND [T].[Type]=[S].[type] AND [T].[Value]=[S].[val] " +
            //                    $"WHEN NOT MATCHED THEN INSERT ([User_id],[Issuer],[OriginalIssuer],[Subject],[Type],[Value],[ValueType]) " +
            //                    $"VALUES ('{user.id}','{c.Issuer ?? string.Empty}','{c.OriginalIssuer ?? string.Empty}','{user.SubjectId}','{c.Type}','{c.Value}','{c.ValueType ?? string.Empty}');";
            //                using (var cmd = new NpgsqlCommand(insertIfNew, conn))
            //                {
            //                    await cmd.ExecuteNonQueryAsync();
            //                }
            //            }
            //        }
            //    }
            //}
            //catch
            //{
            //    success = false;
            //}
            return success;
        }
    }
}
