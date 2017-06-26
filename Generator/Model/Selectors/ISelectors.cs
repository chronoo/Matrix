using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace MatrixGenerator.Selectors
{
	public interface IAreaSelector
	{
		Matrix<double> GetArea(Matrix<double> Matrix);
		string ToText();
	}
	public interface ISetSelector
	{
		List<double> GetSet(Matrix<double> Matrix);
		string ToText();
	}
	public interface ISelectOperation
	{
		double Operate(List<double> Matrix);
		string ToText();
	}
	public interface IMatrixOperation
	{
		object Operate();
		void SetMatrix(Matrix Matrix);
		string ToText();
	}
}