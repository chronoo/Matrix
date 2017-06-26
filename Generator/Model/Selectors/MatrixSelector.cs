using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Random;
using MatrixGenerator.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixGenerator.Operations
{
	public class MatrixCalculator: BasicOperation, IMatrixOperation
	{
		public IAreaSelector AreaSelector;
		public ISetSelector SetSelector;
		public ISelectOperation Operation;
		public MatrixCalculator() {
			MersenneTwister Random = new MersenneTwister();
			List<IAreaSelector> Areas = new List<IAreaSelector>();
			Areas.Add(new MainDiagonalSelector());
			Areas.Add(new SecondDiagonalSelector());
			Areas.Add(new SubMatrixSelector());
			Areas.Add(new FewRowSelector());
			Areas.Add(new SingleRowSelector());
			Areas.Add(new FewColumnSelector());
			Areas.Add(new SingleColumnSelector());
			Areas.Add(new AllMatrixSelector());

			AreaSelector = Areas[Random.Next(0, Areas.Count)];

			List<ISetSelector> Sets = new List<ISetSelector>();
			Sets.Add(new PositiveElements());
			Sets.Add(new NegativeElements());
			Sets.Add(new OddElements());
			Sets.Add(new EvenElements());
			Sets.Add(new AllElements());
			Sets.Add(new NumberEqualElements());

			SetSelector = Sets[Random.Next(0, Sets.Count)];

			List<ISelectOperation> Operations = new List<ISelectOperation>();
			Operations.Add(new SumOfElement());
			Operations.Add(new MinElement());
			Operations.Add(new MaxElement());
			Operations.Add(new AvgOfElement());
			Operations.Add(new CountOfElement());

			Operation = Operations[Random.Next(0, Operations.Count)];
		}
		public MatrixCalculator(Matrix Matrix): this()
		{
			this.Matrix = Matrix;
		}
		public MatrixCalculator(IAreaSelector AreaSelector, ISetSelector SetSelector, ISelectOperation Operation)
		{
			this.AreaSelector = AreaSelector;
			this.SetSelector = SetSelector;
			this.Operation = Operation;
		}
		public object Operate()
		{
			object Ret= Operation.Operate(SetSelector.GetSet(AreaSelector.GetArea(Matrix.Field)));
			return Ret;
		}
		public string ToText()
		{
			return string.Format(@"Для элементов{2} матрицы ${3}$ найдите {0} {1}", Operation.ToText(),SetSelector.ToText(),AreaSelector.ToText(),Matrix.Label).Replace("принадлежащие","принадлежащих");
		}
		public string GetSolveText()
		{
			return string.Format(@"Для элементов{2} матрицы ${3}$ {0} {1}", Operation.ToText(), SetSelector.ToText(), AreaSelector.ToText(), Matrix.Label).Replace("принадлежащие", "принадлежащих");
		}
	}
}