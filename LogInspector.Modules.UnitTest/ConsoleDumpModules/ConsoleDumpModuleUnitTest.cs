using System;
using LogInspector.Modules.LibraryModules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogLib;
using LogInspector.Modules.UnitTest.Mocks;
using LogInspector.Models;
using System.Linq;
using LexerLib;
using LexerLib.Predicates;
using LogInspector.Modules.ConsoleDumpModules;

namespace LogInspector.Modules.ConsoleDumpModules.UnitTest
{
	[TestClass]
	public class ConsoleDumpModuleUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new ConsoleDumpModule(null ,new MockedLogReader(),new MockedStyleProvider() ));
			Assert.ThrowsException<ArgumentNullException>(() => new ConsoleDumpModule(NullLogger.Instance, null, new MockedStyleProvider()));
			Assert.ThrowsException<ArgumentNullException>(() => new ConsoleDumpModule(NullLogger.Instance, new MockedLogReader(), null));
		}

		[TestMethod]
		public void ShouldNotDump()
		{
			ConsoleDumpModule module;
			MemoryLogger logger;

			logger = new MemoryLogger(new DefaultLogFormatter());

			module = new ConsoleDumpModule(logger, new MockedLogReader(),new MockedStyleProvider());
			module.DumpToConsole(null);
			Assert.AreEqual(1, logger.Logs.Where(item => item.Contains("Error")).Count());
		}

		
	}
}
