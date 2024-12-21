using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf页面设计_Prism_WebAPI_.HttpClients
{
    /// <summary>
    /// 接收模型
    /// </summary>
    class ApiResponse
    {
        /// <summary>
        ///  结果编码
        /// </summary>
        public int ResultCode { get; set; }

        /// <summary>
        /// 结果信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        ///  数据
        /// </summary>
        public object ResultData { get; set; }
    }
}
