using Generator.Model.Operations;
using MathNet.Numerics.Random;
using MatrixGenerator;
using System.Collections.Generic;
using System.Linq;
using TemplateHelper;

namespace Generator.Model
{
	public class MatrixCreator
	{
		public static MersenneTwister random;
		public List<Matrix> matrixList { get; }
		public Template template;
		public List<string> taskText;
		public List<string> solveText;
		public MatrixCreator(List<Matrix> matrixList, Template template)
		{
			taskText = new List<string>();
			solveText = new List<string>();
			random = new MersenneTwister();
			this.matrixList = matrixList;
			this.template = template;
		}
		public void Generate()
		{
            taskCount=0;
            if (template.byFormulaFind || template.matrixSizeType== MatrixSizeType.formula)
			{
				GenerateWithFormula();
			}
			else
			{
				if (template.matrixSizeType == MatrixSizeType.random)
				{
					GenerateRandom();
				}
				else
				{
					List<int[]> matrixSize = new List<int[]>();
					if(template.firstMatrix)
					{
						matrixSize.Add(new int[] {(int)template.firstMatrixM,(int)template.firstMatrixN});
						if (template.secondMatrix)
						{
							matrixSize.Add(new int[] { (int)template.secondMatrixM, (int)template.secondMatrixN });
							if (template.thirdMatrix)
							{
								matrixSize.Add(new int[] { (int)template.thirdMatrixM, (int)template.thirdMatrixN });
							}
						}
					}
					GenerateCustomMatrix(matrixSize);
				}
				SetMatrixName();//именуем матрицы
			}			
			AddMatrixTask();
		}
		public void GenerateWithFormula()
		{
			int mSize = (int)template.formulaOutM;
			int nSize = (int)template.formulaOutN;
			Tree tree;
			bool sucsess = false;
			do
			{
				GenericMatrix matrix = new GenericMatrix();
				matrix.mSize = mSize;
				matrix.nSize = nSize;
				tree = new Tree(matrix);
				tree.numberExist = (bool)template.numberExist;
				tree.vectorExist = template.vectorExist;
				tree.quadExist = template.quadExist;
				if (template.invertMatrixType == InvertType.zeroDetMatrix)
					tree.zeroDet = true;
				else
					tree.zeroDet = false;
				sucsess = tree.Build(template.matrixCount,template.elementType);
			} while (!sucsess);

			foreach (var node in tree.nodes)
			{
				matrixList.Add((node as BasicNode).matrix);
			}
            taskCount++;
			taskText.Add(string.Format(@"\item Вычислите матрицу по формуле: {0}.", tree.GetFormulaTeX()));
			solveText.Add(@"\item "+tree.GetOutNatrix());
			//matrixList.AddRange(tree.nodes)
			//заглушка
		}
		public void GenerateCustomMatrix(List<int[]> matrixSize)
		{
			int minValue = (int)MatrixElenemtRange.min;
			int maxValue = (int)MatrixElenemtRange.max;
            if (template.elementType == ElementType.any)
                minValue = -maxValue;
            foreach (int[] size in matrixSize)
			{
				Matrix matrix = new Matrix(size[0], size[1], minValue, maxValue, false, false, "");
				matrixList.Add(matrix);
			}
            if (template.invertMatrixType == InvertType.zeroDetMatrix)//создаём квадратную матрицу
            {
                matrixList[random.Next(0, matrixList.Count)].MakeZeroDeterminant();
            }
        }
		public void GenerateRandom()
		{
			int matrixCount = template.matrixCount;
			int minValue= (int)MatrixElenemtRange.min;
			int maxValue= (int)MatrixElenemtRange.max;
			if (template.elementType == ElementType.any)
				minValue = -maxValue;
			if (template.quadExist)//создаём квадратную матрицу
			{
				matrixCount--;
				int matrixSize = random.Next((int)MatrixRandomSize.min, (int)MatrixRandomSize.max);
				Matrix matrix = new Matrix(matrixSize, matrixSize, minValue, maxValue, false, false, "");
				if(template.invertMatrixType == InvertType.zeroDetMatrix)
				{
					matrix.MakeZeroDeterminant();
				}
				matrixList.Add(matrix);
			}
			if(template.vectorExist)//создаём вектор-строку и вектор-столбец
			{
				int matrixSize = random.Next((int)MatrixRandomSize.min, (int)MatrixRandomSize.max);
				matrixCount -= 2;
				Matrix matrixRow = new Matrix(1, matrixSize, minValue, maxValue, false, false, "");
				Matrix matrixColumn = new Matrix(matrixSize, 1, minValue, maxValue, false, false, "");
				matrixList.Add(matrixRow);
				matrixList.Add(matrixColumn);
			}
			for(; matrixCount>0; matrixCount--)//создаём всё остальное
			{
				int mSize = 0;
				int nSize = 0;
				do
				{
					mSize = random.Next((int)MatrixRandomSize.min, (int)MatrixRandomSize.max);
					nSize = random.Next((int)MatrixRandomSize.min, (int)MatrixRandomSize.max);
				} while (/*mSize == nSize ||*/ mSize == 1 || nSize == 1);//проверяем на "квадратность" и на "векторность"
				Matrix matrix = new Matrix(mSize, nSize, minValue, maxValue, false, false, "");
				matrixList.Add(matrix);
			}			
		}
		public void SetMatrixName()
		{
			List<char> labels = new List<char>();
			foreach (Matrix matrix in matrixList)
			{
				if (labels.Count == 0)
				{
					labels = "ABCDEFGHIGKLMNOPRSTUXYZW".ToList();
					labels.RemoveAll(T => labels.IndexOf(T) >= template.matrixCount);
				}
				int index = random.Next(0, labels.Count);
				matrix.Label=(labels[index].ToString());
				labels.RemoveAt(index);				
			}
			NameComparer matrixCompare = new NameComparer();
			matrixList.Sort(matrixCompare);
		}
        public int taskCount;
        public void AddMatrixTask()
		{
			Operation op = new Operation(this);
            if (template.transeMatrix)
            {
                Operation.TranseMatrix();
                taskCount++;
            }
            if (template.determMatrix)
            {
                Operation.DetermMatrix();
                taskCount++;
            }
            if (template.invertMatrix)
            {
                Operation.InvertMatrix();
                taskCount++;
            }
            if (template.countRow)
            {
                Operation.CountRow();
                taskCount++;
            }
            if (template.countColumn)
            {
                Operation.CountColumn();
                taskCount++;
            }
            if (template.outToScreen)
            {
                Operation.GetElement();
                taskCount++;
            }
            if (template.calcValue)
            {
                Operation.Calculate();
                taskCount++;
            }
            if (template.differSizeMult)
            {
                Operation.InvalidMult();
                taskCount++;
            }
            if (template.differSizeSum)
            {
                Operation.InvalidSum();
                taskCount++;
            }
        }		
		public static Matrix GetRandomMatix(List<Matrix> matrixList)
		{
			int index = random.Next(0, matrixList.Count);
			return matrixList[index];
		}
		public List<string> GetTask()
		{
			List<string> task = new List<string>();
            if (template.comment != null)
            {
                if (template.comment.Count() > 0)
                {
                    task.Add(@"\\Даны матрицы:");
                }
                else
                {
                    task.Add(@"Даны матрицы:");
                }
            }else
            {
                task.Add(@"Даны матрицы:");
            }

			foreach (Matrix matrix in matrixList)
			{
				task.Add(string.Format(@"\\${0}={1}$", matrix.Label, matrix.ToTEX()));
			}
            if (taskCount > 0)
            {
                task.Add(@"\begin{enumerate}");
                task.AddRange(taskText);
                task.Add(@"\end{enumerate}");
            }
			return task;
		}
		public List<string> GetSolve()
		{
			List<string> solve = new List<string>();
            if (taskCount > 0)
            {
                solve.Add(@"\begin{enumerate}");
                solve.AddRange(solveText);
                solve.Add(@"\end{enumerate}");
            }
			return solve;
		}
	}
}