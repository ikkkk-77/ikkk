using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wpf页面设计WebAPI.DataModel
{
    /// <summary>
    /// 登录账号数据模型
    /// </summary>
    
    [Table("AccountInfo")]
    public class AccountInfo
    {
        [Key] // 主键(自增)
        public int AccountId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }
    }
}
