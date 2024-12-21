using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wpf页面设计_Prism_WebAPI_.DTOs;
using Wpf页面设计_Prism_WebAPI_.HttpClients;

namespace Wpf页面设计_Prism_WebAPI_.ViewModels
{
    internal class MeMoViewModel : BindableBase
    {
        // 初始化API工具类
        private readonly HttpRestClient httpRestClient;

        //打开备忘录菜单栏对话框委托
        public DelegateCommand OpenMeMoDialogcmm { get; set; }

        // 添加备忘录对话框信息委托
        public DelegateCommand AddMeMoDialogcmm { get; set; }

        // 查询备忘录数据委托
        public DelegateCommand SearchMeMocmm { get; set; }

        // 删除备忘录数据委托
        public DelegateCommand<MeMoInfoDTO> DeleteMeMocmm { get; set; }


        /// <summary>
        ///  构造函数
        /// </summary>
        public MeMoViewModel(HttpRestClient _httpRestClient)
        {
            // 初始化备忘录数据
            MeMoList = new List<MeMoInfoDTO>();
            MeMoinfoDTO = new MeMoInfoDTO();

            // 赋值
            OpenMeMoDialogcmm = new DelegateCommand(openclosedialog);
            AddMeMoDialogcmm = new DelegateCommand(addMeModialog);
            SearchMeMocmm = new DelegateCommand(SearchMeMo);
            DeleteMeMocmm = new DelegateCommand<MeMoInfoDTO>(DelegateMeMo);

            httpRestClient = _httpRestClient;

            // 刷新数据
            SearchMeMo();
        }


        #region 备忘录数据
        private List<MeMoInfoDTO> memolist;
        public List<MeMoInfoDTO> MeMoList
        {
            get { return memolist; }
            set { memolist = value; this.RaisePropertyChanged(nameof(MeMoList)); }
        }
        #endregion

        #region 搜索备忘录数据
        private int? searchindex { get; set; } = 0;
        public int? SearchIndex 
        {
            get { return searchindex; }
            set { searchindex = value; } 
        }

        public string? SearchTitle { get; set; }
        #endregion

        #region 菜单栏对话框开启或关闭状态(默认关闭)
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

        #region 对话框内添加的数据
        private MeMoInfoDTO memoinfoDTO;
        public MeMoInfoDTO MeMoinfoDTO
        {
            get { return memoinfoDTO; }
            set { memoinfoDTO = value;  this.RaisePropertyChanged(nameof(MeMoinfoDTO)); }
        }
        #endregion




        /// <summary>
        /// 打开关闭对话框方法
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
        /// 添加对话框信息方法
        /// </summary>
        /// <param name="dTO"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void addMeModialog()
        {
            // 调用API添加
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.ContentType = "application/json";
            apiRequest.Method = RestSharp.Method.POST;
            apiRequest.Route = "MeMo/AddMeMoData";
            apiRequest.Parameters = MeMoinfoDTO;

            // 执行
            ApiResponse apiResponse = httpRestClient.Execute(apiRequest);

            if (apiResponse.ResultCode == 1)
            {
                // 刷新面板数据
                SearchMeMo();
                // 关闭对话框
                IsRightDrawerStatus = false;
            }
            if (apiResponse.ResultCode == -99)
            {
                MessageBox.Show(apiResponse.Msg);
            }
        }


        /// <summary>
        /// 搜索备忘录数据
        /// </summary>
        private void SearchMeMo()
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
            if (SearchIndex == 2)
            {
                searchindex = 1;
            }


            // 调用API查询
            ApiRequest apiRequest = new ApiRequest();
            apiRequest.ContentType = "application/json";
            apiRequest.Method = RestSharp.Method.GET;
            apiRequest.Route = $"MeMo/SearchMeMo?title={SearchTitle}&status={searchindex}";

            // 执行
            ApiResponse response = httpRestClient.Execute(apiRequest);

            if (response.ResultCode == 1)
            {
                // 反序列化接收
                MeMoList = JsonConvert.DeserializeObject<List<MeMoInfoDTO>>(response.ResultData.ToString());
            }
        }


        /// <summary>
        /// 删除备忘录数据
        /// </summary>
        private void DelegateMeMo(MeMoInfoDTO meMoInfoDTO)
        {
            // 提示是否删除
            var Result = MessageBox.Show("是否删除该备忘录", "温馨提示", MessageBoxButton.OKCancel);

            // 确定
            if (Result == MessageBoxResult.OK)
            {
                // 调用API删除
                ApiRequest apiRequest = new ApiRequest();
                apiRequest.ContentType = "application/json";
                apiRequest.Method = RestSharp.Method.DELETE;
                apiRequest.Route = $"MeMo/DeleteMeMoData?MeMoId={meMoInfoDTO.WaitId}";

                // 执行
                ApiResponse response = httpRestClient.Execute(apiRequest);

                if (response.ResultCode == 1)
                {
                    // 刷新面板
                    SearchMeMo();
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
    }
}
