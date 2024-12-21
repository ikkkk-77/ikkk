using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf页面设计_Prism_WebAPI_.DTOs
{
    /// <summary>
    /// 账号DTO
    /// </summary>
    public class AccountInfoDTO
    {
        //姓名
        public string Name { get; set; }

        //账号
        public string Account { get; set; }

        // 密码
        public string Pwd { get; set; }

        // 确认密码
        public string ConfirmPwd { get; set; }
    }
}
