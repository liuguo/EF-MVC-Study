using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace efef.Models
{
    public class SysUserRole
    {
        public int ID { get; set; }
        public string SysUserID { get; set; }
        public string SysRoleID { get; set; }
        public  ICollection<SysUserRole> SysUser { get; set; }
        public  ICollection<SysUserRole> SysRole { get; set; }
    }
}