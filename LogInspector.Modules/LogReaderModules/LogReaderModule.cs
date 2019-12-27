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
	public class LogReaderModule : Module, ILogReaderModule
	{
		private ILexer lexer;
		private int lineNumber;

		private string lineFeedClass;
		private Token logStartToken; 

		public LogReaderModule(ILogger Logger,ILexer Lexer,string LineFeedClass,Token LogStartToken) : base(Logger)
		{
			AssertParameterNotNull(Lexer, "Lexer", out lexer);
			AssertParameterNotNull(LineFeedClass, "LineFeedClass", out lineFeedClass);

			this.logStartToken = LogStartToken;
			lineNumber = 1;
		}

		public Log Read(ICharReader Reader)
		{
			Token token;
			Log log;
			long position;

			if (!AssertParameterNotNull(Reader, "Reader", LogLevels.Error)) return null;

			log = new Log();
			log.LineNumber = lineNumber;
			do
			{
				position = Reader.Position;

				if (!lexer.TryRead(Reader, out token))
				{
					Log(LogLevels.Error, $"Failed to read token at line {lineNumber}");
					log.Tokens.Add(token);
					return log;
				}
				if (token.Class == lineFeedClass)
				{
					lineNumber++;
					// line feed triggers a new log
					if (logStartToken.Class == null) return log;
					// line feed doesn't triggers a new log
					continue;
				}

				// start of a new log
				if ((token == logStartToken) && (lineNumber != log.LineNumber))
				{
					Reader.Seek(position);
					return log;
				}

				log.Tokens.Add(token);
			}
			while (!Reader.EOF);

			return log;
		}





	}
}
