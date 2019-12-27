using LexerLib;
using LogInspector.Modules;
using LogInspector.Modules.LibraryModules;
using LogInspector.Modules.LogReaderModules;
using LogLib;
using System;
using System.Collections.Generic;
using System.IO;
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
			StyleSheetLibraryModule styleSheetLibraryModule;
			LexerFactoryModule lexerFactoryModule;
			StyleProviderFactoryModule styleProviderFactoryModule;
			ConsoleLogReaderModule logReaderModule;

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

			styleSheetLibraryModule = new StyleSheetLibraryModule(logger);
			styleSheetLibraryModule.LoadDirectory(Properties.Settings.Default.StyleSheetsLibrariesPath);

			lexerFactoryModule = new LexerFactoryModule(logger, formatHandlerLibraryModule, grammarLibraryModule);
			styleProviderFactoryModule = new StyleProviderFactoryModule(logger, formatHandlerLibraryModule, styleSheetLibraryModule);


			ILexer lexer = lexerFactoryModule.BuildLexer(args[0]);
			if (lexer == null) return;

			IStyleProvider styleProvider = styleProviderFactoryModule.BuildStyleProvider(args[0]);
			if (styleProvider == null) return;


			StreamCharReader reader = new StreamCharReader(new FileStream(args[0], FileMode.Open),Encoding.Default);

			logReaderModule = new ConsoleLogReaderModule(logger, lexer,styleProvider);

			logReaderModule.Read(reader);

			Console.ReadLine();
		}
	}
}
