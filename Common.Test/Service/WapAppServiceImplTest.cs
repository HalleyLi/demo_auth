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


namespace Common.Test.Service
{    
    [TestClass]
    public class WapAppServiceImplTest
    {
        private IWapAppService _appService = null;
        Mock<IAuthSeqRepository> mockSeqRepo = null;//new Mock<IAuthSeqRepository>();
        Mock<IWapAppRepository> mockAppRepo = null;//new Mock<IWapAppRepository>();
        Mock<IWapAuthAuditRepository> mockAuthRepo = null;//new Mock<IWapAuthAuditRepository>();

        public WapAppServiceImplTest()
        {
            mockSeqRepo = new Mock<IAuthSeqRepository>();
            mockAppRepo = new Mock<IWapAppRepository>();
            mockAuthRepo = new Mock<IWapAuthAuditRepository>();

            this._appService = new WapAppServiceImpl(mockAppRepo.Object, mockSeqRepo.Object, mockAuthRepo.Object);
        }

        #region AddApp

        [TestMethod]
        [TestCategory("WapAppService.AddApp")]
        [Description("添加应用信息，正测试")]
        public void AddApp_Normal_Success()
        {
            WapAppAddDto dto = new WapAppAddDto()
            {
                AppIdentity = "plt",
                AppName = "敏捷平台配置管理系统",
                PyCode = "MJPTPZ",
                Active = true,
                Comment = "1",
                Type = 1,
                Extend = "1"
            };

            WapApp model = WapAppAddDto.ToModel(dto);

            WapAuthSequence seq = new WapAuthSequence()
            {
                Sn = 1,
                IdentityKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9"
            };

            mockSeqRepo.Setup(s => s.CreateSequence("app")).Returns(seq);
            model.AppKey = seq.IdentityKey;
            model.SortSn = seq.Sn;

            WapApp modelIn = new WapApp()
            {
                Active = true,
                AppIdentity = "plt",
                AppKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9",
                AppName = "敏捷平台配置管理系统",
                Comment = "",
                Extend = "1",
                PyCode = "MJPTPZ",
                SortSn = 1,
                Type = 1
            };

            mockAppRepo.Setup(a => a.AddApp(model)).Returns(model);

            mockAuthRepo.Setup(p => p.AddAudit(It.IsAny<WapAuthAudit>())).Returns(true);

            var result = this._appService.AddApp(dto);

            var res = Utils.IsEqualEntity(result, WapAppDto.FromModel(model));

            Assert.That(res, Is.EqualTo(true));

            return;
        }

        [TestMethod]
        [TestCategory("WapAppService.AddApp")]
        [Description("添加应用信息，应用不能为空")]
        public void AddApp_NullApp_ThrowException()
        {
            Action action = () => this._appService.AddApp(null);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        #endregion       

        #region ModifyApp

        [TestMethod]
        [TestCategory("WapAppService.ModifyApp")]
        [Description("更新应用信息列表，正测试")]
        public void ModifyApp_Normal_Success()
        {
            string appKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9";

            WapAppUpdateDto dto = new WapAppUpdateDto()
            {
                AppIdentity = "plt",
                AppName = "敏捷平台配置管理系统",
                PyCode = "MJPTPZ",
                Active = true,
                SortSn = 11,
                Type = 1,
                Comment = "1",
                Extend = "1"
            };

            WapApp model = WapAppUpdateDto.ToModel(dto);
            model.AppKey = appKey;

            mockAppRepo.Setup(a => a.ModifyApp(appKey, model)).Returns(model);
            mockAuthRepo.Setup(p => p.AddAudit(It.IsAny<WapAuthAudit>())).Returns(true);

            var result = this._appService.ModifyApp(appKey, dto);

            var ret = Utils.IsEqualEntity(result, WapAppDto.FromModel(model));

            Assert.That(ret, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapAppService.ModifyApp")]
        [Description("更新应用信息列表，appKey不能为空")]
        public void ModifyApp_NullUserKey_ThrowException()
        {
            Action action = () => this._appService.ModifyApp("", new WapAppUpdateDto());
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapAppService.ModifyApp")]
        [Description("更新应用信息列表，app不能为空")]
        public void ModifyApp_NullApp_ThrowException()
        {
            Action action = () => this._appService.ModifyApp("", new WapAppUpdateDto());
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapAppService.ModifyApp")]
        [Description("更新应用信息列表，appKey不是Guid")]
        public void ModifyApp_IsNotGuidAppKey_ThrowException()
        {
            Action action = () => this._appService.ModifyApp("1234",new WapAppUpdateDto());
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_TYPE_ERROR, action);
        }

        #endregion
        
        #region ModifyState

        [TestMethod]
        [TestCategory("WapAppService.ModifyState")]
        [Description("更新应用状态，正测试")]
        public void ModifyState_Normal_Success()
        {
            string appKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9";

            WapAppStateDto dto = new WapAppStateDto()
            {
                Active = true
            };

            WapAppState model = WapAppStateDto.ToModel(dto);

            mockAppRepo.Setup(a => a.ModifyState(appKey, model)).Returns(true);
            mockAuthRepo.Setup(p => p.AddAudit(It.IsAny<WapAuthAudit>())).Returns(true);

            var result = this._appService.ModifyState(appKey, dto);

            var ret = Utils.IsEqualEntity(result, true);

            Assert.That(ret, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapAppService.ModifyState")]
        [Description("更新应用状态，appKey不能为空")]
        public void ModifyState_NullApp_ThrowException()
        {
            Action action = () => this._appService.ModifyState("", new WapAppStateDto());
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapAppService.ModifyState")]
        [Description("更新应用信息列表，appKey不是Guid")]
        public void ModifyState_IsNotGuidAppKey_ThrowException()
        {
            Action action = () => this._appService.ModifyState("1234", new WapAppStateDto());
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_TYPE_ERROR, action);
        }

        #endregion
       
        #region GetAllApps

        [TestMethod]
        [TestCategory("WapAppService.GetAllApps")]
        [Description("获取所有应用信息，正测试")]
        public void GetAllApps_Normal_Success()
        {
            var result = this._appService.GetAllApps();
            Assert.AreEqual(true, result == null);
        }

        #endregion

        #region SelectListByKeyword

        [TestMethod]
        [TestCategory("WapAppService.SelectListByKeyword")]
        [Description("通过搜索关键字获取应用列表，正测试")]
        public void SelectListByKeyword_Normal_Success()
        {
            string keyword = "MJPTPZ";
            IEnumerable<WapApp> list = new List<WapApp>() 
            { 
                new WapApp()
                {
                    AppKey="389B23F0-A2ED-43B0-9F76-8D997A2A57F9",
                    AppIdentity="plt",
                    AppName="敏捷平台配置管理系统",
                    PyCode="MJPTPZ",
                    SortSn=11,
                    Comment="111",
                    Type=1,
                    Extend="111",
                    Active=true
                }
            }.ToArray();

            mockAppRepo.Setup(a => a.SelectListByKeyword(keyword)).Returns(list);

            var result = this._appService.SelectListByKeyword(keyword);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("WapAppService.SelectListByKeyword")]
        [Description("通过搜索关键字获取应用列表，keyword不能为空")]
        public void SelectListByKeyword_NullApp_ThrowException()
        {
            Action action = () => this._appService.SelectListByKeyword("");
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        #endregion
        
        #region SelectAppByKey

        [TestMethod]
        [TestCategory("WapAppService.SelectAppByKey")]
        [Description("通过appKey获取应用列表，正测试")]
        public void SelectAppByKey_Normal_Success()
        {
            string appKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9";

            WapAppDto dto = new WapAppDto()
            {
                AppKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9",
                AppIdentity = "plt",
                AppName = "敏捷平台配置管理系统",
                PyCode = "MJPTPZ",
                SortSn = 11,
                Comment = "111",
                Type = 1,
                Extend = "111",
                Active = true
            };
            WapApp model = WapAppDto.ToModel(dto);
            mockAppRepo.Setup(a => a.SelectAppByKey(appKey)).Returns(model);

            var result = this._appService.SelectAppByKey(appKey);

            var ret = Utils.IsEqualEntity(result, dto);

            Assert.That(ret, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapAppService.SelectAppByKey")]
        [Description("通过appKey获取应用列表，appKey不能为空")]
        public void SelectAppByKey_NullApp_ThrowException()
        {
            Action action = () => this._appService.SelectAppByKey("");
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapAppService.SelectAppByKey")]
        [Description("通过appKey获取应用列表，appKey不是Guid")]
        public void SelectAppByKey_IsNotGuidAppKey_ThrowException()
        {
            Action action = () => this._appService.SelectAppByKey("1234");
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_TYPE_ERROR, action);
        }

        #endregion
        
        #region GetAppByUserKey

        [TestMethod]
        [TestCategory("WapAppService.GetAppByUserKey")]
        [Description("根据用户KEY获取APP列表，正测试")]
        public void GetAppByUserKey_Normal_Success()
        {
            string userKey = "D5EE5448-DC8B-42F7-B259-A43CC29BB68D";
            IEnumerable<WapApp> list = new List<WapApp>() 
            { 
                new WapApp()
                {
                    AppKey="389B23F0-A2ED-43B0-9F76-8D997A2A57F9",
                    AppIdentity="plt",
                    AppName="敏捷平台配置管理系统",
                    PyCode="MJPTPZ",
                    SortSn=11,
                    Comment="111",
                    Type=1,
                    Extend="111",
                    Active=true
                }
            }.ToArray();

            mockAppRepo.Setup(a => a.GetAppByUserKey(userKey)).Returns(list);

            var result = this._appService.GetAppByUserKey(userKey);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory("WapAppService.GetAppByUserKey")]
        [Description("根据用户KEY获取APP列表，userKey不能为空")]
        public void GetAppByUserKey_NullApp_ThrowException()
        {
            Action action = () => this._appService.GetAppByUserKey("");
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapAppService.GetAppByUserKey")]
        [Description("根据用户KEY获取APP列表，userKey不是Guid")]
        public void GetAppByUserKey_IsNotGuidUserKey_ThrowException()
        {
            Action action = () => this._appService.GetAppByUserKey("1234");
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_TYPE_ERROR, action);
        }

        #endregion
        
        #region GetRoleString

        [TestMethod]
        [TestCategory("WapAppService.GetRoleString")]
        [Description("根据应用的appKey获取指定应用的角色导出脚本，正测试")]
        public void GetRoleString_Normal_Success()
        {
            string appKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9";
            string strReturn = "\r\n123\r\n";

            mockAppRepo.Setup(a => a.GetRoleString(appKey)).Returns(strReturn);

            var result = this._appService.GetRoleString(appKey);

            var ret = string.Equals(result, strReturn);

            Assert.That(ret, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapAppService.GetRoleString")]
        [Description("根据应用的appKey获取指定应用的角色导出脚本，appKey不能为空")]
        public void GetRoleString_NullApp_ThrowException()
        {
            Action action = () => this._appService.GetRoleString("");
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapAppService.GetRoleString")]
        [Description("根据应用的appKey获取指定应用的角色导出脚本，appKey不是Guid")]
        public void GetRoleString_IsNotGuidAppKey_ThrowException()
        {
            Action action = () => this._appService.GetRoleString("1234");
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_TYPE_ERROR, action);
        }

        #endregion
       
        #region GetAppString

        [TestMethod]
        [TestCategory("WapAppService.GetAppString")]
        [Description("根据应用的appKey获取指定应用的应用导出脚本，正测试")]
        public void GetAppString_Normal_Success()
        {
            string appKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9";
            string strReturn = "\r\n123\r\n";

            mockAppRepo.Setup(a => a.GetAppString(appKey,true)).Returns(strReturn);

            var result = this._appService.GetAppString(appKey, true);

            var ret = string.Equals(result, strReturn);

            Assert.That(ret, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapAppService.GetAppString")]
        [Description("根据应用的appKey获取指定应用的应用导出脚本，appKey不能为空")]
        public void GetAppString_NullApp_ThrowException()
        {
            Action action = () => this._appService.GetAppString("", true);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapAppService.GetAppString")]
        [Description("根据应用的appKey获取指定应用的应用导出脚本，appKey不是Guid")]
        public void GetAppString_IsNotGuidAppKey_ThrowException()
        {
            Action action = () => this._appService.GetAppString("1234", true);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_TYPE_ERROR, action);
        }

        #endregion       
    }
}
