using LogInspector.Models;
using ModuleLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.LibraryModules
{
	public interface IFormatHandlerLibraryModule : ILibraryModule<FormatHandler>
	{
		FormatHandler GetFormatHandler(string FileName);

	}
}

