using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Wpf页面设计_Prism_WebAPI_.HttpClients
{
    /// <summary>
    ///  MD5密码加密工具类
    /// </summary>
    internal class Md5Helper
    {
        /// <summary>
        /// MD5 utf-8编码 32
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string GetMd5(string content)
        {
            return string.Join("", MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(content)).Select(x=>x.ToString("x2")));
        }
    }
}
