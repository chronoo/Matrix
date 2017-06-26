using MathNet.Numerics.Random;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator.Model
{
	public static class RandDecide
	{
		public static OperationEnum Decide(bool isQuad, int mult, int sum, int transe, int inverse)
		{
			int current = 0;
			if (!isQuad)
				inverse = 0;
			MersenneTwister random=new MersenneTwister();
			Tuple<int, int> multInterval = new Tuple<int, int>(current, current+=mult);
			Tuple<int, int> sumInterval = new Tuple<int, int>(current, current += sum);
			Tuple<int, int> transeInterval = new Tuple<int, int>(current, current += transe);
			Tuple<int, int> inverseInterval = new Tuple<int, int>(current, current += inverse);
			int randNumb = 0;
			if (inverseInterval.Item2 > 0)
				randNumb = random.Next(0, inverseInterval.Item2);
			else return OperationEnum.error;
			if (multInterval.IsInclude(randNumb))
				return OperationEnum.mult;
			else if (sumInterval.IsInclude(randNumb))
				return OperationEnum.sum;
			else if (transeInterval.IsInclude(randNumb))
				return OperationEnum.transe;
			else
				return OperationEnum.inverse;
		}		
	}
	public static class TupleExtension
	{
		public static bool IsInclude(this Tuple<int, int> tuple, int x)
		{
			if (x >= tuple.Item1 && x < tuple.Item2)
				return true;
			else
				return false;
		}
	}
}
