using LexerLib;
using LogInspector.Models;
using LogInspector.Modules.LibraryModules;
using LogLib;
using ModuleLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules
{
	public class LexerFactoryModule : Module, ILexerFactoryModule
	{
		private IFormatHandlerLibraryModule formatHandlerLibraryModule;
		private IGrammarLibraryModule grammarLibraryModule;
		public LexerFactoryModule(ILogger Logger, IFormatHandlerLibraryModule FormatHandlerLibraryModule,IGrammarLibraryModule GrammarLibraryModule) : base(Logger)
		{
			AssertParameterNotNull(FormatHandlerLibraryModule, "FormatHandlerLibraryModule", out formatHandlerLibraryModule);
			AssertParameterNotNull(GrammarLibraryModule, "GrammarLibraryModule", out grammarLibraryModule);
		}

		public ILexer BuildLexer(string FileName)
		{
			FormatHandler formatHandler;
			Grammar grammar;
			List<Rule> rules;
			Lexer lexer;

			LogEnter();

			if (!AssertParameterNotNull(FileName, "FileName")) return null;

			Log(LogLevels.Information, "Building lexer from format handler rules");
			rules = new List<Rule>();
			
			formatHandler=formatHandlerLibraryModule.GetFormatHandler(FileName);
			if (formatHandler == null)
			{
				Log(LogLevels.Error, "Failed to build lexer");
				return null;
			}
			
			foreach(string grammarName in formatHandler.Grammars)
			{
				grammar = grammarLibraryModule.GetGrammar(grammarName);
				if (grammar == null) continue;
				rules.AddRange(grammar.Items);
			}

			if (rules.Count==0)
			{
				Log(LogLevels.Error, "Failed to build lexer");
				return null;
			}

			lexer = new Lexer( rules.ToArray());
			return lexer;
		}
	}
}
