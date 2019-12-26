using ModuleLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.LibraryModules
{
	public interface ILibraryModule<T> : IModule
	{
		int Count
		{
			get;
		}
		void LoadDirectory(string Path);

	}
}

