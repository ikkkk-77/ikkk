using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Wpf页面设计_Prism_WebAPI_.Model;

namespace Wpf页面设计_Prism_WebAPI_.ViewModel
{
    class MainWindowViewModel : BindableBase
    {

        // 区域改变委托
        public DelegateCommand<LeftMenusModel> RegionManagercmm {  get; set; }
        // 视图后退委托
        public DelegateCommand RegionBackcmm { get; set; }
        // 视图前进委托
        public DelegateCommand RegionForwardcmm { get; set; }

        // 区域管理变量
        private IRegionManager regionManager;
        // 导航记录变量
        private IRegionNavigationJournal journal;


        /// <summary>
        /// 构造函数
        /// </summary>
        public MainWindowViewModel(IRegionManager _regionManager) 
		{
            #region 菜单栏数据初始化
            LeftMenusList = new List<LeftMenusModel>();

            LeftMenusList.Add(new LeftMenusModel() { Icon = "Home", MenuName = "首页", ViewName = "IndexViewUC" });
            LeftMenusList.Add(new LeftMenusModel() { Icon = "NotebookOutline", MenuName = "待办事项", ViewName = "ToDoViewUC" });
            LeftMenusList.Add(new LeftMenusModel() { Icon = "NotebookPlus", MenuName = "备忘录", ViewName = "MemoViewUC" });
            LeftMenusList.Add(new LeftMenusModel() { Icon = "Cog", MenuName = "设置", ViewName = "SettingViewUC" });
            #endregion

            // 变量赋值
            regionManager = _regionManager;

            // 委托赋值
            RegionManagercmm = new DelegateCommand<LeftMenusModel>(regionchange);
            RegionBackcmm = new DelegateCommand(RegionBack);
            RegionForwardcmm = new DelegateCommand(RegionForward);

        }



        #region 左侧菜单栏
        private List<LeftMenusModel> leftMenuslist;

		public List<LeftMenusModel> LeftMenusList
        {
            get { return leftMenuslist; }
			set {
                leftMenuslist = value; 
				this.RaisePropertyChanged(nameof(LeftMenusList));
			}
		}
        #endregion



        /// <summary>
        /// 区域改变方法
        /// </summary>
        private void regionchange(LeftMenusModel menu)
        {
            if (menu == null && string.IsNullOrEmpty(menu.ViewName))
            {
                return;
            }

            // 区域导航
            regionManager.Regions["ContentRegion"].RequestNavigate(menu.ViewName, 
                callback =>
                    {
                        journal = callback.Context.NavigationService.Journal;
                    });
        }


        // 区域返回方法
        private void RegionBack()
        {
            if (journal != null && journal.CanGoBack)
            {
                journal.GoBack();
            }
        }

        // 区域前进方法
        private void RegionForward()
        {
            if (journal != null && journal.CanGoForward)
            {
                journal.GoForward();
            }
        }

        // 主界面默认页面方法(在App.cs中调用)
        public void DefaultView(string _str)
        {
            NavigationParameters paras = new NavigationParameters();
            paras.Add("username", _str);

            // 设定打开首页并传递登录用户名字
            regionManager.Regions["ContentRegion"].RequestNavigate("IndexViewUC",
                callback =>
                {
                    journal = callback.Context.NavigationService.Journal;
                },paras);
        }
    }
}
