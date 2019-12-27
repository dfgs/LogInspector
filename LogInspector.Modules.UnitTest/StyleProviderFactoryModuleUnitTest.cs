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
			Assert.ThrowsException<ArgumentNullException>(() => new StyleProviderFactoryModule(null,  new MockedStyleSheetLibraryModule()));
			Assert.ThrowsException<ArgumentNullException>(() => new StyleProviderFactoryModule(NullLogger.Instance,  null));
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

			module = new StyleProviderFactoryModule(NullLogger.Instance,  new MockedStyleSheetLibraryModule(styleSheet));
			result=module.BuildStyleProvider(formatHandler);
			Assert.IsNotNull(result);
			
		}
		[TestMethod]
		public void ShouldBuildEmptyStyleProviderWhenInvalidParameterSet()
		{
			StyleProviderFactoryModule module;
			IStyleProvider result;
			StyleSheet styleSheet;
			MemoryLogger logger;

			logger = new MemoryLogger(new DefaultLogFormatter());

			styleSheet = new StyleSheet();
			styleSheet.NameSpace = "stylesheet";
			styleSheet.Items.Add(new Style() { Class = "C1" });
			styleSheet.Items.Add(new Style() { Class = "C2" });
			styleSheet.Items.Add(new Style() { Class = "C3" });

			module = new StyleProviderFactoryModule(logger, new MockedStyleSheetLibraryModule(styleSheet));

			result = module.BuildStyleProvider(null);
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


			module = new StyleProviderFactoryModule(logger, new MockedStyleSheetLibraryModule(styleSheet));

			result = module.BuildStyleProvider(formatHandler);
			Assert.IsNotNull(result);
			Assert.AreEqual(0, result.Count);

		}
	}
}
