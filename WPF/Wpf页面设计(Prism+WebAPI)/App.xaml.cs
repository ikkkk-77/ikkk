using DailyApp.WPF.Service;
using DryIoc;
using Prism.Common;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System.Configuration;
using System.Data;
using System.Windows;
using Wpf页面设计_Prism_WebAPI_.HttpClients;
using Wpf页面设计_Prism_WebAPI_.ViewModel;
using Wpf页面设计_Prism_WebAPI_.ViewModels;
using Wpf页面设计_Prism_WebAPI_.ViewModels.SettingViewModel;
using Wpf页面设计_Prism_WebAPI_.Views;
using Wpf页面设计_Prism_WebAPI_.Views.DiaLog;
using Wpf页面设计_Prism_WebAPI_.Views.SettingUC;

namespace Wpf页面设计_Prism_WebAPI_
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        /// <summary>
        /// 依赖注入方法
        /// </summary>
        /// <param name="containerRegistry"></param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 注入主窗口和ViewModel
            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();

            // 注入登录用户控件(对话框)
            containerRegistry.RegisterDialog<LoginUC, LoginUCViewModel>();

            // 注入API工具类
            containerRegistry.GetContainer().Register<HttpRestClient>(made: Parameters.Of.Type<string>(serviceKey: "weburl"));

            // 注入首页用户控件
            containerRegistry.RegisterForNavigation<IndexViewUC,IndexViewModel>();
            // 注入待办事项用户控件
            containerRegistry.RegisterForNavigation<ToDoViewUC,ToDOViewModel>();
            // 注入备忘录用户控件
            containerRegistry.RegisterForNavigation<MemoViewUC,MeMoViewModel>();
            // 注入设置页用户控件
            containerRegistry.RegisterForNavigation<SettingViewUC,SetttingViewModel>();

            // 注入设置页的个性化用户控件
            containerRegistry.RegisterForNavigation<PersonalUC, PersonalViewModel>();
            // 注入设置页的系统设置用户控件
            containerRegistry.RegisterForNavigation<SysSetUC,SysSetViewModel>();
            // 注入设置页的更多设置用户控件
            containerRegistry.RegisterForNavigation<AboutUsUC,AboutUsViewModel>();

            // 注入添加待办事项对话框
            containerRegistry.RegisterForNavigation<AddWaitDiaLogUC, AddWaitDialogViewModel>();
            // 注入添加备忘录对话框
            containerRegistry.RegisterForNavigation<AddMeMoDiaLogUC, AddMeMoDialogViewModel>();
            // 自定义对话框服务
            containerRegistry.Register<DialogHostService>();
            // 注入编辑待办事项对话框
            containerRegistry.RegisterForNavigation<EditWaitDialog, EditWaitDialogViewModel>();
            // 注入编辑备忘录对话框
            containerRegistry.RegisterForNavigation<EditMeMoDialog, EditMeMoDialogViewModel>();
        }


        /// <summary>
        /// 初始化方法
        /// </summary>
        protected override void OnInitialized()
        {
            // 创建对话框对象
            var dialog = Container.Resolve<DialogService>();
            // 显示登录对话框
            dialog.ShowDialog("LoginUC",
                callback =>
                {
                    // 返回OK按钮则退出登录对话框
                    if (callback.Result != ButtonResult.OK)
                    {
                        // 退出
                        Environment.Exit(0);
                        return;
                    }

                    string str = "";

                    // 接收登录用户名字
                    if (callback.Parameters.ContainsKey("userName"))
                    {
                        str = callback.Parameters.GetValue<string>("userName");
                    }

                    // 设置主界面的默认页面
                    var mainVM = Current.MainWindow.DataContext as MainWindowViewModel;  //当前主界面数据上下文
                    if (mainVM != null)
                    {
                        // 调用viewmodel定义的方法
                        mainVM.DefaultView(str);
                    }

                    // 显示主窗口
                    base.OnInitialized();
                });
        }
    }

}
