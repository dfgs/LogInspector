using LexerLib;
using LexerLib.Predicates;
using RuleEditor.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RuleEditor.ViewModels
{
	public class RuleViewModel:ViewModel<Rule>
	{

		public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(RuleViewModel));
		public string Name
		{
			get { return (string)GetValue(NameProperty); }
			set { SetValue(NameProperty, value); }
		}


		public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(RuleViewModel));
		public string Description
		{
			get { return (string)GetValue(DescriptionProperty); }
			set { SetValue(DescriptionProperty, value); }
		}


		public static readonly DependencyProperty PatternProperty = DependencyProperty.Register("Pattern", typeof(string), typeof(RuleViewModel),new FrameworkPropertyMetadata(PatternPropertyChanged));
		public string Pattern
		{
			get { return (string)GetValue(PatternProperty); }
			set { SetValue(PatternProperty, value); }
		}



		public static readonly DependencyProperty TestValueProperty = DependencyProperty.Register("TestValue", typeof(string), typeof(RuleViewModel), new FrameworkPropertyMetadata(PatternPropertyChanged));
		public string TestValue
		{
			get { return (string)GetValue(TestValueProperty); }
			set { SetValue(TestValueProperty, value); }
		}



		public static readonly DependencyProperty ErrorMessageProperty = DependencyProperty.Register("ErrorMessage", typeof(string), typeof(RuleViewModel));
		public string ErrorMessage
		{
			get { return (string)GetValue(ErrorMessageProperty); }
			set { SetValue(ErrorMessageProperty, value); }
		}

		public UICommand TestCommand
		{
			get;
			private set;
		}


		public RuleViewModel()
		{
			TestCommand = new UICommand(TestCommandCanExecute, TestCommandExecute);
			ErrorMessage = "Not tested";
		}

		private static void PatternPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			RuleViewModel vm;

			vm = (RuleViewModel)d;
			vm.ErrorMessage = "Not tested";
		}

		

		public bool TestCommandCanExecute(object Parameter)
		{
			return (!string.IsNullOrEmpty(TestValue)) && (!string.IsNullOrEmpty(Pattern));
		}
		public void TestCommandExecute(object Parameter)
		{
			Predicate predicate;
			Lexer lexer;
			StringCharReader reader;
			Token token;

			try
			{
				predicate = PredicateBuilder.Build(Pattern);
			}
			catch
			{
				ErrorMessage = "Invalid pattern";
				return;
			}

			reader = new StringCharReader(TestValue);
			lexer = new Lexer(reader, new Rule(Name, predicate));

			if (!lexer.TryRead(out token))
			{
				ErrorMessage = $"Failed to read token ({token.Value})";
				return;
			}
			ErrorMessage = "Token read successfully";
		}



		public override void Load(Rule Model)
		{
			this.Name = Model.Name;
			this.Description = Model.Description;
			this.Pattern = Model.Predicate?.ToString();
		}

		public override Rule Save()
		{
			Rule rule;
			
			rule = new Rule();
			rule.Name = Name;
			rule.Description = Description;

			try
			{
				rule.Predicate= PredicateBuilder.Build(Pattern);
			}
			catch
			{
				rule.Predicate = null;
			}

			return rule;
		}


	}
}
