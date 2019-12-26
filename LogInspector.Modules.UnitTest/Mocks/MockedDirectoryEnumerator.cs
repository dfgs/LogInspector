using LogInspector.Modules.FileLoaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.UnitTest.Mocks
{
	public class MockedDirectoryEnumerator : IDirectoryEnumerator
	{
		private bool throwException;
		private int count;
		public MockedDirectoryEnumerator(int Count,bool ThrowException=false)
		{
			this.count = Count;
			this.throwException = ThrowException;
		}

		public IEnumerable<string> EnumerateFiles(string Path)
		{
			if (throwException) throw new System.IO.IOException();
			for (int t = 0; t < count; t++) yield return $"File{t}.xml";
		}

		
	}
}
