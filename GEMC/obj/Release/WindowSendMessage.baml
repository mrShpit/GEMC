   M S B A M L   `   `   `  ����   GEMC    GEMC.WindowSendMessage   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     �Name$  SendMessageWindow�� � Title$ WindowSendMessage��$	��540��$	��520�� � 
ResizeMode$ NoResize=� � WindowStyle$
 None=� � AllowsTransparency .  � 	Resources    
StyleColor �� Color  SimpleButtonTemplate �� 
TargetType �� Triggers	 :� RoutedEvent
 �� From	 �� To g� Duration �� 
TargetName �� TargetProperty  CloseButtonTemplate P� Property P� Value �� 
TargetName �� Property �� Value��%�  &	        &	       &	 �    �� $ LightGoldenrodYellow���� " ��.R�� $��GridButt��
�������.+��� $��1q�
���   �._��� $��1q�
�������._��� ��. $��Center=�$��Center=� :� $	 
MouseEnter��.[��� .���� .���� $
 WhiteX�$ 	LightBlueX�$ 0:0:0.3D�$ GridButt��$ Background.Color�:� $	 
MouseLeave��.[��� .���� .���� $
 	LightBlueX�$ WhiteX�$ 0:0:0.3D�$ GridButt��$ Background.Color��� " ��.R�� $��GridButt��
�������.+��� $��Content����. $��Center=�$��Center=� P�  �@J�IsMouseOver . . ��� $ GridButt�� � ��
 �  ����  � ��
 �����.���� 
�������$��1q�._�� ���	#FFF3F3F3 � RowDefinitions �� $��30 ��� $��40 ��� $��40 ��� �� $��96 ��� $��40 �.+��� 
���   � J� Opacity$	 0.5Q� J� Effect -  'System.Windows.Media.Effects.BlurEffect     Radius$ 15Q�� $��0��.+�]� -   $��	MenuPanel����. 
�������.+��� -   $��btClose��$��30��$��Stretch=�$��Right=�$��17(�$��×��#��[ �� � $��1�� � ColumnDefinitions �� $��80 ��� .+��� $��0��$��Center=�$��Получатель��$��Right=��� -   $��tbAdress��$��1��$��5q�� $��2�� �� $��80 ��� .+��� $��0��$��Center=�$��Тема��$��Right=��� -   $��	tbSubject��$��1��$��5q�� $��3��.+��� -   $��	tbMessage�� �� TextWrapping$
 Wrap=�$��5q�$��14(�$
��Auto=���. � $��4��.+�� $��1��.+��� -   $��lbAttachments��$��5q�   ProjectIcon �� 	UriSource��%�   &	       (�� 9     * SystemColors.HighlightBrushKey+)(�� J     *$"SystemColors.HighlightTextBrushKey+)�� $3 -pack://application:,,,/Resources/fileIcon.png?��� $ White���� $ Black������ .R���  �� Orientation$ 
Horizontal=����� .R��� $ Vertical=�$��5q�.+��� $��35��$��35��#��[ �� ���� *ParticalName+ � ContextMenu �� .@�m� -   $��	delAttach��$��Убрать��� $��5��.+��� -	   $��btAddAttach��#��[ $��Добавить файл��$
��Left=�$	��200��$��4q��� -
   $��btSend��#��[ $��Отправить��$��Right=�$	��200��$��4q�