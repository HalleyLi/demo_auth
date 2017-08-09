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
using System.Collections.Generic;
namespace Common.Test.Dto
{
    [TestClass]
    public class WapMenuDtoTest
    {

        [Test]
        [TestCategory("WapMenuDto.Validate")]
        [Description("验证输入,正测试")]
        public void Validate_Normal_Success()
        {
            var c1 = new WapMenuDto()
            {
                MenuName = "1",
                Operation = "2",
                ComponentId = "3",
                Css = "4",
                ParentMenuKey = Guid.Empty.ToString(),
                MenuKey = Guid.NewGuid().ToString(),
                FuncKey = Guid.NewGuid().ToString(),
                Comment="1"
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(true, c1Result.IsValid);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCategory("WapMenuDto.Validate")]
        [Description("新增对象错误")]
        public void Validate_Name_ThrowException(int start)
        {

            var c1 = new WapMenuDto()
            {
                Operation = "2",
                ComponentId = "3",
                Css = "4",
                ParentMenuKey = Guid.Empty.ToString(),
                MenuKey = Guid.NewGuid().ToString()
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result.IsValid);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCategory("WapMenuDto.Validate")]
        [Description("新增对象错误")]
        public void Validate_Operation_ThrowException(int start)
        {

            var c1 = new WapMenuDto()
            {
                MenuName = "1",
                ComponentId = "3",
                Css = "4",
                ParentMenuKey = Guid.Empty.ToString(),
                MenuKey = Guid.NewGuid().ToString()
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result.IsValid);
        }


        [TestCase(0)]
        [TestCase(-1)]
        [TestCategory("WapMenuDto.Validate")]
        [Description("新增对象错误")]
        public void Validate_Component_ThrowException(int start)
        {

            var c1 = new WapMenuDto()
            {
                MenuName = "1",
                Operation = "2",
                ComponentId = "33333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333333",
                Css = "4",
                ParentMenuKey = Guid.Empty.ToString(),
                MenuKey = Guid.NewGuid().ToString()
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result.IsValid);
        }



        [TestCase(0)]
        [TestCase(-1)]
        [TestCategory("WapMenuDto.Validate")]
        [Description("新增对象错误")]
        public void Validate_Css_ThrowException(int start)
        {

            var c1 = new WapMenuDto()
            {
                MenuName = "1",
                Operation = "2",
                ComponentId = "3",
                Css = "44444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444444",
                ParentMenuKey = Guid.Empty.ToString(),
                MenuKey = Guid.NewGuid().ToString()
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result.IsValid);
        }



        [TestCase(0)]
        [TestCase(-1)]
        [TestCategory("WapMenuDto.Validate")]
        [Description("新增对象错误")]
        public void Validate_ParentKey_ThrowException(int start)
        {

            var c1 = new WapMenuDto()
            {
                MenuName = "1",
                Operation = "2",
                ComponentId = "3",
                Css = "4",
                ParentMenuKey = "4444",
                MenuKey = Guid.NewGuid().ToString()
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result.IsValid);
        }



        [TestCase(0)]
        [TestCase(-1)]
        [TestCategory("WapMenuDto.Validate")]
        [Description("新增对象错误")]
        public void Validate_Key_ThrowException(int start)
        {

            var c1 = new WapMenuDto()
            {
                MenuName = "1",
                Operation = "2",
                ComponentId = "3",
                Css = "4",
                ParentMenuKey = Guid.Empty.ToString(),
                MenuKey = "44323432"
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result.IsValid);
        }



    }
}
