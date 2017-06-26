using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixGenerator.Selectors
{
	public class PositiveElements : ISetSelector
	{
		public List<double> GetSet(Matrix<double> Matrix)
		{
			//List<double> B = A.ToArray().Cast<double>().ToList();
			return Matrix.ToArray().Cast<double>().ToList().FindAll(T => T > 0);
		}
		public string ToText()
		{
			return "всех положительных элементов";
		}
	}
	public class NegativeElements : ISetSelector
	{
		public List<double> GetSet(Matrix<double> Matrix)
		{
			return Matrix.ToArray().Cast<double>().ToList().FindAll(T => T < 0);
		}
		public string ToText()
		{
			return "всех отрицательных элементов";
		}
	}
	public class OddElements : ISetSelector
	{
		public List<double> GetSet(Matrix<double> Matrix)
		{
			return Matrix.ToArray().Cast<double>().ToList().FindAll(T => T % 2==1);
		}
		public string ToText()
		{
			return "всех нечётных элементов";
		}
	}
	public class EvenElements : ISetSelector
	{
		public List<double> GetSet(Matrix<double> Matrix)
		{
			return Matrix.ToArray().Cast<double>().ToList().FindAll(T => T % 2 == 0);
		}
		public string ToText()
		{
			return "всех чётных элементов";
		}
	}
	public class AllElements : ISetSelector
	{
		public List<double> GetSet(Matrix<double> Matrix)
		{
			return Matrix.ToArray().Cast<double>().ToList();
		}
		public string ToText()
		{
			return "всех элементов";
		}
	}
	public class NumberEqualElements : ISetSelector
	{
		public double Number;
		public NumberEqualElements(double Number=0)
		{
			this.Number = Number;
		}
		public List<double> GetSet(Matrix<double> Matrix)
		{
			return Matrix.ToArray().Cast<double>().ToList().FindAll(T => T == Number);
		}
		public string ToText()
		{
			return "всех элементов, равных числу "+ Number.ToString()+",";
		}
	}
}