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
		private string logStartClass;
		private string logStartValue;

		public LogReaderModule(ILogger Logger,ILexer Lexer,string LineFeedClass,string LogStartClass = null, string LogStartValue = null) : base(Logger)
		{
			AssertParameterNotNull(Lexer, "Lexer", out lexer);
			AssertParameterNotNull(LineFeedClass, "LineFeedClass", out lineFeedClass);

			this.logStartClass = LogStartClass;
			this.logStartValue = LogStartValue;

			lineNumber = 1;
		}

		public Log Read(ICharReader Reader)
		{
			Token token;
			Log log;
			long position;
			int tokenNumber;

			if (!AssertParameterNotNull(Reader, "Reader", LogLevels.Error)) return null;

			tokenNumber = 0;
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
					tokenNumber = 0;
					// line feed triggers a new log
					if (logStartClass == null) return log;
					// line feed doesn't triggers a new log
					continue;
				}

				// start of a new log
				if ( (token.Class == logStartClass) && (lineNumber != log.LineNumber) && (tokenNumber==0))
				{
					if ((logStartValue == null) || (token.Value == logStartValue))
					{
						Reader.Seek(position);
						return log;
					}
				}

				log.Tokens.Add(token);
				tokenNumber++;
			}
			while (!Reader.EOF);

			return log;
		}





	}
}
