using LogInspector.Models;
using LogInspector.Modules.LibraryModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.UnitTest.Mocks
{
	public class MockedFormatHandlerLibraryModule : IFormatHandlerLibraryModule
	{
		public int Count => throw new NotImplementedException();

		public int ID => 0;

		public string ModuleName => "MockedFormatHandlerLibraryModule";

		private FormatHandler[] items;

		public MockedFormatHandlerLibraryModule(params FormatHandler[] Items)
		{
			this.items = Items;
		}
		public FormatHandler GetFormatHandler(string FileName)
		{
			return items.FirstOrDefault(item => item.FileNamePattern == FileName);
		}

		public void LoadDirectory(string Path)
		{
		}
	}
}
