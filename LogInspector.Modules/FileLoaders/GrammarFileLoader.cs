using LogInspector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.FileLoaders
{
	public class GrammarFileLoader : IFileLoader<Grammar>
	{
		public Grammar Load(string FileName)
		{
			return Grammar.LoadFromFile(FileName);
		}
	}
}
