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
	public class StyleSheetLibraryModule : LibraryModule<StyleSheet>, IStyleSheetLibraryModule
	{
		public StyleSheetLibraryModule(ILogger Logger, IDirectoryEnumerator DirectoryEnumerator, IFileLoader<StyleSheet> FileLoader) : base(Logger, DirectoryEnumerator, FileLoader)
		{
		}

		public StyleSheetLibraryModule(ILogger Logger) : this(Logger, new DirectoryEnumerator(),new StyleSheetFileLoader())
		{
		}

		public StyleSheet GetStyleSheet(string NameSpace)
		{
			LogEnter();
			Log(LogLevels.Information, $"Searching style sheet with name space {NameSpace}");
			foreach (StyleSheet grammar in Items)
			{
				if (grammar.NameSpace == NameSpace)
				{
					Log(LogLevels.Information, $"Style sheet with name space {NameSpace} found");
					return grammar;
				}
			}
			Log(LogLevels.Warning, $"No style sheet with name space {NameSpace} was found");
			return null;
		}
	}
}
