using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace bookstore.Shared.Services
{
    public interface IHttpContextCurrentUser
    {
        string CurrentUsername { get; }
        int? CurrentUserId { get; }
    }

    public class HttpContextCurrentUser : IHttpContextCurrentUser
    {
        private ClaimsPrincipal _user;

        public HttpContextCurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _user = httpContextAccessor.HttpContext.User;
        }

        public string CurrentUsername => _user?.Claims?.Where(c => c.Type == "Username")?.FirstOrDefault()?.Value;

        public int? CurrentUserId
        {
            get
            {
                var value = _user?.Claims?.Where(c => c.Type == "Id")?.FirstOrDefault()?.Value;
                if (value != null) return int.Parse(value);
                return default;
            }
        }
    }
}
