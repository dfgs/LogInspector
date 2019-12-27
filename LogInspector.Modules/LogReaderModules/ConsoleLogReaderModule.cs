using LexerLib;
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
		public ConsoleLogReaderModule(ILogger Logger,ILexer Lexer) : base(Logger)
		{
			AssertParameterNotNull(Lexer, "Lexer",out lexer);
		}

		public void Read(ICharReader Reader)
		{
			Token token;

			if (!AssertParameterNotNull(Reader, "Reader", LogLevels.Error)) return;

			while(!Reader.EOF)
			{
				if (!lexer.TryRead(Reader,out token ))
				{
					Console.WriteLine();
					Log(LogLevels.Error, "Failed to read token");
					return;
				}

				switch(token.Class)
				{
					case "LF":
						Console.ForegroundColor = ConsoleColor.White;
						Console.WriteLine();
						break;
					case "Space":
						Console.ForegroundColor = ConsoleColor.White;
						Console.Write(" ");
						break;
					case "Number":
						Console.ForegroundColor = ConsoleColor.Yellow;
						Console.Write(token.Value);
						break;
					case "Symbol":
						Console.ForegroundColor = ConsoleColor.Blue;
						Console.Write(token.Value);
						break;
					case "DateTime":
						Console.ForegroundColor = ConsoleColor.Green;
						Console.Write(token.Value);
						break;
					default:
						Console.ForegroundColor = ConsoleColor.White;
						Console.Write(token.Value);
						break;
				}
				

			}
		}
	}
}
