   M S B A M L   `   `   `  ����   GEMC    GEMC.WindowAddProfile   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     �Name$  AddProfileWindow�� � Title$ WindowAddProfile��$	��360��$	��350�� � 
ResizeMode$ NoResize=� � WindowStyle$
 None=� � AllowsTransparency .  � 	Resources    
StyleColor �� Color  SimpleButtonTemplate �� 
TargetType �� Triggers	 :� RoutedEvent
 �� From	 �� To g� Duration �� 
TargetName �� TargetProperty  CloseButtonTemplate P� Property P� Value �� 
TargetName �� Property �� Value��%�  &	        &	      &	 �    �� $ White���� " ��.R�� $��GridButt��
�������.+��� $��1q�
���   �._��� $��1q�
�������._��� ��. $��Center=�$��Center=� :� $	 
MouseEnter��.[��� .���� .���� $
 WhiteX�$ 	LightBlueX�$ 0:0:0.3D�$ GridButt��$ Background.Color�:� $	 
MouseLeave��.[��� .���� .���� $
 	LightBlueX�$ WhiteX�$ 0:0:0.3D�$ GridButt��$ Background.Color��� " ��.R�� $��GridButt��
�������.+��� $��Content����. $��Center=�$��Center=� P�  �@J�IsMouseOver . . ��� $ GridButt�� � ��
 �  ����  � ��
 �����.���� $��1q�
�������._�� ���	#FFF3F3F3 � RowDefinitions �� $��30 ��� �� �� �� �� �� �� �� �� $��10 ��� �� $��10 �.+��� 
���   � J� Opacity$	 0.5Q� J� Effect -  'System.Windows.Media.Effects.BlurEffect     Radius$ 15Q�]� -   $��	MenuPanel����. 
�������.+��� -   $��btClose��$��30��$��Stretch=�$��Right=�$��17(�$��×��#��[ �� �� $*��$Введите имя профиля��$��Center=�$��1��$��Center=��� -   $��tbProfileName��$��2��$	��240��$��3q�#���   �� $=��7Введите адрес почтового ящика��$��Center=�$��3��$��Center=��� -   $��tbAdress��$��4��$	��240��$��3q�#���   �� $?��9Введите пароль почтового ящика��$��Center=�$��5��$��Center=�G� -   $��
tbPassword��$��6��$	��240��$��3q�#���   �� $A��;Выберите сервер почтового ящика��$��Center=�$��7��$��Center=��� -   $��cbServer��$��8��$	��240��$��3q�#���   .@��� $��mail.ru���� $��	gmail.com���� $��	yandex.ru���� -   $��byAccept��#��[ $��Создать��$��10��$��2q�$	��200��