   M S B A M L   `   `   `  ����  GEMC    GEMC.WindowAddProfile   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          �Name$  AddProfileWindow��5   	    � Title$ WindowAddProfile��5   	   $	��360��6"   $	��350��6/    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency .  � 	Resources 5
         
StyleColor �� Color  SimpleButtonTemplate �� 
TargetType �� Triggers	 :� RoutedEvent
 �� From	 �� To g� Duration �� 
TargetName �� TargetProperty  CloseButtonTemplate P� Property P� Value �� 
TargetName �� Property �� Value��%E  &	        &	      &	 �    �� 5   
   $ White��6-   �� 5   
   " ��67   .R�5      � $��GridButt��6   
�������.+�5      �� $��1q�6   
���   �._�5      �� $��1q�6   
�������._�5      �� ��. $��Center=�6F   $��Center=�6c   5      5      5       5      :� 5      $	 
MouseEnter��6   .[�5      �� .��5      �� .��5      �� $
 WhiteX�6-   $ 	LightBlueX�6:   $ 0:0:0.3D�6I   $ GridButt��6\   $ Background.Color�6}   5      5      5      :� 5       $	 
MouseLeave��6   .[�5!      �� .��5"      �� .��5#      �� $
 	LightBlueX�6-   $ WhiteX�6>   $ 0:0:0.3D�6I   $ GridButt��6\   $ Background.Color�6}   5$      5%      5&      5)      �� 5+   
   " ��66   .R�5-      � $��GridButt��6   
�������.+�5.      �� $��Content��6#   ��. $��Center=�6M   $��Center=�6j   5/       51      P� 52       �@J�IsMouseOver . . �53      �� $ GridButt��6    � ��
 �  ��6I   �� 54       � ��
 �����64   55      58      59      .��5:      �� $��1q�6   
�������._�5;   
   � ���	#FFF3F3F3 � RowDefinitions 5<      �� 5=      $��30 �6    �� 5>      �� 5?      �� 5@      �� 5A      �� 5B      �� 5C      �� 5D      �� 5E      �� 5F      $��10 �6    �� 5G      �� 5H      $��10 �6    .+�5K      �� 
���   � J� Opacity$	 0.5Q�6%    J� Effect 5L      -  'System.Windows.Media.Effects.BlurEffect  5M         Radius$ 15Q�6!   5O      ]� 5P      -   5        $��	MenuPanel��5P      ��. 
�������.+�5Q      �� -   5        $��btClose��5Q      $��30��6(   $��Stretch=�63   $��Right=�6O   $��17(�5R      $��×��6'   #��[ 63   6c   �� 5S      5T      �� 5U      $*��$Введите имя профиля��6   $��Center=�63   $��1��6Q   $��Center=�6^   �� 5V      -   5        $��tbProfileName��5V      $��2��6,   $	��240��69   $��3q�6E   #���   6P   �� 5X      $=��7Введите адрес почтового ящика��6   $��Center=�6=   $��3��6[   $��Center=�6h   �� 5Y      -   5        $��tbAdress��5Y      $��4��6&   $	��240��63   $��3q�6?   #���   6J   �� 5[      $?��9Введите пароль почтового ящика��6   $��Center=�6>   $��5��6\   $��Center=�6i   G� 5\      -   5        $��
tbPassword��5\      $��6��6,   $	��240��69   $��3q�6E   #���   6P   �� 5^      $A��;Выберите сервер почтового ящика��6   $��Center=�6?   $��7��6]   $��Center=�6j   �� 5_      -   5        $��cbServer��5_      $��8��6'   $	��240��64   $��3q�6@   #���   6K   .@�5`      �� $��mail.ru��6   �� 5a      $��	gmail.com��6   �� 5b      $��	yandex.ru��6   5c      �� 5e      -   5        $��byAccept��5e   F   #��[ 6   $��Создать��6W   $��10��6i   $��2q�6w   $	��200��6�   6�   5f      5g      5h      