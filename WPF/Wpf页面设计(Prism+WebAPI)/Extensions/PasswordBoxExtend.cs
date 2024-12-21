using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Wpf页面设计_Prism_WebAPI_.Extensions
{
    /// <summary>
    /// PasswordBox扩展属性
    /// </summary>
    class PasswordBoxExtend
    {
        //propa
        public static string GetPwd(DependencyObject obj)
        {
            return (string)obj.GetValue(PwdProperty);
        }

        public static void SetPwd(DependencyObject obj, string value)
        {
            obj.SetValue(PwdProperty, value);
        }

        // Using a DependencyProperty as the backing store for Pwd.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PwdProperty =
            DependencyProperty.RegisterAttached("Pwd", typeof(string), typeof(PasswordBoxExtend), new PropertyMetadata
                ("",OnPwdChanged));


        /// <summary>
        /// 自定义附加属性发生变化,改变Password属性
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnPwdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox pwdbox = d as PasswordBox;
            string newpwd = (string) e.NewValue;  // 新值

            if(pwdbox != null && pwdbox.Password != newpwd)
            {
                pwdbox.Password = newpwd;
            }
        }
    }

//======================================================================================================

    /// <summary>
    /// 新类 （Password行为 Password变化 自定义附加属性跟着变化）
    /// </summary>
    public class paswordBoxBahavior : Behavior<PasswordBox>
    {
        /// <summary>
        /// 附加 注入事件
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PasswordChanged += OnpasswordChange;
        }

        /// <summary>
        /// Password变化,自定义附加属性跟着变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void OnpasswordChange(object sender, RoutedEventArgs e)
        {
             PasswordBox passwordBox = sender as PasswordBox;
            string password = PasswordBoxExtend.GetPwd(passwordBox);  //附加属性值

            if (passwordBox != null && passwordBox.Password != password)
            {
                PasswordBoxExtend.SetPwd(passwordBox, passwordBox.Password);
            }
        }

        /// <summary>
        /// 销毁 移出事件
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PasswordChanged -= OnpasswordChange;
        }
    }
}
