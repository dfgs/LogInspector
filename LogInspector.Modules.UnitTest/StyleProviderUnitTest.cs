using System;
using LogInspector.Modules.LibraryModules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogLib;
using LogInspector.Modules.UnitTest.Mocks;
using LogInspector.Models;
using System.Linq;
using LexerLib;
using LexerLib.Predicates;

namespace LogInspector.Modules.UnitTest
{
	[TestClass]
	public class StyleProviderUnitTest
	{
		[TestMethod]
		public void ShouldHaveValidConstructor()
		{
			StyleProvider styleProvider;
			Style s;

			Assert.ThrowsException<ArgumentNullException>(() => new StyleProvider(null));

			// duplicate keys should not throw exceptions
			s = new Style() {Class="test" };
			styleProvider = new StyleProvider(s, s, s);
			Assert.AreEqual(1, styleProvider.Count);
		}

		[TestMethod]
		public void ShouldGetStyle()
		{
			StyleProvider styleProvider;
			Style s1,s2,s3,s;

			s1 = new Style() { Class = "C1" };
			s2 = new Style() { Class = "C2" };
			s3 = new Style() { Class = "C3" };
			styleProvider = new StyleProvider(s1, s2, s3);
			Assert.AreEqual(3, styleProvider.Count);

			s = styleProvider.GetStyle(new Token("C1", null));
			Assert.AreEqual(s1, s);
			s = styleProvider.GetStyle(new Token("C2", null));
			Assert.AreEqual(s2, s);
			s = styleProvider.GetStyle(new Token("C3", null));
			Assert.AreEqual(s3, s);
		}

		[TestMethod]
		public void ShouldNotGetStyle()
		{
			StyleProvider styleProvider;
			Style s1, s2, s3, s;

			s1 = new Style() { Class = "C1" };
			s2 = new Style() { Class = "C2" };
			s3 = new Style() { Class = "C3" };
			styleProvider = new StyleProvider(s1, s2, s3);
			Assert.AreEqual(3, styleProvider.Count);

			s = styleProvider.GetStyle(new Token("C4", null));
			Assert.IsNull(s);
		}





	}
}
