   M S B A M L   `   `   `  ���� I  ECALIGO_Aufgabe, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:CALIGO_AufgabeCALIGO_Aufgabe      CALIGO_Aufgabe.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:CALIGO_Aufgabe       � Title$  
MainWindow��$	��836��$
��1338�� � 
ResizeMode$ CanMinimize=�.����  �� Orientation$ 
Horizontal=�.+�]� $	��669��.+��� -    ��Name$ 
SourceList��$	��Top=�$	��467�� �� SelectionMode$ Single=����� *
Features+ �� View �� .P��� $�� �� �� DisplayMemberBinding �� *Parameters[0].Value+�� $�� �� �� *Parameters[1].Value+�� $�� �� �� *Parameters[2].Value+�� $�� �� �� *Parameters[3].Value+�� $�� �� �� *Parameters[4].Value+�� $�� �� �� *Parameters[5].Value+�� $�� �� �� *Parameters[6].Value+�� $�� �� �� *Parameters[7].Value+�� $�� �� �� *Parameters[8].Value+�� $�� �� �� *Parameters[9].Value+�� $�� �� �� *Parameters[10].Value+�� $�� �� �� *Parameters[11].Value+�� $�� �� �� *Parameters[12].Value+�� $�� �� �� *Parameters[13].Value+�� $�� �� �� *Parameters[14].Value+�� $�� �� �� *Parameters[15].Value+�� $�� �� �� *Parameters[16].Value+�� $�� �� �� *Parameters[17].Value+�� $�� �� �� *Parameters[18].Value+�� $�� �� �� *Parameters[19].Value+�� $�� �� �� *Parameters[20].Value+�� $�� �� �� *Parameters[21].Value+�� $�� �� �� *Parameters[22].Value+�� $�� �� �� *Parameters[23].Value+�� $�� �� �� *Parameters[24].Value+�� $�� �� �� *Parameters[25].Value+�� $�� �� �� *Parameters[26].Value+�� -    ��Name$ SourceListDetails��$	��Top=�$	��259��
������  � IsSharedSizeScope$
 True��$��Stretch=����� .R�� $��4q�	 � ColumnDefinitions	 �� $
��Auto �
 z� SharedSizeGroup$	
 Key���� $��* �.+��� $
��Bold"�$	��200������ *Name+�� -   $��1������ *Value+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� 
IsReadOnly �� *
readOnly+�� -    ��Name$ Buttons��
���   �$��1q�$	��Top=�._��  � RowDefinitions �� $��0* �.+��� -    ��Name$ 
SaveButton��$
��Save��$��1��$	��150��$��30��$��344,32,173,-62q��� -   $ 
LoadButton��$
��Load��$��176,32,341,-62q�$��1��$	��150��$��30���� -   $ Target��
���   �$��1q� �� Padding$ 1,1,1,1q�$	��820��$	��669��._��� -    ��Name$ 
TargetText��$
��Wrap=�$�� ��$��Right=�$	��Top=� �� Padding$ 5,5,5,5q�$��0,0,-4,0q����� *ActualWidth+ �� ElementName$ Target��$ OneWay=����� *ActualHeight+$ Target��$ OneWay=�