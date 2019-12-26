using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RuleEditor.ViewModels
{
	public class RuleLibrariesViewModel:ViewModel<object>
	{
		public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<RuleLibraryViewModel>), typeof(RuleLibrariesViewModel));
		public ObservableCollection<RuleLibraryViewModel> Items
		{
			get { return (ObservableCollection<RuleLibraryViewModel>)GetValue(ItemsProperty); }
			set { SetValue(ItemsProperty, value); }
		}


		public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(RuleLibraryViewModel), typeof(RuleLibrariesViewModel));
		public RuleLibraryViewModel SelectedItem
		{
			get { return (RuleLibraryViewModel)GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}

		public RuleLibrariesViewModel()
		{
			Items = new ObservableCollection<RuleLibraryViewModel>();
		}

		public override void Load(object Model)
		{
			throw new NotImplementedException();
		}
		public override object Save()
		{
			throw new NotImplementedException();
		}
	}
}
