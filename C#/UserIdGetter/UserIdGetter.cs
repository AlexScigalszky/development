using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example.Services.UserIdGetter
{
    public class UserIdGetter : IUserIdGetter
    {
        private string CustomUserId = null;

        public string GetUserId()
        {
            if (String.IsNullOrEmpty(CustomUserId))
                return HttpContext.Current.User.Identity.GetUserId();
            else
                return CustomUserId;
        }

        public void SetUserId(string userId)
        {
            this.CustomUserId = userId;
        }
    }
}