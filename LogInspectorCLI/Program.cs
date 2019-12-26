using LogInspector.Modules.LibraryModules;
using LogLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspectorCLI
{
	class Program
	{
		static void Main(string[] args)
		{
			ILogger logger;
			GrammarLibraryModule grammarLibraryModule;
			FormatHandlerLibraryModule formatHandlerLibraryModule;

			logger = new ConsoleLogger(new DefaultLogFormatter());

			if ((args==null) || (args.Length==0))
			{
				logger.Log(0, "Program", "Main", LogLevels.Error, "No input file provided");
				return;
			}

			grammarLibraryModule = new GrammarLibraryModule(logger);
			grammarLibraryModule.LoadDirectory(Properties.Settings.Default.GrammarLibrariesPath);

			formatHandlerLibraryModule = new FormatHandlerLibraryModule(logger);
			formatHandlerLibraryModule.LoadDirectory(Properties.Settings.Default.FormatHandlerLibrariesPath);

			Console.ReadLine();
		}
	}
}
