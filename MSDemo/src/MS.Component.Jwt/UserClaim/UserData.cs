using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Component.Jwt.UserClaim
{
    /// <summary>
    /// 定义用户数据类
    /// </summary>
    public class UserData
    {
        public long Id { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RoleName { get; set; }
        public string RoleDisplayName { get; set; }


        public string Token { get; set; }
    }
}
