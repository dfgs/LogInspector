﻿using LexerLib;
using LogInspector.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RuleEditor.ViewModels
{
	public class RuleLibraryViewModel:ViewModel<RuleLibrary>
	{
		public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register("FileName", typeof(string), typeof(RuleLibraryViewModel));
		public string FileName
		{
			get { return (string)GetValue(FileNameProperty); }
			set { SetValue(FileNameProperty, value); }
		}

		public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(RuleLibraryViewModel));
		public string Name
		{
			get { return (string)GetValue(NameProperty); }
			set { SetValue(NameProperty, value); }
		}


		public static readonly DependencyProperty NameSpaceProperty = DependencyProperty.Register("NameSpace", typeof(string), typeof(RuleLibraryViewModel));
		public string NameSpace
		{
			get { return (string)GetValue(NameSpaceProperty); }
			set { SetValue(NameSpaceProperty, value); }
		}



		public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(RuleCollectionViewModel), typeof(RuleLibraryViewModel));
		public RuleCollectionViewModel Items
		{
			get { return (RuleCollectionViewModel)GetValue(ItemsProperty); }
			set { SetValue(ItemsProperty, value); }
		}


		public RuleLibraryViewModel()
		{
			Name = "New library";
			Items = new RuleCollectionViewModel();
		}

		public override void Load(RuleLibrary Model)
		{
			RuleViewModel ruleViewModel;

			NameSpace = Model.NameSpace;
			foreach(Rule rule in Model.Items)
			{
				ruleViewModel = new RuleViewModel();
				ruleViewModel.Load(rule);
				Items.Add(ruleViewModel);
			}
		}

		public override RuleLibrary Save()
		{
			RuleLibrary ruleLibrary;

			ruleLibrary = new RuleLibrary();
			ruleLibrary.NameSpace = NameSpace;
			foreach(RuleViewModel ruleViewModel in Items)
			{
				ruleLibrary.Items.Add(ruleViewModel.Save());
			}

			return ruleLibrary;

		}

	}
}
