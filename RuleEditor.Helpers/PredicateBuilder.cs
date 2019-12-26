using LexerLib.Predicates;
using Sprache;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuleEditor.Helpers
{
	public static class PredicateBuilder
	{
		public static Predicate Build(string Value)
		{
			return Grammar.PredicateParser.Parse(Value);
		}
	}
}
