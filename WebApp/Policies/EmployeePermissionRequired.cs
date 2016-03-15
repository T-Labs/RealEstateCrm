using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;

namespace WebApp.Policies
{
    public class EmployeePermissionRequired : AuthorizationHandler<EmployeePermissionRequired>, IAuthorizationRequirement
    {
        public string[] PermissionRole { get; set; }
        public EmployeePermissionRequired(params string[] permissionRole)
        {
            PermissionRole = permissionRole;
        }

        protected override void Handle(AuthorizationContext context, EmployeePermissionRequired requirement)
        {
            if (context.User.IsInRole(RoleNames.Admin))
            {
                context.Succeed(requirement);
                return;
            }

            if (context.User.IsInRole(RoleNames.Employee))
            {
                if (requirement.PermissionRole == null)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    if (requirement.PermissionRole.All(x => context.User.IsInRole(x)))
                    {
                        context.Succeed(requirement);
                    }
                }
                return;
            }

        }
    }
}
