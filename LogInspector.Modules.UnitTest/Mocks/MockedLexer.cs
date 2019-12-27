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
			char value;

			value = Reader.Read();
			return new Token(value.ToString(), value.ToString());
		}

		public bool TryRead(ICharReader Reader, out Token Token)
		{
			char value;

			value = Reader.Read();
			Token= new Token(value.ToString(), value.ToString());
			return true;
		}

	}
}
