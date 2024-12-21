using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wpf页面设计_Prism_WebAPI_.DTOs;
using Wpf页面设计_Prism_WebAPI_.HttpClients;

// 待办事项页
namespace Wpf页面设计_Prism_WebAPI_.ViewModels
{
    internal class ToDOViewModel : BindableBase,INavigationAware
    {
        // API工具类
        private readonly HttpRestClient httpClient;

        // 打开对话框委托
        public DelegateCommand OpenWaitDialogcmm { get; set; }

        // 添加对话框信息委托
        public DelegateCommand AddWaitDialogcmm { get; set; }

        // 查询代办事项数据委托
        public DelegateCommand SearchWaitDatacmm { get; set; }

        // 删除待办事项数据委托
        public DelegateCommand<WaitInfoDTO> DeleteWaitDatacmm { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        public ToDOViewModel(HttpRestClient _httpClient)
        {
            #region 初始化

            // 初始化对话框信息数据
            waitiDTO = new WaitInfoDTO();

            // 初始化代办事项数据
            WaitList = new List<WaitInfoDTO>();

            // 初始化API工具类
            this.httpClient = _httpClient;

            #endregion

            #region 赋值

            // 打开对话框委托
            OpenWaitDialogcmm = new DelegateCommand(openclosedialog);
            // 添加对话框信息委托
            AddWaitDialogcmm = new DelegateCommand(AddWaitdialog);
            // 搜索委托
            SearchWaitDatacmm = new DelegateCommand(SearchWaitData);
            // 删除委托
            DeleteWaitDatacmm = new DelegateCommand<WaitInfoDTO>(DeleteWaitData);
            #endregion
        }

        #region 待办事项数据
        private List<WaitInfoDTO> waitlist;
        public List<WaitInfoDTO> WaitList
        {
            get { return waitlist; }
            set { waitlist = value; this.RaisePropertyChanged(nameof(WaitList)); }
        }
        #endregion

        #region 需要查询数据
        // 搜索标题
        public string SearchTitle { get; set; }

        // 搜索索引
        private int searchIndex;

        public int SearchIndex
        {
            get { return searchIndex; }
            set { searchIndex = value;  this.RaisePropertyChanged(nameof(SearchIndex)); }
        }

        #endregion

        #region 对话框开启或关闭状态(默认关闭)
        private bool Isrightdrawerstatus = false;

        public bool IsRightDrawerStatus
        {
            get
            {
                return Isrightdrawerstatus;
            }
            set
            {
                Isrightdrawerstatus = value;
                this.RaisePropertyChanged(nameof(IsRightDrawerStatus));
            }
        }
        #endregion

        #region 对话框输入的数据
        private WaitInfoDTO waitiDTO;

        public WaitInfoDTO WaitDTO
        {
            get { return waitiDTO; }
            set { 
                waitiDTO = value;
                this.RaisePropertyChanged(nameof(WaitDTO));
            }
        }

        #endregion



        /// <summary>
        /// 打开关闭添加实现对话框方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openclosedialog()
        {
            // 通过判断菜单栏对话框的状态来开启或关闭
            if (IsRightDrawerStatus == false)
            {
                IsRightDrawerStatus = true;
            }
            else IsRightDrawerStatus = false;
        }


        /// <summary>
        /// 添加对话框信息到代办事项中
        /// </summary>
        private void AddWaitdialog()
        {
            // 调用API实现添加代办事项
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.Method = RestSharp.Method.POST;
            apiRequest.ContentType = "application/json";
            apiRequest.Route = "Wait/AddWaitData";
            apiRequest.Parameters = WaitDTO;

            // 执行
            ApiResponse response =  httpClient.Execute(apiRequest);

            if (response.ResultCode == 1)
            {
                // 刷新面板
                SearchWaitData();

                // 关闭对话框
                IsRightDrawerStatus = false;
            }

            if (response.ResultCode == -99)
            {
                MessageBox.Show("添加失败,已存在该待办事项");
            }
        }


        /// <summary>
        /// 搜索待办事项方法
        /// </summary>
        private void SearchWaitData()
        {
            // 对搜索索引进行设置
            int? searchindex = null;
            if (SearchIndex == 0)
            {
                searchindex = null;
            }
            if (SearchIndex == 1)
            {
                searchindex = 0;
            }
            if(SearchIndex == 2) 
            {
                searchindex = 1;
            }


            //调用API搜索
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.ContentType = "application/json";
            apiRequest.Method = RestSharp.Method.GET;
            apiRequest.Route = $"Wait/SearchWait?title={SearchTitle}&status={searchindex}";  // 传递参数直接拼接在地址后面

            // 执行
            ApiResponse response = httpClient.Execute(apiRequest);

            if (response.ResultCode == 1)
            {
                // 反序列化接收
                WaitList = JsonConvert.DeserializeObject<List<WaitInfoDTO>>
                                            (response.ResultData.ToString());
            }
            else MessageBox.Show("搜索失败，找不到该事项");
        }


        /// <summary>
        /// 删除待办事项数据
        /// </summary>
        private void DeleteWaitData(WaitInfoDTO waitInfoDTO)
        {
            // 提示是否删除
            var Result = MessageBox.Show("是否删除该待办事项", "温馨提示", MessageBoxButton.OKCancel);

            // 确定
            if (Result == MessageBoxResult.OK)
            {
                // 调用API删除
                ApiRequest apiRequest = new ApiRequest();
                apiRequest.ContentType = "application/json";
                apiRequest.Method= RestSharp.Method.DELETE;
                apiRequest.Route = $"Wait/DeleteWaitData?WaitId={waitInfoDTO.WaitId}";

                // 执行
                ApiResponse response = httpClient.Execute(apiRequest);

                if (response.ResultCode == 1)
                {
                    // 刷新面板
                    SearchWaitData();
                }
                else
                {
                    MessageBox.Show("删除失败");
                }
            }
            else
            {
                return;
            }
        }



        /// <summary>
        /// 实现InavigationAware接口接收首页切换视图的参数
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // 接收视图传递参数
            if (navigationContext.Parameters.ContainsKey("SelectIndex"))
            {
                // 接收
                SearchIndex = navigationContext.Parameters.GetValue<int>("SelectIndex");
            }
            else {
                // 无参数时, 默认搜索全部
                SearchIndex = 0;
            }

            // 刷新面板数据(搜索)
            SearchWaitData();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
           
        }
    }
}
