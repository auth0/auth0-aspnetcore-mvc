﻿using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;

namespace Auth0.AspNetCore.Mvc
{
    /// <summary>
    /// Used to setup Auth0 specific defaults for <see cref="OpenIdConnectOptions"/>.
    /// </summary>
    internal class Auth0OpenIdConnectPostConfigureOptions : IPostConfigureOptions<OpenIdConnectOptions>
    {
        public void PostConfigure(string name, OpenIdConnectOptions options)
        {
            options.Backchannel.DefaultRequestHeaders.Add("Auth0-Client", Utils.CreateAgentString());
        }
    }
}
