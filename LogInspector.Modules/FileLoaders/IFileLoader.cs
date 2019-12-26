using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.FileLoaders
{
	public interface IFileLoader<T>
	{
		T Load(string FileName);
	}
}
