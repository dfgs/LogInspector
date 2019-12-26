using System;
using LogInspector.Modules.LibraryModules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogLib;
using LogInspector.Modules.UnitTest.Mocks;
using LogInspector.Models;
using System.Linq;

namespace LogInspector.Modules.UnitTest.LibraryModules
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

		[TestMethod]
		public void ShouldGetGrammar()
		{
			GrammarLibraryModule module;
			MemoryLogger logger;
			Grammar f1, f2, f3, result;

			f1 = new Grammar() { NameSpace = "File0.xml" };
			f2 = new Grammar() { NameSpace = "File1.xml" };
			f3 = new Grammar() { NameSpace = "File2.xml" };

			logger = new MemoryLogger(new DefaultLogFormatter());
			module = new GrammarLibraryModule(logger, new MockedDirectoryEnumerator(3), new MockedGrammarFileLoader(f1, f2, f3));
			module.LoadDirectory("path");
			Assert.AreEqual(3, module.Count);

			result = module.GetGrammar("File0.xml");
			Assert.AreEqual(f1, result);
			result = module.GetGrammar("File1.xml");
			Assert.AreEqual(f2, result);
			result = module.GetGrammar("File2.xml");
			Assert.AreEqual(f3, result);


		}
		[TestMethod]
		public void ShouldNotGetGrammar()
		{
			GrammarLibraryModule module;
			MemoryLogger logger;
			Grammar f1, f2, f3, result;

			f1 = new Grammar() { NameSpace = "File0.xml" };
			f2 = new Grammar() { NameSpace = "File1.xml" };
			f3 = new Grammar() { NameSpace = "File2.xml" };

			logger = new MemoryLogger(new DefaultLogFormatter());
			module = new GrammarLibraryModule(logger, new MockedDirectoryEnumerator(3), new MockedGrammarFileLoader(f1, f2, f3));
			module.LoadDirectory("path");
			Assert.AreEqual(3, module.Count);

			result = module.GetGrammar("File4.xml");
			Assert.IsNull(result);
			Assert.AreEqual(1, logger.Logs.Where(item => item.Contains("Warning")).Count());


		}



	}
}
