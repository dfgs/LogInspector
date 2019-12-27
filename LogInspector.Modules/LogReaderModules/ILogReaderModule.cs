using LexerLib;
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
		void Read(ICharReader Reader);
	}
}
