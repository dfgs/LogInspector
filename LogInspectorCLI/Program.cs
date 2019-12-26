using LogLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspectorCLI
{
	class Program
	{
		static void Main(string[] args)
		{
			ILogger logger;

			logger = new ConsoleLogger(new DefaultLogFormatter());

			if ((args==null) || (args.Length==0))
			{
				logger.Log(0, "Program", "Main", LogLevels.Error, "No input file provided");
				return;
			}


			Console.ReadLine();
		}
	}
}
