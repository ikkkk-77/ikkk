using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf页面设计_Prism_WebAPI_.DTOs
{
    /// <summary>
    /// 备忘录DTO
    /// </summary>
    internal class MeMoInfoDTO: BindableBase
    {
        // ID
        [Key]
        public int WaitId { get; set; }

        // 标题
        public string Title { get; set; }

        // 内容
        public string Content { get; set; }

        // 状态 0-代办 1-已完成
        public int Status { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string Color
        {
            get
            {
                return Status == 0 ? "#FFC0CB" : "#1E90EF";
            }
        }

        /// <summary>
        /// 备忘录总数
        /// </summary>
        public int TotalCount {get;set;}
    }
}
