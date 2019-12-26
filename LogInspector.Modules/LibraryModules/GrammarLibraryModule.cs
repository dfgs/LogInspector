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

		

	}
}
