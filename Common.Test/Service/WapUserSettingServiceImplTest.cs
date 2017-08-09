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
    [TestClass]
    public class WapUserSettingServiceImplTest
    {
        private IWapUserSettingService _userSettingService = null;
        Mock<IWapUserSettingRepository> mockUserSettingRepo = new Mock<IWapUserSettingRepository>();
        public WapUserSettingServiceImplTest()
        {
            this._userSettingService = new WapUserSettingServiceImpl(mockUserSettingRepo.Object);
        }

        WapUserSettingDto userSettingDto = new WapUserSettingDto 
        {
            UserId=1,
            UserSettingCode="PRINT",
            AppIdentity="bm",
            UserSettingValue="YES",
            UserSettingId=1,
            ModifierId=1,
            CreatorId=1,
            UserSettingType="UI",
            UserSettingText="界面选择经典样式配置"
        };

        [TestMethod]
        [TestCategory("WapUserSettingService.Add")]
        [Description("创建用户配置信息，正测试")]
        public void Create_UserSetting_Normal_Success()
        {
            mockUserSettingRepo.Setup(p => p.Add(userSettingDto.ToModel())).Returns(userSettingDto.ToModel());
            var result = _userSettingService.Add(userSettingDto);
            var res = Utils.IsEqualEntity(result, userSettingDto);
            Assert.That(res, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapUserSettingService.Add")]
        [Description("创建用户配置信息，模型不能为空")]
        public void Create_UserSetting_ThrowException()
        {
            Action action = () => this._userSettingService.Add(null);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapUserSettingService.Remove")]
        [Description("删除用户配置信息，正测试")]
        public void Remove_UserSetting_Success()
        {
            int settingId = 1;
            mockUserSettingRepo.Setup(p => p.Remove(1)).Returns(true);
            var result = _userSettingService.Remove(settingId);
            Assert.That(result, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapUserSettingService.Modify")]
        [Description("修改用户配置信息，正测试")]
        public void Update_UserSetting_Normarl_Success()
        { 
            mockUserSettingRepo.Setup(p=>p.Modify(userSettingDto.ToModel())).Returns(userSettingDto.ToModel());
            var result = _userSettingService.Modify(userSettingDto);
            var res = Utils.IsEqualEntity(result, userSettingDto);
            Assert.That(res, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapUserSettingService.Modify")]
        [Description("修改用户配置信息，模型不能为空")]
        public void Update_UserSetting_ThrowException()
        {
            Action action = () => this._userSettingService.Modify(null);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action); 
        }

        [TestMethod]
        [TestCategory("WapUserSettingService.ModifyByUserIdAndCode")]
        [Description("根据用户编号和配置编码修改用户配置信息，正测试")]
        public void UpdateByUserIdAndCode_UserSetting_Normarl_Success()
        {
            mockUserSettingRepo.Setup(p => p.ModifyByUserIdAndCode(1,"PRINT",userSettingDto.ToModel())).Returns(userSettingDto.ToModel());
            var result = _userSettingService.ModifyByUserIdAndCode(1, "PRINT", userSettingDto);
            var res = Utils.IsEqualEntity(result, userSettingDto);
            Assert.That(res, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapUserSettingService.ModifyByUserIdAndCode")]
        [Description("修改用户配置信息，参数不能为空")]
        public void UpdateByUserIdAndCode_UserSetting_ThrowException()
        {
            Action action = () => this._userSettingService.ModifyByUserIdAndCode(0,"PRINT",userSettingDto);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapUserSettingService.ModifyByUserIdAndCode")]
        [Description("修改用户配置信息，参数不能为空")]
        public void UpdateByUserIdAndCode_UserSetting_CODE_ThrowException()
        {
            Action action = () => this._userSettingService.ModifyByUserIdAndCode(1, String.Empty, userSettingDto);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapUserSettingService.ModifyByUserIdAndCode")]
        [Description("修改用户配置信息，参数不能为空")]
        public void UpdateByUserIdAndCode_UserSetting_SETTINGVALUE_ThrowException()
        {
            userSettingDto.UserSettingValue = null;
            Action action = () => this._userSettingService.ModifyByUserIdAndCode(1, "PTRINT", userSettingDto);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapUserSettingService.ModifyByUserIdAndCode")]
        [Description("修改用户配置信息，参数不能为空")]
        public void UpdateByUserIdAndCode_UserSetting_MODIFIER_ThrowException()
        {
            userSettingDto.ModifierId = 0;
            Action action = () => this._userSettingService.ModifyByUserIdAndCode(1, "PTRINT", userSettingDto);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapUserSettingService.GetAll")]
        [Description("获取所有用户配置，正测试")]
        public void GetAll_Normal_Successs()
        {
            List<WapUserSetting> r = new List<WapUserSetting>();
            r.Add(userSettingDto.ToModel());
            mockUserSettingRepo.Setup(p => p.GetAll()).Returns(r);
            var result = this._userSettingService.GetAll().Select(a => a.ToModel()).ToArray();
            Assert.AreEqual(r, result);
        }

        [TestMethod]
        [TestCategory("WapUserSettingService.GetByUserSettingId")]
        [Description("根据配置编号获取用户配置参数，正测试")]
        public void GetByUserSettingId_Normal_Successs()
        {
            List<WapUserSetting> r = new List<WapUserSetting>();
            r.Add(userSettingDto.ToModel());
            mockUserSettingRepo.Setup(p=>p.GetByUserSettingId(1)).Returns(r);
            var result = this._userSettingService.GetByUserSettingId(1).Select(a=>a.ToModel()).ToArray();
            Assert.AreEqual(r, result );
        }

        [TestMethod]
        [TestCategory("WapUserSettingService.GetByUserId")]
        [Description("根据配置编号获取用户配置参数，正测试")]
        public void GetByUserId_Normal_Successs()
        {
            List<WapUserSetting> r = new List<WapUserSetting>();
            r.Add(userSettingDto.ToModel());
            mockUserSettingRepo.Setup(p => p.GetByUserId(1)).Returns(r);
            var result = this._userSettingService.GetByUserId(1).Select(a => a.ToModel()).ToArray();
            Assert.AreEqual(r, result);
        }

        [TestMethod]
        [TestCategory("WapUserSettingService.GetByUserIdAndCode")]
        [Description("根据用户编号和配置编码查询用户配置，正测试")]
        public void GetByUserIdAndCode_Normal_Successs()
        {
            List<WapUserSetting> r = new List<WapUserSetting>();
            r.Add(userSettingDto.ToModel());
            mockUserSettingRepo.Setup(p => p.GetByUserIdAndCode(1, "PRINT")).Returns(r);
            var result = this._userSettingService.GetByUserIdAndCode(1, "PRINT").Select(a => a.ToModel()).ToArray();
            Assert.AreEqual(r, result);
        }

        [TestMethod]
        [TestCategory("WapUserSettingService.GetByUserIdAndAppIdentity")]
        [Description("根据用户编号和应用标识查询用户配置，正测试")]
        public void GetByUserIdAndAppIdentity_Normal_Successs()
        {
            List<WapUserSetting> r = new List<WapUserSetting>();
            r.Add(userSettingDto.ToModel());
            mockUserSettingRepo.Setup(p => p.GetByUserIdAndAppIdentity(1, "bm")).Returns(r);
            var result = this._userSettingService.GetByUserIdAndAppIdentity(1, "bm").Select(a => a.ToModel()).ToArray();
            Assert.AreEqual(r, result);
        }
    }
}
