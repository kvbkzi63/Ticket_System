using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ForDemo.Enum;

namespace ForDemo.Models
{
    public class UserViewModel
    {
        [DisplayName("使用者帳號　")]
        public string UserId { get; set; }
        [DisplayName("使用者密碼　")]
        public string UserPd { get; set; }
        [DisplayName("使用者名稱　")]
        public string UserName { get; set; }
        [DisplayName("使用者權限　")]
        public EnumUtility.UserRoles Roles { get; set; }
    }
}
