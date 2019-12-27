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
	public class StyleProviderFactoryModuleUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new StyleProviderFactoryModule(null, new MockedFormatHandlerLibraryModule(), new MockedStyleSheetLibraryModule()));
			Assert.ThrowsException<ArgumentNullException>(() => new StyleProviderFactoryModule(NullLogger.Instance, null, new MockedStyleSheetLibraryModule()));
			Assert.ThrowsException<ArgumentNullException>(() => new StyleProviderFactoryModule(NullLogger.Instance, new MockedFormatHandlerLibraryModule(), null));
		}

		[TestMethod]
		public void ShouldBuildStyleProvider()
		{
			StyleProviderFactoryModule module;
			IStyleProvider result;
			FormatHandler formatHandler;
			StyleSheet styleSheet;

			styleSheet = new StyleSheet();
			styleSheet.NameSpace = "stylesheet";
			styleSheet.Items.Add(new Style() { Class = "C1" });
			styleSheet.Items.Add(new Style() { Class = "C2" });
			styleSheet.Items.Add(new Style() { Class = "C3" });

			formatHandler = new FormatHandler() { FileNamePattern = "test.log" };
			formatHandler.StyleSheets.Add("stylesheet");

			module = new StyleProviderFactoryModule(NullLogger.Instance, new MockedFormatHandlerLibraryModule(formatHandler), new MockedStyleSheetLibraryModule(styleSheet));
			result=module.BuildStyleProvider("test.log");
			Assert.IsNotNull(result);
			
		}
		[TestMethod]
		public void ShouldNotBuildStyleProviderWhenFormatHandlerNotFound()
		{
			StyleProviderFactoryModule module;
			IStyleProvider result;
			FormatHandler formatHandler;
			StyleSheet styleSheet;
			MemoryLogger logger;

			logger = new MemoryLogger(new DefaultLogFormatter());

			styleSheet = new StyleSheet();
			styleSheet.NameSpace = "stylesheet";
			styleSheet.Items.Add(new Style() { Class = "C1" });
			styleSheet.Items.Add(new Style() { Class = "C2" });
			styleSheet.Items.Add(new Style() { Class = "C3" });

			formatHandler = new FormatHandler() { FileNamePattern = "test.log" };
			formatHandler.StyleSheets.Add("stylesheet");


			module = new StyleProviderFactoryModule(logger, new MockedFormatHandlerLibraryModule(formatHandler), new MockedStyleSheetLibraryModule(styleSheet));
			
			result = module.BuildStyleProvider("test1.log");
			Assert.IsNull(result);
			Assert.AreEqual(1, logger.Logs.Where(item => item.Contains("Error")).Count());
		}
		[TestMethod]
		public void ShouldBuildEmptyStyleProviderWhenStyleSheetNotFound()
		{
			StyleProviderFactoryModule module;
			IStyleProvider result;
			FormatHandler formatHandler;
			StyleSheet styleSheet;
			MemoryLogger logger;

			logger = new MemoryLogger(new DefaultLogFormatter());

			styleSheet = new StyleSheet();
			styleSheet.NameSpace = "stylesheet";
			styleSheet.Items.Add(new Style() { Class = "C1" });
			styleSheet.Items.Add(new Style() { Class = "C2" });
			styleSheet.Items.Add(new Style() { Class = "C3" });

			formatHandler = new FormatHandler() { FileNamePattern = "test.log" };
			formatHandler.StyleSheets.Add("stylesheet1");


			module = new StyleProviderFactoryModule(logger, new MockedFormatHandlerLibraryModule(formatHandler), new MockedStyleSheetLibraryModule(styleSheet));

			result = module.BuildStyleProvider("test.log");
			Assert.IsNotNull(result);
			Assert.AreEqual(0, result.Count);

		}
	}
}
