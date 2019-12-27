using LexerLib;
using LogInspector.Models;
using ModuleLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules
{
	public interface IStyleProviderFactoryModule:IModule
	{
		IStyleProvider BuildStyleProvider(string FileName);
	}
}
