using IdentityServer4.Models;
using IdentityServer4;
using System.Collections.Generic;
using System;

public static class Config
{
    //JWT aud parametresi için
    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
       new ApiResource("resource_report"){Scopes={"report_fullpermission"}},
       new ApiResource("resource_counter"){Scopes={"counter_fullpermission"}},
       new ApiResource("resource_gateway"){Scopes={"gateway_fullpermission"}},
       new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
    };
    //client'lar hangi bilgilere erişebilir?
    public static IEnumerable<IdentityResource> IdentityResources =>
               new IdentityResource[]
               {
                       new IdentityResources.Email(),
                       new IdentityResources.OpenId(),
                       new IdentityResources.Profile(),
                       //new IdentityResource(){ Name="roles", DisplayName="Roles", Description="Kullanıcı rolleri", UserClaims=new []{ "role"} }
               };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
                new ApiScope("report_fullpermission","Rapor API için full erişim"),
                new ApiScope("counter_fullpermission","Sayaç API için full erişim"),
                new ApiScope("gateway_fullpermission","Gateway API için full erişim"),
                //IdentityServer sabiti
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
                ////ClientCredentials refresh token yok 
                //new Client
                //{
                //   ClientName="Asp.Net Core MVC",
                //    ClientId="WebMvcClient",
                //    ClientSecrets= {new Secret("secret".Sha256())},
                //    AllowedGrantTypes= GrantTypes.ClientCredentials,
                //    AllowedScopes={ "report_fullpermission", "counter_fullpermission", "gateway_fullpermission", IdentityServerConstants.LocalApi.ScopeName }
                //},
                //token alması gerekiyor.resource owner
                   new Client
                {
                   ClientName="Asp.Net Core MVC",
                    ClientId="WebMvcClientForUser",
                    AllowOfflineAccess=true,
                    ClientSecrets= {new Secret("secret".Sha256())},
                    //izin tipi
                    AllowedGrantTypes= GrantTypes.ResourceOwnerPassword,
                    //izin verilen scope
                    AllowedScopes={ "report_fullpermission", "counter_fullpermission", "gateway_fullpermission", IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile, 
                           //OfflineAccess refresh token almak için izin
                    IdentityServerConstants.StandardScopes.OfflineAccess, IdentityServerConstants.LocalApi.ScopeName,"roles" },
                    //sürekli token alması gerektiği için
                    AccessTokenLifetime=1*60*60,
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime= (int) (DateTime.Now.AddDays(60)- DateTime.Now).TotalSeconds,
                    RefreshTokenUsage= TokenUsage.ReUse
                },
                      new Client
                {
                   ClientName="Token Exchange Client",
                    ClientId="TokenExhangeClient",
                    ClientSecrets= {new Secret("secret".Sha256())},
                    AllowedGrantTypes= new []{ "urn:ietf:params:oauth:grant-type:token-exchange" },
                    AllowedScopes={ "report_fullpermission", "counter_fullpermission", IdentityServerConstants.StandardScopes.OpenId }
                },
        };
}