using Generator.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject
{
	[TestClass]
	public class GeneratorTest
	{
		[TestMethod]
		public void DeciderEnumeration()
		{
			int[] count = { 0, 0, 0, 0 };
			for (int i1 = 0; i1 < 10; i1++)
				for (int i2 = 0; i2 < 10; i2++)
					for (int i3 = 0; i3 < 10; i3++)
						for (int i4 = 0; i4 < 10; i4++)
							switch (RandDecide.Decide(false,i1, i2, i3, i4))
							{
								case OperationEnum.mult:
									count[0]++;
									break;
								case OperationEnum.sum:
									count[1]++;
									break;
								case OperationEnum.transe:
									count[2]++;
									break;
								case OperationEnum.inverse:
									count[3]++;
									break;
							}
		}
	}
}
