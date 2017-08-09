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
using SH3H.WAP.Auth.Contracts;
using SH3H.WAP.Auth.Service;
using SH3H.WAP.Auth.DataAccess.Repo;
using System.Linq;
using Moq;
using SH3H.WAP.Auth.DataAccess.Repo.Contact;
using Test.Tools;
using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Share;
using SH3H.WAP.Auth.Core;

namespace Common.Test.Service
{
    public class WapUserInfoServiceImplTest
    {
        private IWapUserInfoService _userInfoService = null;
        Mock<IWapUserInfoRepository> mockUserInfoRepo = new Mock<IWapUserInfoRepository>();
        public WapUserInfoServiceImplTest()
        {
            this._userInfoService = new WapUserInfoServiceImpl(mockUserInfoRepo.Object);
        }
       private static WapUserInfo userInfo = new WapUserInfo
        {
            UserId = 1,
            UserName = "admin",
            FileHash = "e8d4d2fe05816dccdedd4388ddb3c90a",
            OrganizationCode = "DXYY",
            OrganizationName = "鹿城站点"
        };
        WapUserInfoDto userInfoDto = WapUserInfoDto.FromModel(userInfo);
        [TestMethod]
        [TestCategory("WapUserInfoService.GetUserInfoByUserId")]
        [Description("根据用户编号和配置编码修改用户配置信息，正测试")]
        public void UpdateByUserIdAndCode_UserSetting_Normarl_Success()
        {
            mockUserInfoRepo.Setup(p => p.GetUserInfoByUserId(1)).Returns(userInfo);
            var result = _userInfoService.GetUserInfoByUserId(1);
            var res = Utils.IsEqualEntity(result, userInfoDto);
            Assert.That(res, Is.EqualTo(true));
        }
    }
}
