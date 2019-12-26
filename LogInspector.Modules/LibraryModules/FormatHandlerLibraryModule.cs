using LogInspector.Models;
using LogInspector.Modules.FileLoaders;
using LogLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LexerLib.Predicates;
using LexerLib;
using System.Text.RegularExpressions;

namespace LogInspector.Modules.LibraryModules
{
	public class FormatHandlerLibraryModule : LibraryModule<FormatHandler>,IFormatHandlerLibraryModule
	{
		public FormatHandlerLibraryModule(ILogger Logger, IDirectoryEnumerator DirectoryEnumerator, IFileLoader<FormatHandler> FileLoader) : base(Logger, DirectoryEnumerator, FileLoader)
		{
		}

		public FormatHandlerLibraryModule(ILogger Logger) : this(Logger, new DirectoryEnumerator(),new FormatHandlerFileLoader())
		{
		}

		public FormatHandler GetFormatHandler(string FileName)
		{
			Match match;

			LogEnter();
			Log(LogLevels.Information, $"Searching format handler for file {FileName}");
			foreach (FormatHandler formatHandler in Items)
			{
				if (!Try(() => Regex.Match(FileName, formatHandler.FileNamePattern)).OrAlert(out match, $"Failed to parse file name pattern {formatHandler.FileNamePattern} in format handler {formatHandler.Name}")) continue;
				
				if (match.Success)
				{
					Log(LogLevels.Information , $"Format handler {formatHandler.Name} found for file {FileName}");
					return formatHandler;
				}
			}
			Log(LogLevels.Warning, $"No format handler found for file {FileName}");
			return null;
		}
	}
}
