   M S B A M L   `   `   `  ����X  TWpf页面设计(Prism+WebAPI), Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:Wpf页面设计_Prism_WebAPI_.Views#Wpf页面设计_Prism_WebAPI_.Views  5      4    .Wpf页面设计_Prism_WebAPI_.Views.MemoViewUC   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Clocal1clr-namespace:Wpf页面设计_Prism_WebAPI_.Views     5      _ [MaterialDesignThemes.Wpf, Version=4.9.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5      .��5      )  #MaterialDesignThemes.Wpf.DialogHost  .��5   
   )  #MaterialDesignThemes.Wpf.DrawerHost      IsRightDrawerOpen  6   �� *IsRightDrawerStatus+   RightDrawerContent 5      ]� 5      $	��300��6   $��False��6(   .+�5      �� $��添加备忘录��6    $
��Bold"�6-   $��20(�6?   $��20,10q�6M   $	��Top=�6\   6s   �� 5      $��20q�6!   $	��Top=�6-    �� Orientation$ 
Horizontal=�6B   .+�5      �� $��状态:��6$   $��0,0,5,0q�6/   $��Center=�6@   6]   �� 5       �� SelectedIndex 6#   �� *MeMoinfoDTO.Status+.@�5      �� .��6+   代办6/   �� 5      .��6+   	已完成60   5      5      �� 5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 请输入备忘录标题��6   $
��20,0q�6`   $	��Top=�6n   ��6<   �� *MeMoinfoDTO.Title+6�   �� 5      $ 请输入备忘录内容��6   $��20,10q�6b   $	��Top=�6q   $	��100��6�   ��6<   �� *MeMoinfoDTO.Content+6�   �� 5      $��添加到备忘录��6   $	��Top=�6S   $
��20,0q�6h   ��6.   �� *AddMeMoDialogcmm+6x   5      .��5#      �  � RowDefinitions 5%      �� 5&      $
��auto �6$   64   �� 5'      6&   .+�5+      ]� $��False��6   $��Center=�62   $
��10,0q�6M   .+�5,      �� $��筛选:��6    $
��10,0q�6+   $��Center=�69   6V   �� 5/      $ 查找备忘录...��6   .  (MaterialDesignThemes.Wpf.TextFieldAssist   HasClearButton$
 True��6Y   $��10,10q�6�   $
��Left=�6�   ��6<   �� *SearchTitle+6�   �� 52      $
��10,0q�6E    6   �� *SearchIndex+.@�53      �� .��6'   全部6+   �� 54      .��6'   代办6+   �� 55      .��6'   	已完成6,   56      �� 59      $
��10,0q�6   ��6+   �� *SearchMeMocmm+.��5:      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Search=�6&   66   5;      �� 5=      $��添加备忘录��6   $
��10,0q�6S   $��Right=�6a   ��6-   �� *OpenMeMoDialogcmm+6z   5>      �� 5A      $��1��6   $��Hidden=�6,   .��5B      �� $��Center=�6D   ��6#   �� *
MeMoList+��5C      �� 5D      .R�5E      � 6*   5F      ��5I      �� 5J      .R�5L   "   ?  9MaterialDesignThemes.Wpf.Transitions.TransitioningContent  C  =MaterialDesignThemes.Wpf.Transitions.TransitioningContentBase   OpeningEffect 6:   D  >MaterialDesignThemes.Wpf.Transitions.TransitionEffectExtension  	   Kind$	 ExpandIn=�.��5N   &   �� $	��220��6-   $	��180��69   
 �� CornerRadius$
 5��6F   $��8q�6W   $	��250��6b   ��6q   �� *Color+��5P   *   �� 5Q   .    �� 
TargetType" ��64    �� Triggers 5R   2   P� 5S   6    P� Property �@J�IsMouseOver P� Value$
 True��6U   . �5T   :   ��  �� Property �@J�Effect �� Value 5U   >   3	  -System.Windows.Media.Effects.DropShadowEffect	  5V   B    	  Color$ #dddddd��6S   6e   5X   ;   5Y   7   5[   /   ._�5^   *   �  5`   .   �� 5a   2   6B   �� 5b   2   6B   .+�5f   .   �� $
��10,5q�6O   $��20(�6]   $
��Bold"�6k   $
��Left=�6}   ��68   �� *Title+6�   '
  !MaterialDesignThemes.Wpf.PopupBox
  5g   .   $��Right=�6:    K� ZIndex$ 1��6V    
  PopupContent. 5i   2   �� $��删除��69   ��6F   �� *DataContext.DeleteMeMocmm+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType ItemsControl��5j   e   �� 5k   3   5l   /   �� 5m   .   $��1��68   $��12(�6^   $
��10,5q�6l   ��6E   �� *	Content+6|   �� 5p   .   $
}�True��65   $��2��6I   .+�5q   2   �� 
 �� Top$ 10��69    �� Right$	 -50��6I   $	��120��6\   $	��120��6h   ���#ffffff$	
 100��6�    J� Opacity$	 0.1Q�6�   6�   �� 5r   2   $ 80��69   $	 -30��6I   $	��120��6\   $	��120��6h   ���#ffffff$	
 100��6�   $	 0.1Q�6�   6�   5s   /   5t   +   5u   '   5v   #   5w      5y      6&   5{      5|      5}      5~      