   M S B A M L   `   `   `  ����B  >Notepad, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:NotepadNotepad  5   	       Notepad.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Notepad     5   	     � Title$  Notepad��5   	    � 
ResizeMode$ NoResize=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	   $	��400��5   	   $	��300��5   	    � Icon$ Images/AppLogo.png��5   	   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��200 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $	��150 �6   �� 5      $��20 �6   �� 5      $��30 �6   �� 5      $��20 �6   �� 5      $��30 �6   �� 5      $��10 �6   �� 5      $��20 �6   �� 5      $��10 �6   �� 5      $��30 �6   �� 5       $��10 �6   �� 5!      $��20 �6   �� 5"      $��* �6   .+�5%   
   �� $��Images/AppLogo.png��6   $��1��5&      $��0��5'      �� 5)   
   $��Login��6   $��1��5*      $��1��5+      $��Center=�5,      $
��Left=�5-      �� 5/   
   -   5         ��Name$ UsernameTextBox��5/      $��2��50      $��1��51      $��Center=�52      $��Center=�53      �� 55   
   $��Password��6   $��3��56      $��1��57      $��Center=�58      $
��Left=�59      G� 5;   
   -   5         G�Name$ PasswordBox��5;      $��4��5<      $��1��5=      $��Center=�5>      $��Center=�5?      
������ 5@      �� 5B   
   -   5         ��Name$ SaveLoginCheckBox��5B      $��
Save login��5C      $��6��5D      $��1��5E      �� 5G   
   -   5        	 ��Name$	 LoginButton��5G      $��Login��5H      $��8��5J      $��1��5K      �� 5M   
   -   5        $��10��5N      $��1��5O      
����  �
������ 
������ 
 J� 	AllowDrop$
 False��5S      .��5T      �� B� 5U      .�6   .�5V      �� $�Register��6   5W      5X      5Y      5]      5^      