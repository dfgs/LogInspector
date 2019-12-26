using LogInspector.Modules.FileLoaders;
using LogLib;
using ModuleLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules.LibraryModules
{
	public abstract class LibraryModule<T> : Module, ILibraryModule<T>
	{
		private IDirectoryEnumerator directoryEnumerator;
		private IFileLoader<T> fileLoader;

		private List<T> items;
		protected IEnumerable<T> Items
		{
			get { return items; }
		}
		public int Count
		{
			get { return items.Count; }
		}

		public LibraryModule(ILogger Logger, IDirectoryEnumerator DirectoryEnumerator, IFileLoader<T> FileLoader) : base(Logger)
		{
			AssertParameterNotNull(DirectoryEnumerator, "DirectoryEnumerator", out directoryEnumerator);
			AssertParameterNotNull(FileLoader, "FileLoader", out fileLoader);
			items = new List<T>();
		}

		protected void OnItemLoaded(T Item)
		{
			items.Add(Item);
		}

		public void LoadDirectory(string Path)
		{
			T item;

			if (!AssertParameterNotNull(Path, "Path")) return;

			Log(LogLevels.Information, $"Parsing directory {Path}");
			try
			{
				foreach (string FileName in directoryEnumerator.EnumerateFiles(Path))
				{
					Log(LogLevels.Information, $"Loading file {FileName}");
					if (!Try(() => fileLoader.Load(FileName)).OrAlert(out item, "Failed to load file")) continue;
					Try(() => OnItemLoaded(item)).OrAlert("Failed to add item in library");
				}
			}
			catch (Exception ex)
			{
				Log(ex);
			}
		}




	}
}

