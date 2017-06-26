using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Random;

namespace MatrixGenerator.Selectors
{
	public class MainDiagonalSelector : IAreaSelector
	{
		public Matrix<double> GetArea(Matrix<double> Matrix)
		{
			int MinSize = new int[] { Matrix.ColumnCount, Matrix.RowCount }.Min();
			List<double> Area = new List<double>();
			for (int i = 0; i < MinSize; i++)
				Area.Add(Matrix[i, i]);
			return Matrix<double>.Build.DenseOfRowMajor(MinSize, 1, Area);
		}

		public string ToText()
		{
			return (" на главной диагонали");
		}
	}
	public class SecondDiagonalSelector : IAreaSelector
	{
		public Matrix<double> GetArea(Matrix<double> Matrix)
		{
			int MinSize = new int[] { Matrix.ColumnCount, Matrix.RowCount }.Min();
			List<double> Area = new List<double>();
			for (int i = 0; i < MinSize; i++)
                Area.Add(Matrix[i,MinSize - 1 - i]);//Area.Add(Matrix[MinSize-1-i, i]);
            return Matrix<double>.Build.DenseOfRowMajor(MinSize, 1, Area);
		}
		public string ToText()
		{
			return (" на побочной диагонали");
		}
	}
	public class SubMatrixSelector : IAreaSelector
	{
		public bool IsCreated;
		public int StartRow;
		public int StartColumn;
		public int RowCount;
		public int ColumnCount;
		public bool AllColumn;
		public bool AllRow;
		public SubMatrixSelector(int StartRow, int StartColumn, int RowCount, int ColumnCount,bool AllRow, bool AllColumn)
		{
			IsCreated = true;
			this.StartRow = StartRow;
			this.StartColumn = StartColumn;
			this.RowCount = RowCount;
			this.ColumnCount = ColumnCount;
			this.AllColumn = AllColumn;
			this.AllRow = AllRow;
		}
		public SubMatrixSelector()
		{
			IsCreated = false;
			AllRow = false;
			AllColumn = false;
		}
		public Matrix<double> GetArea(Matrix<double> Matrix)
		{
			if(!IsCreated){
				MersenneTwister Random = new MersenneTwister();
				int X1, X2, Y1, Y2;
				X1 = Random.Next(0, Matrix.ColumnCount);
				Y1 = Random.Next(0, Matrix.RowCount);
				X2 = Random.Next(0, Matrix.ColumnCount);
				Y2 = Random.Next(0, Matrix.RowCount);
				StartRow = Math.Min(Y1, Y2);
				StartColumn = Math.Min(X1, X2);
				RowCount = Math.Abs(Y1 - Y2) + 1;
				ColumnCount = Math.Abs(X1 - X2) + 1;
				if (RowCount == Matrix.RowCount) AllRow = true;
				if (ColumnCount == Matrix.ColumnCount) AllColumn = true;
			}
			List<double> Area = new List<double>();
			for (int i = StartRow; i < StartRow + RowCount; i++)
				for (int j = StartColumn; j < StartColumn+ ColumnCount; j++)	
					Area.Add(Matrix[i, j]);
			return Matrix<double>.Build.DenseOfRowMajor(RowCount, ColumnCount, Area);
		}
		public string ToText()
		{
			string OutText = "";
			if (AllColumn && AllRow) return " во всей матрице"; ;
			if (!AllColumn && !AllRow)
			{
				OutText += " на пересечении ";
				if (ColumnCount > 1)
					OutText += StartColumn + 1 + "-" + (StartColumn + ColumnCount) + " столбцов";
				else
					OutText += StartColumn + 1 + "-го"  + " столбца";
				OutText += " и ";
				if (RowCount > 1)
					OutText += StartRow + 1 + "-" + (StartRow + RowCount) + " строк";
				else
					OutText += StartRow + 1 + "-й" + " строки";
			}
			else
			{
				OutText += ", принадлежащие ";
				if (AllRow)
				{
					if (ColumnCount > 1)
						OutText += StartColumn + 1 + "-" + (StartColumn + ColumnCount) + " столбцам";
					else
						OutText += StartColumn + 1 + "-му столбцу";
					if (!AllRow) OutText += " и ";
				}
				if (AllColumn)
				{
					if (RowCount > 1)
						OutText += StartRow + 1 + "-" + (StartRow + RowCount) + " строкам";
					else
						OutText += StartRow + 1 + "-й" + " строке";
				}
				/*if (!AllColumn)
				{
					if (ColumnCount > 1)
						OutText += StartColumn + 1 + "-" + (StartColumn + ColumnCount) + " столбцам";
					else
						OutText += StartColumn + 1 + "-му столбцу";
					if(!AllRow) OutText += " и ";
				}
				if (!AllRow)
				{
					if (RowCount > 1)
						OutText += StartRow + 1 + "-" + (StartRow + RowCount) + " строкам";
					else
						OutText += StartRow + 1 + "-й" + " строке";
				}*/
			}
			return OutText;
		}
	}
	public class FewRowSelector : IAreaSelector
	{
		public bool IsCreated;
		public bool AllRow;
		public int StartRow;
		public int RowCount;
		public IAreaSelector SubMatrix;
		public FewRowSelector(int StartRow, int RowCount,bool AllRow)
		{
			IsCreated = true;
			this.AllRow = AllRow;
			this.StartRow = StartRow;
			this.RowCount = RowCount;
		}
		public FewRowSelector()
		{
			IsCreated = false;
			AllRow = false;
		}
		public Matrix<double> GetArea(Matrix<double> Matrix)
		{		
			if (!IsCreated)
			{
				MersenneTwister Random = new MersenneTwister();
				int Y1, Y2;
				Y1 = Random.Next(0, Matrix.RowCount);
				Y2 = Random.Next(0, Matrix.RowCount);
				StartRow = Math.Min(Y1, Y2);
				RowCount = Math.Abs(Y1 - Y2) + 1;
				if (RowCount == Matrix.RowCount) AllRow = true;
			}
			SubMatrix = new SubMatrixSelector(StartRow, 0, RowCount, Matrix.ColumnCount,AllRow, true);	
			return SubMatrix.GetArea(Matrix);
		}

		public string ToText()
		{
			return SubMatrix.ToText();
		}
	}
	public class SingleRowSelector : IAreaSelector
	{
		public bool IsCreated;
		public int Row;
		public bool AllRow;
		public IAreaSelector SubMatrix;
		public SingleRowSelector(int Row,bool AllRow)
		{
			IsCreated = true;
			this.Row = Row;
			this.AllRow = AllRow;
		}
		public SingleRowSelector()
		{
			IsCreated = false;
			AllRow = false;
		}
		public Matrix<double> GetArea(Matrix<double> Matrix)
		{
			if (!IsCreated)
			{
				MersenneTwister Random = new MersenneTwister();
				Row = Random.Next(0, Matrix.RowCount);
				if (Matrix.RowCount==1) AllRow = true;
			}
			SubMatrix = new FewRowSelector(Row,1, AllRow);
			return SubMatrix.GetArea(Matrix);
		}

		public string ToText()
		{
			return SubMatrix.ToText();
		}
	}
	public class FewColumnSelector : IAreaSelector
	{
		public bool IsCreated;
		public int StartColumn;
		public int ColumnCount;
		public bool AllColumn;
		public IAreaSelector SubMatrix;
		public FewColumnSelector(int StartColumn, int ColumnCount, bool AllColumn)
		{
			IsCreated = true;
			this.StartColumn = StartColumn;
			this.ColumnCount = ColumnCount;
			this.AllColumn = AllColumn;
		}
		public FewColumnSelector()
		{
			IsCreated = false;
		}
		public Matrix<double> GetArea(Matrix<double> Matrix)
		{
			if (!IsCreated)
			{
				MersenneTwister Random = new MersenneTwister();
				int X1, X2;
				X1 = Random.Next(0, Matrix.ColumnCount);
				X2 = Random.Next(0, Matrix.ColumnCount);
				StartColumn = Math.Min(X1, X2);
				ColumnCount = Math.Abs(X1 - X2) + 1;
				if (ColumnCount == Matrix.ColumnCount) AllColumn = true;
			}
			SubMatrix = new SubMatrixSelector(0, StartColumn, Matrix.RowCount, ColumnCount,true, AllColumn);
			return SubMatrix.GetArea(Matrix);
		}

		public string ToText()
		{
			return SubMatrix.ToText();
		}
	}
	public class SingleColumnSelector : IAreaSelector
	{
		public bool IsCreated;
		public int Column;
		public IAreaSelector FewColumn;
		public bool AllColumn;
		public SingleColumnSelector(int Column,bool AllColumn)
		{
			IsCreated = true;
			this.Column = Column;
			this.AllColumn = AllColumn;
		}
		public SingleColumnSelector()
		{
			IsCreated = false;
			AllColumn = false;
		}
		public Matrix<double> GetArea(Matrix<double> Matrix)
		{
			if (!IsCreated)
			{
				MersenneTwister Random = new MersenneTwister();
				Column = Random.Next(0, Matrix.ColumnCount);
				if (Matrix.ColumnCount==1) AllColumn = true;
			}
			FewColumn = new FewColumnSelector(Column, 1, AllColumn);
			return FewColumn.GetArea(Matrix);
		}

		public string ToText()
		{
			return FewColumn.ToText();
		}
	}
	public class AllMatrixSelector : IAreaSelector
	{
		public Matrix<double> GetArea(Matrix<double> Matrix)
		{
			List<double> Area = new List<double>();
			for (int i = 0; i < Matrix.ColumnCount; i++)
				for (int j = 0; j < Matrix.RowCount; j++)
					Area.Add(Matrix[i, j]);
			return Matrix<double>.Build.DenseOfRowMajor(Matrix.RowCount, Matrix.ColumnCount, Area);
		}

		public string ToText()
		{
			return "во всей матрице";
		}
	}
}