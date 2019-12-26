using LogInspector.Models;
using Microsoft.Win32;
using RuleEditor.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace RuleEditor
{
	/// <summary>
	/// Logique d'interaction pour MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private RuleLibrariesViewModel libraries;
		public MainWindow()
		{
			libraries = new RuleLibrariesViewModel();
			InitializeComponent();
			DataContext = libraries;
		}

		private void ShowError(Exception ex)
		{
			MessageBox.Show(ex.Message);

		}

		private void NewCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true;e.Handled = true;
		}

		private void NewCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			RuleLibraryViewModel project;
			project = new RuleLibraryViewModel();
			
			libraries.Items.Add(project);
			libraries.SelectedItem = project;
		}

		private void OpenCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = true; e.Handled = true;
		}

		private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			RuleLibraryViewModel ruleLibrary;
			OpenFileDialog dialog;

			dialog = new OpenFileDialog();
			dialog.Filter = "Xml file|*.xml|All files|*.*";
			dialog.DefaultExt = "xml";
			dialog.Title = "Open rule library";

			if (dialog.ShowDialog(this)??false)
			{
				try
				{

					ruleLibrary = new RuleLibraryViewModel();
					ruleLibrary.FileName = dialog.FileName;
					ruleLibrary.Name = Path.GetFileNameWithoutExtension(dialog.FileName);
					ruleLibrary.Load(RuleLibrary.LoadFromFile(dialog.FileName));

					libraries.Items.Add(ruleLibrary);
					libraries.SelectedItem = ruleLibrary;
				}
				catch(Exception ex)
				{
					ShowError(ex);
				}
			}

		}

		private void SaveCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = (libraries.SelectedItem!=null) && (libraries.SelectedItem.FileName!=null); e.Handled = true;
		}

		private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			RuleLibraryViewModel ruleLibrary;
	
			try
			{
				ruleLibrary = libraries.SelectedItem;
				ruleLibrary.Save().SaveToFile(ruleLibrary.FileName);
			}
			catch (Exception ex)
			{
				ShowError(ex);
			}
			
		}

		private void SaveAsCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = libraries.SelectedItem != null; e.Handled = true;
		}

		private void SaveAsCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			RuleLibraryViewModel ruleLibrary;
			SaveFileDialog dialog;

			dialog = new SaveFileDialog();
			dialog.Filter = "Xml file|*.xml|All files|*.*";
			dialog.DefaultExt = "xml";
			dialog.Title = "Save rule library";

			if (dialog.ShowDialog(this) ?? false)
			{
				try
				{
					ruleLibrary = libraries.SelectedItem;
					ruleLibrary.FileName = dialog.FileName;
					ruleLibrary.Name = Path.GetFileNameWithoutExtension(dialog.FileName);
					ruleLibrary.Save().SaveToFile(dialog.FileName);
				}
				catch (Exception ex)
				{
					ShowError(ex);
				}
			}
		}

		private void CloseCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = libraries.SelectedItem != null; e.Handled = true;
		}

		private void CloseCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			libraries.Items.Remove(libraries.SelectedItem);
			libraries.SelectedItem = libraries.Items.FirstOrDefault();
		}









	}
}
