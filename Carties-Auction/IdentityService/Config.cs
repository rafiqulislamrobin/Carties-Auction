using Duende.IdentityServer.Models;

namespace IdentityService
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("auctionApp", "Auction app full access"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // for developing postman as a client
                new Client
                {
                   ClientId = "postman",
                   ClientName = "Postman",
                   AllowedScopes = { "auctionApp", "openid", "profile" },
                   RedirectUris = { "https://www.postman.com/oauth2/callback"},
                   ClientSecrets = {new Secret("NotASecret".Sha256())},
                   AllowedGrantTypes = { GrantType.ResourceOwnerPassword}
                },
            };
    }
}
