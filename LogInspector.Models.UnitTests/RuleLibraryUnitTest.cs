using System;
using System.IO;
using System.Xml.Serialization;
using LexerLib;
using LexerLib.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogInspector.Models.UnitTests
{
	[TestClass]
	public class RuleLibraryUnitTest
	{
		/*[TestMethod]
		public void CreateRuleSample()
		{
			XmlSerializer serializer;
			RuleLibrary ruleLibraryA;
			Stream stream;

			ruleLibraryA = new RuleLibrary();
			ruleLibraryA.NameSpace = "Default";
			ruleLibraryA.Items.Add(new Rule("RuleA", Parse.Character('a')));
			ruleLibraryA.Items.Add(new Rule("RuleB", Parse.Character('b')));

			using (stream = new FileStream(@"d:\example.xml",FileMode.Create))
			{
				serializer = new XmlSerializer(typeof(RuleLibrary));
				serializer.Serialize(stream, ruleLibraryA);
				
			}
			Assert.Fail("Do not run");
		}//*/

		[TestMethod]
		public void ShouldSerializeAndDeserialize()
		{
			XmlSerializer serializer;
			Grammar ruleLibraryA, ruleLibraryB;
			MemoryStream stream;

			ruleLibraryA = new Grammar();
			ruleLibraryA.NameSpace = "Default";
			ruleLibraryA.Items.Add(new Rule("RuleA", Parse.Character('a')));
			ruleLibraryA.Items.Add(new Rule("RuleB", Parse.Character('b')));

			using (stream = new MemoryStream())
			{
				serializer = new XmlSerializer(typeof(Grammar));
				serializer.Serialize(stream, ruleLibraryA);
				stream.Seek(0, SeekOrigin.Begin);
				ruleLibraryB = serializer.Deserialize(stream) as Grammar;
			}

			Assert.IsNotNull(ruleLibraryB);
			Assert.AreEqual(ruleLibraryA.NameSpace, ruleLibraryB.NameSpace);
			Assert.AreEqual(ruleLibraryA.Items.Count, ruleLibraryB.Items.Count);
			Assert.AreEqual(ruleLibraryA.Items[0].Name, ruleLibraryB.Items[0].Name);


		}
	}
}
