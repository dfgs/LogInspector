using LexerLib;
using LogInspector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogInspector.Modules
{
	public class StyleProvider:IStyleProvider
	{
		private IDictionary<string, Style> items;
		public int Count
		{
			get { return items.Count; }
		}
		public StyleProvider(params Style[] Styles)
		{
			if (Styles == null) throw new ArgumentNullException("Styles");

			items = new Dictionary<string, Style>();
			foreach(Style style in Styles)
			{
				if (!items.ContainsKey(style.Class)) items.Add(style.Class, style);
			}
				
		}

		public Style GetStyle(Token Token)
		{
			Style result;
			items.TryGetValue(Token.Class, out result);
			return result;
		}


	}
}
