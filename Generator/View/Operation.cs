using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixGenerator;
using MatrixGenerator.Operations;
using MatrixGenerator.Selectors;
using MathNet.Numerics.LinearAlgebra;

namespace Generator.Model.Operations
{
    public class Operation
    {
        private static MatrixCreator creator;
        public Operation(MatrixCreator creator)
        {
            Operation.creator = creator;
        }
        public static void CountRow()
        {
            switch (creator.template.countRowType)
            {
                case CountType.allMatrix:
                    creator.solveText.Add(@"\item Количество строк:");
                    creator.solveText.Add(@"\begin{itemize}");
                    foreach (Matrix matrix in creator.matrixList)
                    {
                        GetRowCount op = new GetRowCount(matrix);
                        int countRow = (int)op.Operate();
                        creator.solveText.Add(string.Format(@"\item в матрице ${0}={1}$;", matrix.Label, countRow));
                    }
                    creator.taskText.Add((string.Format(@"\item Найдите количество строк для каждой матрицы.")));
                    creator.solveText.Add(@"\end{itemize}");
                    break;
                case CountType.singleMatrix:
                    {
                        Matrix matrix = new Matrix();
                        do
                        {
                            matrix = MatrixCreator.GetRandomMatix(creator.matrixList);
                        } while (matrix.GetRowCount() == 1);
                        GetRowCount op = new GetRowCount(matrix);
                        int countRow = (int)op.Operate();
                        creator.solveText.Add(string.Format(@"\item Количество строк в матрице ${0}={1}$.", matrix.Label, countRow));
                        creator.taskText.Add(@"\item"+ op.ToText());
                    }
                    break;
                default:
                    break;
            }
        }
        public static void CountColumn()
        {
            switch (creator.template.countColumnType)
            {
                case CountType.allMatrix:
                    creator.solveText.Add(@"\item Количество столбцов:");
                    creator.solveText.Add(@"\begin{itemize}");
                    foreach (Matrix matrix in creator.matrixList)
                    {
                        GetColumnCout op = new GetColumnCout(matrix);
                        int countColumn = (int)op.Operate();
                        creator.solveText.Add(string.Format(@"\item в матрице ${0}={1}$;", matrix.Label, countColumn));
                    }
                    creator.taskText.Add((string.Format(@"\item Найдите количество столбцов для каждой матрицы.")));
                    creator.solveText.Add(@"\end{itemize}");
                    break;
                case CountType.singleMatrix:
                    {
                        Matrix matrix = new Matrix();
                        do
                        {
                            matrix = MatrixCreator.GetRandomMatix(creator.matrixList);
                        } while (matrix.GetColumnCout() == 1);
                        GetColumnCout op = new GetColumnCout(matrix);
                        int countColumn = (int)op.Operate();
                        creator.solveText.Add(string.Format(@"\item Количество столбцов в матрице ${0}={1}$. ", matrix.Label, countColumn));
                        creator.taskText.Add(@"\item " + op.ToText());
                    }
                    break;
                default:
                    break;
            }
        }
        public static void TranseMatrix()
        {
            switch (creator.template.transeMatrixType)
            {
                case (int)TransposeType.allMatrix:
                    creator.solveText.Add(@"\item Транспонированние матриц:");
                    creator.solveText.Add(@"\begin{itemize}");
                    foreach (Matrix matrix in creator.matrixList)
                    {
                        Transposition op = new Transposition(matrix);
                        Matrix tranceMatrix = (Matrix)op.Operate();
                        creator.solveText.Add(string.Format(@"\item ${0}^T={1}$", tranceMatrix.Label, tranceMatrix.ToTEX()));
                    }
                    creator.taskText.Add((string.Format(@"\item Транспонируйте все исходные матрицы.")));
                    creator.solveText.Add(@"\end{itemize}");
                    break;
                case TransposeType.singleMatrix:
                    {
                        Matrix matrix = MatrixCreator.GetRandomMatix(creator.matrixList);
                        Transposition op = new Transposition(matrix);
                        Matrix tranceMatrix = (Matrix)op.Operate();
                        creator.taskText.Add(@"\item " + op.ToText());
                        creator.solveText.Add(string.Format(@"\item ${0}^T={1}$", tranceMatrix.Label, tranceMatrix.ToTEX()));
                    }
                    break;
                default:
                    break;
            }
        }
        public static void DetermMatrix()
        {
            try
            {
                switch (creator.template.determMatrixType)
                {
                    case (int)DetermenantType.allMatrix:
                        creator.solveText.Add(@"\item Нахождение определителя:");
                        creator.solveText.Add(@"\begin{itemize}");
                        foreach (Matrix matrix in creator.matrixList)
                        {
                            FindDeter op = new FindDeter(matrix);
                            if (matrix.Field.ColumnCount == matrix.Field.RowCount)
                            {
                                double determ = (double)op.Operate();
                                string det = @"\begin{vmatrix}" + matrix.Label + @"\end{vmatrix}";
                                creator.solveText.Add(string.Format(@"\item ${0}={1:0.###}$;", det, determ));
                            }
                            else
                            {
                                creator.solveText.Add(string.Format(@"\item матрица ${0}$ не имеет определителя;", matrix.Label));
                            }
                        }
                        creator.solveText.Add(@"\end{itemize}");
                        creator.taskText.Add(@"\item Найдите определители всех матриц.");
                        break;
                    case DetermenantType.singleMatrix:
                        {
                            Matrix matrix = MatrixCreator.GetRandomMatix(creator.matrixList);
                            FindDeter op = new FindDeter(matrix);
                            creator.taskText.Add(@"\item " + op.ToText());
                            if (matrix.Field.ColumnCount == matrix.Field.RowCount)
                            {
                                double determ = (double)op.Operate();
                                string det = @"\begin{vmatrix}" + matrix.Label + @"\end{vmatrix}";
                                creator.solveText.Add(string.Format(@"\item ${0}={1:0.###}$.", det, determ));
                            }
                            else
                            {
                                creator.solveText.Add(string.Format(@"\item Матрица ${0}$ не имеет определителя.", matrix.Label));
                            }
                        }
                        break;
                    case DetermenantType.quadMatrix:
                        {
                            Matrix matrix = creator.matrixList.Find(T => T.Field.RowCount == T.Field.ColumnCount);
                            if(matrix==null)
                            {
                                matrix= MatrixCreator.GetRandomMatix(creator.matrixList);
                            }
                            FindDeter op = new FindDeter(matrix);
                            creator.taskText.Add(@"\item " + op.ToText());

                            if (matrix.Field.ColumnCount == matrix.Field.RowCount)
                            {
                                double determ = (double)op.Operate();
                                string det = @"\begin{vmatrix}" + matrix.Label + @"\end{vmatrix}";
                                creator.solveText.Add(string.Format(@"\item ${0}={1:0.###}$. ", det, determ));
                            }
                            else
                            {
                                creator.solveText.Add(string.Format(@"\item Матрица ${0}$ не имеет определителя.", matrix.Label));
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

        }
        public static void InvertMatrix()
        {
            try
            {
                switch (creator.template.invertMatrixType)
                {
                    case (int)InvertType.allMatrix:
                        creator.solveText.Add(@"\item Нахождение обратной матрицы:");
                        creator.solveText.Add(@"\begin{itemize}");
                        foreach (Matrix matrix in creator.matrixList)
                        {
                            Inverse op = new Inverse(matrix);
                            if (matrix.GetColumnCout() == matrix.GetRowCount())
                            {
                                if (matrix.GetColumnCout() != matrix.GetRowCount() || matrix.DeterminantFind() != 0.0)
                                {
                                    Matrix inversMatrix = (Matrix)op.Operate();
                                    creator.solveText.Add(string.Format(@"\item ${0}^{{-1}}={1}$", inversMatrix.Label, inversMatrix.ToTEX("N3")));
                                }
                                else
                                {
                                    creator.solveText.Add(string.Format(@"\item матрица ${0}$ не имеет обратной матрицы (детерминант равен нулю);", matrix.Label));
                                }
                            }
                            else
                            {
                                creator.solveText.Add(string.Format(@"\item матрица ${0}$ не имеет обратной матрицы (матрица не квадратная);", matrix.Label));
                            }
                        }
                        creator.solveText.Add(@"\end{itemize}");
                        creator.taskText.Add(@"\item Для всех матриц найдите обратные матрицы.");
                        break;
                    case InvertType.singleMatrix:
                        {
                            Matrix matrix = MatrixCreator.GetRandomMatix(creator.matrixList);

                            Inverse op = new Inverse(matrix);
                            creator.taskText.Add(@"\item " + op.ToText());

                            if (matrix.GetColumnCout() == matrix.GetRowCount())
                            {
                                if (matrix.GetColumnCout() != matrix.GetRowCount() || matrix.DeterminantFind() != 0.0)
                                {
                                    Matrix inversMatrix = (Matrix)op.Operate();
                                    creator.solveText.Add(string.Format(@"\item ${0}^{{-1}}={1}$.", inversMatrix.Label, inversMatrix.ToTEX()));
                                }
                                else
                                {
                                    creator.solveText.Add(string.Format(@"\item Матрица ${0}$ не имеет обратной матрицы (детерминант равен нулю).", matrix.Label));
                                }
                            }
                            else
                            {
                                creator.solveText.Add(string.Format(@"\item Матрица ${0}$ не имеет обратной матрицы (матрица не квадратная).", matrix.Label));
                            }
                        }
                        break;
                    case InvertType.quadMatrix:
                        {
                            Matrix matrix = creator.matrixList.Find(T => T.Field.RowCount == T.Field.ColumnCount);
                            if (matrix == null)
                            {
                                matrix = MatrixCreator.GetRandomMatix(creator.matrixList);
                                Inverse _op = new Inverse(matrix);
                                creator.taskText.Add(@"\item " + _op.ToText());
                                creator.solveText.Add(string.Format(@"\item Матрица ${0}$ не имеет обратной матрицы (детерминант равен нулю).", matrix.Label));
                                return;
                            }
                            Inverse op = new Inverse(matrix);
                            creator.taskText.Add(@"\item " + op.ToText());

                            if (matrix.DeterminantFind() != 0.0)
                            {
                                Matrix inversMatrix = (Matrix)op.Operate();
                                creator.solveText.Add(string.Format(@"\item ${0}^{{-1}}={1}$.", inversMatrix.Label, inversMatrix.ToTEX()));
                            }
                            else
                            {
                                creator.solveText.Add(string.Format(@"\item Матрица ${0}$ не имеет обратной матрицы (детерминант равен нулю).", matrix.Label));
                            }
                        }
                        break;
                    case InvertType.zeroDetMatrix:
                        {
                            Matrix matrix = creator.matrixList.Find(T => Math.Abs(T.DeterminantFind()) < 0.001);

                            Inverse op = new Inverse(matrix);
                            creator.taskText.Add(@"\item " + op.ToText());

                            if (matrix != null)
                            {
                                creator.solveText.Add(string.Format(@"\item Матрица ${0}$ не имеет обратной матрицы (детерминант равен нулю).", matrix.Label));
                            }
                            else
                            {
                                Matrix inversMatrix = (Matrix)op.Operate();
                                creator.solveText.Add(string.Format(@"\item ${0}^{{-1}}={1}$.", inversMatrix.Label, inversMatrix.ToTEX()));
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        public static void GetElement()
        {
            if (creator.template.outRowByNumber || creator.template.outColumnByNumber || creator.template.outSubMatrix)
            {
                creator.taskText.Add(@"\item Нахождение элемента матрицы:");
                creator.taskText.Add(@"\begin{itemize}");
                creator.solveText.Add(@"\item Нахождение элемента матрицы:");
                creator.solveText.Add(@"\begin{itemize}");
                List<IAreaSelector> selectors = new List<IAreaSelector>();
                if (creator.template.outRowByNumber == true)
                    selectors.Add(new SingleRowSelector());
                if (creator.template.outColumnByNumber == true)
                    selectors.Add(new SingleColumnSelector());
                if (creator.template.outSubMatrix == true)
                {
                    switch (creator.template.outSubMatrixType)
                    {
                        case SubMatrixType.MainDiagonal:
                            selectors.Add(new MainDiagonalSelector());
                            break;
                        case SubMatrixType.SecondDiagonal:
                            selectors.Add(new SecondDiagonalSelector());
                            break;
                        case SubMatrixType.FewRow:
                            selectors.Add(new FewRowSelector());
                            break;
                        case SubMatrixType.FewColumn:
                            selectors.Add(new FewColumnSelector());
                            break;
                        case SubMatrixType.SubMatrix:
                            selectors.Add(new SubMatrixSelector());
                            break;
                    }
                }
                List<Matrix> matrixList = new List<Matrix>();
                matrixList.AddRange(creator.matrixList);
                foreach (IAreaSelector selector in selectors)
                {
                    Matrix matrix = matrixList[MatrixCreator.random.Next(0, matrixList.Count)];
                    matrixList.Remove(matrix);
                    if (matrixList.Count == 0)
                        matrixList.AddRange(creator.matrixList);
                    GetElement op = new GetElement(selector, matrix);
                    Matrix res = new Matrix(op.Operate() as Matrix<double>, matrix.Label);
                    creator.taskText.Add(string.Format(@"\item для матрицы ${1}$ найдите элементы{0}.", selector.ToText(), matrix.Label));
                    creator.solveText.Add(string.Format(@"\item матрица ${1}$: элементы{0} $={2}$;", selector.ToText(), matrix.Label, res.ToTEX()));
                }
                creator.taskText.Add(@"\end{itemize}");
                creator.solveText.Add(@"\end{itemize}");
            }
        }
        public static void Calculate()
        {
            IAreaSelector AreaSelector = null;
            ISetSelector SetSelector = null;
            ISelectOperation Operation = null;
            switch (creator.template.calcValueArea)
            {
                case SubMatrixType.MainDiagonal:
                    AreaSelector = new MainDiagonalSelector();
                    break;
                case SubMatrixType.SecondDiagonal:
                    AreaSelector = new SecondDiagonalSelector();
                    break;
                case SubMatrixType.FewRow:
                    AreaSelector = new FewRowSelector();
                    break;
                case SubMatrixType.FewColumn:
                    AreaSelector = new FewColumnSelector();
                    break;
                case SubMatrixType.SubMatrix:
                    AreaSelector = new SubMatrixSelector();
                    break;
                case SubMatrixType.SingleRow:
                    AreaSelector = new SingleRowSelector();
                    break;
                case SubMatrixType.SingleColumn:
                    AreaSelector = new SingleColumnSelector();
                    break;
                case SubMatrixType.AllMatrix:
                    AreaSelector = new AllMatrixSelector();
                    break;
            }
            switch (creator.template.calcValueType)
            {
                case ElementValueType.Positive:
                    SetSelector = new PositiveElements();
                    break;
                case ElementValueType.Negative:
                    SetSelector = new NegativeElements();
                    break;
                case ElementValueType.Odd:
                    SetSelector = new OddElements();
                    break;
                case ElementValueType.Even:
                    SetSelector = new EvenElements();
                    break;
                case ElementValueType.Number:
                    SetSelector = new NumberEqualElements();
                    break;
                case ElementValueType.All:
                    SetSelector = new AllElements();
                    break;
            }
            switch (creator.template.calcValueOperation)
            {
                case CalculationType.Max:
                    Operation = new MaxElement();
                    break;
                case CalculationType.Min:
                    Operation = new MinElement();
                    break;
                case CalculationType.Sum:
                    Operation = new SumOfElement();
                    break;
                case CalculationType.Avg:
                    Operation = new AvgOfElement();
                    break;
                case CalculationType.Count:
                    Operation = new CountOfElement();
                    break;
            }
            MatrixCalculator calculator = new MatrixCalculator(AreaSelector, SetSelector, Operation);
            List<Matrix> matrixList = new List<Matrix>();
            matrixList.AddRange(creator.matrixList);
            matrixList.Sort(new FildComparer());
            calculator.Matrix = matrixList[0];
            double res = (double)calculator.Operate();
            creator.taskText.Add(@"\item " + calculator.ToText());
            if (res == double.NaN)
            {
                creator.solveText.Add(string.Format(@"\item Нет решений."));
            }
            else
            {
                creator.solveText.Add(string.Format(@"\item {0} $={1}$.", calculator.GetSolveText(), res));
            }
        }
        public static void InvalidMult()
        {
            BasicBynOperation multOp = new InvalidMult(creator.matrixList);
            creator.taskText.Add((string.Format(@"\item {0}", multOp.taskText)));
            creator.solveText.Add(string.Format(@"\item {0}", multOp.solveText));
        }
        public static void InvalidSum()
        {
            InvalidSum sumOp = new InvalidSum(creator.matrixList);
            creator.taskText.Add((string.Format(@"\item {0}", sumOp.taskText)));
            creator.solveText.Add(string.Format(@"\item {0}", sumOp.solveText));
        }
        public class FildComparer : IComparer<Matrix>
        {
            public int Compare(Matrix X, Matrix Y)
            {
                if (Assess(X) > Assess(Y))
                {
                    return 1;
                }
                else if (Assess(X) < Assess(Y))
                {
                    return -1;
                }
                return 0;
            }
            public double Assess(Matrix x)
            {
                return Math.Min(x.Field.RowCount, x.Field.ColumnCount) + Math.Abs(x.Field.RowCount - x.Field.ColumnCount) / Math.Max(x.Field.RowCount, x.Field.ColumnCount);
            }
        }
        
    }
}