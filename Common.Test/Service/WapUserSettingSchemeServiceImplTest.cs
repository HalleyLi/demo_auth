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
using SH3H.WAP.Auth.DataAccess.Repo;
using System.Linq;
using Moq;
using Test.Tools;
using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Share;
using SH3H.WAP.DataAccess.Repo.Contact;
using SH3H.WAP.Service;
using SH3H.WAP.Contracts;
using SH3H.WAP.Model.Dto;
using SH3H.WAP.Model;
namespace Common.Test.Service
{
    public class WapUserSettingSchemeServiceImplTest
    {
        private IWapUserSettingSchemeService _userSettingSchemeService = null;
        Mock<IWapUserSettingSchemeRepository> mockUserSettingSchemeRepo = new Mock<IWapUserSettingSchemeRepository>();
        public WapUserSettingSchemeServiceImplTest()
        {
            this._userSettingSchemeService = new WapUserSettingSchemeServiceImpl(mockUserSettingSchemeRepo.Object);
        }

        WapUserSettingSchemeDto userSettingSchemeDto = new WapUserSettingSchemeDto()
        {
            SchemeId = 1,
            DataType = "string",
            DefaultValue = "5",
            MinValue = "2",
            MaxValue = "10",
            Precision = "2",
            DataLength = 3
        };

        [TestMethod]
        [TestCategory("WapUserSettingSchemeService.Add")]
        [Description("创建用户配置参数信息，正测试")]
        public void Create_UserSettingScheme_Normal_Success()
        {
            mockUserSettingSchemeRepo.Setup(p => p.Add(userSettingSchemeDto.ToModel())).Returns(userSettingSchemeDto.ToModel());
            var result = _userSettingSchemeService.Add(userSettingSchemeDto);
            var res = Utils.IsEqualEntity(result, userSettingSchemeDto);
            Assert.That(res, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapUserSettingSchemeService.Add")]
        [Description("创建用户配置参数信息，模型不能为空")]
        public void Create_UserSettingScheme_ThrowException()
        {
            Action action = () => this._userSettingSchemeService.Add(null);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapUserSettingSchemeService.Add")]
        [Description("创建用户配置参数信息，数据类型不能为空")]
        public void Create_UserSettingScheme_DataType_ThrowException()
        {
            WapUserSettingSchemeDto userSettingSchemeDto2 = new WapUserSettingSchemeDto()
            {
                SchemeId = 1,
                DefaultValue = "5",
                MinValue = "2",
                MaxValue = "10",
                Precision = "2",
                DataLength = 3
            };
            Action action = () => this._userSettingSchemeService.Add(userSettingSchemeDto2);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapUserSettingSchemeService.Remove")]
        [Description("删除用户配置参数信息，正测试")]
        public void Remove_UserSetting_Success()
        {
            int schemeId = 1;
            mockUserSettingSchemeRepo.Setup(p => p.Remove(schemeId)).Returns(true);
            var result = _userSettingSchemeService.Remove(schemeId);
            Assert.That(result, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapUserSettingSchemeService.Modify")]
        [Description("修改用户配置参数信息，正测试")]
        public void Update_UserSettingScheme_Normal_Success()
        {
            mockUserSettingSchemeRepo.Setup(p => p.Modify(userSettingSchemeDto.ToModel())).Returns(userSettingSchemeDto.ToModel());
            var result = _userSettingSchemeService.Modify(userSettingSchemeDto);
            var res = Utils.IsEqualEntity(result, userSettingSchemeDto);
            Assert.That(res, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapUserSettingSchemeService.Modify")]
        [Description("修改用户配置参数信息，模型不能为空")]
        public void Update_UserSettingScheme_ThrowException()
        {
            Action action = () => this._userSettingSchemeService.Modify(null);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapUserSettingSchemeService.Modify")]
        [Description("修改用户配置参数信息，数据类型不能为空")]
        public void Update_UserSettingScheme_DataType_ThrowException()
        {
            WapUserSettingSchemeDto userSettingSchemeDto2 = new WapUserSettingSchemeDto()
            {
                SchemeId = 1,
                DefaultValue = "5",
                MinValue = "2",
                MaxValue = "10",
                Precision = "2",
                DataLength = 3
            };
            Action action = () => this._userSettingSchemeService.Modify(userSettingSchemeDto2);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapUserSettingSchemeService.Get")]
        [Description("根据参数编号获取用户配置参数，正测试")]
        public void Get_Normal_Successs()
        {
            var result = this._userSettingSchemeService.Get(1);
            Assert.AreEqual(null, result);
        }
    }
}
