using LogInspector.Models;
using LogInspector.Modules.FileLoaders;
using LogLib;
using RuleEditor.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LexerLib.Predicates;
using LexerLib;

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
			Predicate predicate;
			Lexer lexer;
			Token token;

			LogEnter();
			Log(LogLevels.Information, $"Searching format handler for file {FileName}");
			foreach (FormatHandler formatHandler in Items)
			{
				if (!Try(() => PredicateBuilder.Build(formatHandler.FileNamePattern)).OrAlert(out predicate, $"Failed to parse file name pattern {formatHandler.FileNamePattern} in format handler {formatHandler.Name}")) continue;
				lexer = new Lexer(new StringCharReader(FileName), new Rule("FormatHandler", predicate));
				if (lexer.TryRead(out token))
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
