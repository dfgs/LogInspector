using LexerLib;
using LogInspector.Models;
using ModuleLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.LogReaderModules
{
	public interface ILogReaderModule : IModule
	{
		Log Read(ICharReader Reader);
	}
}
