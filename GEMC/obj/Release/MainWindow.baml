   M S B A M L   `   `   `  ����   GEMC    GEMC.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     b ^Telerik.Windows.Data, Version=2015.3.1104.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f bTelerik.Windows.Controls, Version=2015.3.1104.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7q mTelerik.Windows.Controls.Navigation, Version=2015.3.1104.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Ctelerik1http://schemas.telerik.com/2008/xaml/presentation    -     �Name$  MainPostWindow�� � Title$ 
MainWindow��$	��600��$
��1200��$	��600��$
��1200�� � 
ResizeMode$ CanResizeWithGrip=� � WindowStyle$
 None=� � AllowsTransparency .  � Icon$8 2pack://application:,,,/Resources/Letter G blue.ico�� � 	Resources    
StyleColor �� Color  	BackColor  CloseButtonTemplate �� 
TargetType	 �Name
 ��Name �� Triggers P� Property P� Value �� 
TargetName �� Property �� Value  TextButtonTemplate ��Name :� RoutedEvent �� From	 �� To g� Duration �� 
TargetName �� TargetProperty  SimpleButtonTemplate  ImageButton
 � Tag  LittleImageButton �� CornerRadius �� 
TargetType � ColumnDefinitions � RowDefinitions J� Opacity �� VerticalOffset � Value  �� ScrollableHeight! � Maximum" �� ViewportHeight# �� ViewportSize*$ �� #ComputedVerticalScrollBarVisibility��%S  &	        &	      &	 &     &	 �     &	 �    &	 *    &	 0    '��     �� $ 	#FF356092���� $ White���� " ��.R�� $	 GridButt��
�������.+��� $
 Content����. $��Center=�$��Center=� P�  �@J�IsMouseOver . . ��� $ GridButt�� � ��
 �  ����  � ��
 ������� " ��.R��� $ 
BorderButt��$��1q�#���  ._�� $	 GridButt��#���  .+��� ��. $��Center=�$��Center=� :� $ UIElement.MouseLeave��.[��� .���� .���� $ YellowX�$ 	#FFF1F1F1X�$ 0:0:0.3D�$ GridButt��$ Background.Color��� .���� .���� $ OrangeX�$ 	#FFF1F1F1X�$ 0:0:0.3D�$ 
BorderButt��$ BorderBrush.Color�:� $ UIElement.MouseEnter��.[��� .���� .���� $ 	#FFF1F1F1X�$ YellowX�$ 0:0:0.3D�$ GridButt��$ Background.Color��� .���� .���� $ 	#FFF1F1F1X�$ OrangeX�$ 0:0:0.3D�$ 
BorderButt��$ BorderBrush.Color�P�  �@J�IsMouseOver .  . ���  � ��
 �   ��� " ��.R�� $	 GridButt��
�������.+��� $��1q�
���   �._��� $��1q�
�������._��� ��. $��Center=�$��Center=� :� $ UIElement.MouseEnter��.[��� .���� .���� $ WhiteX�$ 	LightBlueX�$ 0:0:0.3D�$ GridButt��$ Background.Color�:� $ UIElement.MouseLeave��.[��� .���� .���� $ 	LightBlueX�$ WhiteX�$ 0:0:0.3D�$ GridButt��$ Background.Color��� " ��.R�� $��BackGrid��$��61��.+�]� $��Center=�.+��� $	��pic��$	��Top=�$��5q�$
��auto��$��40��#��z �� $	��Top=�$��Center=�$��15��$��0,-7,0,0q� P�  �@J�IsMouseOver . . ��� $ BackGrid�� � �� �	#FF4A6D97�� " ��.R��� $��
BackBorder��$ 7��._�]� $��Center=�.+��� $	��pic��$	��Top=�$��5q�$��30��$
��auto��#��z  P�  �@J�IsMouseOver . . ��� $ 
BackBorder�� � �� �	#FF4A6D97�� " ��.���  �@�OverridesDefaultStyle . ��  � �� �� " ��.R��  �� �� $
��Auto � �� �� $
��Auto �.+��� $��0���� $��PART_VerticalScrollBar��$��1��$	 0.5Q�# z #! z  ## z" $��20��$��20��#y�z$ .���� $��1q�
�������._�� -   $	 	DragPanel��#���   �� $��60 ��� .+�� $��0��.+��� 
���   �$	 0.6Q�% J� Effect% -  'System.Windows.Media.Effects.BlurEffect  &   Radius$& 17Q��� ���	#FF1F497D' �� Orientation$' Vertical=�.+�� $	��Top=�$��30�����	#FF173461�� -   $��Создать��
�������#��[  ��1-pack://application:,,,/Resources/userIcon.png���� -   $��0,3,0,0q�$��Затянуть��
�������#��[  ��40pack://application:,,,/Resources/getMailIcon.png���� -   $��0,3,0,0q�$��Написать��
�������#��[  ��3/pack://application:,,,/Resources/letterIcon.png���� $��2��
�������$��5q��� $��
Опции��
�������#��[  ��1-pack://application:,,,/Resources/gearIcon.png���� $��Помощь��
�������#��[  ��1-pack://application:,,,/Resources/helpIcon.png���� $��2��
�������$��5q��� � $��1�� �� $��30 ��� .+�� $��0��.+��� 
���   �$	 0.5Q�%   $& 15Q�]� -   ( ]�Name$( 	MenuPanel����. 
�������.+��� -   ) ��Name$) btClose��$��30��$��Stretch=�$��Right=�$��17(�$��×��#��[ �� -   $) 
btMinimize��$��30��$��Stretch=�$��Right=�$��17(�$��_��#��[ � $��Stretch=�$��Right=�$��28��$��0,0,15,0q�.+��� -	   * ��Name$* 	SearchBox��$	��140��
�������
�������$��0,3,0,0q�$��22��$	��Top=� ��%   &	        �� $ 	#FF3998D6��.@��� $��20��$��Center=�$��Собеседники��
���   ��� 
���   �$��	-5,0,-5,0q��� $��20��$��Center=�$��Тема письма��
���   ��� -
   $��tbSearch��$��0,3,22,3q�.����O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem �� 4sys&clr-namespace:System;assembly=mscorlib    " ��+ �� 	Resources+   CueBannerBrush, j� 
AlignmentX- j� 
AlignmentY. j� Stretch/ !� Visual��%X   &	       !� $
, Left=�$- Center=�$
. None=�/ �� $��SearchLabel��
�������0 �� Triggers0 P�  � ��1 �� Empty# Z1 . ���  � ��# [ P�  � �� Z� . ���  � ��# [ P�  �@J�IsKeyboardFocused . . ���  � ��
 �����.���� 
�������� $��2�� �� $	��340��$	��500���� $	��580��.+�� $��0��.+�R� -   $��tvMain��$��10q���.  $��Disabled=�$��0q����� *
ProfList+���� 2 �� ItemsSource2 �� *ProxyMailFolders+.R��� $��
ItemBorder��-   $��4q�$ 7��3 � ContextMenu3 �� .@�m� $��miEdit��-   $��25��$ ��Редактировать��m� $��miDelete��-   $��25��$��Удалить��._�� $��ItemGrid��$��40������ 4 �� RelativeSource4 �� 5 �� AncestorType"5 R�6 �� AncestorLevel$6 1��7 �� Path$7 ActualWidth� �� �� .+�� $��0�� �� $	��140���� $	��180��.+��� $��tbName��$��0��$��15(�$
��Bold"�
���   �$��Bottom=�$��10,0,0,0q����� *Name+�� $��tbAdress��$��1��$��14(�
���   �$��Center=�$��Right=�$��0,0,30,0q����� *Adress+� $��1��.+��� $��tbDate��$��0��$��12(�
���   �$��Center=�$��Right=�$��0,0,30,0q����� *LastTimeChecked+8 �� Triggers8 ^� 9 ^� 
Conditions9 �� : �� Binding: �� 4 �� $5 TreeViewItemL�$7 IsMouseOver�; �� Value; True�� : �� 4 �� $5 TreeViewItemL�$7 
IsSelected�	; False.0��� $ 
ItemBorder�� � �� �	#ffe5f3fb�� $ 
ItemBorder�� � �� �	#ffa5d7f0^� 9 �� : �� 4 �� $5 TreeViewItemL�$7 
IsSelected�; True.0��� $ 
ItemBorder��# �    � ���� $ tbName�� � ��
 ������� $ tbAdress�� � ��
 ������� $ tbDate�� � ��
 �����< �� ItemTemplate< �� .R��� $��ItemBorder2��$ 6��._�� $��	ItemGrid2��$��30������ 4 �� "5 R�$6 1��$7 ActualWidth� �� $	��140���� $��70 �.+��� $��
tbListName��$��0��$��Center=�$��13(�$��10,0,0,0q����� *
listName+�� $��tbCount��$��1��$��Center=�$��13(����� *listItemsCount+8 ^� 9 �� : �� 4 �� $5 TreeViewItemL�$7 IsMouseOver�; True�� : �� 4 �� $5 TreeViewItemL�$7 
IsSelected�	; False.0��� $ ItemBorder2�� � �� �	#ffe5f3fb�� $ ItemBorder2�� � �� �	#ffa5d7f0^� 9 �� : �� 4 �� $5 TreeViewItemL�$7 
IsSelected�; True.0��� $ ItemBorder2��# �    � ���� $ 
tbListName�� � ��
 ������� $ tbCount�� � ��
 ����� ��%�   (��       * SystemColors.HighlightBrushKey+)(��      *1/SystemColors.InactiveSelectionHighlightBrushKey+)�� $ Transparent���� $ Transparent���� $
��Auto��$��0��$��	0,20,0,20q�$��Stretch=�$��5��$��Right=����	#FFF0EEF1� $��1�� �� -   = �Name$= rowMailBoxWorkPanel��$
��auto �$��57���� -   $= 
rowMailBox��$	��200���� -   $= rowletterTab��$��50��$	��450���� -   > ��Name$> rowLetterWorkPlace��$
��auto �.+�� -   $	 MailDesk��$��0��$��0��$	��Top=�$��	8,15,15,0q�.+��� 
���   �#���   $��1q�$ 7��._�]� ��. .+��� -   $) btDelete��#��[ $��Right=� ��.*pack://application:,,,/Resources/trash.png���� -   $) btSpam��#��[ $��Right=� ��-)pack://application:,,,/Resources/spam.png���� -   $) 	btRestore��#��[ $��Right=� ��1-pack://application:,,,/Resources/UndoIcon.png���� -   ? ��Name$? checkBoxAbs��$
��Left=�$��Center=�$��6q��� �� -   @ ��Name$@ 	lbMailBox��$��1��$��8,1,15,4q�$��0q����� " ��.���  � ��$ 1q���  � ��$ 30����  � �� �� .��� A � Color$A 	#FF94ABC7��B � Offset$	B 0.0Q�� $A 	#ffe5f3fb��$	B 0.0Q���  � �� �� " ��.R�� $	 
lbMailGrid��#��z��.+�]� $��5,0,0,0q�.+��� $? checkBox��$
��Left=�$��Center=�$��0,0,5,0q�C d� 	IsCheckedC �� *	IsChecked+D �� UpdateSourceTrigger$D PropertyChanged=��� $��14(�$
��Left=�$	��200��$��0,0,5,0q�$
��Bold"�$��Center=����� *Interlocutor+E �� Mode$E TwoWay=��� $��14(�$
��Left=�$	��180��$��0,0,5,0q�$��Center=����� *SubjectPreview+�� $��13(�$��Right=�$	��140��$��Right=�$��Center=�$
��Wrap=����� *
SendTime+$E TwoWay=���  :� $ UIElement.MouseEnter��.[��� .���� .��W� $G ABackground.(GradientBrush.GradientStops)[1].(GradientStop.Offset)�F W� From$F 0X�	G W� To$	G 1.5X�$ 0:0:0.2D�:� $ UIElement.MouseLeave��.[��� .���� .��W� $G ABackground.(GradientBrush.GradientStops)[1].(GradientStop.Offset)�$F 1X�$G 0X�$ 0:0:0.1D��� -   $��splitLetterBox��$ 0Q�$
��Auto��$��1��$��5,5,5,-4q�$��Stretch=�$��5��$��Bottom=����	#FFF0EEF1�� -   H ��Name$H tabLetterBox��$��2��$��8,6,15,6q�$��2q�
�������$ 0Q�#���   I �� ContentTemplateI �� .R��� $
��Auto=�.��� .+��� .���� $
��Left=�.+��� $
��Bold"�$��15(����� *SubjectDisplay+�� $
��Bold"�$��13(����� *FromDisplay+�� $
��Bold"�$��13(����� *	ToDisplay+�� $
��Bold"�$��13(����� *WhenDisplay+�� �� $
��Wrap=����� *Body+ J � ToolTipK ]� 
ConditionsL �� Property��%  '��       �� " ��.���  � �� �� " ��.R�� .+�� $	 Panel��.+��� $ Border��
�������$ 9,9,9,0��$��Stretch=�#���   $��2q�$��2q�._��� $ Border2��$��1q�
�������$ 7,7,7,0��$��Stretch=�._��� $
��Bold"�$��4q����� *SubjectPreview+J _� .���� $��3q����� *	Subject+ P�  �@��
IsSelected . . ��� $ Border�� � ��
 ������� $ Border�� �@��CornerRadius$ 7,7,0,0���� $ Border�� � ��$ 2,2,2,0q��� $ Border�� � ��$ 0,0,0,-3q��� $ Border2�� � ��
 ������� $ Border2�� �@��CornerRadius$ 9,9,0,0���� $ Border2�� � ��$ 0q�]� K �� L �@��
IsSelected; .  �� L �@J�IsMouseOver; . ./��� $ Border�� � �� �	#ffe5f3fb� -   $	 LetterReplyDesk��$��3��$��
8,-5,15,15q�$��0��$��Bottom=�.+��� 
���   �$��1q�$ 7��#���   ._�]� ��. .+��� -   $) btAnswer��$��
Ответ��#��[ $
��Left=�$��70��$��10,5,5,5q�$��30���� -   $) btResend��$��Переслать��#��[ $
��Left=�$��70��$��10,5,5,5q�$��30���� -   $) btCloseAllTabs��$��Закрыть все��#��[ $��Right=�$��80��$��10,5,5,5q�$��30���� �� -   $) 
btCloseTab��$��Закрыть��#��[ $��Right=�$��70��$��10,5,5,5q�$��30���� 