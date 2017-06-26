using Dapper;
using MySql.Data.MySqlClient;
using Parser.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SetGroupHelper.Model
{
	public class GroupSettingHelper
	{
		public GroupSettingHelper(string connectionString)
		{
			this.connectionString = connectionString;
		}
		public string connectionString;
		public void DeleteRecord(int Id_template)
		{
			using (IDbConnection db = new MySqlConnection(connectionString))
			{
				db.Query("DELETE FROM template_setting WHERE Id_template=@a", new { a = Id_template });
			}
		}
		public List<TeachSetting> GetRecord(int Id_template)
		{
			List<TeachSetting> templateSettingList = new List<TeachSetting>();
			using (IDbConnection db = new MySqlConnection(connectionString))
			{
				templateSettingList = db.Query<TeachSetting>(@"select teach_setting.*
																from template_setting, teach_setting
																where template_setting.Id_template = @a and
																teach_setting.Id_teach_setting = template_setting.Id_teach_setting",
																new { a = Id_template }).ToList();
			}
			return templateSettingList;
		}
		/*public List<TeachSetting> GetTemplateGroups(int Id_template)
		{
			List<TeachSetting> templateSettingList = new List<TeachSetting>();
			using (IDbConnection db = new MySqlConnection(connectionString))
			{
				templateSettingList = db.Query<TeachSetting>(@"select teach_setting.*
																from template_setting, teach_setting
																where template_setting.Id_template = @a and
																teach_setting.Id_teach_setting = template_setting.Id_teach_setting",
																new { a = Id_template }).ToList();
			}
			return templateSettingList;
		}*/
		public List<TeachSetting> GetTeachRecord(string Id_teacher)
		{
			List<TeachSetting> templateTypeList = new List<TeachSetting>();
			using (IDbConnection db = new MySqlConnection(connectionString))
			{
				templateTypeList = db.Query<TeachSetting>(@"SELECT * FROM teach_setting 
																WHERE teach_setting.Id_teacher=@a", new { a = Id_teacher }).ToList();
			}
			return templateTypeList;
		}
		public List<TemplateSetting> GetTeachRecord(string Id_teacher, int Id_template)
		{
			List<TemplateSetting> templateTypeList = new List<TemplateSetting>();
			using (IDbConnection db = new MySqlConnection(connectionString))
			{
				templateTypeList = db.Query<TemplateSetting>(@"SELECT teach_settings.* FROM groups,teach_settings 
																WHERE groups.Id_template=@a AND groups.Id_group=teach_settings.Id_group
																	AND teach_settings.Id_teacher=@b", new { a = Id_template,b= Id_teacher }).ToList();
			}
			return templateTypeList;
		}
		public void InsertRecord(List<TemplateSetting> records)
		{
			using (IDbConnection db = new MySqlConnection(connectionString))
			{
				foreach (TemplateSetting record in records)
					db.Query(@"INSERT INTO template_setting (Id_template, Id_teach_setting) 
								VALUES(@a, @b)", 
								new { a = record.Id_template, b = record.Id_setting });
			}
		}
	}
}
