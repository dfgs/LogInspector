using LogInspector.Models;
using LogInspector.Modules.FileLoaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.UnitTest.Mocks
{
	public class MockedStyleSheetFileLoader : IFileLoader<StyleSheet>
	{
		public StyleSheet[] items;

		public MockedStyleSheetFileLoader(params StyleSheet[] StyleSheets)
		{
			this.items = StyleSheets;
		}

		public StyleSheet Load(string FileName)
		{
			StyleSheet item;

			item = items.FirstOrDefault(i => i.NameSpace == FileName);
			if (item == null) throw new InvalidOperationException($"Failed to find style sheet with name space {FileName}");
			return item;
		}
	}
}
