using Dapper;
using MySql.Data.MySqlClient;
using Parser.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaskHelper.Model;

namespace TaskHelper.View
{
	public class TaskSender
	{
		public TaskSender(string connectionString, JSONConverter converter)
		{
			_connectionString = connectionString;
			_converter = converter;
			//using (IDbConnection db = new MySqlConnection(_connectionString))
			//{
			//	if(db.State== ConnectionState.Closed)
			//	{
					
			//	}
			//}
		}
		private string _connectionString { get; set; }
		public StudentTask task { get; set; }
		private JSONConverter _converter { get; set; }
		public bool? TaskSend(string Id_student, int Id_template)
		{
			//JSONConverter 
			task = new StudentTask();//template.template = Path.GetRandomFileName();
			task.Id_student = Id_student;
			task.Id_template = Id_template;
			task.Id_group = _converter.GetStudentFromID(Id_student)._links.groups[0].id;

			using (IDbConnection db = new MySqlConnection(_connectionString))
			{
				if (db.Query<int>(@"SELECT EXISTS(SELECT 1 FROM task WHERE Id_student=@a AND Id_template=@b)",
								new	{a = task.Id_student,b = task.Id_template}).ToList()[0] == 0)
				{
					task.maket = Regex.Replace(Path.GetRandomFileName(), @"[^a-zA-Z0-9\-]", "") + ".ltx";
					task.solve = Regex.Replace(Path.GetRandomFileName(), @"[^a-zA-Z0-9\-]", "") + ".ltx";
					db.Query(@"INSERT INTO task (Id_student, Id_template,solve,maket,issue_date,Id_group) 
								VALUES(@a,@b,@c,@d,NOW(),@e)",
									new{a = task.Id_student,b = task.Id_template,c = task.solve,d = task.maket,e=task.Id_group});
					return true;
				}else
				{
					db.Query<StudentTask>(@"UPDATE task SET issue_date=NOW() WHERE Id_student=@a AND Id_template=@b", new { a = task.Id_student, b = task.Id_template });
					StudentTask temp = db.Query<StudentTask>(@"SELECT * FROM task WHERE Id_student=@a AND Id_template=@b",
								new { a = task.Id_student, b = task.Id_template }).ToList()[0];
					task.maket = temp.maket;
					task.solve = temp.solve;
					return false;
				}
			}
		}
	}
}
