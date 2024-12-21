using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf页面设计_Prism_WebAPI_.Views
{
    /// <summary>
    /// LoginUC.xaml 的交互逻辑
    /// </summary>
    public partial class LoginUC : UserControl
    {

        // 事件聚合器变量
        private readonly IEventAggregator eventAggregator;

        // 构造器
        public LoginUC(IEventAggregator _eventAggregator)
        {
            InitializeComponent();

            // 赋值
            eventAggregator = _eventAggregator;

            // 订阅
            _eventAggregator.GetEvent<MegEvent>().Subscribe(Sub);
        }

        // 订阅委托方法
        private void Sub(string obj)
        {
            RegLogInBar.MessageQueue.Enqueue(obj);
        }
    }
}
