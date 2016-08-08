
using backendapi.EntityClasses;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace backendapi.Models
{
    public class AuthRepository : IDisposable
    {
        private AuthContext ctx;

        private UserManager<IdentityUser> userManager;

        public AuthRepository()
        {
            ctx = new AuthContext();
            userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(ctx));
        }

        public async Task<IdentityResult> RegisterUser(UserModel client)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = client.UserName
            };

            var res = await userManager.CreateAsync(user, client.Password);
            return res;
        }

        public async Task<IdentityUser> FindUser(string UserName, string Password)
        {
            IdentityUser user = await userManager.FindAsync(UserName, Password);

            return user;
        }

        public Client Find_Client(string clientId)
        {
            var client = ctx.Clients.Find(clientId);
            return client;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            var exist_Token = ctx.RefreshTokens.Where(r => r.Subject == token.Subject & r.ClientId == token.ClientId).SingleOrDefault();
            if (exist_Token != null)
            {
                var res = await RemoveRefreshToken(exist_Token);
            }

            ctx.RefreshTokens.Add(token);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await ctx.RefreshTokens.FindAsync(refreshTokenId);
            if (refreshToken != null)
            {
                ctx.RefreshTokens.Remove(refreshToken);
                return await ctx.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            ctx.RefreshTokens.Remove(refreshToken);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await ctx.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<RefreshToken> GetAll()
        {
            return ctx.RefreshTokens.ToList();
        }

        //public async Task<IdentityUser> FindAsync(UserLoginInfo loginInfo)
        //{
        //    IdentityUser user = await userManager.FindAsync(loginInfo);

        //    return user;
        //}

        //public async Task<IdentityResult> CreateAsync(IdentityUser user)
        //{
        //    var result = await userManager.CreateAsync(user);

        //    return result;
        //}

        //public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        //{
        //    var result = await userManager.AddLoginAsync(userId, login);

        //    return result;
        //}

        public void Dispose()
        {
            ctx.Dispose();
            userManager.Dispose();
        }
    }
}