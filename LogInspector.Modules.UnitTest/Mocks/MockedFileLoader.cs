using LogInspector.Modules.FileLoaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.UnitTest.Mocks
{
	public class MockedFileLoader<T> : IFileLoader<T>
		where T:new()
	{
		private bool throwException;
		public MockedFileLoader(bool ThrowException=false)
		{
			this.throwException = ThrowException;
		}

		public T Load(string FileName)
		{
			if (throwException) throw new System.IO.IOException();
			return new T();
		}
	}
}
