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
	public class StyleSheetLibraryModuleUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new StyleSheetLibraryModule(null));
			Assert.ThrowsException<ArgumentNullException>(() => new StyleSheetLibraryModule(NullLogger.Instance,null,new MockedFileLoader<StyleSheet>()));
			Assert.ThrowsException<ArgumentNullException>(() => new StyleSheetLibraryModule(NullLogger.Instance,new MockedDirectoryEnumerator(5), null));
		}

		[TestMethod]
		public void ShouldLoadDirectory()
		{
			StyleSheetLibraryModule module;

			module = new StyleSheetLibraryModule(NullLogger.Instance, new MockedDirectoryEnumerator(5), new MockedFileLoader<StyleSheet>());
			module.LoadDirectory("path");
			Assert.AreEqual(5, module.Count);
		}
		[TestMethod]
		public void ShouldNotLoadDirectory()
		{
			StyleSheetLibraryModule module;
			MemoryLogger logger;

			logger = new MemoryLogger(new DefaultLogFormatter());
			module = new StyleSheetLibraryModule(logger, new MockedDirectoryEnumerator(5), new MockedFileLoader<StyleSheet>());
			module.LoadDirectory(null);
			Assert.AreEqual(0, module.Count);
			Assert.AreEqual(1, logger.Logs.Where(item => item.Contains("Error")).Count());
		}

		[TestMethod]
		public void ShouldNotCrashWhenDirectoryEnumeratorThrowsException()
		{
			StyleSheetLibraryModule module;
			MemoryLogger logger;

			logger = new MemoryLogger(new DefaultLogFormatter());
			module = new StyleSheetLibraryModule(logger, new MockedDirectoryEnumerator(5,true), new MockedFileLoader<StyleSheet>());
			module.LoadDirectory("path");
			Assert.AreEqual(0, module.Count);
			Assert.AreEqual(1, logger.Logs.Where(item=>item.Contains("Error")).Count());
		}
		[TestMethod]
		public void ShouldNotCrashWhenFileLoaderThrowsException()
		{
			StyleSheetLibraryModule module;
			MemoryLogger logger;


			logger = new MemoryLogger(new DefaultLogFormatter());
			module = new StyleSheetLibraryModule(logger, new MockedDirectoryEnumerator(5), new MockedFileLoader<StyleSheet>(true));
			module.LoadDirectory("path");
			Assert.AreEqual(0, module.Count);
			Assert.AreEqual(5, logger.Logs.Where(item => item.Contains("Error")).Count());
		}

		[TestMethod]
		public void ShouldGetStyleSheet()
		{
			StyleSheetLibraryModule module;
			MemoryLogger logger;
			StyleSheet f1, f2, f3, result;

			f1 = new StyleSheet() { NameSpace = "File0.xml" };
			f2 = new StyleSheet() { NameSpace = "File1.xml" };
			f3 = new StyleSheet() { NameSpace = "File2.xml" };

			logger = new MemoryLogger(new DefaultLogFormatter());
			module = new StyleSheetLibraryModule(logger, new MockedDirectoryEnumerator(3), new MockedStyleSheetFileLoader(f1, f2, f3));
			module.LoadDirectory("path");
			Assert.AreEqual(3, module.Count);

			result = module.GetStyleSheet("File0.xml");
			Assert.AreEqual(f1, result);
			result = module.GetStyleSheet("File1.xml");
			Assert.AreEqual(f2, result);
			result = module.GetStyleSheet("File2.xml");
			Assert.AreEqual(f3, result);


		}
		[TestMethod]
		public void ShouldNotGetStyleSheet()
		{
			StyleSheetLibraryModule module;
			MemoryLogger logger;
			StyleSheet f1, f2, f3, result;

			f1 = new StyleSheet() { NameSpace = "File0.xml" };
			f2 = new StyleSheet() { NameSpace = "File1.xml" };
			f3 = new StyleSheet() { NameSpace = "File2.xml" };

			logger = new MemoryLogger(new DefaultLogFormatter());
			module = new StyleSheetLibraryModule(logger, new MockedDirectoryEnumerator(3), new MockedStyleSheetFileLoader(f1, f2, f3));
			module.LoadDirectory("path");
			Assert.AreEqual(3, module.Count);

			result = module.GetStyleSheet("File4.xml");
			Assert.IsNull(result);
			Assert.AreEqual(1, logger.Logs.Where(item => item.Contains("Warning")).Count());


		}



	}
}
