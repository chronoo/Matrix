using Parser.Model;
using Parser.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using TaskHelper.Model;

namespace TaskHelper
{
	public class StudTemplateTree
	{
		public StudTemplateTree(string id, string DBCOnnet, JSONConverter converter)
		{
			Nodes = new List<Node>();

			Student stud = converter.GetStudentFromID(id);
			TemplateRep repo = new TemplateRep(DBCOnnet);
			TempTypeRep rep = new TempTypeRep(DBCOnnet);

			List<TemplateType> types = rep.GetTypes();
			foreach (Student.Links.Groups group in stud._links.groups)
			{
				List<Template> templates = repo.GetGroupTemplates(group.id);
                templates.Sort(delegate (Template x, Template y)
                {
                    return x.number.CompareTo(y.number);
                });
                foreach (TemplateType type in types)
				{
					Node node = new Node();
					node.type = type;
					node.templates = templates.FindAll(x => x.Id_type == type.Id_type);
					if (node.templates.Count > 0)
						Nodes.Add(node);
				}
				templates.Sort(delegate (Template x, Template y)
				{
					return x.number.CompareTo(y.number);
				});
			}
		}
		public List<Node> Nodes;
		public class Node
		{
			public TemplateType type;
			public List<Template> templates;
			public Node() { }
		}
	}
}
