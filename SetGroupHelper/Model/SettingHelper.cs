using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetGroupHelper.Model
{
	public class SettingHelper
	{
		public SettingHelper(string connectionString)
		{
			this.connectionString = connectionString;
		}
		public string connectionString;
		public void DeleteSettings(string Id_teacher)
		{
			using (IDbConnection db = new MySqlConnection(connectionString))
			{

				//db.Query("DELETE FROM teach_setting WHERE Id_teacher=@a", new { a = Id_teacher });
				List<int> indexes=db.Query<int>(@"SELECT Id_teach_setting FROM teach_setting 
						WHERE Id_teach_setting IN 
						(SELECT Id_teach_setting FROM teach_setting
						WHERE Id_teacher=@a) 
						AND Id_teach_setting NOT IN
						(SELECT template_setting.Id_teach_setting FROM template, template_setting
						WHERE template.Id_teacher = @a AND template.Id_template= template_setting.Id_template)", new { a = Id_teacher }).ToList();
				if(indexes.Count>0)
					foreach(int index in indexes)
					{
						db.Query<int>(@"DELETE FROM teach_setting WHERE Id_teach_setting=@a", new { a = index });
					}
			}
		}
		public List<TeachSetting> GetSettings(string Id_teacher)
		{
			List<TeachSetting> templateTypeList = new List<TeachSetting>();
			using (IDbConnection db = new MySqlConnection(connectionString))
			{
				templateTypeList = db.Query<TeachSetting>("SELECT * FROM teach_setting WHERE Id_teacher=@a", new { a = Id_teacher }).ToList();
			}
			return templateTypeList;
		}
		public void InsertSettings(List<TeachSetting> settings)
		{
			using (IDbConnection db = new MySqlConnection(connectionString))
			{
				foreach (TeachSetting setting in settings)
					db.Query("INSERT INTO teach_setting (Id_teacher, Id_group) VALUES(@a, @b)", new { a = setting.Id_teacher, b = setting.Id_group });
			}
		}
	}
}
