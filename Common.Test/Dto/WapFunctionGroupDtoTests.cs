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

namespace Common.Test.Dto
{

     [TestClass]
    public class WapFunctionGroupDtoTests
    {
        [Test]
        [TestCategory("WapFunctionGroupDto.Validate")]
        [Description("验证输入,正测试")]
        public void Validate_Normal_Success()
        {
            var c1 = new WapFunctionGroupDto()
            {
                FuncGroupName = "测试1",
                FuncGroupPycode = "11111",
                FuncAppKey = Guid.NewGuid().ToString(),
                FuncGroupComment = "22222"
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(true, c1Result.IsValid);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCategory("WapFunctionGroupDto.Validate")]
        [Description("验证功能组名为空")]
        public void Validate_FuncGroupName_ThrowException(int start)
        {

            var c1 = new WapFunctionGroupDto()
            {
                FuncGroupPycode = "11111",
                FuncAppKey = Guid.NewGuid().ToString(),
                FuncGroupComment = "22222"
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result.IsValid);
        }


        [TestCase(0)]
        [TestCase(-1)]
        [TestCategory("WapFunctionGroupDto.Validate")]
        [Description("验证拼音为空")]
        public void Validate_FuncGroupPycode_ThrowException(int start)
        {

            var c1 = new WapFunctionGroupDto()
            {
                FuncGroupName = "测试1",
                FuncAppKey = Guid.NewGuid().ToString(),
                FuncGroupComment = "22222"
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result.IsValid);
        }


        [TestCase(0)]
        [TestCase(-1)]
        [TestCategory("WapFunctionGroupDto.Validate")]
        [Description("验证所属应用不合法")]
        public void Validate_FuncAppKey_ThrowException(int start)
        {

            var c1 = new WapFunctionGroupDto()
            {
                FuncGroupName = "测试1",
                FuncGroupPycode = "11111",
                FuncAppKey = "1231312",
                FuncGroupComment = "22222"
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result.IsValid);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCategory("WapFunctionGroupDto.Validate")]
        [Description("验证备注超长")]
        public void Validate_FuncGroupComment_ThrowException(int start)
        {

            var c1 = new WapFunctionGroupDto()
            {
                FuncGroupName = "测试1",
                FuncGroupPycode = "11111",
                FuncAppKey = Guid.NewGuid().ToString(),
                FuncGroupComment = "22222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222222"
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result.IsValid);
        }
    }
}
