namespace backendapi.Migrations
{
    using backendapi.EntityClasses;
    using backendapi.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<backendapi.Models.AuthContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(backendapi.Models.AuthContext context)
        {
           if (context.Clients.Count() > 0)
            {
                return;
            }

            context.Clients.AddRange(CreateClientList());
            context.SaveChanges();
        }

        private static List<Client> CreateClientList()
        {
            List<Client> ClientList = new List<Client>
            {
                new Client
                {
                    Id = "N0110C",
                    Secret = Helper.GetHash("yDqWI8aVyg+hs5JnfpAJyUyB6xuYp9UJLdyLUc4b7Vo="),
                    Name = "ClientNonConfidential",
                    AppType = Models.Enum.ApplicationTypes.JavaScript,
                    Active_Flag = true,
                    RefreshToken_LifeSpan = 1440,
                    Allowed_Origin = "*"
                },

                new Client
                {
                    Id = "N0211C",
                    Secret = Helper.GetHash("yDqWI8aVyg+hs5JnfpAJyUyB6xuYp9UJLdyLUc4b7Vo="),
                    Name = "ClientNonConfidential",
                    AppType = Models.Enum.ApplicationTypes.JavaScript,
                    Active_Flag = true,
                    RefreshToken_LifeSpan = 1440,
                    Allowed_Origin = "*"
                },

                new Client
                {
                    Id = "N0312C",
                    Secret = Helper.GetHash("yDqWI8aVyg+hs5JnfpAJyUyB6xuYp9UJLdyLUc4b7Vo="),
                    Name = "ClientNonConfidential",
                    AppType = Models.Enum.ApplicationTypes.JavaScript,
                    Active_Flag = true,
                    RefreshToken_LifeSpan = 1440,
                    Allowed_Origin = "*"
                },

                new Client
                {
                    Id = "C1120C",
                    Secret = Helper.GetHash("zlSsOHt9HToE98xsCdX9ihWXJFXwnYzhjk884UU3hoc="),
                    Name = "ClientConfidential",
                    AppType = Models.Enum.ApplicationTypes.NativeConfidential,
                    Active_Flag = true,
                    RefreshToken_LifeSpan = 1440,
                    Allowed_Origin = "*"
                },

                new Client
                {
                    Id = "C1221C",
                    Secret = Helper.GetHash("zlSsOHt9HToE98xsCdX9ihWXJFXwnYzhjk884UU3hoc="),
                    Name = "ClientConfidential",
                    AppType = Models.Enum.ApplicationTypes.NativeConfidential,
                    Active_Flag = true,
                    RefreshToken_LifeSpan = 1440,
                    Allowed_Origin = "*"
                },

                new Client
                {
                    Id = "C1322C",
                    Secret = Helper.GetHash("zlSsOHt9HToE98xsCdX9ihWXJFXwnYzhjk884UU3hoc="),
                    Name = "ClientConfidential",
                    AppType = Models.Enum.ApplicationTypes.NativeConfidential,
                    Active_Flag = true,
                    RefreshToken_LifeSpan = 1440,
                    Allowed_Origin = "*"
                },

                new Client
                {
                    Id = "TestClient",
                    Secret = Helper.GetHash("TestSecret"),
                    Name = "ClientConfidential",
                    AppType = Models.Enum.ApplicationTypes.NativeConfidential,
                    Active_Flag = true,
                    RefreshToken_LifeSpan = 1440,
                    Allowed_Origin = "*"
                }
            };
            return ClientList;
        }
        }
    }
