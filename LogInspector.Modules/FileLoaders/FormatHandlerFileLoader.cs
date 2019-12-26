using LogInspector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.FileLoaders
{
	public class FormatHandlerFileLoader : IFileLoader<FormatHandler>
	{
		public FormatHandler Load(string FileName)
		{
			return FormatHandler.LoadFromFile(FileName);
		}
	}
}
