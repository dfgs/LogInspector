using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RuleEditor.ViewModels
{
	public abstract class CollectionViewModel<VMT,T> : ObservableCollection<VMT>
		where VMT : ViewModel<T>
	{

		private VMT selectedItem;
		public VMT SelectedItem
		{
			get { return selectedItem; }
			set 
			{ 
				selectedItem=value;
				OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("SelectedItem"));
			}
		}


		public UICommand AddCommand
		{
			get;
			private set;
		}

		public UICommand DeleteCommand
		{
			get;
			private set;
		}



		public CollectionViewModel()
		{
			AddCommand = new UICommand(AddCommandCanExecute, AddCommandExecute);
			DeleteCommand = new UICommand(DeleteCommandCanExecute, DeleteCommandExecute);
		}

		public abstract bool AddCommandCanExecute(object Parameter);
		public abstract void AddCommandExecute(object Parameter);
		public abstract bool DeleteCommandCanExecute(object Parameter);
		public abstract void DeleteCommandExecute(object Parameter);

	}
}
