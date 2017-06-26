using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrixGenerator;

namespace Generator.Model.Operations
{
    public abstract class BasicBynOperation: IBynaryOperation
    {
        protected List<Matrix> _matrixList;
        protected bool isComplete;
        protected Matrix _result;
        protected Matrix _firstMatrix;
        protected Matrix _secondMatrix;
        protected string _solveText;
        protected string _taskText;
        public string solveText { get {
                if(!isComplete)
                {
                    Operate();
                }
                return _solveText;
            } }
        public string taskText { get {
                if (!isComplete)
                {
                    Operate();
                }
                return _taskText; } }
        public Matrix firstMatrix { get {
                if (!isComplete)
                {
                    Operate();
                }
                return _firstMatrix; } }
        public Matrix secondMatrix { get {
                if (!isComplete)
                {
                    Operate();
                }
                return _secondMatrix; } }
        public Matrix result { get {
                if (!isComplete)
                {
                    Operate();
                }
                return _result; } }

        abstract protected void Operate();
        public BasicBynOperation(List<Matrix> matrixList)
        {
            if(matrixList == null || matrixList.Count==0)
            {
                throw new ArgumentNullException(nameof(matrixList));
            }
            this._secondMatrix = null;
            this._matrixList = matrixList;
            this._result = null;
            isComplete = false;
        }
    }
    public interface IBynaryOperation
    {
        string solveText { get; }
        string taskText { get; }
        Matrix firstMatrix { get; }
        Matrix secondMatrix { get; }
        Matrix result { get; }
    }
    public class InvalidMult : BasicBynOperation
    {
        public InvalidMult(List<Matrix> matrixList) : base(matrixList)
        {
        }

        protected override void Operate()
        {
            List<Matrix> tempList = new List<Matrix>();
            tempList.AddRange(_matrixList);
            do
            {
                int index = MatrixCreator.random.Next(0, tempList.Count);
                _firstMatrix = tempList[index];
                tempList.RemoveAt(index);
                List<Matrix> fitList = new List<Matrix>();
                fitList.AddRange(tempList.FindAll(T =>( T.Field.RowCount != _firstMatrix.Field.ColumnCount )));

                if (fitList.Count > 0)
                {
                    _secondMatrix = fitList[MatrixCreator.random.Next(0, fitList.Count)];
                }
            } while (_secondMatrix == null&&tempList.Count>0);
            if(_secondMatrix == null)
            {
                tempList.AddRange(_matrixList);
                int index = MatrixCreator.random.Next(0, tempList.Count);
                _firstMatrix = tempList[index];
                tempList.RemoveAt(index);
                _secondMatrix = tempList[MatrixCreator.random.Next(0, tempList.Count)];
            }
            try
            {
                _result = new Matrix(_firstMatrix.Field.Multiply(_secondMatrix.Field), "result");
            }
            catch
            {
                _result = null;
            }
            _taskText = string.Format(@"Найдите произведение матриц ${0}*{1}$.",_firstMatrix.Label,_secondMatrix.Label);
            if (_result != null)
            {
                _solveText = string.Format(@"Произведение матриц ${0}*{1}={2}$.", _firstMatrix.Label, _secondMatrix.Label, _result.ToTEX());
            }
            else
            {
                _solveText = string.Format(@"Произведение матриц ${0}*{1}$ ответа не имеет.", _firstMatrix.Label, _secondMatrix.Label);
            }
            isComplete = true;
        }
    }
    public class InvalidSum : BasicBynOperation
    {
        public InvalidSum(List<Matrix> matrixList) : base(matrixList)
        {
        }

        protected override void Operate()
        {
            List<Matrix> tempList = new List<Matrix>();
            tempList.AddRange(_matrixList);
            do
            {
                int index = MatrixCreator.random.Next(0, tempList.Count);
                _firstMatrix = tempList[index];
                tempList.RemoveAt(index);
                List<Matrix> fitList = new List<Matrix>();
                fitList.AddRange(tempList.FindAll(T => (T.Field.RowCount != _firstMatrix.Field.RowCount) || (T.Field.ColumnCount != _firstMatrix.Field.ColumnCount)));

                if (fitList.Count > 0)
                {
                    _secondMatrix = fitList[MatrixCreator.random.Next(0, fitList.Count)];
                }
            } while (_secondMatrix == null && tempList.Count > 0);
            if (_secondMatrix == null)
            {
                tempList.AddRange(_matrixList);
                int index = MatrixCreator.random.Next(0, tempList.Count);
                _firstMatrix = tempList[index];
                tempList.RemoveAt(index);
                _secondMatrix = tempList[MatrixCreator.random.Next(0, tempList.Count)];
            }
            try
            {
                _result = new Matrix(_firstMatrix.Field.Add(_secondMatrix.Field), "result");
            }
            catch
            {
                _result = null;
            }
            _taskText = string.Format(@"Найдите сумму матриц ${0}+{1}$.", _firstMatrix.Label, _secondMatrix.Label);
            if (_result != null)
            {
                _solveText = string.Format(@"Сумма матриц ${0}+{1}={2}$.", _firstMatrix.Label, _secondMatrix.Label, _result.ToTEX());
            }
            else
            {
                _solveText = string.Format(@"Сумма матриц ${0}+{1}$ ответа не имеет.", _firstMatrix.Label, _secondMatrix.Label);
            }
            isComplete = true;
        }
    }

}
