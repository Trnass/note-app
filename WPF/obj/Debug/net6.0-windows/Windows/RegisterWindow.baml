   M S B A M L   `   `   `  ����B  >Notepad, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:NotepadNotepad  5   	       Notepad.RegisterWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Notepad     5   	     � Title$  Register��5   	    � 
ResizeMode$ NoResize=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	   $	��400��5   	   $	��300��5   	    � Icon 5   	   �� *Logo+ � 	Resources 5         TextBlockStyle �� 
TargetType �� Property �� Value  TextBoxStyle��%n  &	        &	 �     �� 5   
   " ��5      .�5      ��  � ��$ 1��6,   �� 5       � ��$ Center=�62   �� 5       � ��$
 Left=�64   5      �� 5   
   " ��5      .�5      ��  � ��$ 1��6,   �� 5       � ��$ Center=�69   �� 5       � ��$ Center=�6;   �� 5       � ��
 ���� 6+   5      5      .��5!      �  � ColumnDefinitions 5"   
   �� 5#      $��* �6   �� 5$      $	��200 �6   �� 5%      $��* �6   	 � RowDefinitions	 5(   
   �� 5)      $��* �6   �� 5*      $��20 �6   �� 5+      $��30 �6   �� 5,      $��20 �6   �� 5-      $��30 �6   �� 5.      $��20 �6   �� 5/      $��30 �6   �� 50      $��20 �6   �� 51      $��30 �6   �� 52      $��20 �6   �� 53      $��30 �6   �� 54      $��20 �6   �� 55      $��30 �6   �� 56      $��10 �6   �� 57      $��30 �6   �� 58      $��* �6   .+�5;   
   �� #��[  6   $��Username��5<      $��1��5=      �� 5?   
   -   5        
 ��Name$
 UsernameTextBox��5@      #��[ 5?      $��2��5B      $��1��5C      ��5A      �� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=�5C      �� 5E   
   #��[  6   $��Password��5F      $��3��5G      G� 5I   
   -   5         G�Name$ PasswordBox��5I      $��4��5J      $��1��5K      $��Center=�5L      $��Center=�5M      
������ 5N      �� 5P   
   #��[  6   $
��Name��5Q      $��5��5R      �� 5T   
   -   5        $
 NameTextBox��5U      #��[ 5T      $��6��5W      ��5V      �� *
Username+$ PropertyChanged=�5W      �� 5Y   
   #��[  6   $��Surename��5Z      $��7��5[      �� 5]   
   -   5        $
 SurenameTextBox��5^      #��[ 5]      $��8��5`      ��5_      �� *
Username+$ PropertyChanged=�5`      �� 5b   
   #��[  6   $��Email��5c      $��9��5d      �� 5f   
   -   5        $
 EmailTextBox��5g      #��[ 5f      $��10��5i      ��5h      �� *
Username+$ PropertyChanged=�5i      �� 5k   
   -   5        $��Register��5k      $��12��5m      $��1��5n      �� 5p   
   -   5        $��Close��5p      $��14��5r      $��1��5s      5t      5u      