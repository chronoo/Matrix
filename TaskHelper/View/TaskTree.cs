using Dapper;
using MySql.Data.MySqlClient;
using Parser.Model;
using Parser.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace TaskHelper.View
{
	public class TaskTree
	{
		public List<TemplateType> types;
		public class TemplateType
		{
			public int Id_type;
			public string type;
			public List<Template> templates;
			public class Template
			{
				public int Id_template;
				public string title;
				public int number;
				public List<Group> groups;
				public class Group
				{
					public int Id_group;
					public int studCount;
					public string title;
					public List<Student> students;
					public class Student
					{
						public string Id_student;
						public string fio;
						public StudTask task;
						public class StudTask
						{
							public int Id_task;
							public string solve;
							public string maket;
						}

					}
				}
			}
		}
		public List<Record> records;
		public class Record
		{
			public int Id_task;
			public string Id_student;
			public int Id_group;
			public int Id_template;
			public int Id_type;
			public string typeTitle;
			public string templateTitle;
			public int number;
			public string solve;
			public string maket;
			public DateTime issue_date;
		}
		public string connectionString;
		public TaskTree(string id, string connectionString)
		{
			types = new List<TemplateType>();
			using (IDbConnection db = new MySqlConnection(connectionString))
			{
				records = db.Query<Record>(@"SELECT task.*, template.Id_type, template.title as templateTitle,template.number,type.title as typeTitle
								FROM template, task, type
								WHERE template.Id_teacher=@a AND
								template.Id_template = task.Id_template AND
								type.Id_type = template.Id_type",
							new { a = id }).ToList();
				if (records.Count > 0)
				{
					foreach (Record record in records)
					{
						TemplateType type = new TemplateType();
						type.type = record.typeTitle;
						type.Id_type = record.Id_type;
						type.templates = new List<TemplateType.Template>();
						if (types.FindAll(T=> T.Id_type==type.Id_type).Count == 0)
							types.Add(type);
					}
					//if(types.Count>0)
					foreach (Record record in records)
					{
						TemplateType.Template template = new TemplateType.Template();
						template.Id_template = record.Id_template;
						template.number = record.number;
						template.title = record.templateTitle;
						template.groups = new List<TemplateType.Template.Group>();
						if (types.Find(x => x.Id_type == record.Id_type).templates.FindAll(T => T.Id_template == template.Id_template).Count == 0)
							types.Find(x => x.Id_type == record.Id_type).templates.Add(template);
					}
					foreach (Record record in records)
					{
						JSONConverter converter = new JSONConverter(WebConfigurationManager.AppSettings["PeopleURL"], WebConfigurationManager.AppSettings["GroupURL"]);
						Group groupInfo = converter.GetGroupFromID(record.Id_group);
						TemplateType.Template.Group group = new TemplateType.Template.Group();
						group.Id_group = record.Id_group;
						group.title = groupInfo.name;
						group.studCount = groupInfo._embedded.students.Count();
						group.students = new List<TemplateType.Template.Group.Student>();
						foreach(TemplateType type in types)
						{
							if(type.Id_type==record.Id_type)
							foreach (TemplateType.Template template in type.templates)
							{
									if(template.groups.FindAll(T=>T.Id_group== group.Id_group).Count==0)
								if (template.Id_template == record.Id_template)
									template.groups.Add(group);
							}
						}						
					}
					foreach (Record record in records)
					{
						TemplateType.Template.Group.Student student = new TemplateType.Template.Group.Student();
						student.Id_student = record.Id_student;
						JSONConverter converter = new JSONConverter(WebConfigurationManager.AppSettings["PeopleURL"], WebConfigurationManager.AppSettings["GroupURL"]);
						student.fio = converter.GetStudentFromID(record.Id_student).displayName;
						student.task = new TemplateType.Template.Group.Student.StudTask();
						student.task.Id_task = record.Id_task;
						student.task.maket = record.maket;
						student.task.solve = record.solve;
						foreach (TemplateType type in types)
						{
							if (type.Id_type == record.Id_type)
								foreach (TemplateType.Template template in type.templates)
							{
								if (template.Id_template == record.Id_template)
									foreach (TemplateType.Template.Group group in template.groups)
								{
											if(group.students.FindAll(T=>T.Id_student==student.Id_student).Count==0)
									if (group.Id_group == record.Id_group)
										group.students.Add(student);
								}
							}
						}
					}
				}
			}
		}
	}
}
