﻿using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Auth0.AspNetCore.Mvc
{
    /// <summary>
    /// Options used to configure the SDK
    /// </summary>
    public class Auth0Options
    {
        /// <summary>
        /// Auth0 domain name, e.g. tenant.auth0.com.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Client ID of the application.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Client Secret of the application.
        /// </summary>
        /// <remarks>
        /// Required when using <see cref="ResponseType"/> set to `code` or `code id_token`.
        /// </remarks>
        public string ClientSecret { get; set; }

        /// <summary>
        /// The audience to be used for requesting API access.
        /// </summary>
        /// <remark>
        /// When set, ensure to set <see cref="ResponseType"/> to `code` or `code id_token`.
        /// </remark>
        public string Audience { get; set; }

        /// <summary>
        /// Scopes to be used to request token(s). (e.g. "Scope1 Scope2 Scope3")
        /// </summary>
        public string Scope { get; set; } = "openid profile email";

        /// <summary>
        /// The path within the application to redirect the user to.
        /// </summary>
        /// <remarks>Processed internally by the Open Id Connect middleware.</remarks> 
        public string CallbackPath { get; set; }

        /// <summary>
        /// The Id of the organization to which the users should log in to.
        /// </summary>
        public string Organization { get; set; }

        /// <summary>
        /// Extra parameters to be send to `/authorize`.
        /// </summary>
        /// <example>
        /// services.AddAuth0Mvc(options =>
        /// {
        ///     options.ExtraParameters = new Dictionary<string, string>() { {"Test", "123" } };
        /// });
        /// </example>
        public IDictionary<string, string> ExtraParameters { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Events allowing you to hook into specific moments in the OpenID Connect pipeline.
        /// </summary>
        public Auth0OptionsEvents Events { get; set; }

        /// <summary>
        /// Set the ResponseType to be used.
        /// </summary>
        /// <remarks>
        /// Supports `id_token`, `code` or `code id_token`, defaults to `id_token` when omitted.
        /// </remarks>
        public string ResponseType { get; set; }

        /// <summary>
        /// Backchannel used to communicate with the Identity Provider.
        /// </summary>
        public HttpClient Backchannel { get; set; }

        public TimeSpan? MaxAge { get; set; }
    }
}
