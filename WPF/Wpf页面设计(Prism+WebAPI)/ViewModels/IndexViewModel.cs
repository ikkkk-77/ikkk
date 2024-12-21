using DailyApp.WPF.Service;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wpf页面设计_Prism_WebAPI_.DTOs;
using Wpf页面设计_Prism_WebAPI_.HttpClients;
using Wpf页面设计_Prism_WebAPI_.Model;
using Wpf页面设计_Prism_WebAPI_.Models;

namespace Wpf页面设计_Prism_WebAPI_.ViewModels
{
    /// <summary>
    /// 首页视图模型
    /// </summary>
    internal class IndexViewModel : BindableBase,INavigationAware
    {
        // 登录用户姓名
        string loginName;

        // 自定义对话框服务变量
        private readonly DialogHostService DialoghostService;
        // API工具类变量
        private readonly HttpRestClient httpRestClient;
        // 区域管理变量
        private IRegionManager regionManager;

        /// <summary>
        /// 视图转换委托
        /// </summary>
        public DelegateCommand<stackpanelModel> RegionManagercmm {  get; set; }

        #region 添加编辑对话框委托

        // 打开添加待办事项对话框委托
        public DelegateCommand OpenDiaLogAddWaitCmm {  get; set; }

        // 打开添加备忘录对话框委托
        public DelegateCommand OpenDiaLogAddMeMoCmm { get; set; }

        // 打开编辑待办事项对话框委托
        public DelegateCommand<WaitInfoDTO> OpenDiaLogEditWaitCmm { get; set; }

        // 打开编辑备忘录对话框委托
        public DelegateCommand<MeMoInfoDTO> OpenDialogEditMeMoCmm { get; set; }

        #endregion

        #region 切换状态委托

        // 切换代办状态委托
        public DelegateCommand<WaitInfoDTO> ChangeWaitStatusCmm { get; set; }

        // 切换备忘录状态委托
        public DelegateCommand<MeMoInfoDTO> ChangeMeMoStatusCmm { get; set; }

        #endregion


        /// <summary>
        ///  构造函数
        /// </summary>
        public IndexViewModel(DialogHostService _dialoghostService, HttpRestClient _httpRestClient, IRegionManager _regionManager)
        {
            #region 初始化

            #region 初始化统计面板数据
            StackPanelList = new List<stackpanelModel>();
            StackPanelList.Add(new stackpanelModel() { Icon = "ClockFast", ItemName = "汇总",Backcolor = "#FF0CA0FF", ViewName = "ToDoViewUC" });
            StackPanelList.Add(new stackpanelModel() { Icon = "ClockCheckOutline", ItemName = "已完成", Backcolor = "#FF1ECA3A", ViewName = "ToDoViewUC" });
            StackPanelList.Add(new stackpanelModel() { Icon = "ChartLineVariant", ItemName = "完成比例", Backcolor = "#FF02C6DC"});
            StackPanelList.Add(new stackpanelModel() { Icon = "PlaylistStar", ItemName = "备忘录", Backcolor = "#FFFFA000", ViewName = "MemoViewUC" });
            #endregion

            // 初始化代办事项列表对象
            WaitList = new List<WaitInfoDTO>();

            // 初始化备忘录列表对象
            MeMoList = new List<MeMoInfoDTO>();

            #endregion

            #region 赋值

            // 变量
            DialoghostService = _dialoghostService;
            httpRestClient = _httpRestClient;
            regionManager = _regionManager;

            // 委托
            OpenDiaLogAddWaitCmm = new DelegateCommand(Opendialog);
            OpenDiaLogAddMeMoCmm = new DelegateCommand(Opendialog2);
            OpenDiaLogEditWaitCmm = new DelegateCommand<WaitInfoDTO>(EditWaitDialog);
            OpenDialogEditMeMoCmm = new DelegateCommand<MeMoInfoDTO>(EditMeMoDialog);
            ChangeWaitStatusCmm = new DelegateCommand<WaitInfoDTO>(ChangeWaitStatus);
            ChangeMeMoStatusCmm = new DelegateCommand<MeMoInfoDTO>(ChangeMeMoStatus);
            RegionManagercmm = new DelegateCommand<stackpanelModel>(RegionChange);
            #endregion

            #region 调用通过WebAPI获取待办事项和备忘录面板统计和列表数据方法

            GetWaitData();
            GetMeMoData();
            GetWaitDataDetail();
            GetMeMoDataDetail();
            #endregion
        }



        #region 标题
        private string time;
        public string Time
        {
            get
            {
                // 返回自定义字符
                string str1 = string.Format("你好,{0} 今天是:",loginName);
                // 返回日期
                string str2 = DateTime.Now.ToString("yyyy-MM-dd");
                // 返回星期
                int index = (int)DateTime.Now.DayOfWeek;  //获取当前星期的索引
                string[] week = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
                string str3 = week[index];

                return str1 + str2 + str3;
            }
            set { time = value; }
        }
        #endregion

        #region 统计面板数据
        private List<stackpanelModel> stackpanellist;
        public List<stackpanelModel> StackPanelList
        {
            get { return stackpanellist; }
            set { stackpanellist = value; this.RaisePropertyChanged(nameof(StackPanelList)); }
        }
        #endregion

        #region 待办事项数据
        private List<WaitInfoDTO> waitlist;
        public List<WaitInfoDTO> WaitList
        {
            get { return waitlist; }
            set { waitlist = value; this.RaisePropertyChanged(nameof(WaitList)); }
        }
        #endregion

        #region 备忘录数据
        private List<MeMoInfoDTO> memolist;
        public List<MeMoInfoDTO> MeMoList
        {
            get { return memolist; }
            set { memolist = value; this.RaisePropertyChanged(nameof(MeMoList)); }
        }
        #endregion


        
        /// <summary>
        /// 打开待办事项对话框方法
        /// </summary>
        private async void Opendialog() // async 异步
        {
            // 打开对话框
            var result = await DialoghostService.ShowDialog("AddWaitDiaLogUC", null);  // await 等待

            // 接收数据对象WaitInfoDTO(展示在待办事项列表中)
            if (result.Result == ButtonResult.OK)
            {
                if (result.Parameters.ContainsKey("WaitInfoDTO"))
                {
                    //接收数据对象WaitInfoDTO
                    WaitInfoDTO waitInfoDTO = result.Parameters.GetValue<WaitInfoDTO>("WaitInfoDTO");


                //添加WaitInfoDTO对象数据进数据库
                    // 调用API 
                    ApiRequest apiRequest = new ApiRequest();
                    apiRequest.ContentType = "application/json";
                    apiRequest.Method = RestSharp.Method.POST;
                    apiRequest.Parameters = waitInfoDTO;
                    apiRequest.Route = "Wait/AddWaitData";
                    // 执行
                    ApiResponse apiResponse = httpRestClient.Execute(apiRequest);

                    // 判断
                    if (apiResponse.ResultCode == 1)
                    {    // 执行成功
                        MessageBox.Show("待办事项添加成功");

                        // 刷新面板数据和列表数据
                        GetWaitData();
                        GetWaitDataDetail();
                        GetMeMoDataDetail();
                    }

                    if (apiResponse.ResultCode == -99)
                    {
                        MessageBox.Show("该待办事项已存在");
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 打开备忘录对话框方法
        /// </summary>
        private async void Opendialog2() // async 异步
        {
            // 打开对话框
            var result = await DialoghostService.ShowDialog("AddMeMoDiaLogUC", null);  // await 等待

            // 接收数据对象WaitInfoDTO(展示在待办事项列表中)
            if (result.Result == ButtonResult.OK)
            {
                if (result.Parameters.ContainsKey("MeMoInfoDTO"))
                {
                    //接收数据对象MeMoInfoDTO
                    MeMoInfoDTO meMoInfoDTO = result.Parameters.GetValue<MeMoInfoDTO>("MeMoInfoDTO");


                    //添加WaitInfoDTO对象数据进数据库
                    // 调用API 
                    ApiRequest apiRequest = new ApiRequest();
                    apiRequest.ContentType = "application/json";
                    apiRequest.Method = RestSharp.Method.POST;
                    apiRequest.Parameters = meMoInfoDTO;
                    apiRequest.Route = "MeMo/AddMeMoData";
                    // 执行
                    ApiResponse apiResponse = httpRestClient.Execute(apiRequest);

                    // 判断
                    if (apiResponse.ResultCode == 1)
                    {    // 执行成功
                        MessageBox.Show("备忘录添加成功");

                        // 刷新面板数据和列表数据
                        GetMeMoDataDetail();
                        GetMeMoData();
                    }

                    if (apiResponse.ResultCode == -99)
                    {
                        MessageBox.Show("该备忘录已存在");
                        return;
                    }
                }
            }
        }


        /// <summary>
        /// 打开编辑待办事项对话框方法
        /// </summary>
        /// <param name="dTO"></param>
        private async void EditWaitDialog(WaitInfoDTO waitInfoDTOs)  // async 异步
        {
            // 接收选中的waitinfoDTO并传递给对话框
            DialogParameters paras = new DialogParameters();
            paras.Add("OldWaitDTO", waitInfoDTOs);

            // 打开对话框
            var result = await DialoghostService.ShowDialog("EditWaitDialog", paras);  // await 等待


            // 接收数据对象WaitInfoDTO(展示在待办事项列表中)
            if (result.Result == ButtonResult.OK)
            {
                if (result.Parameters.ContainsKey("NewWaitDTO"))
                {
                    //接收修改后的数据对象WaitInfoDTO
                    WaitInfoDTO waitInfoDTO = result.Parameters.GetValue<WaitInfoDTO>("NewWaitDTO");


                    //添加WaitInfoDTO对象数据进数据库
                    // 调用API 
                    ApiRequest apiRequest = new ApiRequest();
                    apiRequest.ContentType = "application/json";
                    apiRequest.Method = RestSharp.Method.PUT;
                    apiRequest.Parameters = waitInfoDTO;
                    apiRequest.Route = "Wait/EditWaitData";
                    // 执行
                    ApiResponse apiResponse = httpRestClient.Execute(apiRequest);

                    // 判断
                    if (apiResponse.ResultCode == 1)
                    {    // 执行成功
                        MessageBox.Show("待办事项修改成功");

                        // 刷新面板数据和列表数据
                        GetWaitData();
                        GetWaitDataDetail();
                    }

                    if (apiResponse.ResultCode == -99)
                    {
                        MessageBox.Show("该待办事项已存在");
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 打开编辑备忘录对话框方法
        /// </summary>
        /// <param name="dTO"></param>
        private async void EditMeMoDialog(MeMoInfoDTO meMoInfoDTO)   // await异步
        {
            // 打开对话框,并传递选中的备忘录信息给对话框
            DialogParameters pas = new DialogParameters();
            pas.Add("OldMeMoData", meMoInfoDTO);

            // 打开
            var result = await DialoghostService.ShowDialog("EditMeMoDialog", pas);

            // 对话框关闭后接收新信息
            if (result.Result == ButtonResult.OK)
            {
                if (result.Parameters.ContainsKey("NewMeMoData"))
                {
                    // 接收
                    MeMoInfoDTO meMoInfo = result.Parameters.GetValue<MeMoInfoDTO>("NewMeMoData");

                    // 调用API添加
                    ApiRequest apiRequest = new ApiRequest();
                    apiRequest.ContentType = "application/json";
                    apiRequest.Method= RestSharp.Method.POST;
                    apiRequest.Route = "MeMo/EditMeMoData";
                    apiRequest.Parameters = meMoInfo;

                    ApiResponse response = httpRestClient.Execute(apiRequest);

                    if (response.ResultCode == 1)
                    {
                        MessageBox.Show("修改成功");

                        // 刷新
                        GetMeMoData();
                        GetMeMoDataDetail();
                    }
                    if (response.ResultCode == -99)
                    {
                        MessageBox.Show("该待备忘录不存在");
                        return;
                    }
                }

            }
        }


        /// <summary>
        /// 接收待办事项统计数据方法
        /// </summary>
        /// <param name="navigationContext"></param>
        private void GetWaitData()
        {
            // 调用API 
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.GET;
            apiRequest.ContentType = "application/json";
            apiRequest.Route = "Wait/GetWaitData";

            // 执行并接收响应变量
            ApiResponse apiResponse =  httpRestClient.Execute(apiRequest);

            if (apiResponse.ResultCode == 1)
            {
                // 执行成功,反序列化接收
                WaitInfoDTO waitInfoDTO = JsonConvert.DeserializeObject<WaitInfoDTO>
                    (apiResponse.ResultData.ToString());


                // 更新代办实现统计数据
                StackPanelList[0].Result = waitInfoDTO.TotalCount.ToString();
                StackPanelList[1].Result = waitInfoDTO.FinishCount.ToString();
                StackPanelList[2].Result = waitInfoDTO.FinishPercent.ToString();
            }
        }


        /// <summary>
        ///  接收待办事项具体的数据 (在列表显示)
        /// </summary>
        private void GetWaitDataDetail()
        {
            // 调用API 
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.GET;
            apiRequest.ContentType = "application/json";
            apiRequest.Route = "Wait/GetWaitDataDetail";

            // 执行并接收响应变量
            ApiResponse apiResponse = httpRestClient.Execute(apiRequest);

            // 判断
            if (apiResponse.ResultCode == 1)
            {
                // 反序列接收并添加 (List<WaitInfoDTO>类型)
                WaitList = JsonConvert.DeserializeObject<List<WaitInfoDTO>>
                                        (apiResponse.ResultData.ToString());

            }
        }



        /// <summary>
        ///  接收备忘录统计数据方法
        /// </summary>
        private void GetMeMoData()
        {
            // 调用API 
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method= RestSharp.Method.GET;
            apiRequest.ContentType = "application/json";
            apiRequest.Route = "MeMo/GetMeMoData";

            // 执行并接收响应对象
            ApiResponse apiResponse = httpRestClient.Execute (apiRequest);

            // 判断
            if (apiResponse.ResultCode == 1)
            {
                // 接收数据
                MeMoInfoDTO meMoInfoDTO = JsonConvert.DeserializeObject<MeMoInfoDTO>
                    (apiResponse.ResultData.ToString());

                StackPanelList[3].Result = meMoInfoDTO.TotalCount.ToString();
            }
        }


        /// <summary>
        ///  接收备忘录具体的数据 (在列表显示)
        /// </summary>
        private void GetMeMoDataDetail()
        {
            // 调用API 
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.GET;
            apiRequest.ContentType = "application/json";
            apiRequest.Route = "MeMo/GetMeMoDataDetail";

            // 执行并接收响应变量
            ApiResponse apiResponse = httpRestClient.Execute(apiRequest);

            // 判断
            if (apiResponse.ResultCode == 1)
            {
                // 反序列接收并添加 (List<WaitInfoDTO>类型)
                MeMoList = JsonConvert.DeserializeObject<List<MeMoInfoDTO>>
                                        (apiResponse.ResultData.ToString());

            }
        }




        /// <summary>
        /// 改换待办事项状态方法
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void ChangeWaitStatus(WaitInfoDTO waitInfoDTO)
        {
            if (waitInfoDTO != null)
            {
                // 调用API 
                ApiRequest apiRequest = new ApiRequest();
                apiRequest.Method = RestSharp.Method.PUT;
                apiRequest.ContentType = "application/json";
                apiRequest.Route = "Wait/UpdateStatus";
                apiRequest.Parameters = waitInfoDTO;


                // 执行并接收响应变量
                ApiResponse apiResponse = httpRestClient.Execute(apiRequest);

                if (apiResponse.ResultCode == 1)
                {
                    GetWaitData();
                    GetWaitDataDetail();
                }
            }
        }

        /// <summary>
        ///  改变备忘录状态方法
        /// </summary>
        /// <param name="dTO"></param>
        private void ChangeMeMoStatus(MeMoInfoDTO memoinfodTO)
        {
            if (memoinfodTO != null)
            {
                ApiRequest apiRequest = new ApiRequest();
                apiRequest.Method= RestSharp.Method.PUT;
                apiRequest.ContentType = "application/json";
                apiRequest.Route = "MeMo/UpdateMeMoStatus";
                apiRequest.Parameters= memoinfodTO;

                ApiResponse apiResponse = httpRestClient.Execute(apiRequest);

                if (apiResponse.ResultCode == 1)
                {
                    // 刷新数据
                    GetMeMoData();
                    GetMeMoDataDetail();
                }
            }


        }



        /// <summary>
        /// 视图改变方法
        /// </summary>
        /// <param name="obj"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void RegionChange(stackpanelModel stackpanel)
        {
            // 导航转换视图
            if (stackpanel!=null && !string.IsNullOrEmpty(stackpanel.ViewName))
            {
                // 判断在点击已完成时,传递搜索为"已完成"的下标
                if (stackpanel.ItemName == "已完成")
                {
                    // 视图传递参数 (在待办事项页用InavigationAware接口接收)
                    NavigationParameters pas = new NavigationParameters();
                    pas.Add("SelectIndex", 2);

                    regionManager.Regions["ContentRegion"].RequestNavigate(stackpanel.ViewName,pas);
                }

                else
                {
                    regionManager.Regions["ContentRegion"].RequestNavigate(stackpanel.ViewName);
                }
            }
            
        }


        #region 用户控件接收登录页导航传递的参数,实现接口INavigationAware

        /// <summary>
        /// 当视图模型导航到时调用
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // 接收登录的用户参数
            if (navigationContext.Parameters.ContainsKey("username"))
            {
                loginName = navigationContext.Parameters.GetValue<string>("username");
            }
        }


        /// <summary>
        /// 当视图模型导航离开时调用
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
        
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }
        #endregion
    }
}
