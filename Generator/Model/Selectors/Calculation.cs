using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixGenerator.Selectors
{
	public class SumOfElement : ISelectOperation
	{
		public double Operate(List<double> Matrix)
		{
			if (Matrix.Count > 0)
				return Matrix.Sum();
			return
				double.NaN;
		}

		public string ToText()
		{
			return "сумму";
		}
	}
	public class MinElement : ISelectOperation
	{
		public double Operate(List<double> Matrix)
		{
			if (Matrix.Count > 0)
				return Matrix.Min();
			return
				double.NaN;
		}

		public string ToText()
		{
			return "минимальное значение среди";
		}
	}
	public class MaxElement : ISelectOperation
	{
		public double Operate(List<double> Matrix)
		{
			if(Matrix.Count>0)			
				return Matrix.Max();
			return 
				double.NaN;
		}
		public string ToText()
		{
			return "максимальное значение среди";
		}
	}
	public class AvgOfElement : ISelectOperation
	{
		public double Operate(List<double> Matrix)
		{
			if (Matrix.Count > 0)
				return Matrix.Average();
			return
				double.NaN;
		}
		public string ToText()
		{
			return "среднее арифметическое среди";
		}
	}
	public class CountOfElement : ISelectOperation
	{
		public double Operate(List<double> Matrix)
		{
			return Matrix.Count();
		}
		public string ToText()
		{
			return "количество";
		}
	}
}