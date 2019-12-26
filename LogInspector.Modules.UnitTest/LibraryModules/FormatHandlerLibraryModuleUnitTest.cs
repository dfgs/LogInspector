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
	public class LexerFactoryModuleUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new FormatHandlerLibraryModule(null));
			Assert.ThrowsException<ArgumentNullException>(() => new FormatHandlerLibraryModule(NullLogger.Instance,null,new MockedFileLoader<FormatHandler>()));
			Assert.ThrowsException<ArgumentNullException>(() => new FormatHandlerLibraryModule(NullLogger.Instance,new MockedDirectoryEnumerator(5), null));
		}

		[TestMethod]
		public void ShouldLoadDirectory()
		{
			FormatHandlerLibraryModule module;

			module = new FormatHandlerLibraryModule(NullLogger.Instance, new MockedDirectoryEnumerator(5), new MockedFileLoader<FormatHandler>());
			module.LoadDirectory("path");
			Assert.AreEqual(5, module.Count);
		}
		[TestMethod]
		public void ShouldNotLoadDirectory()
		{
			FormatHandlerLibraryModule module;
			MemoryLogger logger;

			logger = new MemoryLogger(new DefaultLogFormatter());
			module = new FormatHandlerLibraryModule(logger, new MockedDirectoryEnumerator(5), new MockedFileLoader<FormatHandler>());
			module.LoadDirectory(null);
			Assert.AreEqual(0, module.Count);
			Assert.AreEqual(1, logger.Logs.Where(item => item.Contains("Error")).Count());
		}

		[TestMethod]
		public void ShouldNotCrashWhenDirectoryEnumeratorThrowsException()
		{
			FormatHandlerLibraryModule module;
			MemoryLogger logger;

			logger = new MemoryLogger(new DefaultLogFormatter());
			module = new FormatHandlerLibraryModule(logger, new MockedDirectoryEnumerator(5,true), new MockedFileLoader<FormatHandler>());
			module.LoadDirectory("path");
			Assert.AreEqual(0, module.Count);
			Assert.AreEqual(1, logger.Logs.Where(item=>item.Contains("Error")).Count());
		}
		[TestMethod]
		public void ShouldNotCrashWhenFileLoaderThrowsException()
		{
			FormatHandlerLibraryModule module;
			MemoryLogger logger;


			logger = new MemoryLogger(new DefaultLogFormatter());
			module = new FormatHandlerLibraryModule(logger, new MockedDirectoryEnumerator(5), new MockedFileLoader<FormatHandler>(true));
			module.LoadDirectory("path");
			Assert.AreEqual(0, module.Count);
			Assert.AreEqual(5, logger.Logs.Where(item => item.Contains("Error")).Count());
		}


		[TestMethod]
		public void ShouldGetFormatHandler()
		{
			FormatHandlerLibraryModule module;
			MemoryLogger logger;
			FormatHandler f1, f2, f3,result;

			f1 = new FormatHandler() { Name = "File0.xml", FileNamePattern = "F1" };
			f2 = new FormatHandler() { Name = "File1.xml", FileNamePattern = "F2" };
			f3 = new FormatHandler() { Name = "File2.xml", FileNamePattern = "F3" };

			logger = new MemoryLogger(new DefaultLogFormatter());
			module = new FormatHandlerLibraryModule(logger, new MockedDirectoryEnumerator(3), new MockedFormatHandlerFileLoader(f1,f2,f3));
			module.LoadDirectory("path");
			Assert.AreEqual(3, module.Count);

			result = module.GetFormatHandler("F1");
			Assert.AreEqual(f1, result);
			result = module.GetFormatHandler("F2");
			Assert.AreEqual(f2, result);
			result = module.GetFormatHandler("F3");
			Assert.AreEqual(f3, result);


		}
		[TestMethod]
		public void ShouldNotGetFormatHandler()
		{
			FormatHandlerLibraryModule module;
			MemoryLogger logger;
			FormatHandler f1, f2, f3, result;

			f1 = new FormatHandler() { Name = "File0.xml", FileNamePattern = "F1" };
			f2 = new FormatHandler() { Name = "File1.xml", FileNamePattern = "F2" };
			f3 = new FormatHandler() { Name = "File2.xml", FileNamePattern = "F3" };

			logger = new MemoryLogger(new DefaultLogFormatter());
			module = new FormatHandlerLibraryModule(logger, new MockedDirectoryEnumerator(3), new MockedFormatHandlerFileLoader(f1, f2, f3));
			module.LoadDirectory("path");
			Assert.AreEqual(3, module.Count);

			result = module.GetFormatHandler("F4");
			Assert.IsNull(result);
			Assert.AreEqual(1, logger.Logs.Where(item => item.Contains("Warning")).Count());


		}

		[TestMethod]
		public void ShouldNotCrashWhenFileNamePatternIsInvalid()
		{
			FormatHandlerLibraryModule module;
			MemoryLogger logger;
			FormatHandler f1, f2, f3, result;

			f1 = new FormatHandler() { Name = "File0.xml", FileNamePattern = "F1" };
			f2 = new FormatHandler() { Name = "File1.xml", FileNamePattern = "F[2" };
			f3 = new FormatHandler() { Name = "File2.xml", FileNamePattern = "F3" };

			logger = new MemoryLogger(new DefaultLogFormatter());
			module = new FormatHandlerLibraryModule(logger, new MockedDirectoryEnumerator(3), new MockedFormatHandlerFileLoader(f1, f2, f3));
			module.LoadDirectory("path");
			Assert.AreEqual(3, module.Count);

			result = module.GetFormatHandler("F1");
			Assert.AreEqual(f1,result);
			// doesn't try to build f2 pattern because f1 matches first		
			Assert.AreEqual(0, logger.Logs.Where(item => item.Contains("Error")).Count());

			result = module.GetFormatHandler("F2");
			Assert.IsNull(result);
			Assert.AreEqual(1, logger.Logs.Where(item => item.Contains("Warning")).Count());
			// tried to build f2 pattern because no matches		
			Assert.AreEqual(1, logger.Logs.Where(item => item.Contains("Error")).Count());

		}


	}
}
