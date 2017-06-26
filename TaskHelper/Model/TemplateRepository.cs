using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskHelper.Model;

namespace TaskHelper
{
	public class TemplateRep
	{
		public TemplateRep(string connectionString)
		{
			this.connectionString = connectionString;
		}
		public string connectionString;
		public List<Template> GetTemplates()
		{
			List<Template> templateList = new List<Template>();
			using (IDbConnection db = new MySqlConnection(connectionString))
			{
				templateList = db.Query<Template>("SELECT * FROM template").ToList();
			}
			return templateList;
		}

		public int CreateTemplate(Template template)
		{
            List<Template> templateList = new List<Template>();
            //if (template.Id_teacher == null) return null;
            using (IDbConnection db = new MySqlConnection(connectionString))
			{
                templateList.AddRange(db.Query<Template>("SELECT * FROM template WHERE number=@c AND Id_type=@d AND Id_teacher=@e AND Id_template<>@f", new { c = template.number, d = template.Id_type, e = template.Id_teacher, f = template.Id_template }).ToList());
                if (templateList.Count > 0)
                {
                    throw new Exception("TemplateByNumberExist");
                }
                db.Query<Template>(@"INSERT INTO template 
                                        VALUES(NULL,@b,@c,@d,@e,@f)",
									   new
									   {
										   b = template.Id_teacher,
										   c = template.template,
										   d = template.title,
										   e = template.Id_type,
										   f = template.number
									   });
				return db.Query<int>("SELECT LAST_INSERT_ID()").ToList()[0];
			}
		}
		public List<Template> GetGroupTemplates(int id)
		{
			List<Template> templateList = new List<Template>();
			using (IDbConnection db = new MySqlConnection(connectionString))
			{
				templateList = db.Query<Template>(@"SELECT template.* FROM template_setting,teach_setting,template 
													WHERE template_setting.Id_template=template.Id_template and 
													template_setting.`Id_teach_setting`=teach_setting.`Id_teach_setting` and
													teach_setting.Id_group=@a", 
													new { a = id }).ToList();
			}
			return templateList;
		}
		public List<Template> GetTemplates(string id)
		{
			List<Template> templateList = new List<Template>();
			using (IDbConnection db = new MySqlConnection(connectionString))
			{
				templateList = db.Query<Template>("SELECT * FROM template WHERE Id_teacher= @id", new { id }).ToList();
			}
			return templateList;
		}
        public void UpdateTemplate(Template template)
        {
            List<Template> templateList = new List<Template>();
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                templateList.AddRange(db.Query<Template>("SELECT * FROM template WHERE number=@c AND Id_type=@d AND Id_teacher=@e AND Id_template<>@f", new { c = template.number, d=template.Id_type,e=template.Id_teacher, f = template.Id_template }).ToList());
                if(templateList.Count>0)
                {
                    throw new Exception("TemplateExist");
                }
                db.Query<Template>("UPDATE template SET Id_type=@a, title=@b, number=@c WHERE Id_template= @id", new { id=template.Id_template, a=template.Id_type,b=template.title,c=template.number });
            }
        }
        public Template GetTemplateFromID(int id)
		{
			using (IDbConnection db = new MySqlConnection(connectionString))
			{
                List<Template> templates = db.Query<Template>("SELECT * FROM template WHERE Id_template= @id", new { id }).ToList();
                if(templates.Count==0)
                {
                    return null;
                }else
                {
                    return templates[0];
                }
			}
		}
		public void Delete(int Id_template)
		{
			using (IDbConnection db = new MySqlConnection(connectionString))
			{
				db.Query<Template>("DELETE FROM template WHERE Id_template= @a", new { a = Id_template });
			}
		}
        public List<string> GetTaskFromTemplate(int Id_template)
        {
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                return db.Query<string>("SELECT maket FROM task WHERE Id_template= @a", new { a = Id_template }).ToList();
            }
        }
        public List<string> GetSolveFromTemplate(int Id_template)
        {
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                return db.Query<string>("SELECT solve FROM task WHERE Id_template= @a", new { a = Id_template }).ToList();
            }
        }
        public List<StudentTask> GetOldTask()
        {
            DateTime date = GetYearStart();
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                return db.Query<StudentTask>("SELECT * FROM task WHERE issue_date<@a", new { a = date }).ToList();
            }            
        }
        public void DeleteOldTask()
        {
            DateTime date = GetYearStart();
            using (IDbConnection db = new MySqlConnection(connectionString))
            {
                db.Query("DELETE FROM task WHERE issue_date<@a", new { a = date });
            }
        }
        public DateTime GetYearStart()
        {
            int currentYear = DateTime.Now.Year;
            if (DateTime.Now.Month < 9)
            {
                currentYear--;
            }
            return new DateTime(currentYear, 9, 1);
        }
        /*public class TaskInfoList
        {
            public List<string> solve;
            public List<string> task;
            public List<int> id;
            public TaskInfoList()
            {
                solve = new List<string>();
                task = new List<string>();
                id = new List<int>();
            }
        }*/
    }
}
