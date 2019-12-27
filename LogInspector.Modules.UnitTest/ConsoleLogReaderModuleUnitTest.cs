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
	public class ConsoleLogReaderModuleUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new ConsoleLogReaderModule(null,new MockedLexer() ));
			Assert.ThrowsException<ArgumentNullException>(() => new ConsoleLogReaderModule(NullLogger.Instance, null));
		}

		[TestMethod]
		public void ShouldNotRead()
		{
			ConsoleLogReaderModule module;
			MemoryLogger logger;

			logger = new MemoryLogger(new DefaultLogFormatter());

			module = new ConsoleLogReaderModule(logger, new MockedLexer());
			module.Read(null);
			Assert.AreEqual(1, logger.Logs.Where(item => item.Contains("Error")).Count());
		}

	}
}
