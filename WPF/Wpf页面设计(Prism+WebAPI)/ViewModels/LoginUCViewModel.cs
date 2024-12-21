using Newtonsoft.Json;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wpf页面设计_Prism_WebAPI_.DTOs;
using Wpf页面设计_Prism_WebAPI_.HttpClients;

namespace Wpf页面设计_Prism_WebAPI_.ViewModel
{
    class LoginUCViewModel : BindableBase , IDialogAware
    {

        // 登录
        public DelegateCommand LoginCmm {  get; set; }
        // 登录注册改变界面
        public DelegateCommand<string> ChangeLogRegCmm { get; set; }
        // 注册
        public DelegateCommand Register {  get; set; }      

        // API工具类变量
        public readonly HttpRestClient httpRestClient;
        // 事件聚合器变量
        public readonly IEventAggregator eventAggregator;


        /// <summary>
        /// 构造函数
        /// </summary>
        public LoginUCViewModel(HttpRestClient _httpRestClient, IEventAggregator _eventAggregator)
        {
            // 赋值
            LoginCmm = new DelegateCommand(Login);
            ChangeLogRegCmm = new DelegateCommand<string>(changelogreg);
            Register = new DelegateCommand(register);

            User = new AccountInfoDTO();
            httpRestClient = _httpRestClient;
            eventAggregator = _eventAggregator;
        }


        #region 登录界面选中的索引
        private string selected { get; set; }
        public string Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                this.RaisePropertyChanged(nameof(Selected));
            }
        }
        #endregion

        #region 密码控件
        private string _Pwd;

        public string Pwd
        {
            get { return _Pwd; }
            set { 
                _Pwd = value;
                this.RaisePropertyChanged(nameof(Pwd));
            }
        }
        #endregion

        #region 用户注册登录信息
        private AccountInfoDTO _user;
        public AccountInfoDTO User
        {
            get { return _user; }
            set {
                _user = value;
                this.RaisePropertyChanged(nameof(User));
            }
        }
        #endregion




        /// <summary>
        /// 改变登录注册界面方法
        /// </summary>
        /// <param name="index"></param>
        private void changelogreg(string index)
        {
            // 根据索引值改变
            Selected = index;
        }


        /// <summary>
        /// 注册方法
        /// </summary>
        private void register()
        {
            // 注册信息验证
            if (string.IsNullOrEmpty(User.Name) || string.IsNullOrEmpty(User.Account) ||
                string.IsNullOrEmpty(User.Pwd) || string.IsNullOrEmpty(User.ConfirmPwd) )
            {
                // 发布
                eventAggregator.GetEvent<MegEvent>().Publish("信息不全,请重新填写");
                return;
            }
            if (User.Pwd != User.ConfirmPwd)
            {
                // 发布
                eventAggregator.GetEvent<MegEvent>().Publish("密码不一致,请重新填写");
                return;
            }


            // 调用API
            ApiRequest apiRequest = new ApiRequest(); // 创建自定义Api请求对象
            apiRequest.Method = RestSharp.Method.POST; // 请求方法
            apiRequest.Route = "account_/Reg";  // 赋值WebAPI的路由

            // 对密码进行加密处理
            User.Pwd = Md5Helper.GetMd5(User.Pwd);
            User.ConfirmPwd = Md5Helper.GetMd5(User.ConfirmPwd);

            apiRequest.Parameters = User; // 传参

            ApiResponse apiResponse = httpRestClient.Execute(apiRequest); //API结果响应

            // 结果显示
            if (apiResponse.ResultCode == 1)
            {
                // 发布
                eventAggregator.GetEvent<MegEvent>().Publish(apiResponse.Msg);
                // 返回登录
                Selected = "0";
            }
            else // 发布
                eventAggregator.GetEvent<MegEvent>().Publish(apiResponse.Msg);
        }



        /// <summary>
        /// 登录方法
        /// </summary>
        private void Login()
        {
            // 登录信息验证
            if (string.IsNullOrEmpty(User.Account) || string.IsNullOrEmpty(User.Pwd))
            {
                //发布
                eventAggregator.GetEvent<MegEvent>().Publish("登录失败,填写不完整");
                return;
            }


            // 调用ApI
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.POST;
            apiRequest.ContentType = "application/json";
            apiRequest.Route = "account_/Login";
            apiRequest.Parameters = User; 

            // 执行,接收APIiResponse对象
            ApiResponse apiresponse = httpRestClient.Execute(apiRequest);

            // 判断登录成功
            if (apiresponse.ResultCode == 1)
            {
                // 发布
                eventAggregator.GetEvent<MegEvent>().Publish("登录成功");

                // 进入主界面
                if (RequestClose != null)
                {
                    // AccountInfoDTO对象反序列化接收登录信息
                    AccountInfoDTO accountdto = JsonConvert.DeserializeObject<AccountInfoDTO>
                        (apiresponse.ResultData.ToString());

                    // 传递参数给主界面
                    DialogParameters paras = new DialogParameters(); // 创建传递参数对象
                    paras.Add("userName",accountdto.Name);  // 添加

                    // 返回OK按钮结果和登录姓名
                    RequestClose(new DialogResult(ButtonResult.OK,paras));
                }
            }
            else
            {
                //发布
                eventAggregator.GetEvent<MegEvent>().Publish("登录失败,填写不完整");
            }
        }



        #region 实现接口
        public string Title => "登录界面";

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }
    #endregion
    }
}
