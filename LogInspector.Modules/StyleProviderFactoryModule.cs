using LexerLib;
using LogInspector.Models;
using LogInspector.Modules.LibraryModules;
using LogLib;
using ModuleLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules
{
	public class StyleProviderFactoryModule : Module, IStyleProviderFactoryModule
	{
		private IFormatHandlerLibraryModule formatHandlerLibraryModule;
		private IStyleSheetLibraryModule styleSheetLibraryModule;
	
		public StyleProviderFactoryModule(ILogger Logger, IFormatHandlerLibraryModule FormatHandlerLibraryModule, IStyleSheetLibraryModule StyleSheetLibraryModule) : base(Logger)
		{
			AssertParameterNotNull(FormatHandlerLibraryModule, "FormatHandlerLibraryModule", out formatHandlerLibraryModule);
			AssertParameterNotNull(StyleSheetLibraryModule, "GrammarLibraryModule", out styleSheetLibraryModule);

			
		}

		public IStyleProvider BuildStyleProvider(string FileName)
		{
			FormatHandler formatHandler;
			StyleSheet styleSheet;
			List<Style> styles;
			StyleProvider styleProvider;

			LogEnter();

			if (!AssertParameterNotNull(FileName, "FileName")) return null;

			Log(LogLevels.Information, "Building style provider from format handler rules");
			styles = new List<Style>();
			
			formatHandler=formatHandlerLibraryModule.GetFormatHandler(FileName);
			if (formatHandler == null)
			{
				Log(LogLevels.Error, "Failed to build style provider");
				return null;
			}
			
			foreach(string styleSheetName in formatHandler.StyleSheets)
			{
				styleSheet = styleSheetLibraryModule.GetStyleSheet(styleSheetName);
				if (styleSheet == null) continue;
				styles.AddRange(styleSheet.Items);
			}

			styleProvider = new StyleProvider(styles.ToArray());
			return styleProvider;
		}
	}
}
