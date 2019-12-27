using LogInspector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.FileLoaders
{
	public class StyleSheetFileLoader : IFileLoader<StyleSheet>
	{
		public StyleSheet Load(string FileName)
		{
			return StyleSheet.LoadFromFile(FileName);
		}
	}
}
