﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RuleEditor.Views
{
	/// <summary>
	/// Logique d'interaction pour LibraryView.xaml
	/// </summary>
	public partial class GrammarView : UserControl
	{
		public GrammarView()
		{
			InitializeComponent();
		}

		private void AddCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;
			e.Handled = true;
		}

		private void AddCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{

		}
	}
}
