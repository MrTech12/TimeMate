using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeMate.Services
{
    public class SessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool CheckSessionValue()
        {
            bool sessionHasValue = false;
            if (_session.GetInt32("accountID").HasValue)
            {
                sessionHasValue = true;
            }
            return sessionHasValue;
        }
    }
}
