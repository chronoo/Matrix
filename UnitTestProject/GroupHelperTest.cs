using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SetGroupHelper;
using SetGroupHelper.Model;

namespace SetGroupHelper.Tests
{
	[TestClass()]
	public class GroupHelperTest
	{
		[TestMethod]
		public void DeleteRecord0()
		{
			GroupSettingHelper helper = new GroupSettingHelper("");
			helper.DeleteRecord(0);
		}
		[TestMethod]
		public void DeleteImpossibleRecord()
		{
			GroupSettingHelper helper = new GroupSettingHelper("");
			helper.DeleteRecord(-1);
		}
		[TestMethod]
		public void GetZeroRecors()
		{
			GroupSettingHelper helper = new GroupSettingHelper("");
			helper.GetRecord(0);
		}
		[TestMethod]
		public void GetRecords()
		{
			GroupSettingHelper helper = new GroupSettingHelper("");
			helper.GetRecord(10000);
		}
	}
}