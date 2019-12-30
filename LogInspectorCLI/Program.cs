using LexerLib;
using LogInspector.Models;
using LogInspector.Modules;
using LogInspector.Modules.ConsoleDumpModules;
using LogInspector.Modules.LibraryModules;
using LogInspector.Modules.LogReaderModules;
using LogLib;
using System;
using System.IO;
using System.Text;

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
			LogInspector.Modules.LogReaderModules.LogReaderModule logReaderModule;
			LogInspector.Modules.ConsoleDumpModules.ConsoleDumpModule consoleDumpModule;

			FormatHandler formatHandler;


			logger = new ConsoleLogger(new DefaultLogFormatter());

			if ((args == null) || (args.Length == 0))
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

			lexerFactoryModule = new LexerFactoryModule(logger,  grammarLibraryModule);
			styleProviderFactoryModule = new StyleProviderFactoryModule(logger, styleSheetLibraryModule);

			formatHandler = formatHandlerLibraryModule.GetFormatHandler(args[0]);
			if (formatHandler==null)
			{
				logger.Log(0, "Program", "Main", LogLevels.Error, "No format handler found");
				return;
			}

			ILexer lexer = lexerFactoryModule.BuildLexer(formatHandler);
			if (lexer == null)
			{
				logger.Log(0, "Program", "Main", LogLevels.Error, "Failed to build lexer");
				return;
			}

			IStyleProvider styleProvider = styleProviderFactoryModule.BuildStyleProvider(formatHandler);
			if (styleProvider == null)
			{
				logger.Log(0, "Program", "Main", LogLevels.Error, "Failed to build style provider");
				return;
			}

			StreamCharReader reader = new StreamCharReader(new FileStream(args[0], FileMode.Open), Encoding.Default);

			logReaderModule = new LogReaderModule(logger, lexer, formatHandler.LineFeedClass);
			logReaderModule.LogStartClass = formatHandler.LogStartClass;
			logReaderModule.LogStartValue = formatHandler.LogStartValue;
			logReaderModule.AddIgnoredTokens(formatHandler.IgnoredTokens.ToArray());

			consoleDumpModule = new ConsoleDumpModule(logger, logReaderModule,styleProvider);

			consoleDumpModule.DumpToConsole(reader);

		}
	}
}
