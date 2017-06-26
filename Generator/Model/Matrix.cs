using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Random;

namespace MatrixGenerator
{
    public class NameComparer : IComparer<Matrix>
    {
        public int Compare(Matrix X, Matrix Y)
        {
            if (X == null)
            {
                throw new ArgumentNullException(nameof(X));
            }
            else if (X.Label == null || X.Label == "")
            {
                throw new ArgumentNullException(nameof(X.Label));
            }
            if (Y == null)
            {
                throw new ArgumentNullException(nameof(Y));
            }
            else if (Y.Label == null || Y.Label == "")
            {
                throw new ArgumentNullException(nameof(Y.Label));
            }

            if (X.Label[0] > Y.Label[0])
            {
                return 1;
            }
            else if (X.Label[0] < Y.Label[0])
            {
                return -1;
            }
            return 0;
        }
    }
    public class Matrix
    {
        public Matrix<double> Field;
        public string Label;
        public Matrix()
        {
            Label = "";
        }
        public Matrix(int XSize, int YSize, double MinValue, double MaxValue, bool real, bool zeroDet, string name)
        {
            if(XSize<1|| YSize<1)
            {
                throw new ArgumentOutOfRangeException("количество строк и столбцов должно быть больше нуля");
            }
            Random rand = new Random();
            double[] random = MathNet.Numerics.Generate.Uniform(XSize * YSize);
            double[,] matrix = new double[XSize, YSize];
            for (int i = 0; i < XSize; i++)
                for (int j = 0; j < YSize; j++)
                {
                    int temp = (int)Math.Round((random[i * YSize + j] * (MaxValue - MinValue) + MinValue) * 10);

                    matrix[i, j] = (double)(temp - temp % 2) / 10;
                    if (!real)
                        matrix[i, j] = Math.Round(matrix[i, j]);
                }
            Field = Matrix<double>.Build.DenseOfArray(matrix);
            if (zeroDet && XSize == YSize)
            {
                if (!real)
                    MakeZeroDeterminant();
                else
                {
                    MakeZeroDeterminant();//MakeRealZeroDet();
                }
            }
            Label = name;
        }
        public void MakeRealZeroDet()
        {
            if(Field==null)
            {
                throw new ArgumentNullException(nameof(Field));
            }
            Matrix Temp = new Matrix();
            Matrix<double> Rand = Matrix<double>.Build.Random(Field.RowCount, Field.ColumnCount);
            for (int i = 0; i < Field.RowCount; i++)
                for (int j = 0; j < Field.ColumnCount; j++)
                    Rand[i, j] = Rand[i, j] % 2 + 1;
            Temp.Field = Rand;
            Temp.MakeZeroDeterminant();
            Field = Field.Multiply(Temp.Field);
        }
        public Matrix(Matrix<double> Field, string Name = "A")
        {
            if (Field != null)
            {
                this.Field = Field;
                Field.CopyTo(this.Field);
            }
            else
                this.Field = null;
            Label = Name;
        }
        public void SetMatrix(double[,] DoubleMatrix)
        {
            Field = Matrix<double>.Build.DenseOfArray(DoubleMatrix);
        }
        public string ToTEX(string format = "G3")
        {
            List<string> LaTEX = new List<string>();

            LaTEX.Add(@"\begin{pmatrix}");
            for (int i = 0; i < Field.RowCount; i++)
            {
                string Row = "";
                for (int j = 0; j < Field.ColumnCount; j++)
                {
                    if (Math.Abs(Field[i, j]) % 1 < 0.001)
                        Row += Field[i, j].ToString("N0");
                    else
                        Row += Field[i, j].ToString(format);
                    if (j != Field.ColumnCount - 1)
                        Row += @"&";
                }
                if (i != Field.RowCount - 1)
                    Row += @"\\";
                LaTEX.Add(Row);
            }
            LaTEX.Add(@"\end{pmatrix}");
            return string.Concat(LaTEX.ToArray());
        }
        public double DeterminantFind()
        {
            double Determinant = double.NaN;
            if (GetRowCount() == GetColumnCout())
                Determinant = Field.Determinant();
            return Determinant;
        }
        public Matrix InverseMatrix()
        {
            Matrix Inverse = new Matrix(null, Label);
            if (DeterminantFind() != 0 && !double.IsNaN(DeterminantFind()))
                Inverse = new Matrix(this.Field.Inverse(), Label);
            return Inverse;
        }
        public string OutToString()
        {
            if (Field != null)
                return Field.ToString();
            else
                return "error";
        }
        public Matrix TranspositionMatrix()
        {
            Matrix Transposition = new Matrix(Field.Transpose(), Label);
            return Transposition;
        }
        public void MakeZeroDeterminant()
        {
            MersenneTwister Random = new MersenneTwister();
            if (Random.NextDouble() > 0.5)
            {
                int RandColumn = Random.Next(Field.ColumnCount);
                int RandColumnReplaced;
                do
                {
                    RandColumnReplaced = Random.Next(Field.ColumnCount);
                } while (RandColumnReplaced == RandColumn);
                Field.SetColumn(RandColumnReplaced, Field.Column(RandColumn).Multiply(Random.Next(2, 4)));
            }
            else
            {
                int RandRow = Random.Next(Field.RowCount);
                int RandRowReplaced;
                do
                {
                    RandRowReplaced = Random.Next(Field.RowCount);
                } while (RandRowReplaced == RandRow);
                Field.SetRow(RandRowReplaced, Field.Row(RandRow).Multiply(Random.Next(2, 4)));
            }
        }
        public int GetRowCount()
        {
            return Field.RowCount;
        }
        public int GetColumnCout()
        {
            return Field.ColumnCount;
        }
    }
}