﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth0.AspNetCore.Mvc.IntegrationTests.Controllers
{
    public class AccountController : Controller
    {
        public async Task Login(
            string returnUrl = "/",
            string scope = null,
            [FromQuery(Name = "extraParameters")] Dictionary<string, string> extraParameters = null,
            string organization = null,
            string invitation = null,
            string audience = null)
        {
            var authenticationPropertiesBuilder = new AuthenticationPropertiesBuilder().WithRedirectUri(returnUrl);

            if (!string.IsNullOrWhiteSpace(scope))
            {
                authenticationPropertiesBuilder = authenticationPropertiesBuilder.WithScope(scope);
            }
            if (extraParameters != null)
            {
                foreach (KeyValuePair<string, string> entry in extraParameters)
                {
                    authenticationPropertiesBuilder = authenticationPropertiesBuilder.WithExtraParameter(entry.Key, entry.Value);
                }
                
            }

            if (!string.IsNullOrWhiteSpace(organization))
            {
                authenticationPropertiesBuilder = authenticationPropertiesBuilder.WithOrganization(organization);
            }

            if (!string.IsNullOrWhiteSpace(invitation))
            {
                authenticationPropertiesBuilder = authenticationPropertiesBuilder.WithInvitation(invitation);
            }

            if (!string.IsNullOrWhiteSpace(audience))
            {
                authenticationPropertiesBuilder = authenticationPropertiesBuilder.WithAudience(audience);
            }

            var authenticationProperties = authenticationPropertiesBuilder.Build();
            await HttpContext.ChallengeAsync(Constants.AuthenticationScheme, authenticationProperties);
        }

        [Authorize]
        public async Task Logout()
        {
            // Indicate here where Auth0 should redirect the user after a logout.
            // Note that the resulting absolute Uri must be whitelisted in the
            // **Allowed Logout URLs** settings for the client.
            var authenticationProperties = new AuthenticationPropertiesBuilder().WithRedirectUri(Url.Action("Index", "Home")).Build();

            await HttpContext.SignOutAsync(Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            return View(new
            {
                Name = User.Identity.Name,
                EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
            });
        }

        /// <summary>
        /// This is just a helper action to enable you to easily see all claims related to a user. It helps when debugging your
        /// application to see the in claims populated from the Auth0 ID Token
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Claims()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private Dictionary<string, string> ObjectToDictionary(object values)
        {
            if (values is Dictionary<string, string> dictionary)
                return dictionary;

            dictionary = new Dictionary<string, string>();
            if (values != null)
                foreach (var prop in values.GetType().GetRuntimeProperties())
                {
                    var value = prop.GetValue(values) as string;
                    if (!string.IsNullOrEmpty(value))
                        dictionary.Add(prop.Name, value);
                }

            return dictionary;
        }
    }
}