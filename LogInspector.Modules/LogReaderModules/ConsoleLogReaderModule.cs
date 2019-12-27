using LexerLib;
using LogInspector.Models;
using LogLib;
using ModuleLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.LogReaderModules
{
	public class ConsoleLogReaderModule : Module, ILogReaderModule
	{
		private ILexer lexer;
		private IStyleProvider styleProvider;

		public ConsoleLogReaderModule(ILogger Logger,ILexer Lexer,IStyleProvider StyleProvider) : base(Logger)
		{
			AssertParameterNotNull(Lexer, "Lexer", out lexer);
			AssertParameterNotNull(StyleProvider, "StyleProvider", out styleProvider);
		}

		public void Read(ICharReader Reader)
		{
			Token token;
			Style style;
			ConsoleColor color;

			if (!AssertParameterNotNull(Reader, "Reader", LogLevels.Error)) return;

			while(!Reader.EOF)
			{
				if (!lexer.TryRead(Reader,out token ))
				{
					Console.WriteLine();
					Log(LogLevels.Error, "Failed to read token");
					return;
				}

				style = styleProvider.GetStyle(token);
				if (style==null)
				{
					Console.ForegroundColor = ConsoleColor.White;
				}
				else
				{
					if (Enum.TryParse<ConsoleColor>(style.Foreground, out color)) Console.ForegroundColor = color;
					else Console.ForegroundColor = ConsoleColor.White;
				}
				switch (token.Class)
				{
					case "LF":
						Console.WriteLine();
						break;
					case "Space":
						Console.Write(" ");
						break;
					default:
						Console.Write(token.Value);
						break;
				}
				

			}
		}
	}
}
