using System;

using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using TestCategory = NUnit.Framework.CategoryAttribute;
using System.Collections.Generic;
using SH3H.WAP.Model.Dto;
namespace Common.Test.Dto
{
    [TestClass]
    public class WapUserSettingDtoTest
    {
        [Test]
        [TestCategory("WapUserSettingDto.Validate")]
        [Description("验证输入,正测试")]
        public void Validate_Normal_Success()
        {
            var c1 = new WapUserSettingDto()
            {
               UserSettingId=1,
               UserId=2,
               UserSettingCode="PRINT",
               UserSettingValue="YES",
               AppIdentity="bm"
            };
            var c1Result = c1.Validate();
            Assert.AreEqual(true, c1Result.IsValid);
        }

        [Test]
        [TestCase(null)]
        [TestCase("ASDFGHJKLQWERTYUIOPZZMNBVCRRTXSXXXXXCFFVSSTYYHJKKJHGGBJKJKLLLKJNJKKLKLLPPPPP")]
        [TestCategory("WapUserSettingDto.Validate")]
        [Description("配置编码为空以及长度测试")]
        public void Validate_UserSettingCode_ThrowException(string code)
        {
            var c1 = new WapUserSettingDto()
            {
                UserSettingId = 1,
                UserId = 2,
                UserSettingCode=code,
                UserSettingValue = "YES",
                AppIdentity = "bm"
            };
            var c1Result = c1.Validate();
            Assert.AreEqual(false, c1Result.IsValid);
        }

        [Test]
        [TestCategory("WapUserSettingDto.Validate")]
        [Description("配置值为空测试")]
        public void Validate_UserSettingValue_ThrowException()
        {
            var c1 = new WapUserSettingDto()
            {
                UserSettingId = 1,
                UserId = 2,
                UserSettingCode = "PRINT",
                AppIdentity = "bm"
            };
            var c1Result = c1.Validate();
            Assert.AreEqual(false, c1Result.IsValid);
        }

        [Test]
        [TestCategory("WapUserSettingDto.Validate")]
        [Description("应用标识为空测试")]
        public void Validate_AppIdentity_ThrowException()
        {
            var c1 = new WapUserSettingDto()
            {
                UserSettingId = 1,
                UserId = 2,
                UserSettingCode = "PRINT",
                UserSettingValue = "YES"
            };
            var c1Result = c1.Validate();
            Assert.AreEqual(false, c1Result.IsValid);
        }
    }
}
