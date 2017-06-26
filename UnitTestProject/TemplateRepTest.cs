using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskHelper;
using TaskHelper.Model;

namespace UnitTestProject
{
	[TestClass]
	public class TemplateRepTest
	{
		[TestMethod]
		public void CreateEmptyTemplate()
		{
			Template temp = new Template();
			TemplateRep repos = new TemplateRep("");
			repos.CreateTemplate(temp);
		}
	}
}
