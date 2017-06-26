using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TemplateHelper
{
	/// <summary>
	/// Настройки шаблона задания
	/// </summary>
	[Serializable]
    public class Template
    {
		public int Id_type;
		public string title;
		public string typeTitle;
		public int number;
        public string[] comment;
        //исходные данные
        public int matrixCount;
		public ElementType elementType;
		public MatrixSizeType matrixSizeType;
		public CountType countType;
		//размерности
		public bool firstMatrix;
		public int? firstMatrixM;
		public int? firstMatrixN;

		public bool secondMatrix;
		public int? secondMatrixM;
		public int? secondMatrixN;

		public bool thirdMatrix;
		public int? thirdMatrixM;
		public int? thirdMatrixN;

		public bool vectorExist;
		public bool quadExist;
		//матрица по формуле
		public bool byFormulaFind;
		public int? formulaOutM;
		public int? formulaOutN;
		public bool numberExist;
		//определитель
		public bool determMatrix;
		public DetermenantType determMatrixType;
		//обратная матрица
		public bool invertMatrix;
		public InvertType invertMatrixType;
		//транспонирование
		public bool transeMatrix;
		public TransposeType transeMatrixType;
		//определить количество строк
		public bool countRow;
		public CountType countRowType;
		//определить количество столбцов
		public bool countColumn;
		public CountType countColumnType;
		//вывод на экран
		public bool outToScreen;
		public bool outRowByNumber;
		public bool outColumnByNumber;
		public bool outSubMatrix;
		public SubMatrixType outSubMatrixType;
		//вычислить значение
		public bool calcValue;
		public CalculationType calcValueOperation;
		public ElementValueType calcValueType;
		public SubMatrixType calcValueArea;
		//задания повышенной сложности
		public bool differSizeSum;
		public bool differSizeMult;
		/// <summary>
		/// Сохранение задания в XML-файл
		/// </summary>
		/// <param name="XMLFilePath"></param>
		/// <returns></returns>
		public bool Serialize(string XMLFilePath)
		{
			TextWriter textWriter = new StreamWriter(XMLFilePath);
			XmlSerializer serializer = new XmlSerializer(GetType());
			serializer.Serialize(textWriter, this);
			textWriter.Close();
			textWriter.Dispose();
			return true;
		}
		/// <summary>
		/// Загрузка задания из XML-файла
		/// </summary>
		/// <param name="XMLFilePath"></param>
		/// <returns></returns>
		public static Template DeSerialize(string XMLFilePath)
		{
			FileStream fs = new FileStream(XMLFilePath, FileMode.Open, FileAccess.Read);
			XmlSerializer serializer = new XmlSerializer(typeof(Template));
			Template Task = serializer.Deserialize(fs) as Template;
			fs.Close();
			return Task;
		}
	}
}
