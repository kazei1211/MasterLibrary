   M S B A M L   `   `   `  ����H  DMasterLibrary, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullc6clr-namespace:MasterLibrary.Views.Admin.BookManagePage(MasterLibrary.Views.Admin.BookManagePage  5      =    7MasterLibrary.Views.Admin.BookManagePage.BookManagePage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Hlocal6clr-namespace:MasterLibrary.Views.Admin.BookManagePage     5      Bdxb2http://schemas.devexpress.com/winfx/2008/xaml/bars     5      Adx2http://schemas.devexpress.com/winfx/2008/xaml/core     5      _ [MaterialDesignThemes.Wpf, Version=4.7.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5        N� Title$  BookManagePage��5         BookManageVM � DataContext# [  5      .��5      � 
������� � RowDefinitions 5   
   �� 5      $
��1.2* �6   �� 5      $��8* �6   �� 5      $
��0.5* �6   .+�5   
   (  "MaterialDesignThemes.Wpf.ColorZone     Mode$ 
PrimaryMid=�5      $��16 16 16 16q�5      
�������.��5      ]� .+�5      ��  �� Orientation$ 
Horizontal=�5      +  %MaterialDesignThemes.Wpf.RippleAssist   
IsCentered . .+�5      d�  ' #MaterialDesignHamburgerToggleButton#���  5         5#      $	��991��5$      $ Standard=�5%      $��8 0 8 4q�5&         CornerRadius$ 2��5'       K� ZIndex$ 1��5(      $��16 0 0 0q�5)      +  %MaterialDesignThemes.Wpf.ShadowAssist   ShadowDepth$ Depth1=�5*      .��5+      � 	 � ColumnDefinitions	 5,      �� 5-   "   $
��Auto �5.   *   �� 5/   "   $��* �50   *   �� 51   "   $
��Auto �52   *   .+�54      ��   MaterialDesignToolButton#���  55   %   .��56   "   '  !MaterialDesignThemes.Wpf.PackIcon  $	��0 4q�57   )   
   Kind$
 Magnify=�58   )    J� Opacity$	 .56Q�59   )   $��Center=�67   $��Center=�6T   5:      �� 5;      -   5        $��	txbFilter��5>   %   $��1��5=   %   $��8,7,0,0q�5?   %   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Typing something��5@   %   .  (MaterialDesignThemes.Wpf.TextFieldAssist   DecorationVisibility$ Hidden=�5A   %   $��0q�5B   %   $	��200��5C   %   $��Center=�5D   %   $��32��6@   5E      5F      5G      5H      5I      �� 5L   
   -   5         ��Name$ listview_managebook��5N      $��1��5L      $��14(�5M      ?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid$   SelectiveScrollingOrientation$ Vertical=�5P      1  +MaterialDesignThemes.Wpf.ScrollViewerAssist   SupportHorizontalScroll . .	  (MaterialDesignThemes.Wpf.ColorZoneAssist 	  
Background �#E8E8E8$��5q�5R   E   ��5O      �� *Listbookmanage+ � ContextMenu 5T      �� 5U      .@�5V      m� -   5         m�Name$ Deleting��5W      $��
Xóa sách��5X      {�. $��12(�5Z       m� Command 5[      �� *DeletingBook+ m� CommandParameter 5\      ��  �� ElementName$ Deleting��m� 5]      -   5        $ Updating��5^      $��Chỉnh sửa��5_      {�. $��12(�5a       5b      �� *UpdatingBook+ 5c      �� $ Updating��5d       �� View 5f      �� 5g      .P�5h      ��  �� Width$
 auto��5j   "    �� DisplayMemberBinding 5i   !   �� *MaSach+.��5k      �� $��16(�5l   !   $	��-18q�5m   !   $	��Mã��5n   !   5p      �� 5r      $
 auto��5t   !    5s   !   �� *	TenSach+.��5u      �� $��16(�5v   %   $	��-18q�5w   %   $��
Tên sách��5x   %   5z      �� 5|      $
 auto��5~   !    5}   !   �� *TacGia+.��5      �� $��16(�5�   %   $	��-18q�5�   %   $��
Tác giả��5�   %   5�      �� 5�      $
 auto��5�   !    5�   !   �� *NXB+.��5�      �� $��16(�5�   %   $	��-18q�5�   %   $��Nhà xuất bản��5�   %   $	��179��6<   5�      �� 5�      $	 100��5�   !    5�   !   �� *Gia+.��5�      �� $	��-18q�5�   %   $
��Giá��5�   %   $��16(�5�   %   5�      �� 5�      $
 auto��5�   !    5�   !   �� *	TheLoai+.��5�      �� $��16(�5�   %   $	��-18q�5�   %   $��Thể loại��5�   %   5�      �� 5�      $
 auto��5�   !    5�   !   �� *NamXB+.��5�      �� $��16(�5�   %   $	��-18q�5�   %   $��Năm xuất bản��5�   %   5�      �� 5�      $
 auto��5�   !    5�   !   �� *	SoLuong+.��5�      �� $��16(�5�   %   $	��-18q�5�   %   $��Số lượng trong kho��5�   %   5�      �� 5�      $
 auto��5�   !    5�   !   �� *TenDay+.��5�      �� $��16(�5�   %   $	��-18q�5�   %   $
��Dãy��5�   %   5�      �� 5�      $
 auto��5�   !    5�   !   �� *	TenTang+.��5�      �� $��16(�5�   %   $	��-18q�5�   %   $��Tầng��5�   %   5�      5�      5�      �� 5�   
   $��2��6   .
  (System.Windows.Interactivity.Interaction 
  Triggers 5�      /  )System.Windows.Interactivity.EventTrigger  5�         	EventName$ Loaded��6!   .  (System.Windows.Interactivity.TriggerBase   Actions. 5�      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6,   �� *LoadManageBookData+   CommandParameter 5�   ,   �� $ listview_managebook��5�      5�      5�      5�      