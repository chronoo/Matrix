using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskHelper.View
{
	public class PathGenerator
	{
		public string tempPath;
		public string templatePath;
		public string pdfPath;
		public string maketPath;
		public string solvePath;
		public string pdfFile;
		public string virtualTaskFile;
		public string virtualSolveFile;
		public PathGenerator(string template, string maket, string solve, string templateFolder, string maketFolder, string solveFolder, string pdFolder, string tempFolder, string rootPath)
		{
            if(rootPath.Last()=='\\')
            {
                rootPath = rootPath.Substring(0, rootPath.Length - 1);
            }
			templatePath = string.Format(@"{0}\{1}\{2}", rootPath, templateFolder, template);
			maketPath = string.Format(@"{0}\{1}\{2}", rootPath, maketFolder, maket);
			solvePath = string.Format(@"{0}\{1}\{2}", rootPath, solveFolder, solve);

			tempPath = string.Format(@"{0}\{1}", rootPath, tempFolder);
			pdfPath = string.Format(@"{0}\{1}", rootPath, pdFolder);
			pdfFile = string.Format(@"{0}\{1}.pdf", pdfPath, maket.Split('.')[0]);
			virtualTaskFile = string.Format(@"~/{0}/{1}.pdf", pdFolder, maket.Split('.')[0]);
			virtualSolveFile = string.Format(@"~/{0}/{1}.pdf", pdFolder, solve.Split('.')[0]);
		}
	}
}
