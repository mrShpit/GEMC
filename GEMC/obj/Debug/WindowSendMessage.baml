   M S B A M L   `   `   `  ����  GEMC    GEMC.WindowSendMessage   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          �Name$  SendMessageWindow��5   	    � Title$ WindowSendMessage��5   	   $	��400��6#   $	��500��60    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	    � AllowsTransparency .  � 	Resources 5         
StyleColor �� Color  SimpleButtonTemplate �� 
TargetType �� Triggers	 :� RoutedEvent
 �� From	 �� To g� Duration �� 
TargetName �� TargetProperty  CloseButtonTemplate P� Property P� Value �� 
TargetName �� Property �� Value��%T  &	        &	 .     &	 �    �� 5   
   $ LightGoldenrodYellow��6-   �� 5   
   " ��67   .R�5      � $��GridButt��6   
�������.+�5      �� $��1q�6   
���   �._�5      �� $��1q�6   
�������._�5      �� ��. $��Center=�6F   $��Center=�6c   5      5      5       5      :� 5      $	 
MouseEnter��6   .[�5      �� .��5      �� .��5      �� $
 WhiteX�6-   $ 	LightBlueX�6:   $ 0:0:0.3D�6I   $ GridButt��6\   $ Background.Color�6}   5      5      5      :� 5!      $	 
MouseLeave��6   .[�5"      �� .��5#      �� .��5$      �� $
 	LightBlueX�6-   $ WhiteX�6>   $ 0:0:0.3D�6I   $ GridButt��6\   $ Background.Color�6}   5%      5&      5'      5*      �� 5,   
   " ��66   .R�5.      � $��GridButt��6   
�������.+�5/      �� $��Content��6#   ��. $��Center=�6M   $��Center=�6j   50       52      P� 53       �@J�IsMouseOver . . �54      �� $ GridButt��6    � ��
 �  ��6I   �� 55       � ��
 �����64   56      59      5:      .��5=      �� 
�������$��1q�6    ._�5>      � ���	#FFF3F3F3 � RowDefinitions 5?   
   �� 5@      $��30 �6   �� 5A      $��40 �6   �� 5B      $��40 �6   �� 5C      �� 5D      $��40 �6   .+�5G   
   �� 
���   � J� Opacity$	 0.5Q�6!    J� Effect 5H      -  'System.Windows.Media.Effects.BlurEffect  5I         Radius$ 15Q�6   5K      � 5M   
   $��0��6   .+�5N      ]� -   5        $��	MenuPanel��5N      ��. 
�������.+�5O      �� -   5        $��btClose��5O      $��30��6(   $��Stretch=�63   $��Right=�6O   $��17(�5P      $��×��6'   #��[ 63   6c   �� 5Q      5R      5S      � 5T   
   $��1��6    � ColumnDefinitions 5U      �� 5V      $��80 �6#   �� 5W      .+�5Y      �� $��0��6   $��Center=�6(   $��Получатель��6C   $��Right=�6U   �� 5Z      -   5        $��tbAdress��5Z      $��1��6&   $��5q�66   5[      � 5\   
   $��2��6    5]      �� 5^      $��80 �6#   �� 5_      .+�5a      �� $��0��6   $��Center=�6(   $��Тема��6C   $��Right=�6O   �� 5b      -   5        $��	tbSubject��5b      $��1��6'   $��5q�67   5c      � 5d   
   $��3��6   .+�5e      �� -   5        $��	tbMessage��5e       �� TextWrapping$
 Wrap=�6'   $��5q�6;   $
��Auto=�5f      ��. 6i   5g      � 5h   
   $��4��6   .+�5i      �� -   5        $��btSend��5i      #��[ 6#   $��Отправить��5j      $��Center=�6)   $	��200��5k      $��4q�6!   6,   5l      5m      5n      5o      