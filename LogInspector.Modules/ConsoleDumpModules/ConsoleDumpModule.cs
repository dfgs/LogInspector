using LexerLib;
using LogInspector.Models;
using LogInspector.Modules.LogReaderModules;
using LogLib;
using ModuleLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.ConsoleDumpModules
{
	public class ConsoleDumpModule : Module, IConsoleDumpModule
	{
		private ILogReaderModule logReaderModule;
		private IStyleProvider styleProvider;

		public ConsoleDumpModule(ILogger Logger, ILogReaderModule LogReaderModule,IStyleProvider StyleProvider) : base(Logger)
		{
			AssertParameterNotNull(LogReaderModule, "LogReaderModule", out logReaderModule);
			AssertParameterNotNull(StyleProvider, "StyleProvider", out styleProvider);
		}

		public void DumpToConsole(ICharReader Reader)
		{
			Log log;
			Style style;
			ConsoleColor color;

			//int index;

			if (!AssertParameterNotNull(Reader, "Reader", LogLevels.Error)) return;
			
			Console.BufferWidth = 1024;

			//index = 0;
			while (!Reader.EOF)
			{
				log = logReaderModule.Read(Reader);

				Console.ForegroundColor = ConsoleColor.White;
				Console.Write($"{log.LineNumber}: ");
				foreach(Token token in log.Tokens)
				{
					style = styleProvider.GetStyle(token);
					if ((style == null) || (!Enum.TryParse<ConsoleColor>(style.Foreground,out color))) color = ConsoleColor.White;
					
					Console.ForegroundColor = color;
					Console.Write(token.Value);
					//Console.Write(" ");
				}
				Console.WriteLine();

				if (Console.CursorTop >= Console.WindowHeight) Console.ReadKey();
				
			}
		}
	}
}
