using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example.Services.UserIdGetter
{
    public class UserIdGetterMock : IUserIdGetter
    {
        public string GetUserId()
        {
            return "134b3602-23e2-4459-ab95-1e1d64592db7s";
        }

        public void SetUserId(string userId)
        {
            return;
        }
    }
}