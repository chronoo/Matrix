// <copyright file="BasicBynOperationTest.cs">Copyright ©  2017</copyright>
using System;
using Generator.Model.Operations;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Generator.Model.Operations.Tests
{
    /// <summary>Этот класс содержит параметризованные модульные тесты для BasicBynOperation</summary>
    [PexClass(typeof(BasicBynOperation))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class BasicBynOperationTest
    {
    }
}
