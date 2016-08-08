using backendapi.EntityClasses;
using backendapi.Models;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace backendapi.Providers
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
            public async Task CreateAsync(AuthenticationTokenCreateContext context)
            {
                var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

                if (string.IsNullOrEmpty(clientid))
                {
                    return;
                }

                var refreshTokenId = Guid.NewGuid().ToString("n");

                using (AuthRepository _repo = new AuthRepository())
                {
                    var refreshTokenLifeSpan = context.OwinContext.Get<string>("as:clientRefreshTokenLifeSpan");

                    var token = new RefreshToken()
                    {
                        Id = Helper.GetHash(refreshTokenId),
                        ClientId = clientid,
                        Subject = context.Ticket.Identity.Name,
                        IssuedUTC = DateTime.UtcNow,
                        ExpiresUTC = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeSpan))
                    };

                    context.Ticket.Properties.IssuedUtc = token.IssuedUTC;
                    context.Ticket.Properties.ExpiresUtc = token.ExpiresUTC;

                    token.Protected_Ticket = context.SerializeTicket();

                    var result = await _repo.AddRefreshToken(token);

                    if (result)
                    {
                        context.SetToken(refreshTokenId);
                    }

                }
            }


            public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
            {

                var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

                string hashedTokenId = Helper.GetHash(context.Token);

                using (AuthRepository _repo = new AuthRepository())
                {
                    var refreshToken = await _repo.FindRefreshToken(hashedTokenId);

                    if (refreshToken != null)
                    {
                        context.DeserializeTicket(refreshToken.Protected_Ticket);
                        var result = await _repo.RemoveRefreshToken(hashedTokenId);
                    }
                }
            }

            public void Create(AuthenticationTokenCreateContext context)
            {
                throw new NotImplementedException();
            }

            public void Receive(AuthenticationTokenReceiveContext context)
            {
                throw new NotImplementedException();
            }
        }
    }

