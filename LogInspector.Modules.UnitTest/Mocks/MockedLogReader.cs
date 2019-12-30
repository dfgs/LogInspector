using LexerLib;
using LogInspector.Models;
using LogInspector.Modules.LogReaderModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.UnitTest.Mocks
{
	public class MockedLogReader : ILogReaderModule
	{
		public int ID => throw new NotImplementedException();

		public string ModuleName => throw new NotImplementedException();

		public void AddIgnoredTokens(params string[] Tokens)
		{
			throw new NotImplementedException();
		}

		public Log Read(ICharReader Reader)
		{
			throw new NotImplementedException();
		}
	}
}
