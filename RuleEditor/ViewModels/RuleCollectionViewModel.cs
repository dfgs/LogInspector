using LexerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEditor.ViewModels
{
	public class RuleCollectionViewModel : CollectionViewModel<RuleViewModel, Rule>
	{

		public override bool AddCommandCanExecute(object Parameter)
		{
			return true;
		}

		public override void AddCommandExecute(object Parameter)
		{
			RuleViewModel rule;

			rule = new RuleViewModel();
			rule.Name = "New rule";
			
			this.Add(rule);
			SelectedItem = rule;
		}

		public override bool DeleteCommandCanExecute(object Parameter)
		{
			return SelectedItem!=null;
		}

		public override void DeleteCommandExecute(object Parameter)
		{
			this.Remove(SelectedItem);
			
		}
	}
}
