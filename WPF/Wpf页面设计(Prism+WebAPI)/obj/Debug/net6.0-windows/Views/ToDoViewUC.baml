   M S B A M L   `   `   `  ����X  TWpf页面设计(Prism+WebAPI), Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:Wpf页面设计_Prism_WebAPI_.Views#Wpf页面设计_Prism_WebAPI_.Views  5      4    .Wpf页面设计_Prism_WebAPI_.Views.ToDoViewUC   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Clocal1clr-namespace:Wpf页面设计_Prism_WebAPI_.Views     5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5      $��微软雅黑)�5	      .��5      )  #MaterialDesignThemes.Wpf.DialogHost  .��5   
   )  #MaterialDesignThemes.Wpf.DrawerHost      IsRightDrawerOpen  6   �� *IsRightDrawerStatus+   RightDrawerContent 5      ]� 5      $	��300��6   $��False��6(   .+�5      �� $��添加代办��6   $
��Bold"�6*   $��20(�6<   $��20,10q�6J   $	��Top=�6Y   �� 5      $��20q�6!   $	��Top=�6-    �� Orientation$ 
Horizontal=�6B   .+�5      �� $��状态:��6$   $��0,0,5,0q�6/   $��Center=�6@   6]   �� 5       �� SelectedIndex 6#   �� *WaitDTO.Status+.@�5      �� .��6+   代办6/   �� 5      .��6+   	已完成60   5      5      �� 5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 请输入代办标题��6   $
��20,0q�6Z   $	��Top=�6h   ��6;   �� *WaitDTO.Title+6   �� 5      $ 请输入代办内容��6   $��20,10q�6\   $	��Top=�6k   $	��100��6�   ��6;   �� *WaitDTO.Content+6�   �� 5       $��添加到代办��6   $	��Top=�6-   $
��20,0q�6B   ��6P   �� *AddWaitDialogcmm+6w   5!      .��5'      �  � RowDefinitions 5)      �� 5*      $
��auto �6$   64   �� 5+      6&   .+�50      �� $��0��6   $ 
Horizontal=�6*   $��15,0,0,0q�6C   .+�51      �� $��筛选:��6    $
��10,0q�6+   $��Center=�69   6V   �� 54      $ 查找待办事项..��6   .  (MaterialDesignThemes.Wpf.TextFieldAssist   HasClearButton$
 True��6Y   $��Center=�6�   ��6<   �� *SearchTitle+.��6�   .��58   4   59      �� 5<      $
��10,0q�6E    6   �� *SearchIndex+.@�5A      �� .��6'   全部6+   �� 5B      .��6'   代办6+   �� 5C      .��6'   	已完成6,   5D      �� 5F      $
��10,0q�6   ��6+   �� *SearchWaitDatacmm+.��5G      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Search=�6&   66   5H      5I      �� 5L      $��添加代办��6   $��Right=�6(   $
��10,5q�6D   ��6R   �� *OpenWaitDialogcmm+6z   �� 5P      $��1��6   $��Hidden=�6,   .��5Q      �� $��Center=�6#   ��6@   �� *
WaitList+��5R      �� 5S      .R�5V   "   � 6.   5X      ��5[      �� 5\      .R�5]   "   ?  9MaterialDesignThemes.Wpf.Transitions.TransitioningContent  C  =MaterialDesignThemes.Wpf.Transitions.TransitioningContentBase   OpeningEffect 6:   D  >MaterialDesignThemes.Wpf.Transitions.TransitionEffectExtension  	   Kind$	 ExpandIn=�.��5^   &   �� $	��220��6-   $	��180��69   $	��250��6F   $��8q�6U   
 �� CornerRadius$
 5��6}   ��6`   �� *Color+��5`   *   �� 5a   .    �� 
TargetType" ��64    �� Triggers 5b   2   P� 5c   6    P� Property �@J�IsMouseOver P� Value$
 True��6U   . �5d   :   ��  �� Property �@J�Effect �� Value 5e   >   3	  -System.Windows.Media.Effects.DropShadowEffect	  5f   B    	  Color$ #dddddd��6S   6e   5h   ;   5i   7   5k   /   ._�5o   *   �  5q   .   �� 5r   2   $
��auto �6@   6P   �� 5s   2   6B   .+�5w   .   �� $
��10,5q�6O   $��20(�6]   $
��Bold"�6k   $
��Left=�6}   ��68   �� *Title+6�   '
  !MaterialDesignThemes.Wpf.PopupBox
  5x   .   $��Right=�6:    K� ZIndex$ 1��6V    
  PopupContent. 5z   2   �� $��删除��69   ��6F   �� *DataContext.DeleteWaitDatacmm+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType ItemsControl��5|   9   �� 5}   3   5~   /   �� 5   .   $��1��68   $��12(�6^   $
��10,5q�6l   ��6E   �� *	Content+6|   �� 5�   .   $
}�True��65   $��2��6I   .+�5�   2   �� 
 �� Top$ 10��69    �� Right$	 -50��6I   $	��120��6\   $	��120��6h   ���#ffffff$	
 100��6�    J� Opacity$	 0.1Q�6�   6�   �� 5�   2   $ 80��69   $	 -30��6I   $	��120��6\   $	��120��6h   ���#ffffff$	
 100��6�   $	 0.1Q�6�   6�   5�   /   5�   +   5�   '   5�   #   5�      5�      5�      5�      5�      5�      5�      