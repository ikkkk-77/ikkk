using DailyApp.WPF.Service;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wpf页面设计_Prism_WebAPI_.DTOs;
using Wpf页面设计_Prism_WebAPI_.HttpClients;

namespace Wpf页面设计_Prism_WebAPI_.ViewModels
{
    /// <summary>
    /// 添加实现对话框
    /// </summary>
    class AddWaitDialogViewModel : IDialogHostAware
    {
        /// <summary>
        /// 确定命令
        /// </summary>
        public DelegateCommand SaveCommand { get; set; }

        /// <summary>
        /// 取消命令
        /// </summary>
        public DelegateCommand CancelCommand { get; set; }

        /// <summary>
        /// 打开过程执行
        /// </summary>
        /// <param name="parameters"></param>
        public void OnDialogOpening(IDialogParameters parameters)
        {}


//============================================================================================

        // HttpRestClient变量
        private readonly HttpRestClient httpRestClient;

        // 待办事项对象
        public WaitInfoDTO WaitInfoDTO { get; set; } = new WaitInfoDTO();

        /// <summary>
        /// 构造函数
        /// </summary>
        public AddWaitDialogViewModel(HttpRestClient _HttpRestClient)
        {
            // 赋值
            SaveCommand = new DelegateCommand(save);
            CancelCommand = new DelegateCommand(cancel);

            httpRestClient = _HttpRestClient;
        }


        // 保存方法
        private void save()
        {
            // 数据基本验证
            if (string.IsNullOrEmpty(WaitInfoDTO.Title) && string.IsNullOrEmpty(WaitInfoDTO.Content) && WaitInfoDTO.Status == null)
            {
                MessageBox.Show("填写待办事项不全");
            }
            else
            {
                //传递WaitInfoDTO给首页
                    // 创建DialogParameters对象
                    DialogParameters parameters = new DialogParameters();
                    parameters.Add("WaitInfoDTO", WaitInfoDTO);

                    // 传递添加待办事项对象给首页
                    if (DialogHost.IsDialogOpen("RootDialog"))
                    {
                        // 关闭并传递
                        DialogHost.Close("RootDialog", new DialogResult(ButtonResult.OK, parameters));
                    }
                }
            }

        // 取消方法
        private void cancel()
        {
            if (DialogHost.IsDialogOpen("RootDialog"))
            {
                DialogHost.Close("RootDialog",new DialogResult(ButtonResult.No));
            }
        }
    }
}
