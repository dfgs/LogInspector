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
		private IStyleSheetLibraryModule styleSheetLibraryModule;
	
		public StyleProviderFactoryModule(ILogger Logger,  IStyleSheetLibraryModule StyleSheetLibraryModule) : base(Logger)
		{
			AssertParameterNotNull(StyleSheetLibraryModule, "GrammarLibraryModule", out styleSheetLibraryModule);

			
		}

		public IStyleProvider BuildStyleProvider(FormatHandler FormatHandler)
		{
	
			StyleSheet styleSheet;
			List<Style> styles;
			StyleProvider styleProvider;

			LogEnter();

			if (!AssertParameterNotNull(FormatHandler, "FormatHandler")) return null;

			Log(LogLevels.Information, "Building style provider from format handler rules");
			styles = new List<Style>();
			
	
			foreach(string styleSheetName in FormatHandler.StyleSheets)
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
