using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf页面设计_Prism_WebAPI_.HttpClients
{
    /// <summary>
    /// 请求模型
    /// </summary>
    class ApiRequest
    {
        /// <summary>
        /// 请求地址/api路由地址
        /// </summary>
        public string Route {  get; set; }

        /// <summary>
        /// 请求方式(get/post/..)
        /// </summary>
        public Method Method { get; set; }
        
        /// <summary>
        /// 请求参数
        /// </summary>
        public object Parameters { get; set; }

        /// <summary>
        ///  发送数据类型
        /// </summary>
        public string ContentType { get; set; } = "application/json";
    }
}
