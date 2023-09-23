using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Account
{
    public interface IRoleBusiness
    {
        List<string> GetRoles();
    }
    public class RoleBusiness : IRoleBusiness
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleBusiness(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public List<string> GetRoles()
        {
            try
            {
                var roles = _roleManager.Roles.Select(x => x.Name).ToList();
                return roles;
            }
            catch
            {
                return new List<string>();
            }
        }
    }
}
