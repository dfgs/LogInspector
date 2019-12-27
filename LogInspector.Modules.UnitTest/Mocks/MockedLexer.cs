using LexerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.UnitTest.Mocks
{
	public class MockedLexer : ILexer
	{
		public Token Read(ICharReader Reader)
		{
			throw new NotImplementedException();
		}

		public bool TryRead(ICharReader Reader, out Token Token)
		{
			throw new NotImplementedException();
		}
	}
}
