using DailyApp.WPF.Service;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wpf页面设计_Prism_WebAPI_.DTOs;

// 备忘录编辑对话框
namespace Wpf页面设计_Prism_WebAPI_.ViewModels
{
    internal class EditMeMoDialogViewModel : BindableBase,IDialogHostAware
    {
        // 实现接口

        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        // 对话框打开时方法
        public void OnDialogOpening(IDialogParameters parameters)
        {
            // 接收旧数据在对话框
            if (parameters.ContainsKey("OldMeMoData"))
            {
                MeMoinfoDTO =  parameters.GetValue<MeMoInfoDTO>("OldMeMoData");
            }
        }


        // 构造函数
        public EditMeMoDialogViewModel()
        {
            SaveCommand = new DelegateCommand(save);
            CancelCommand = new DelegateCommand(cancel);

            MeMoinfoDTO = new MeMoInfoDTO();
        }


        #region 对话框数据
        private MeMoInfoDTO memoinfoDTO;
        public MeMoInfoDTO MeMoinfoDTO
        {
            get { return memoinfoDTO; }
            set {
                memoinfoDTO = value;
                this.RaisePropertyChanged(nameof(MeMoinfoDTO));
            }
        }
        #endregion

        // 确定方法
        private void save()
        {
            // 信息基本验证
            if (string.IsNullOrEmpty(MeMoinfoDTO.Title) && string.IsNullOrEmpty(MeMoinfoDTO.Content))
            {
                MessageBox.Show("信息填写不完整, 请重新填写");
            }
            else
            {
                // 传递新数据给首页
                DialogParameters pas = new DialogParameters();
                pas.Add("NewMeMoData", MeMoinfoDTO);

                if (DialogHost.IsDialogOpen("RootDialog"))
                {
                    // 关闭并传递
                    DialogHost.Close("RootDialog", new DialogResult(ButtonResult.OK, pas));
                }
            }
        }


        // 取消方法
        private void cancel()
        {
            if (DialogHost.IsDialogOpen("RootDialog"))
            {
                DialogHost.Close("RootDialog", new DialogResult(ButtonResult.No));
            }
        }
    }
}
