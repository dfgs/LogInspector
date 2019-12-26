using LogInspector.Models;
using LogInspector.Modules.FileLoaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.UnitTest.Mocks
{
	public class MockedGrammarFileLoader : IFileLoader<Grammar>
	{
		public Grammar[] items;

		public MockedGrammarFileLoader(params Grammar[] Grammars)
		{
			this.items = Grammars;
		}

		public Grammar Load(string FileName)
		{
			Grammar item;

			item = items.FirstOrDefault(i => i.NameSpace == FileName);
			if (item == null) throw new InvalidOperationException($"Failed to find grammar with name space {FileName}");
			return item;
		}
	}
}
