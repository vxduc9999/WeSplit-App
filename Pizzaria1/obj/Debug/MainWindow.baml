   M S B A M L   `   `   `  ����D  @Pizzaria1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Pizzaria1	Pizzaria1  5   	       Pizzaria1.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Pizzaria1     5   	   U QMaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   $	��600��5      $
��1024��6'     � WindowState$  	Maximized=�64    � WindowStartupLocation$ CenterScreen=�6L    � 
ResizeMode$ NoResize=�6q    � WindowStyle$
 None=�6�    � 	Resources 5	         OpenMenu �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime O� Value  	CloseMenu��%�  &	        &	 �     �� 5   
   .��5      U� $ (FrameworkElement.Width)�6,   $ GridMenu��6a   .\�5        $ 0��6'   $ 70Q�63     5      $ 0:0:0.5��6'   $	 250Q�69   5      5      �� 5   
   .��5      U� $ (FrameworkElement.Width)�6,   $ GridMenu��6a   .\�5        $ 0��6'   $	 250Q�63     5      $ 0:0:0.5��6'   $ 70Q�69   5      5      5      	 � Triggers	 5      :� 5   
   
 :� RoutedEvent$
 ButtonBase.Click��6    :� 
SourceName$ ButtonOpenMenu��66   .[�5      �� #��[  6   5      :� 5   
   $
 ButtonBase.Click��6   $ ButtonCloseMenu��66   .[�5      �� #��[ 6   5      .��5       � ���	#FFEEEEEE.+�5!   
   �� $	��Top=�6   .+�5"      � -   5         �Name$ _bgtop��5"      ���	#FF0069C0$��40��6:   .+�5#      �� $��WeSplit��6   $��20(�6+   $��Center=�69   $��Center=�6T   
�������$��DemiBold"�6�   6�   �� 5$       �� Orientation$ 
Horizontal=�6   $��Right=�66   .+�5%      �� -   5         ��Name$ ButtonFechar��5%       K� ZIndex$ 99��63   $��40��6E   $��30��6P   $��0q�6�   
���������6\   Z� ��6r   Z� .��5&      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.3.1.953, Culture=neutral, PublicKeyToken=null]]   Kind$ Power=�62   5'      5(      5)      5*      � 5+   
   -   5        $ GridPrincipal��5+      $��Center=�6&   $��	0,40,0,20q�6C   � 5,   
   -   5        $ GridMenu��5,   5   $��70��6   $
��Left=�6   ���	#FF222222 J� Effect 5-      3  -System.Windows.Media.Effects.DropShadowEffect  5.         
BlurRadius$ 1Q�6#      ShadowDepth$ 3Q�62      Color$ 	#FFAEAEAE��6B   .+�50      � -   5        $ Topmenu��50      $	��100��6$   $ 99��61   $	��250��6C   $
��Left=�51      $	��Top=�6.   ���	#FF222222 J� RenderTransformOrigin$ 0.5,0.5&�52      z�53      V� 54      .~�55      �� �� 56      �� 57      T� 58      59      .+�5;      �� -   5        $ ButtonOpenMenu��5;      $��60��61   $��60��6=   $��5q�6H   $
��Left=�6S   $	��Top=�6n    0 ,MaterialDesignFloatingActionMiniAccentButton#��[ 6�   
���������6�   Z� ��6�   Z� .��5<      �� $��/Assets/list-menu.png��6   $��40��6;   $��30��6G    �� BitmapScalingMode$ HighQuality=�5=       6P   5>      �� 5?      -   5        $ ButtonCloseMenu��5?      $y�	Collapsed=�62   $��60��6I   $��60��6U   $��5q�6`   $��Right=�6k   $	��Top=�6�   #��[ 6�   
���������6�   Z� ��6�   Z� .��5@      �� $��/Assets/arrow-left.png��6   $��30��6<   $��30��6H   $ HighQuality=�5A       6P   5B      5C      ?  9MaterialDesignThemes.Wpf.Transitions.TransitioningContent  5D      -   5          Name$ TrainsitionigContentSlide��5D   2   C  =MaterialDesignThemes.Wpf.Transitions.TransitioningContentBase   OpeningEffect 6U   D  >MaterialDesignThemes.Wpf.Transitions.TransitionEffectExtension  *SlideInFromLeft+   Duration$ 0:0:0.2X�.��5E      � -	   5        $ 
GridCursor��5E      $��	0 100 0 0q�6+   ���	#FF0069C0$��10��6U   $
��Left=�6`   $��60��6{   $	��Top=�6�   5F      �� 5G      -
   5         ��Name$ ListViewMenu��5G      $��
0,100,0,68q�6-   $��Disabled=�6B   
�������$��Champagne & Limousines)�5H   .   $��18(�6V    �� SelectedIndex$ 0��6�   .@�5J      �� -   5         ��Name$ ItemHome��5J      $��60��61   .��5K      �� $ 
Horizontal=�6!   .+�5L      �� -   5        $��30��5L   !   $��30��6-   $
��10,0q�68     HomeBtnStyle#���  6F   .��5N      �� $��/Assets/home.png��6$   $��30��6>   $��30��6J   $ HighQuality=�5O       6P   5P      �� 5Q      $
��Home��6$   $��Center=�60   $��20 10q�6K   5R      5S      �� 5T      -   5        $ ItemTrip��5T      $��60��61   .��5U      �� $ 
Horizontal=�6!   .+�5V      �� -   5        $��30��5V   !   $��30��6-   $
��10,0q�68     TripBtnStyle#���  6F   .��5X      �� $��/Assets/list-trip.png��6$   $��30��6C   $��30��6O   $ HighQuality=�5Y       6P   5Z      �� 5[      $
��Trip��6$   $��Center=�60   $��20 10q�6K   5\      5]      �� 5^      -   5        $ 
ItemFinish��5^      $��60��63   .��5_      �� $ 
Horizontal=�6!   .+�5`      �� -   5        $��30��5`   !   $��30��6-   $
��10,0q�68     FinishBtnStyle#���  6F   .��5b      �� $��/Assets/list-finish.png��6$   $��30��6E   $��30��6Q   $ HighQuality=�5c       6P   5d      �� 5e      $��
On the way��6$   $��Center=�66   $��20 10q�6Q   5f      5g      �� 5h      -   5        $ ItemAdd��5h      $��60��60   .��5i      �� $ 
Horizontal=�6!   .+�5j      �� -   5        $��40��5j   !   $��30��6-   $
��10,0q�68     AddBtnStyle#���  6F   .��5l      �� $��/Assets/plus.png��6$   $��30��6>   $��30��6J   $ HighQuality=�5m       6P   5n      �� 5o      $��Add trip��6$   $��Center=�64   $��20 10q�6P   5p      5q      �� 5r      -   5        $ 
ItemMember��5r      $��60��63   .��5s      �� $ 
Horizontal=�6!   .+�5t      �� -   5        $��40��5t   !   $��30��6-   $
��10,0q�68     MemberBtnStyle#���  6F   .��5v      �� $��/Assets/user.png��6$   $��40��6>   $��30��6J   $ HighQuality=�5w       6P   5x      �� 5y      $��Members list��6$   $��Center=�68   $��20 10q�6S   5z      5{      �� 5|      -   5        $ ItemSettings��5|      $��60��65   .��5}      �� $ 
Horizontal=�6!   .+�5~      �� -   5        $��40��5~   !   $��30��6-   $
��10,0q�68     SettingsBtnStyle#���  6F   .��5�      �� $��/Assets/settings.png��6$   $��30��6B   $��30��6N   $ HighQuality=�5�       6P   5�      �� 5�      $��Settings��6$   $��Center=�64   $��20 10q�6O   5�      5�      �� 5�      -   5        $ ItemHelp��5�      $��60��61   .��5�      �� $ 
Horizontal=�6!   .+�5�      �� -   5        $��30��5�   !   $��30��6-   $
��10,0q�68    	 HelpBtnStyle#��� 	 6F   .��5�      �� $��/Assets/help.png��6$   $��30��6>   $��30��6J   $ HighQuality=�5�       6P   5�      �� 5�      $��Help?��6$   $��Center=�61   $��20 10q�6L   5�      5�      5�      5�      � 5�   
   -   5         �Name$ popup_uc��5�       � AllowsTransparency .  � HorizontalOffset$ 10��5�        � VerticalOffset$  10��5�      .��5�      � .+�5�          Pizzaria1.PopupUserControl  -   5        !  Name$! Header��5�   )   ���#E0FFFF69   5�      5�      5�      5�      