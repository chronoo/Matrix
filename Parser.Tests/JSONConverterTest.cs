// <copyright file="JSONConverterTest.cs">Copyright ©  2017</copyright>
using System;
using System.Collections.Generic;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Parser.Model;
using Parser.View;

namespace Parser.View.Tests
{
    /// <summary>Этот класс содержит параметризованные модульные тесты для JSONConverter</summary>
    [PexClass(typeof(JSONConverter))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class JSONConverterTest
    {
        /// <summary>Тестовая заглушка для .ctor(String, String)</summary>
        [PexMethod]
        public JSONConverter ConstructorTest(string peopleURL, string groupURL)
        {
            JSONConverter target = new JSONConverter(peopleURL, groupURL);
            return target;
            // TODO: добавление проверочных утверждений в метод JSONConverterTest.ConstructorTest(String, String)
        }

        /// <summary>Тестовая заглушка для GetFirstCourseGroupList()</summary>
        [PexMethod]
        public List<Group> GetFirstCourseGroupListTest([PexAssumeUnderTest]JSONConverter target)
        {
            List<Group> result = target.GetFirstCourseGroupList();
            return result;
            // TODO: добавление проверочных утверждений в метод JSONConverterTest.GetFirstCourseGroupListTest(JSONConverter)
        }

        /// <summary>Тестовая заглушка для GetGroupFromID(Int32)</summary>
        [PexMethod]
        public Group GetGroupFromIDTest([PexAssumeUnderTest]JSONConverter target, int ID)
        {
            Group result = target.GetGroupFromID(ID);
            return result;
            // TODO: добавление проверочных утверждений в метод JSONConverterTest.GetGroupFromIDTest(JSONConverter, Int32)
        }

        /// <summary>Тестовая заглушка для GetGroupsList()</summary>
        [PexMethod]
        public List<Group> GetGroupsListTest([PexAssumeUnderTest]JSONConverter target)
        {
            List<Group> result = target.GetGroupsList();
            return result;
            // TODO: добавление проверочных утверждений в метод JSONConverterTest.GetGroupsListTest(JSONConverter)
        }

        /// <summary>Тестовая заглушка для GetObjectFromURL(String)</summary>
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public T GetObjectFromURLTest<T>(string URL)
        {
            T result = JSONConverter.GetObjectFromURL<T>(URL);
            return result;
            // TODO: добавление проверочных утверждений в метод JSONConverterTest.GetObjectFromURLTest(String)
        }

        /// <summary>Тестовая заглушка для GetObject(String)</summary>
        [PexGenericArguments(typeof(int))]
        [PexMethod]
        public T GetObjectTest<T>(string JSON)
        {
            T result = JSONConverter.GetObject<T>(JSON);
            return result;
            // TODO: добавление проверочных утверждений в метод JSONConverterTest.GetObjectTest(String)
        }

        /// <summary>Тестовая заглушка для GetStudentFromID(String)</summary>
        [PexMethod]
        public Student GetStudentFromIDTest([PexAssumeUnderTest]JSONConverter target, string ID)
        {
            Student result = target.GetStudentFromID(ID);
            return result;
            // TODO: добавление проверочных утверждений в метод JSONConverterTest.GetStudentFromIDTest(JSONConverter, String)
        }

        /// <summary>Тестовая заглушка для GetTeacherFromID(String)</summary>
        [PexMethod]
        public Teacher GetTeacherFromIDTest([PexAssumeUnderTest]JSONConverter target, string ID)
        {
            Teacher result = target.GetTeacherFromID(ID);
            return result;
            // TODO: добавление проверочных утверждений в метод JSONConverterTest.GetTeacherFromIDTest(JSONConverter, String)
        }

        /// <summary>Тестовая заглушка для IsStudent(String)</summary>
        [PexMethod]
        public bool IsStudentTest([PexAssumeUnderTest]JSONConverter target, string ID)
        {
            bool result = target.IsStudent(ID);
            return result;
            // TODO: добавление проверочных утверждений в метод JSONConverterTest.IsStudentTest(JSONConverter, String)
        }
    }
}
