// <copyright file="MatrixTest.cs">Copyright ©  2017</copyright>

using System;
using MatrixGenerator;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatrixGenerator.Tests
{
    [TestClass]
    [PexClass(typeof(Matrix))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class MatrixTest
    {

        [PexMethod(MaxRunsWithoutNewTests = 200, MaxConditions = 1000)]
        public Matrix Constructor(
            int XSize,
            int YSize,
            double MinValue,
            double MaxValue,
            bool real,
            bool zeroDet,
            string name
        )
        {
            Matrix target = new Matrix(XSize, YSize, MinValue, MaxValue, real, zeroDet, name);
            return target;
            // TODO: добавление проверочных утверждений в метод MatrixTest.Constructor(Int32, Int32, Double, Double, Boolean, Boolean, String)
        }

        [PexMethod]
        public void SetMatrix([PexAssumeUnderTest]Matrix target, double[,] DoubleMatrix)
        {
            target.SetMatrix(DoubleMatrix);
            // TODO: добавление проверочных утверждений в метод MatrixTest.SetMatrix(Matrix, Double[,])
        }
    }
}
