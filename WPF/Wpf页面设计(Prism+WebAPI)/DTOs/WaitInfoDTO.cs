using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf页面设计_Prism_WebAPI_.DTOs
{
    /// <summary>
    /// 待办事项DTO
    /// </summary>
    internal class WaitInfoDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int WaitId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string Color
        { 
            get 
            {
                return Status == 0 ? "#1E90EF" : "#3CB371"; 
            } 
        }

        /// <summary>
        /// 待办事项总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 已完成数量
        /// </summary>
        public int FinishCount { get; set; }

        /// <summary>
        /// 完成比例
        /// </summary>
        public string FinishPercent
        {
            get
            {
                if (TotalCount == 0)
                {
                    return "0.00%";
                }
                return (FinishCount * 100.00 / TotalCount).ToString("f2");
            }
        }
    }
}
