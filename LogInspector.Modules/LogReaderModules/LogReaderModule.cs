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
		
		public string LogStartClass
		{
			get;
			set;
		}
		public string LogStartValue
		{
			get;
			set;
		}

		private Dictionary<string, string> ignoredTokens;

		public LogReaderModule(ILogger Logger,ILexer Lexer,string LineFeedClass) : base(Logger)
		{
			AssertParameterNotNull(Lexer, "Lexer", out lexer);
			AssertParameterNotNull(LineFeedClass, "LineFeedClass", out lineFeedClass);
	
			ignoredTokens = new Dictionary<string, string>();
		

			lineNumber = 1;
		}
		public void AddIgnoredTokens(params string[] Tokens)
		{
			foreach (string item in Tokens)
			{
				if (!ignoredTokens.ContainsKey(item)) ignoredTokens.Add(item, item);
			}
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
			log.Position = Reader.Position;

			do
			{
				position = Reader.Position;

				if (!lexer.TryRead(Reader, out token))
				{
					Log(LogLevels.Error, $"Failed to read token at line {lineNumber}");
					log.Tokens.Add(token);
					return log;
				}

				if (ignoredTokens.ContainsKey(token.Class)) continue;

				if (token.Class == lineFeedClass)
				{
					lineNumber++;
					tokenNumber = 0;
					// line feed triggers a new log
					if (LogStartClass == null) return log;
					// line feed doesn't triggers a new log
					continue;
				}

				// start of a new log
				if ((token.Class == LogStartClass) && (lineNumber != log.LineNumber) && (tokenNumber == 0))
				{
					if ((LogStartValue == null) || (token.Value == LogStartValue))
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
