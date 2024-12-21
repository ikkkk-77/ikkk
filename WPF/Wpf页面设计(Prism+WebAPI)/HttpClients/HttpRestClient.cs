using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf页面设计_Prism_WebAPI_.HttpClients
{
    /// <summary>
    /// 调用api工具类 (使用RestClient请求和接收)
    /// </summary>
    class HttpRestClient
    {
        private readonly RestClient client; //客户端

        private string baseUrl = "http://localhost:8085/api/";   // webapi发布后新baseurl

        /// <summary>
        /// 构造函数
        /// </summary>
        public HttpRestClient()
        {
            //赋值
            client = new RestClient();
        }


        /// <summary>
        /// 请求方法Execute
        /// </summary>
        /// <param name="apirequest">请求数据</param>
        /// <returns>接收的数据</returns>
        public ApiResponse Execute (ApiRequest apirequest)
        {
            // 创建RestRequest对象
            RestRequest restrequest = new RestRequest (apirequest.Method); // 请求方式

            restrequest.AddHeader("Content-Type", apirequest.ContentType); // 内容类型

            if (apirequest.Parameters != null) // 参数
            {
                // json序列化:SerializeObject (对象->字符串)
                restrequest.AddParameter("param", JsonConvert.SerializeObject(apirequest.Parameters), ParameterType.RequestBody);
            }

            client.BaseUrl = new Uri(baseUrl + apirequest.Route); //url

            var res = client.Execute(restrequest);  //请求

            // 成功则接收
            if (res.StatusCode == System.Net.HttpStatusCode.OK)
            {
                // json反序列化DeserializeObject  json->object
                return JsonConvert.DeserializeObject<ApiResponse>(res.Content);
            }
            else
            {
                return new ApiResponse { ResultCode = -99, Msg = "服务器繁忙.." };
            }
        }
    }
}
