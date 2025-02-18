
using Duende.IdentityServer.Models;

namespace WTE.TintTrack.Api.Helpers.Configurations;

public static class IdentityConfig
{
    public static IEnumerable<IdentityResource> GetIdentityResources() =>
       [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
       ];

    public static IEnumerable<ApiScope> GetApiScopes() =>
        [
            new ApiScope("api1", "My API")
        ];

    public static IEnumerable<ApiResource> GetApiResources() =>
        [
            new("api1", "My API")
            {
                //Scopes = { "api1" }

                // Ensure the API resource has a secret for introspection
                ApiSecrets = { new Secret("api-secret".Sha256()) },

                Scopes = { "api1.read", "api1.write" }
            }
        ];

    /// <summary>
    /// set up IdentityServer to issue tokens
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<Client> GetClients(IdentityServerSettings settings) =>
        [
            new Client
            {
                ClientId = settings.ClientId, // API client ID (e.g., "api_client")

                // For non-interactive API clients (machine-to-machine communication), we typically use GrantTypes.ClientCredentials
                // rather than GrantTypes.Code (which is used for user authentication in a browser).
                AllowedGrantTypes = GrantTypes.ClientCredentials, // Use Client Credentials flow for APIs

                ClientSecrets = { new Secret(settings.ClientSecret.Sha256()) }, // Client secret
                AllowedScopes = settings.Scopes ?? ["openid", "profile", "api1"], // Scopes the client is allowed to access
            }
        ];
}
