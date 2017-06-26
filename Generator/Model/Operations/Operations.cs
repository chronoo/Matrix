using MatrixGenerator.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Random;

namespace MatrixGenerator.Operations
{
	public class BasicOperation
	{
		public Matrix Matrix;
		public void SetMatrix(Matrix Matrix)
		{
			this.Matrix = Matrix;
		}
		public BasicOperation() { }
		public BasicOperation(Matrix Matrix)
		{
			this.Matrix = Matrix;
		}
	}
	public class FindDeter : BasicOperation, IMatrixOperation
	{
		public FindDeter(Matrix Matrix) : base(Matrix) { }
		public FindDeter() { }
		public object Operate()
		{
			return Matrix.DeterminantFind();
		}
		public string ToText()
		{
			return string.Format(@"Найдите определитель матрицы ${0}$.",Matrix.Label);
		}
	}
	public class Inverse : BasicOperation, IMatrixOperation
	{
		public Inverse(Matrix Matrix) : base(Matrix) { }
		public Inverse() { }
		public object Operate()
		{
			return Matrix.InverseMatrix();
		}
		public string ToText()
		{
			return string.Format(@"Для матрицы ${0}$ найдите обратную матрицу.", Matrix.Label);
		}
	}
	public class Transposition : BasicOperation, IMatrixOperation
	{
		public Transposition(Matrix Matrix) : base(Matrix){}
		public Transposition() { }

		public object Operate()
		{
			return Matrix.TranspositionMatrix();
		}

		public string ToText()
		{
			return string.Format(@"Транспонируйте матрицу ${0}$.", Matrix.Label);
		}
	}
	public class GetRowCount : BasicOperation, IMatrixOperation
	{
		public GetRowCount(Matrix Matrix) : base(Matrix){}
		public GetRowCount() { }

		public object Operate()
		{
			return Matrix.GetRowCount();
		}

		public string ToText()
		{
			return string.Format(@"Определите количество строк в матрице ${0}$.", Matrix.Label);
		}
	}
	public class GetColumnCout : BasicOperation, IMatrixOperation
	{
		public GetColumnCout(Matrix Matrix) : base(Matrix){	}
		public GetColumnCout() { }

		public object Operate()
		{
			return Matrix.GetColumnCout();
		}

		public string ToText()
		{
			return string.Format(@"Определите количество столбцов в матрице ${0}$.", Matrix.Label);
		}
	}
	public class GetElement : BasicOperation, IMatrixOperation
	{
		public IAreaSelector Selector;
		public string OutText;
		public GetElement() {
			OutText = "";
				}
		//public GetElement(Matrix Matrix) : base(Matrix){ }
		public GetElement(IAreaSelector Selector, Matrix Matrix) : base(Matrix)
		{
			this.Selector = Selector;
		}
		public GetElement(Matrix Matrix) : base(Matrix)
		{
			List<IAreaSelector> Selectors = new List<IAreaSelector>();
			Selectors.Add(new MainDiagonalSelector());
			Selectors.Add(new SecondDiagonalSelector());
			Selectors.Add(new SubMatrixSelector());
			Selectors.Add(new AllMatrixSelector());
			Selectors.Add(new SingleRowSelector());
			Selectors.Add(new FewColumnSelector());
			MersenneTwister Random = new MersenneTwister();
			this.Selector = Selectors[Random.Next(0, Selectors.Count)];
		}
		public object Operate()
		{
			return Selector.GetArea(Matrix.Field);
		}

		public string ToText()
		{
			return OutText;
		}
	}
}