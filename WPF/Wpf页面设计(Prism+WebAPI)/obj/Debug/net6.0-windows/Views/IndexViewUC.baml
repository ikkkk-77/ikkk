   M S B A M L   `   `   `  ����X  TWpf页面设计(Prism+WebAPI), Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:Wpf页面设计_Prism_WebAPI_.Views#Wpf页面设计_Prism_WebAPI_.Views  5      5    /Wpf页面设计_Prism_WebAPI_.Views.IndexViewUC   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Clocal1clr-namespace:Wpf页面设计_Prism_WebAPI_.Views     5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      $��微软雅黑)�5
      .��5      �   � RowDefinitions  5   
   �� 5      $
��auto �6   6,   �� 5      $
��auto �6   6,   �� 5      6   .+�5   
   �� $��0��6   $��22(�67   $��15,10q�6E   ��6!   �� *Time+6V   �� 5   
   $��1��6   ��6$   �� *StackPanelList+��5      �� 5      .R�5      A�  A� Columns$ 4��6"   60   5      ��5      �� 5       .R�5!      ��  �� CornerRadius$ 5��6>   $��10q�6O   ��6   �� *	Backcolor+ � Cursor 6[   �� *Hand+ J� InputBindings 5$      d� 5%       d� MouseAction$ 	LeftClicke�6+    �� Command 6C   �� *DataContext.RegionManagercmm+ �� RelativeSource ��  �� Mode$ FindAncestor=�	 �� AncestorType	 ItemsControl
 �� CommandParameter
 5'   +   �� 5(      ��5,      �� 5-       �� 
TargetType" ��6$    �� Triggers 5.   "   P� 5/   &    P� Property �@J�IsMouseOver P� Value$
 True��6E   . �51   *   ��  �� Property �@J�Effect �� Value 52   .   3  -System.Windows.Media.Effects.DropShadowEffect  53   2      Color$ #dddddd��6C   6U   55   +   56   '   58      ._�5>      � .+�5@      �� $��20,10q�6)   .+�5A   "   '  !MaterialDesignThemes.Wpf.PackIcon  $��30��6P   $��30��6[      Kind 6:   �� *Icon+6[   �� 5B   "   $
��0,10q�6F   $��15(�6T   ��6,   �� *
ItemName+6T   �� 5C   "   $��40(�6D   $
��Bold"�6R   ��6,   �� *Result+6R   5D      �� 5G      $
}�True��6%   .+�5H   "   �� 
 �� Top$ 10��6)    �� Right$	 -50��69   $	��120��6L   $	��120��6X   ���#ffffff$	 100��6z    J� Opacity$	 0.1Q�6�   6�   �� 5I   "   $ 80��6)   $	 -30��69   $	��120��6L   $	��120��6X   ���#ffffff$	 100��6z   $	 0.1Q�6�   6�   5J      5K      5L      5M      5O      � 5R   
   $��2��6   $
��0,10q�6    � ColumnDefinitions 5T      �� 5U      6%   �� 5V      6%   .+�5Z      �� $��0��6   $ 5��6%   $
��10,0q�66   $	 0.1Q�6D   ���	#bebebebe6k   ]� 5[      $��0��6   $
��10,0q�6(   .+�5]      ]� $
��10,5q�6   $��False��6*   $	��Top=�6@   .+�5^      �� $��待办事项��6    $��20(�6,   $
��Bold"�6:   6N   �� 5_      $��30��6F   $��30��6Q   $��Right=�6]   $	��Top=�6t    &  "MaterialDesignFloatingActionButton#��[  6�   ��6   �� *OpenDiaLogAddWaitCmm+.��5`        $	 Add=�62   6?   5a      5b      �� 5e      $��Stretch=�6;   $��Hidden=�6`   ��6   �� *
WaitList+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5h      +  %Microsoft.Xaml.Behaviors.EventTrigger  5i         	EventName$ MouseDoubleClick��6)   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5j      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 64   �� *OpenDiaLogEditWaitCmm+   CommandParameter 5k   !   �� *SelectedItem+ �� $ FindAncestor=�	 ListBox5m      5n      ��5r      �� 5s      .R�5t      ]� $��80��6(   $��False��67   .+�5v   "   d� $��Right=�6/    d� 	IsChecked 6F   �� *Status+��5w   %   �� *!DataContext.ChangeWaitStatusCmm+ �� $ FindAncestor=�	 ListBox��5y   )   �� 5z   #   �� 5}   "   $
��Left=�6-   .+�5~   &   �� $
��Bold"�6G   $��16(�6Y   ��60   �� *Title+6i   �� 5   &   $	 0.5Q�6I   $	��0,5q�6W   ��60   �� *	Content+6f   5�   #   5�      5�      5�      5�      �� 5�      $��1��6   $ 5��6&   $
��10,0q�67   $	 0.1Q�6E   ���	#bebebebe6l   ]� 5�      $��1��6   $
��10,0q�6(   .+�5�      ]� $
��10,5q�6   $��False��6*   $	��Top=�6@   .+�5�      �� $��	备忘录��6    $��20(�6+   $
��Bold"�69   
�������6`   �� 5�      $��30��6F   $��30��6Q   $��Right=�6]   $	��Top=�6t   #��[  6�   ��6   �� *OpenDiaLogAddMeMoCmm+.��5�        $	 Add=�62   6?   5�      5�      �� 5�      $��Stretch=�6;   $��Hidden=�6`   ��6   �� *
MeMoList+ 5�        5�      $ MouseDoubleClick��6)   . 5�         64   �� *OpenDialogEditMeMoCmm+ 6^   �� *SelectedItem+ �� $ FindAncestor=�	 ListBox5�      5�      ��5�      �� 5�      .R�5�      ]� $��80��6(   $��False��67   .+�5�   "   d� $��Right=�6/    6F   �� *Status+��5�   /   �� *!DataContext.ChangeMeMoStatusCmm+ �� $ FindAncestor=�	 ListBox��5�   /   �� 5�   #   �� 5�   "   $
��Left=�6-   .+�5�   &   �� $
��Bold"�6G   $��16(�6Y   ��60   �� *Title+6i   �� 5�   &   $	 0.5Q�6I   $	��0,5q�6W   ��60   �� *	Content+6f   5�   #   5�      5�      5�      5�      5�      5�      5�      