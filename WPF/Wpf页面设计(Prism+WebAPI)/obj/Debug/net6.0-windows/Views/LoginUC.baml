   M S B A M L   `   `   `  ����X  TWpf页面设计(Prism+WebAPI), Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:Wpf页面设计_Prism_WebAPI_.Views#Wpf页面设计_Prism_WebAPI_.Views  5      c6clr-namespace:Wpf页面设计_Prism_WebAPI_.Extensions(Wpf页面设计_Prism_WebAPI_.Extensions  5	      1    +Wpf页面设计_Prism_WebAPI_.Views.LoginUC   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Clocal1clr-namespace:Wpf页面设计_Prism_WebAPI_.Views     5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      BpwdEx6clr-namespace:Wpf页面设计_Prism_WebAPI_.Extensions  5	      $	��350��5      $	��600��6   .��5      �   � ColumnDefinitions  5   
   �� 5      $
��1.5* �6   6.   �� 5      6!   .+�5   
   '  !MaterialDesignThemes.Wpf.Snackbar  -   5          Name$ RegLogInBar��5      $��2��6+      MessageQueue 6?   4  .MaterialDesignThemes.Wpf.MessageQueueExtension  6b   7  1MaterialDesignThemes.Wpf.Transitions.Transitioner  5   
   $��1��6    �� SelectedIndex 6*   �� *
Selected+ �� FallbackValue$ 0��.@�5      <  6MaterialDesignThemes.Wpf.Transitions.TransitionerSlide  .��5      ]� $��15q�6   $��Center=�6(   .+�5      �� $��欢迎使用��6    $	��Top=�6,   $
��Bold"�6A   $��22(�6S   $
��0,10q�6a   6q   �� 5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 请输入账号��6   $	��Top=�6W   $
��0,10q�6l   ��69   �� *User.Account+6|   G� 5!      $ 请输入密码��6"   $	��Top=�6=   $
��0,10q�6R   @  :Wpf页面设计_Prism_WebAPI_.Extensions.PasswordBoxExtend
   Pwd 5"   "   �� *
User.Pwd+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�*  $Microsoft.Xaml.Behaviors.Interaction	   	Behaviors	 5#      A   ;Wpf页面设计_Prism_WebAPI_.Extensions.paswordBoxBahavior  5$      69   5&      �� 5'      $��登录��6   $	��Top=�6G   ��6*   �� *
LoginCmm+6^   ]� 5)      $	��0,5q�6    $��False��6-   .+�5*      �� $��注册账号��6$   $
��Left=�60   
   Triggers
 5,      +	  %Microsoft.Xaml.Behaviors.EventTrigger	  5-   "    	  	EventName$ MouseLeftButtonDown��61   *
  $Microsoft.Xaml.Behaviors.TriggerBase 
  Actions. 5.   &   2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     CommandParameter$ 1��6`      Command 6<   �� *ChangeLogRegCmm+6w   5/   #   51      �� 53      $��忘记密码��6$   $��Right=�60   6I   54      55      56        59      .��5:      ]� $��15q�6   $��Center=�6(   .+�5;      �� $ 请输入姓名��6   $	��Top=�6T   $
��0,10q�6i   ��69   �� *	User.Name+6y   �� 5<      $ 请输入账号��6   $	��Top=�6W   $
��0,10q�6l   ��69   �� *User.Account+6|   G� 5=      $ 请输入密码��6"   $	��Top=�6>   $
��0,10q�6S    5>   +   �� *
User.Pwd+$ TwoWay=�$ PropertyChanged=�	 5?        5@      69   5B      G� 5C      $ 请再次输入密码��6"   $	��Top=�6@   $
��0,10q�6U    5D      �� *User.ConfirmPwd+$ TwoWay=�$ PropertyChanged=�	 5E        5F      69   5H      �� 5I      $��注册��6   $	��Top=�6G   $
��0,10q�6\   ��6*   �� *
Register+6l   �� 5J      $��返回登录��6   $��0��6P   ��6,   �� *ChangeLogRegCmm+6g   5K      5L      5M      5N      5O      