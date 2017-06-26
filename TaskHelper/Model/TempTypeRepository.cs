using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHelper
{
	public class TemplateType
	{
		public int Id_type;
		public string title;
	}

	public class TempTypeRep
	{
		public TempTypeRep(string connectionString)
		{
			this.connectionString = connectionString;
		}
		public string connectionString;
		public List<TemplateType> GetTypes()
		{
			List<TemplateType> templateTypeList = new List<TemplateType>();
			using (IDbConnection db = new MySqlConnection(connectionString))
			{
				templateTypeList = db.Query<TemplateType>("SELECT * FROM type").ToList();
			}
			return templateTypeList;
		}
		public string GetFromID(int id)
		{
			List<TemplateType> types = GetTypes();
			return types.Find(x => x.Id_type == id).title;
		}
	}
}
