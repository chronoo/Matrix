using Generator.Model;
using MatrixGenerator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateHelper;

namespace Generator
{
    public class TaskGenerator
    {
		public Template template;
		public string templatePath;
		public string maketPath;
		public string solvePath;
		public MatrixCreator creator;

		public List<Matrix> matrixList;

		public TaskGenerator(string templatePath, string maketPath,string solvePath)
		{
			this.templatePath = templatePath;
			this.maketPath = maketPath;
			this.solvePath = solvePath;
			template = Template.DeSerialize(templatePath);
			TaskGenerate();
		}
        public TaskGenerator(Template template, string maketPath, string solvePath, int count)
        {
            this.maketPath = maketPath;
            this.solvePath = solvePath;
            this.template = template;
            TaskGenerate(count);
        }
        public TaskGenerator(string templatePath, string maketPath, string solvePath, int count)
        {
            this.templatePath = templatePath;
            this.maketPath = maketPath;
            this.solvePath = solvePath;
            template = Template.DeSerialize(templatePath);
            TaskGenerate(count);
        }
        public TaskGenerator(Template template, string maketPath, string solvePath)
		{
			//this.templatePath = templatePath;
			this.maketPath = maketPath;
			this.solvePath = solvePath;
			this.template = template;
			TaskGenerate();
		}
		public void TaskGenerate()
		{
			List<string> LaTEX = new List<string>();
			LaTEX.Add(@"\documentclass[12pt]{article}");
			LaTEX.Add(@"\usepackage[utf8x]{inputenc}");
			LaTEX.Add(@"\usepackage{amsmath}");
			LaTEX.Add(@"\usepackage[russian]{babel}");
			LaTEX.Add(@"\usepackage[left=2cm,right=2cm,
    top=2cm,bottom=2cm,bindingoffset=0cm]{geometry}");
			LaTEX.Add(@"\begin{document}");
			LaTEX.Add(@"\begin{center}");
			LaTEX.Add(string.Format(@"{0} №{1}",template.typeTitle,template.number));
			LaTEX.Add(string.Format(@"\\{0}", template.title));
			LaTEX.Add(@"\end{center}");
			matrixList = new List<Matrix>();
			creator = new MatrixCreator(matrixList, template);
            if(template.comment!=null)
                if(template.comment.Count()>0)
                {
                    foreach (var item in template.comment)
                    {
                        string text = "";
                        if(item!=template.comment.First())
                        {
                            text += @"\\";
                        }
                        text += item;
                        LaTEX.Add(text);
                    }
                }
			creator.Generate();			
			LaTEX.AddRange(creator.GetTask());
			/*LaTEX.Add(task.taskDescr.text[0]);
			LaTEX.AddRange(inp);*/
			LaTEX.Add(@"\end{document}");
			StreamWriter sw = new StreamWriter(maketPath, false, new System.Text.UTF8Encoding(false));
			sw.Write(string.Join("\r\n", LaTEX));
			sw.Close();
		}
        public void TaskGenerate(int count)
        {
            try
            {
                List<string> LaTEXSolve = new List<string>();
                LaTEXSolve.Add(@"\documentclass[12pt]{article}");
                LaTEXSolve.Add(@"\usepackage[utf8x]{inputenc}");
                LaTEXSolve.Add(@"\usepackage{amsmath}");
                LaTEXSolve.Add(@"\usepackage[russian]{babel}");
                LaTEXSolve.Add(@"\usepackage[left=2cm,right=2cm,
    top=2cm,bottom=2cm,bindingoffset=0cm]{geometry}");
                LaTEXSolve.Add(@"\begin{document}");

                /*LaTEX.Add(task.taskDescr.text[0]);
                LaTEX.AddRange(inp);*/


                List<string> LaTEXTask = new List<string>();
                LaTEXTask.Add(@"\documentclass[12pt]{article}");
                LaTEXTask.Add(@"\usepackage[utf8x]{inputenc}");
                LaTEXTask.Add(@"\usepackage{amsmath}");
                LaTEXTask.Add(@"\usepackage[russian]{babel}");
                LaTEXTask.Add(@"\usepackage[left=2cm,right=2cm,
    top=2cm,bottom=2cm,bindingoffset=0cm]{geometry}");
                LaTEXTask.Add(@"\begin{document}");
                for (int i = 0; i < count; i++)
                {
                    LaTEXTask.Add(@"\begin{center}");
                    LaTEXTask.Add(string.Format(@"{0} №{1}", template.typeTitle, template.number));
                    LaTEXTask.Add(string.Format(@"\\{0}", template.title));
                    LaTEXTask.Add(string.Format(@"\\Вариант №{0}", i + 1));
                    LaTEXTask.Add(@"\end{center}");
                    matrixList = new List<Matrix>();
                    creator = new MatrixCreator(matrixList, template);
                    if (template.comment != null)
                        if (template.comment.Count() > 0)
                        {
                            foreach (var item in template.comment)
                            {
                                string text = "";
                                if (item != template.comment.First())
                                {
                                    text += @"\\";
                                }
                                text += item;
                                LaTEXTask.Add(text);
                            }
                        }
                    creator.Generate();
                    LaTEXTask.AddRange(creator.GetTask());

                    LaTEXSolve.Add(@"\begin{center}");
                    LaTEXSolve.Add(string.Format(@"{0} №{1}", template.typeTitle, template.number));
                    LaTEXSolve.Add(string.Format(@"\\{0} ", template.title));
                    LaTEXSolve.Add(string.Format(@"\\Вариант №{0}", i + 1));
                    LaTEXSolve.Add("(решение)");
                    //LaTEX.Add(string.Format(@"\\{0}", template.title));
                    LaTEXSolve.Add(@"\end{center}");
                    LaTEXSolve.AddRange(creator.GetSolve());
                    if (i != count - 1)
                    {
                        LaTEXTask.Add(@"\newpage");
                        LaTEXSolve.Add(@"\newpage");
                    }
                }
                /*LaTEX.Add(task.taskDescr.text[0]);
                LaTEX.AddRange(inp);*/
                LaTEXTask.Add(@"\end{document}");
                StreamWriter swTask = new StreamWriter(maketPath, false, new System.Text.UTF8Encoding(false));
                swTask.Write(string.Join("\r\n", LaTEXTask));
                swTask.Close();

                LaTEXSolve.Add(@"\end{document}");
                StreamWriter swSolve = new StreamWriter(solvePath, false, new System.Text.UTF8Encoding(false));
                swSolve.Write(string.Join("\r\n", LaTEXSolve));
                swSolve.Close();
            }catch(Exception ex)
            {
                string message = ex.Message;
            }
        }
        public void SolveGenerate()
		{
			List<string> LaTEX = new List<string>();
			LaTEX.Add(@"\documentclass[12pt]{article}");
			LaTEX.Add(@"\usepackage[utf8x]{inputenc}");
			LaTEX.Add(@"\usepackage{amsmath}");
			LaTEX.Add(@"\usepackage[russian]{babel}");
			LaTEX.Add(@"\usepackage[left=2cm,right=2cm,
    top=2cm,bottom=2cm,bindingoffset=0cm]{geometry}");
			LaTEX.Add(@"\begin{document}");
			LaTEX.Add(@"\begin{center}");
			LaTEX.Add(string.Format(@"{0} №{1}", template.typeTitle, template.number));
			LaTEX.Add(string.Format(@"\\{0} ", template.title));
			LaTEX.Add("(решение)");			
			//LaTEX.Add(string.Format(@"\\{0}", template.title));
			LaTEX.Add(@"\end{center}");
			LaTEX.AddRange(creator.GetSolve());
			/*LaTEX.Add(task.taskDescr.text[0]);
			LaTEX.AddRange(inp);*/
			LaTEX.Add(@"\end{document}");
			StreamWriter sw = new StreamWriter(solvePath, false, new System.Text.UTF8Encoding(false));
			sw.Write(string.Join("\r\n", LaTEX));
			sw.Close();
		}
	}
}