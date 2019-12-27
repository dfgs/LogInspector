using LexerLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Models
{
	public class Log
	{
		public int LineNumber
		{
			get;
			set;
		}
		public List<Token> Tokens
		{
			get;
			set;
		}

		public Log()
		{
			this.Tokens = new List<Token>();
		}

		public override string ToString()
		{
			return LineNumber+": "+string.Join("", Tokens.Select(item => item.Value));
		}

	}
}
