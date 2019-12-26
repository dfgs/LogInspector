using LogInspector.Models;
using LogInspector.Modules.LibraryModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.UnitTest.Mocks
{
	public class MockedGrammarLibraryModule : IGrammarLibraryModule
	{
		public int Count => items.Length;

		public int ID => 0;

		public string ModuleName => "MockedGrammarLibraryModule";

		private Grammar[] items;

		public MockedGrammarLibraryModule(params Grammar[] Items)
		{
			this.items = Items;
		}
		public Grammar GetGrammar(string NameSpace)
		{
			return items.FirstOrDefault(item => item.NameSpace == NameSpace);
		}

		public void LoadDirectory(string Path)
		{
			
		}
	}
}
