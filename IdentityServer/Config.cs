// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles", new[] { "role" }) //Add this line
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        { new ApiScope("api1", "My API") };

    public static IEnumerable<Client> Clients =>
        new Client[]
         {
             new Client{ ClientId = "catelog",
                 AllowedGrantTypes = GrantTypes.ClientCredentials,            
                 ClientSecrets = { new Secret("78195A38-7268-7268-8F2E-8F4EB3FECF34".Sha256()) },            
                 AllowedScopes = {  "openid", "profile", "api1", "roles" } },
             new Client{ ClientId = "cart", 
                 AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,  
                 ClientSecrets = { new Secret("78195A38-7269-7269-7269-8F4EB3FECF34".Sha256()) }, 
                 AllowedScopes = { "openid", "profile", "api1", "roles" } }

         };
}