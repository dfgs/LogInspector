using LexerLib;
using LogInspector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules
{
	public interface IStyleProvider
	{
		int Count
		{
			get;
		}
		Style GetStyle(Token Token);
	}
}
