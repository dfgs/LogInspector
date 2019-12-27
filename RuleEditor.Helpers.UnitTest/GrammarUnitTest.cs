using System;
using LexerLib.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RuleEditor.Helpers;
using Sprache;

namespace RuleEditor.Helpers.UnitTest
{
	[TestClass]
	public class GrammarUnitTest
	{
		[TestMethod]
		public void ShouldParseAnyCharacter()
		{
			AnyCharacter predicate;

			predicate = Grammar.AnyCharacterParser.Parse(".");
			Assert.IsNotNull(predicate);
		}

		[TestMethod]
		public void ShouldParseEscapedCharacter()
		{
			Character predicate;

			predicate = Grammar.CharacterParser.Parse(@"\.");
			Assert.IsNotNull(predicate);
			Assert.AreEqual('.', predicate.Value);
			predicate = Grammar.CharacterParser.Parse(@"\a");
			Assert.IsNotNull(predicate);
			Assert.AreEqual('a', predicate.Value);
			predicate = Grammar.CharacterParser.Parse(@"\\");
			Assert.IsNotNull(predicate);
			Assert.AreEqual('\\', predicate.Value);
		}
		[TestMethod]
		public void ShouldParseEscapedCharacterUsingCode()
		{
			Character predicate;

			predicate = Grammar.CharacterParser.Parse(@"\32");
			Assert.IsNotNull(predicate);
			Assert.AreEqual(' ', predicate.Value);
			predicate = Grammar.CharacterParser.Parse(@"\65");
			Assert.IsNotNull(predicate);
			Assert.AreEqual('A', predicate.Value);
			
		}
		[TestMethod]
		public void ShouldParseExceptCharacter()
		{
			ExceptCharacter predicate;

			predicate = Grammar.ExceptCharacterParser.Parse("!a");
			Assert.IsNotNull(predicate);
			Assert.AreEqual('a', predicate.Value);
		}

		[TestMethod]
		public void ShouldParseCharacter()
		{
			Character predicate;

			predicate = Grammar.CharacterParser.Parse("a");
			Assert.IsNotNull(predicate);
			Assert.AreEqual('a', predicate.Value);

			Assert.ThrowsException<Sprache.ParseException>(() => Grammar.CharacterParser.Parse("."));
			Assert.ThrowsException<Sprache.ParseException>(() => Grammar.CharacterParser.Parse(@"\"));
		}
		[TestMethod]
		public void ShouldParseCharacterRange()
		{
			CharacterRange predicate;

			predicate = Grammar.CharacterRangeParser.Parse("[a-z]");
			Assert.IsNotNull(predicate);
			Assert.AreEqual('a', predicate.FirstValue);
			Assert.AreEqual('z', predicate.LastValue);
			
			predicate = Grammar.CharacterRangeParser.Parse(@"[\32-\65]");
			Assert.IsNotNull(predicate);
			Assert.AreEqual(' ', predicate.FirstValue);
			Assert.AreEqual('A', predicate.LastValue);
		}

		[TestMethod]
		public void ShouldParseExceptCharacterRange()
		{
			ExceptCharacterRange predicate;

			predicate = Grammar.ExceptCharacterRangeParser.Parse("![a-z]");
			Assert.IsNotNull(predicate);
			Assert.AreEqual('a', predicate.FirstValue);
			Assert.AreEqual('z', predicate.LastValue);

			predicate = Grammar.ExceptCharacterRangeParser.Parse(@"![\32-\65]");
			Assert.IsNotNull(predicate);
			Assert.AreEqual(' ', predicate.FirstValue);
			Assert.AreEqual('A', predicate.LastValue);

			predicate = Grammar.ExceptCharacterRangeParser.Parse(@"![\0-\255]");
			Assert.IsNotNull(predicate);
			Assert.AreEqual(char.MinValue, predicate.FirstValue);
			Assert.AreEqual((char)255, predicate.LastValue);
		}

		[TestMethod]
		public void ShouldParsePerhaps()
		{
			Perhaps predicate;

			predicate = Grammar.PerhapsParser.Parse("a?");
			Assert.IsNotNull(predicate);
			Assert.IsNotNull(predicate.Item);

			predicate = Grammar.PerhapsParser.Parse(".?");
			Assert.IsNotNull(predicate);
			Assert.IsNotNull(predicate.Item);

			predicate = Grammar.PerhapsParser.Parse(@"\.?");
			Assert.IsNotNull(predicate);
			Assert.IsNotNull(predicate.Item);
		}

		[TestMethod]
		public void ShouldParseOneOrMoreTimes()
		{
			OneOrMoreTimes predicate;

			predicate = Grammar.OneOrMoreTimesParser.Parse("a+");
			Assert.IsNotNull(predicate);
			Assert.IsNotNull(predicate.Item);

			predicate = Grammar.OneOrMoreTimesParser.Parse(".+");
			Assert.IsNotNull(predicate);
			Assert.IsNotNull(predicate.Item);

			predicate = Grammar.OneOrMoreTimesParser.Parse(@"\++");
			Assert.IsNotNull(predicate);
			Assert.IsNotNull(predicate.Item);
		}

		[TestMethod]
		public void ShouldParseZeroOrMoreTimes()
		{
			ZeroOrMoreTimes predicate;

			predicate = Grammar.ZeroOrMoreTimesParser.Parse("a*");
			Assert.IsNotNull(predicate);
			Assert.IsNotNull(predicate.Item);

			predicate = Grammar.ZeroOrMoreTimesParser.Parse(".*");
			Assert.IsNotNull(predicate);
			Assert.IsNotNull(predicate.Item);

			predicate = Grammar.ZeroOrMoreTimesParser.Parse(@"\+*");
			Assert.IsNotNull(predicate);
			Assert.IsNotNull(predicate.Item);
		}

		[TestMethod]
		public void ShouldParseOr()
		{
			Or predicate;

			predicate = Grammar.OrParser.Parse("a|b");
			Assert.IsNotNull(predicate);
			Assert.AreEqual(2,predicate.Items.Count);

			predicate = Grammar.OrParser.Parse(@"a|b|.|\+");
			Assert.IsNotNull(predicate);
			Assert.AreEqual(4, predicate.Items.Count);

			predicate = Grammar.OrParser.Parse(@"a*|b+|.|\+");
			Assert.IsNotNull(predicate);
			Assert.AreEqual(4, predicate.Items.Count);

		}

		[TestMethod]
		public void ShouldParseSequence()
		{
			Sequence predicate;

			predicate = Grammar.SequenceParser.Parse(@"a?c*a.b\+b+a|b");
			Assert.IsNotNull(predicate);
			Assert.AreEqual(8, predicate.Items.Count);

			predicate = Grammar.SequenceParser.Parse(@"a?![\0-\255]b!a[c-d]");
			Assert.IsNotNull(predicate);
			Assert.AreEqual(5, predicate.Items.Count);

		}

	}
}
