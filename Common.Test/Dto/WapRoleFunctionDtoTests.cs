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
    public class WapRoleFunctionDtoTests
    {
        [Test]
        [TestCategory("WapRoleFunctionDto.Validate")]
        [Description("验证输入,正测试")]
        public void Validate_Normal_Success()
        {
            var c1 = new WapRoleFunctionDto()
            {
                FuncKey = Guid.NewGuid().ToString(),
                RoleKey = Guid.NewGuid().ToString()
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(true, c1Result.IsValid);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCategory("WapRoleFunctionDto.Validate")]
        [Description("验证功能点主键为空")]
        public void Validate_FuncKey_ThrowException(int start)
        {
            
            var c1 = new WapRoleFunctionDto()
            {
                RoleKey = Guid.NewGuid().ToString()
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result.IsValid);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCategory("WapRoleFunctionDto.Validate")]
        [Description("验证角色主键为空")]
        public void Validate_RoleKey_ThrowException(int start)
        {
            
            var c1 = new WapRoleFunctionDto()
            {
                FuncKey = Guid.NewGuid().ToString(),
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result.IsValid);
        }


        [TestCase(0)]
        [TestCase(-1)]
        [TestCategory("WapRoleFunctionDto.Validate")]
        [Description("验证关联主键为空")]
        public void Validate_RelationKey_ThrowException(int start)
        {
            
            var c1 = new WapRoleFunctionDto()
            {
                FuncKey = Guid.NewGuid().ToString(),
                RoleKey = Guid.NewGuid().ToString(),
                RelationKey = "123123"
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result.IsValid);
        }
    }
}
