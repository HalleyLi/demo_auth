using System;
using System.Collections.Generic;
using System;

using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using TestCategory = NUnit.Framework.CategoryAttribute;
using SH3H.WAP.Auth.Model;
using SH3H.WAP.Auth.Model.Dto;


namespace Common.Test.Condition
{
    [TestClass]
    public class WapFunctionDtoTests
    {
        [Test]
        [TestCategory("WapFunctionDto.Validate")]
        [Description("验证输入,正测试")]
        public void Validate_Normal_Success()
        {
            var c1 = new WapFunctionDto()
            {
                Name = "测试1",
                Code = "11111",
                Pycode = "11111",
                Comment = "22222"
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(true, c1Result.IsValid);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCategory("WapFunctionDto.Validate")]
        [Description("验证功能名为空")]
        public void Validate_Name_ThrowException(int start)
        {
            var c1 = new WapFunctionDto()
            {
                Code = "11111",
                Pycode = "11111",
                Comment = "22222"
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result.IsValid);
        }


        [TestCase(0)]
        [TestCase(-1)]
        [TestCategory("WapFunctionDto.Validate")]
        [Description("验证拼音为空")]
        public void Validate_Pycode_ThrowException(int start)
        {
            var c1 = new WapFunctionDto()
            {
                Name = "测试1",
                Code = "11111",
                Comment = "22222"
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result.IsValid);
        }


        [TestCase(0)]
        [TestCase(-1)]
        [TestCategory("WapFunctionDto.Validate")]
        [Description("验证功能Code为空")]
        public void Validate_Code_ThrowException(int start)
        {
            var c1 = new WapFunctionDto()
            {
                Name = "测试1",
                Pycode = "11111",
                Comment = "22222"
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result.IsValid);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCategory("WapFunctionDto.Validate")]
        [Description("验证备注超长")]
        public void Validate_Comment_ThrowException(int start)
        {
            var c1 = new WapFunctionDto()
            {
                Name = "测试1",
                Code = "11111",
                Pycode = "11111",
                Comment = "22222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222"
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result.IsValid);
        }
    }
}
