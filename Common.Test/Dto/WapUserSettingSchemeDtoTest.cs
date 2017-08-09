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
using SH3H.WAP.Model;
namespace Common.Test.Dto
{
    [TestClass]
    public class WapUserSettingSchemeDtoTest
    {
        [Test]
        [TestCategory("WapUserSettingSchemeDto.Validate")]
        [Description("验证输入,正测试")]
        public void Validate_Normal_Success()
        {
            var c1 = new WapUserSettingSchemeDto()
            {
                SchemeId=1,
                DataType="string",
                DefaultValue="5",
                MinValue="2",
                MaxValue="10",
                Precision="2",
                DataLength=3
            };
            var c1Result = c1.Validate();
            Assert.AreEqual(true, c1Result.IsValid);
        }

        [Test]
        [TestCategory("WapUserSettingSchemeDto.Validate")]
        [Description("配置编码为空测试")]
        public void Validate_DataType_ThrowException()
        {
            var c1 = new WapUserSettingSchemeDto()
            {
                SchemeId = 1,
                DefaultValue = "5",
                MinValue = "2",
                MaxValue = "10",
                Precision = "2",
                DataLength = 3
            };
            var c1Result = c1.Validate();
            Assert.AreEqual(false, c1Result.IsValid);
        }
    }
}
