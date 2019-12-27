using LogInspector.Models;
using LogInspector.Modules.LibraryModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.UnitTest.Mocks
{
	public class MockedStyleSheetLibraryModule : IStyleSheetLibraryModule
	{
		public int Count => items.Length;

		public int ID => 0;

		public string ModuleName => "MockedStyleSheetLibraryModule";

		private StyleSheet[] items;

		public MockedStyleSheetLibraryModule(params StyleSheet[] Items)
		{
			this.items = Items;
		}
		public StyleSheet GetStyleSheet(string NameSpace)
		{
			return items.FirstOrDefault(item => item.NameSpace == NameSpace);
		}

		public void LoadDirectory(string Path)
		{
			
		}
	}
}
