using System;
using LogInspector.Modules.LibraryModules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogLib;
using LogInspector.Modules.UnitTest.Mocks;
using LogInspector.Models;
using System.Linq;
using LexerLib;
using LexerLib.Predicates;
using LogInspector.Modules.LogReaderModules;

namespace LogInspector.Modules.UnitTest
{
	[TestClass]
	public class LogReaderModuleUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new LogReaderModule(null, new MockedLexer(), "LF"));
			Assert.ThrowsException<ArgumentNullException>(() => new LogReaderModule(NullLogger.Instance, null, "LF"));
			Assert.ThrowsException<ArgumentNullException>(() => new LogReaderModule(NullLogger.Instance, new MockedLexer(), null));
		}

		[TestMethod]
		public void ShouldNotRead()
		{
			Log log;
			LogReaderModule module;
			MemoryLogger logger;

			logger = new MemoryLogger(new DefaultLogFormatter());

			module = new LogReaderModule(logger, new MockedLexer(),"LF");
			log=module.Read(null);
			Assert.AreEqual(1, logger.Logs.Where(item => item.Contains("Error")).Count());
			Assert.IsNull(log);
		}

		[TestMethod]
		public void ShouldRead()
		{
			Log log;
			LogReaderModule module;
			MemoryLogger logger;
			ILexer lexer;
			ICharReader reader;


			logger = new MemoryLogger(new DefaultLogFormatter());
			lexer = new MockedLexer();
			reader = new StringCharReader("abc efg hij");

			module = new LogReaderModule(logger,lexer, " ");
			log = module.Read(reader);
			Assert.IsNotNull(log);
			Assert.AreEqual(3, log.Tokens.Count);
			Assert.AreEqual(1, log.LineNumber);
			log = module.Read(reader);
			Assert.IsNotNull(log);
			Assert.AreEqual(3, log.Tokens.Count);
			Assert.AreEqual(2, log.LineNumber);
			log = module.Read(reader);
			Assert.IsNotNull(log);
			Assert.AreEqual(3, log.Tokens.Count);
			Assert.AreEqual(3, log.LineNumber);
		}
		[TestMethod]
		public void ShouldReadAndMergeLines()
		{
			Log log;
			LogReaderModule module;
			MemoryLogger logger;
			ILexer lexer;
			ICharReader reader;


			logger = new MemoryLogger(new DefaultLogFormatter());
			lexer = new MockedLexer();
			reader = new StringCharReader("abc efg abc");

			// Start a new log when a char is read
			module = new LogReaderModule(logger, lexer, " ", "a","a");
			log = module.Read(reader);
			Assert.IsNotNull(log);
			Assert.AreEqual(6, log.Tokens.Count);
			Assert.AreEqual(1, log.LineNumber);
			log = module.Read(reader);
			Assert.IsNotNull(log);
			Assert.AreEqual(3, log.Tokens.Count);
			Assert.AreEqual(3, log.LineNumber);
			
		}
	}
}
