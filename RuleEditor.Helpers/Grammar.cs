using LexerLib.Predicates;
using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parse = Sprache.Parse;

namespace RuleEditor.Helpers
{
    public static class Grammar
    {
        private static IEnumerable<Predicate> Concat(Predicate First,IEnumerable<Predicate> Next)
        {
            yield return First;
            foreach (Predicate next in Next) yield return next;
        }

        public static readonly Parser<AnyCharacter> AnyCharacterParser =
            from _ in Parse.Char('.')
            select new AnyCharacter();
        public static readonly Parser<Character> EscapedCharacterParser =
             from _ in Parse.Char('\\')
             from value in Parse.AnyChar
             select new Character(value);
        public static readonly Parser<ExceptCharacter> ExceptCharacterParser =
              from _ in Parse.Char('!')
              from value in Parse.AnyChar
              select new ExceptCharacter(value);

        public static readonly Parser<Character> CharacterParser =
             from value in Parse.CharExcept(@".\[!")
             select new Character(value);

        public static readonly Parser<CharacterRange> CharacterRangeParser =
            from _ in Parse.Char('[')
            from firstValue in Parse.AnyChar
            from __ in Parse.Char('-')
            from lastValue in Parse.AnyChar
            from ___ in Parse.Char(']')
            select new CharacterRange(firstValue, lastValue);
        
        public static readonly Parser<ExceptCharacterRange> ExceptCharacterRangeParser =
             from _ in Parse.Char('!')
             from __ in Parse.Char('[')
             from firstValue in Parse.AnyChar
             from ___ in Parse.Char('-')
             from lastValue in Parse.AnyChar
             from ____ in Parse.Char(']')
             select new ExceptCharacterRange(firstValue, lastValue);


        private static readonly Parser<Predicate> ShiftPredicateParser =
             from predicate in AnyCharacterParser.Or<Predicate>(CharacterRangeParser).Or<Predicate>(ExceptCharacterRangeParser).Or<Predicate>(EscapedCharacterParser).Or<Predicate>(ExceptCharacterParser).Or<Predicate>(CharacterParser)
             select predicate;


        public static readonly Parser<Perhaps> PerhapsParser =
             from predicate in ShiftPredicateParser
             from _ in Parse.Char('?')
             select new Perhaps(predicate);

        public static readonly Parser<OneOrMoreTimes> OneOrMoreTimesParser =
             from predicate in ShiftPredicateParser
             from _ in Parse.Char('+')
             select new OneOrMoreTimes(predicate);

        public static readonly Parser<ZeroOrMoreTimes> ZeroOrMoreTimesParser =
             from predicate in ShiftPredicateParser
             from _ in Parse.Char('*')
             select new ZeroOrMoreTimes(predicate);

        private static readonly Parser<Predicate> OrItemParser = PerhapsParser.Or<Predicate>(OneOrMoreTimesParser).Or<Predicate>(ZeroOrMoreTimesParser).Or<Predicate>(ShiftPredicateParser);
        
        private static readonly Parser<Predicate> OrGroupParser =
            from _ in Parse.Char('|')
            from predicate in OrItemParser
            select predicate;

        public static readonly Parser<Or> OrParser =
            from firstPredicate in OrItemParser
            from nextPredicates in OrGroupParser.AtLeastOnce()
            select new Or(Concat(firstPredicate,nextPredicates).ToArray());

        private static readonly Parser<Predicate> SequenceItemParser = PerhapsParser.Or<Predicate>(OneOrMoreTimesParser).Or<Predicate>(ZeroOrMoreTimesParser).Or<Predicate>(OrParser).Or<Predicate>(ShiftPredicateParser);

        public static readonly Parser<Sequence> SequenceParser =
            from predicates in SequenceItemParser.AtLeastOnce()
            select new Sequence(predicates.ToArray());

        public static readonly Parser<Predicate> PredicateParser =
            from predicate in PerhapsParser.Or<Predicate>(OneOrMoreTimesParser).Or<Predicate>(ZeroOrMoreTimesParser).Or<Predicate>(OrParser).Or<Predicate>(SequenceParser).Or<Predicate>(ShiftPredicateParser).End()
            
            select predicate;
    }
}
