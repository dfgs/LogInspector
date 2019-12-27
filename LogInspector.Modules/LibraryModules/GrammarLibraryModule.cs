using LogInspector.Models;
using LogInspector.Modules.FileLoaders;
using LogLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.LibraryModules
{
	public class GrammarLibraryModule : LibraryModule<Grammar>,IGrammarLibraryModule
	{
		public GrammarLibraryModule(ILogger Logger, IDirectoryEnumerator DirectoryEnumerator, IFileLoader<Grammar> FileLoader) : base(Logger, DirectoryEnumerator, FileLoader)
		{
		}

		public GrammarLibraryModule(ILogger Logger) : this(Logger, new DirectoryEnumerator(),new GrammarFileLoader())
		{
		}

		public Grammar GetGrammar(string NameSpace)
		{
			LogEnter();
			Log(LogLevels.Information, $"Searching grammar with name space {NameSpace}");
			foreach (Grammar grammar in Items)
			{
				if (grammar.NameSpace == NameSpace)
				{
					Log(LogLevels.Information, $"Grammar with name space {NameSpace} found");
					return grammar;
				}
			}
			Log(LogLevels.Warning, $"No grammar with name space {NameSpace} was found");
			return null;
		}
	}
}
