using System;
using LogInspector.Modules.LibraryModules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogLib;
using LogInspector.Modules.UnitTest.Mocks;
using LogInspector.Models;
using System.Linq;
using LexerLib;
using LexerLib.Predicates;

namespace LogInspector.Modules.UnitTest
{
	[TestClass]
	public class LexerFactoryModuleUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new LexerFactoryModule(null,  new MockedGrammarLibraryModule()));
			Assert.ThrowsException<ArgumentNullException>(() => new LexerFactoryModule(NullLogger.Instance, null));
		}

		[TestMethod]
		public void ShouldBuildLexer()
		{
			LexerFactoryModule module;
			ILexer result;
			FormatHandler formatHandler;
			Grammar grammar;

			grammar = new Grammar();
			grammar.NameSpace = "grammar";
			grammar.Items.Add(new Rule("A", Parse.Character('a')));
			grammar.Items.Add(new Rule("B", Parse.Character('b')));
			grammar.Items.Add(new Rule("C", Parse.Character('c')));

			formatHandler = new FormatHandler() { FileNamePattern = "test.log" };
			formatHandler.Grammars.Add("grammar");

			module = new LexerFactoryModule(NullLogger.Instance,  new MockedGrammarLibraryModule(grammar));
			result=module.BuildLexer(formatHandler);
			Assert.IsNotNull(result);
			
		}
		[TestMethod]
		public void ShouldNotBuildLexerWhenInvalidParameterSet()
		{
			LexerFactoryModule module;
			ILexer result;
			Grammar grammar;
			MemoryLogger logger;

			logger = new MemoryLogger(new DefaultLogFormatter());

			grammar = new Grammar();
			grammar.NameSpace = "grammar";
			grammar.Items.Add(new Rule("A", Parse.Character('a')));
			grammar.Items.Add(new Rule("B", Parse.Character('b')));
			grammar.Items.Add(new Rule("C", Parse.Character('c')));

			module = new LexerFactoryModule(logger, new MockedGrammarLibraryModule(grammar));

			result = module.BuildLexer(null);
			Assert.IsNull(result);
			Assert.AreEqual(1, logger.Logs.Where(item => item.Contains("Error")).Count());

		}

		[TestMethod]
		public void ShouldNotBuildLexerWhenGrammarNotFound()
		{
			LexerFactoryModule module;
			ILexer result;
			FormatHandler formatHandler;
			Grammar grammar;
			MemoryLogger logger;

			logger = new MemoryLogger(new DefaultLogFormatter());

			grammar = new Grammar();
			grammar.NameSpace = "grammar";
			grammar.Items.Add(new Rule("A", Parse.Character('a')));
			grammar.Items.Add(new Rule("B", Parse.Character('b')));
			grammar.Items.Add(new Rule("C", Parse.Character('c')));

			formatHandler = new FormatHandler() { FileNamePattern = "test.log" };
			formatHandler.Grammars.Add("grammar1");

			module = new LexerFactoryModule(logger,  new MockedGrammarLibraryModule(grammar));

			result = module.BuildLexer(formatHandler);
			Assert.IsNull(result);
			Assert.AreEqual(1, logger.Logs.Where(item => item.Contains("Error")).Count());

		}
	}
}
