using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RuleEditor.ViewModels
{
	public class GrammarsViewModel:ViewModel<object>
	{
		public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(ObservableCollection<GrammarViewModel>), typeof(GrammarsViewModel));
		public ObservableCollection<GrammarViewModel> Items
		{
			get { return (ObservableCollection<GrammarViewModel>)GetValue(ItemsProperty); }
			set { SetValue(ItemsProperty, value); }
		}


		public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(GrammarViewModel), typeof(GrammarsViewModel));
		public GrammarViewModel SelectedItem
		{
			get { return (GrammarViewModel)GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}

		public GrammarsViewModel()
		{
			Items = new ObservableCollection<GrammarViewModel>();
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
