   M S B A M L   `   `   `  ����
  monet2    monet2.EtiquetasWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  EtiquetasWindow��5   	   $��565.672��6!   $��637.314��62   .��5      � .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ tabela��5         AutoGenerateColumns .  $
��Left=�6?   $��175,107,0,0q�6Z   $	��Top=�6o   $	��221��6�   $	��339��6�      Columns 5      4  .System.Windows.Controls.DataGridCheckBoxColumn  5      -   5        checkboxTabela��,  &System.Windows.Controls.DataGridColumn   Header$ Bool property��5   A    �� 
IsSelected 6X   ��  �� Path$ 	isChecked� �� Mode$ TwoWay=�1  +System.Windows.Controls.DataGridBoundColumn   Binding 6�   �� *BoolProperty+	 �� UpdateSourceTrigger$	 PropertyChanged=�6�   0  *System.Windows.Controls.DataGridTextColumn  5	      $
 Nome��6%    64   �� *nome+  5
      $	 Rua��6%    63   �� *rua+  5      $ Bairro��6%    66   �� *bairro+  5      $ Cidade��6%    66   �� *cidade+  5      $ UF��6%    62   �� *uf+  5      $	 CEP��6%    63   �� *cep+5      �� 5   
   -   5        
 ��Name$
 buttonImprimir��5      $��Imprimir��6)   $
��Left=�6<   $��286,372,0,0q�6W   $	��Top=�6l   $��75��6�   6�   5      5      