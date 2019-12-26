using LexerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules
{
	public interface ILexerFactoryModule
	{
		ILexer BuildLexer(string FileName);
	}
}
