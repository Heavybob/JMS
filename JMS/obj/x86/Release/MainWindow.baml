   M S B A M L   `   `   `  ���� >  :JMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:JMSJMS      JMS.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     X TXceed.Wpf.Toolkit, Version=3.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  localclr-namespace:JMS  -     � Title$	  JMS��$	��668��$	��585�� � 
ResizeMode$ CanMinimize=� � 	Resources    ToggleButtonStyle �� 
TargetType �� Property �� Value��%E   &	        �� " d�.���  � ��$	 5,3q���  � ��$	 5,0q�.��� .+��� -   $��Connect Bot��$
��Left=�$��23��$��	10,10,0,0q�$	��Top=�$	��100���� -   $��Disconnect Bot��$
��Left=�$��23��$��
469,10,0,0q�$	��Top=�$	��100���� $��10,37,485,576q���Z� ._��� $��Username��$��Center=��� -    ��Name$ usernameTextBox��$
��Left=�$��23��$��	94,40,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��473���� $��10,63,485,550q���Z� ._��� $��OAuth Token��$��Center=�G� -    G�Name$ OAuthTextBox��$
��Left=�$��23��$��	94,66,0,0q�$	��Top=�$	��473���� $��10,97,347,516q���Z� ._��� -   	 ��Name$	 statusLabel��$��Status: Disconnected��$��Center=��� $��232,97,257,516q���Z� ._��� $��Output Device��$��Center=��� -   
 ��Name$
 deviceComboBox��$
��Left=�$��23��$��327,100,0,0q�$	��Top=�$	��240���� $��362,123,175,490q���Z� ._��� $��Voice��$��Center=��� -   $
 voiceComboBox��$
��Left=�$��23��$��409,126,0,0q�$	��Top=�$	��158�� �� SelectedIndex$ 4��.@��� $
��Paul���� $��Betty���� $��Harry���� $��Frank���� $��Dennis���� $	��Kit���� $��Ursula���� $
��Rita���� $��Wendy���� -	    ��Name$ chatTextBox��$
��Left=�$	��221��$��
10,151,0,0q�$	��Top=�$	��559�� ��%3   'J�       �� " J�.���  �@��Margin$ 0q��� $��10,377,537,236q���Z� ._��� $	��Say��$��Center=��� -
   $ chatCommandBox��$
��Left=�$��23��$��
47,380,0,0q�$
 Wrap=�$	��Top=�$	��442���� -   $	��Say��$
��Left=�$��23��$��494,380,0,0q�$	��Top=�$��75���  � Orientation$ 
Horizontal=�$��10,408,256,143q�$	��313��.+�d� -    d�Name$ autoConnectCheckBox��$��Auto Connect��$��23��#��[  d� -   $ moderatorsIgnoreCheckBox��$��
Moderators��$��23��#��[   d� 	IsChecked$
 TrueY�d� -   $ bitsIgnoreCheckBox��$
��Bits��$��23��#��[  $
 TrueY�d� -   $ subsIgnoreCheckBox��$��Subscribers��$��23��#��[  $
 TrueY�d� -   $ usersIgnoreCheckBox��$��Users��$��23��#��[  $
 TrueY�d� -   $ dialToneCheckBox��$��
Dial Tones��$��23��#��[  d� -   $ cleverBotCheckBox��$��	Cleverbot��$��23��#��[  $
 TrueY�� .+��� $��VP Tempo��$��Center=�%  Xceed.Wpf.Toolkit.IntegerUpDown  -   $��vpTempo���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Minimum$ 1X�   Maximum$
 1000X�   DefaultValue$	 200X�$��48��� .+��� $��Bits Threshold��$��Center=�  -   $��bitsThreshold��$ 0X�$ 0X�$��48���� $��364,405,173,208q���Z� ._��� $
��Mode��$��Center=��� -   $
 chatModeComboBox��$
��Left=�$��23��$��411,408,0,0q�$	��Top=�$	��158��$ 0��.@��� $
��Skip���� $��Queue���� $��Anarchy���� -   $��Show Retro Chat��$
��Left=�$��411,436,0,0q�$	��Top=�$��23��$	��158��d� -   $ openChatOnConnectCheckBox��$��Open Chat on Connect��$��411,464,10,152q�$��23���� -   $��Save Settings��$
��Left=�$��23��$��
10,501,0,0q�$	��Top=�$	��313���� -   $
��Left=�$	��100��$��	9,529,0,0q�$	��Top=�$	��560��$&�� https://ggs.sx/downloads/jms.png��