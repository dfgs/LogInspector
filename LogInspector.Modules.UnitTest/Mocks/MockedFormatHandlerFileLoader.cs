using LogInspector.Models;
using LogInspector.Modules.FileLoaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.UnitTest.Mocks
{
	public class MockedFormatHandlerFileLoader : IFileLoader<FormatHandler>
	{
		public FormatHandler[] items;

		public MockedFormatHandlerFileLoader(params FormatHandler[] FormatHandlers)
		{
			this.items = FormatHandlers;
		}

		public FormatHandler Load(string FileName)
		{
			FormatHandler item;

			item = items.FirstOrDefault(i => i.Name == FileName);
			if (item == null) throw new InvalidOperationException($"Failed to find format handler with name {FileName}");
			return item;
		}
	}
}
