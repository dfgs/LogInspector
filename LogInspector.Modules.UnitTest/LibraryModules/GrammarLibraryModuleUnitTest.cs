using System;
using LogInspector.Modules.LibraryModules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogLib;
using LogInspector.Modules.UnitTest.Mocks;
using LogInspector.Models;
using System.Linq;

namespace LogInspector.Modules.UnitTest
{
	[TestClass]
	public class GrammarLibraryModuleUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new GrammarLibraryModule(null));
			Assert.ThrowsException<ArgumentNullException>(() => new GrammarLibraryModule(NullLogger.Instance,null,new MockedFileLoader<Grammar>()));
			Assert.ThrowsException<ArgumentNullException>(() => new GrammarLibraryModule(NullLogger.Instance,new MockedDirectoryEnumerator(5), null));
		}

		[TestMethod]
		public void ShouldLoadDirectory()
		{
			GrammarLibraryModule module;

			module = new GrammarLibraryModule(NullLogger.Instance, new MockedDirectoryEnumerator(5), new MockedFileLoader<Grammar>());
			module.LoadDirectory("path");
			Assert.AreEqual(5, module.Count);
		}
		[TestMethod]
		public void ShouldNotLoadDirectory()
		{
			GrammarLibraryModule module;
			MemoryLogger logger;

			logger = new MemoryLogger(new DefaultLogFormatter());
			module = new GrammarLibraryModule(logger, new MockedDirectoryEnumerator(5), new MockedFileLoader<Grammar>());
			module.LoadDirectory(null);
			Assert.AreEqual(0, module.Count);
			Assert.AreEqual(1, logger.Logs.Where(item => item.Contains("Error")).Count());
		}

		[TestMethod]
		public void ShouldNotCrashWhenDirectoryEnumeratorThrowsException()
		{
			GrammarLibraryModule module;
			MemoryLogger logger;

			logger = new MemoryLogger(new DefaultLogFormatter());
			module = new GrammarLibraryModule(logger, new MockedDirectoryEnumerator(5,true), new MockedFileLoader<Grammar>());
			module.LoadDirectory("path");
			Assert.AreEqual(0, module.Count);
			Assert.AreEqual(1, logger.Logs.Where(item=>item.Contains("Error")).Count());
		}
		[TestMethod]
		public void ShouldNotCrashWhenFileLoaderThrowsException()
		{
			GrammarLibraryModule module;
			MemoryLogger logger;


			logger = new MemoryLogger(new DefaultLogFormatter());
			module = new GrammarLibraryModule(logger, new MockedDirectoryEnumerator(5), new MockedFileLoader<Grammar>(true));
			module.LoadDirectory("path");
			Assert.AreEqual(0, module.Count);
			Assert.AreEqual(5, logger.Logs.Where(item => item.Contains("Error")).Count());
		}

	}
}
