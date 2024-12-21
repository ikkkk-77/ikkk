using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf页面设计_Prism_WebAPI_.Models
{
    /// <summary>
    /// 首页统计面板信息
    /// </summary>
    internal class stackpanelModel:BindableBase
    {
        // 图标
        public string Icon { get; set; }
        // 统计项名称
        public string ItemName { get; set; }

        //统计项结果(进行通知更改)
        private string result;
        public string Result 
        { 
            get { return result; } 
            set { result = value;  RaisePropertyChanged(); } 
        }

        // 背景颜色
        public string Backcolor { get; set; }

        //界面名称
        public string ViewName { get; set; }

        // 面板点击手
        public string Hand {
            get 
            {
                if (ViewName != null)
                {
                    return "Hand";
                }
                else
                {
                    return "";
                }
            }

        }
    }
}
