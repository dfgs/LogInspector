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
		private IGrammarLibraryModule grammarLibraryModule;
		public LexerFactoryModule(ILogger Logger, IGrammarLibraryModule GrammarLibraryModule) : base(Logger)
		{
			AssertParameterNotNull(GrammarLibraryModule, "GrammarLibraryModule", out grammarLibraryModule);
		}

		public ILexer BuildLexer(FormatHandler FormatHandler)
		{
			Grammar grammar;
			List<Rule> rules;
			LexerLib.Lexer lexer;

			LogEnter();

			if (!AssertParameterNotNull(FormatHandler, "FormatHandler")) return null;

			Log(LogLevels.Information, "Building lexer from format handler rules");
			rules = new List<Rule>();
			
						
			foreach(string grammarName in FormatHandler.Grammars)
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

			lexer = new LexerLib.Lexer( rules.ToArray());
			return lexer;
		}
	}
}
