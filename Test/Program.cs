using ExceptionHelper;
using Generator;
using Generator.Model;
using JWT;
using JWT.Serializers;
using MatrixGenerator;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TaskHelper;
using TemplateHelper;

namespace Test
{
	class Program
	{
		static void Main(string[] args)
		{

			//TaskGenerator generator = new TaskGenerator("", "", "");
			//List<Matrix> matrixList = new List<Matrix>();
			Template template = new Template();
			template.elementType = ElementType.any;
			template.byFormulaFind = false;
			template.matrixSizeType = MatrixSizeType.custom;
            template.comment = null;
            //template.
            template.matrixCount = 3;
			template.formulaOutM = 4;
			template.formulaOutN = 2;
			template.number = 1;
			template.title = "Матричные операции";
			template.typeTitle = "Лабораторная работа";

			template.byFormulaFind = false;
			/*template.outToScreen = true;
			template.outRowByNumber = true;
			template.outColumnByNumber = true;
			template.outSubMatrix = true;
			template.outSubMatrixType = SubMatrixType.SubMatrix;
			template.calcValue = true;*/

			/*template.calcValueArea = SubMatrixType.SubMatrix;
			template.calcValueOperation = CalculationType.Count;
			template.calcValueType = ElementValueType.All;*/
            //template.matrixSizeType = MatrixSizeType.custom;

            template.firstMatrix = true;
			template.firstMatrixM = 3;
			template.firstMatrixN = 3;

			template.secondMatrix = true;
			template.secondMatrixM = 3;
			template.secondMatrixN = 3;

			template.thirdMatrix = true;
            template.thirdMatrixM = 3;
            template.thirdMatrixN = 3;


            template.differSizeMult = true;
            template.differSizeSum = true;

			/*template.numberExist = true;

			template.transeMatrix = true;
			template.transeMatrixType = TransposeType.allMatrix;
			template.determMatrix = true;
			template.determMatrixType= DetermenantType.allMatrix;
			template.invertMatrix = true;
			template.invertMatrixType= InvertType.allMatrix;
			template.countRow = true;
			template.countRowType = CountType.allMatrix;
			template.countColumn = true;
			template.countColumnType = CountType.allMatrix;*/

			/*TaskGenerator generator = new TaskGenerator(template, "1.ltx", "2.ltx");
			//generator.TaskGenerate();
			generator.SolveGenerate();*/

            TaskGenerator generator = new TaskGenerator(template, "1.ltx", "2.ltx",10);

            Process p1 = new Process();
			p1.StartInfo.FileName = @"pdflatex";
			p1.StartInfo.Arguments = "1.ltx";
			//p1.StartInfo.UseShellExecute = false;
			p1.Start();
			p1.WaitForExit();

			Process.Start("1.pdf");

			p1 = new Process();
			p1.StartInfo.FileName = @"pdflatex";
			p1.StartInfo.Arguments = "2.ltx";
			//p1.StartInfo.UseShellExecute = false;
			p1.Start();
			p1.WaitForExit();

			Process.Start("2.pdf");
			//generator.SolveGenerate();
			//Console.ReadKey();
		}
		//static void Main(string[] args)
		//{
		//	try
		//	{
		//		/*MySqlConnection db = new MySqlConnection("server=localhost;user=root;database=generator;password=;charset=utf8;");
		//		db.Open();

		//		MySqlCommand cmd = db.CreateCommand();				
		//		cmd.CommandText = "SELECT * from task";
		//		cmd.ExecuteNonQuery();*/
		//		string login = "ushakov-aleksandr-dmitrievich";
		//		string pass = "ushakov";
		//		var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://82.179.88.27:82801/authentication/authenticate");
		//		httpWebRequest.ContentType = "application/json";
		//		httpWebRequest.Method = "POST";
		//		using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
		//		{
		//			userDate user = new userDate(login, pass);
		//			IJsonSerializer serializer = new JsonNetSerializer();
		//			string json = serializer.Serialize(user);
		//			streamWriter.Write(json);
		//			streamWriter.Flush();
		//			streamWriter.Close();
		//		}
		//		var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

		//	}
		//	catch(Exception ex)
		//	{
		//		Console.WriteLine(ExcepthionHelper.GetMessage(ex));
		//	}
		//	Console.WriteLine("....жми что-нибудь....");
		//	Console.ReadKey();
		//}
		public class userDate
		{
			public string user;
			public string password;
			public userDate(string user, string password)
			{
				this.user = user;
				this.password = password;
			}
		}
	}
}
