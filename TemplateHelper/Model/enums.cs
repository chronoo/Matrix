public enum MatrixRandomSize
{
	min = 2,
	max = 10
}
public enum MatrixSizeType
{
	random,
	formula,
	custom
}
public enum ElementType
{
	positive,
	any
}
public enum MatrixElenemtRange
{
	min=0,
	max=10
}
public enum TransposeType
{
	allMatrix,
	singleMatrix
}
public enum CountType
{
	allMatrix,
	singleMatrix
}
public enum DetermenantType
{
	allMatrix,
	singleMatrix,
	quadMatrix
}
public enum InvertType
{
	allMatrix,
	singleMatrix,
	quadMatrix,
	zeroDetMatrix
}
public enum SubMatrixType
{
	MainDiagonal,
	SecondDiagonal,
	FewRow,
	FewColumn,
	SubMatrix,
	SingleRow,
	SingleColumn,
	AllMatrix
}
public enum ElementValueType
{
	Positive,
	Negative,
	Odd,
	Even,
	Number,
	All
}
public enum CalculationType
{
	Max,
	Min,
	Sum,
	Avg,
	Count
}