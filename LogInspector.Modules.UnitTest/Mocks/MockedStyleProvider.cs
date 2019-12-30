using LexerLib;
using LogInspector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.UnitTest.Mocks
{
	public class MockedStyleProvider : IStyleProvider
	{
		public int Count => throw new NotImplementedException();

		public Style GetStyle(Token Token)
		{
			throw new NotImplementedException();
		}
	}
}
