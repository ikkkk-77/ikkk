using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf页面设计_Prism_WebAPI_.Models;

namespace Wpf页面设计_Prism_WebAPI_.ViewModels
{
    class SetttingViewModel : BindableBase
    {
        // 导航管理变量
        private IRegionManager regionManager;

        // 导航切换用户控件委托
        public DelegateCommand<string> RegionChangecmm {  get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_regionManager"></param>
        public SetttingViewModel(IRegionManager _regionManager)
        {
            #region 初始化数据
            SettingList = new List<SettingLeftModel>();
            SettingList.Add(new SettingLeftModel { Icon = "Palette", MenuName = "个性化", ViewName = "PersonalUC" });
            SettingList.Add(new SettingLeftModel { Icon = "Cog", MenuName = "系统设置", ViewName = "SysSetUC" });
            SettingList.Add(new SettingLeftModel { Icon = "Information", MenuName = "关于更多", ViewName = "AboutUsUC" });
            #endregion

            // 变量赋值
            regionManager = _regionManager;

            // 导航委托赋值
            RegionChangecmm = new DelegateCommand<string>(viewname => 
            {
                //if (regionManager == null)
                //{
                //    throw new InvalidOperationException("regionManager has not been initialized.");
                //}

                //if (!regionManager.Regions.ContainsRegionWithName("SettingRegion"))
                //{
                //    throw new InvalidOperationException("The region named 'SettingRegion' does not exist.");
                //}

                if (regionManager.Regions["SettingRegion"] != null)
                {
                    regionManager.Regions["SettingRegion"].RequestNavigate(viewname);
                }
                });
        }


        #region 设置页数据
        private List<SettingLeftModel> settinglist;
        public List<SettingLeftModel> SettingList
        {
            get { return settinglist; }
            set
            {
                settinglist = value;
                this.RaisePropertyChanged(nameof(SettingList));
            }
        }
        #endregion

    }
}
