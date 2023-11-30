using HomeMade.Core.Entities;
using HomeMade.Core.ViewModels;
using HomeMade.Infrastructure.Data.DbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeMade.Api.Filters
{
    public class UsageAttribute : ActionFilterAttribute
    {
        private readonly FamomAuditContext _context;
        public UsageAttribute(FamomAuditContext context)
        {
            _context = context;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                var userInfo = (UserModel)filterContext.HttpContext.Items["User"];
                var usageData = new UsageData() { ActionName = filterContext.ActionDescriptor.DisplayName, ApplicationUserId = userInfo.ApplicationUserId, LastActionDate = DateTime.Now };
                _context.Add(usageData);
                _context.SaveChanges();
            }
        }
    }
}
