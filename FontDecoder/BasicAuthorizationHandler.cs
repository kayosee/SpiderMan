using FontDecoder.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace FontDecoder
{
    internal class BasicAuthorizationHandler : AuthenticationHandler<BasicAuthorizationOption>
    {
        private IConfiguration Configuration;
        private IUserService UserService;
        public BasicAuthorizationHandler(IUserService userService, IOptionsMonitor<BasicAuthorizationOption> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IConfiguration configuration) : base(options, logger, encoder)
        {
            Configuration = configuration;
            UserService = userService;
        }
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Get the header
            string authHeader = Request.Headers[HeaderNames.Authorization];
            if (string.IsNullOrEmpty(authHeader))
            {
                return new Task<AuthenticateResult>(() => AuthenticateResult.Fail(string.Empty));
            }

            // Parse config this way
            var username = Configuration.GetValue<string>("BasicCredentials:username");
            var password = Configuration.GetValue<string>("BasicCredentials:password");

            if (UserService.Authorize(username, password, out var user))
            {
                ClaimsPrincipal principal = new ClaimsPrincipal();
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return new Task<AuthenticateResult>(() => AuthenticateResult.Success(ticket));
            }

            return new Task<AuthenticateResult>(() => AuthenticateResult.Fail(string.Empty));
        }
    }
}