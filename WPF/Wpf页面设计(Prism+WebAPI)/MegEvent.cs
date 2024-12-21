using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf页面设计_Prism_WebAPI_
{
    /// <summary>
    /// 发布 订阅 消息信息事件类
    /// </summary>
     internal class MegEvent:PubSubEvent<string>
    {
    }
}
