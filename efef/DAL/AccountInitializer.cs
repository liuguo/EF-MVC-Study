using efef.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace efef.DAL
{
    public class AccountInitializer:DropCreateDatabaseIfModelChanges<AccountContext>
    {
        protected override void Seed(AccountContext context)
        {
            var sysUsers = new List<SysUser>
            {
                new SysUser {UserName="tom",Email="tom@qq.com",Password="1" },
                new SysUser {UserName="jeery",Email="jeery@qq.com",Password="2" }
            };
            sysUsers.ForEach(s => context.SysUsers.Add(s));
            context.SaveChanges();
            var sysRoles = new List<SysRole>
            {
                new SysRole {RoleName="admin",RoleDesc="administrator have all role" },
                  new SysRole {RoleName="User",RoleDesc="user have path role" }
            };

            sysRoles.ForEach(s => context.SysRoles.Add(s));
            context.SaveChanges();
        }
    }
}