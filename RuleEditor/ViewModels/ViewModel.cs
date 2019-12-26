using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RuleEditor.ViewModels
{
	public abstract class ViewModel<T>:DependencyObject
	{
		public abstract void Load(T Model);
		public abstract T Save();
	}
}
