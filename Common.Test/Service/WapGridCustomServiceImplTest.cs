using System;
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using TestCategory = NUnit.Framework.CategoryAttribute;
using System.Linq;
using Moq;
using Test.Tools;
using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Suite.Contracts;
using SH3H.WAP.Suite.Service;
using SH3H.WAP.Suite.DataAccess.Repo.Contact;
using SH3H.WAP.Suite.Model;
using SH3H.WAP.Share;
using System.Collections.Generic;
using SH3H.WAP.Suite.Model.Dto;

namespace Common.Test.Service
{
    [TestClass]
    public class WapGridCustomServiceImplTest
    {
        private IWapGridCustomService _gridService = null;
        Mock<IWapGridCustomRepository> mockGridCustomRepo = null;//new Mock<IWapGridCustomRepository>();


        public WapGridCustomServiceImplTest()
        {
            mockGridCustomRepo = new Mock<IWapGridCustomRepository>();
            this._gridService = new WapGridCustomServiceImpl(mockGridCustomRepo.Object);
        }

        #region AddOrUpdateGridCus

        [TestMethod]
        [TestCategory("WapGridCustomService.AddOrUpdateGridCus")]
        [Description("添加或更新定制表格信息，正测试")]
        public void AddOrUpdateGridCus_Normal_Success()
        {
            WapGridCustomDto modelDto = new WapGridCustomDto()
            {
                Id =100,
                UserId=100,
                CusJson="",                
                GridTempId = "GD2016000003",
                Active=true
            };
            WapGridCustom model = modelDto.ToModel();
            mockGridCustomRepo.Setup(a => a.AddOrUpdateGridCus(model)).Returns(model);
            var result = this._gridService.AddOrUpdateGridCus(modelDto);
            var res = Utils.IsEqualEntity(result, WapGridCustomDto.FromModel(model));
            Assert.That(res, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapGridCustomService.AddOrUpdateGridCus")]
        [Description("添加定制表格信息，grid不能为空")]
        public void AddOrUpdateGridCus_NullGrid_ThrowException()
        {
            Action action = () => this._gridService.AddOrUpdateGridCus(null);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapGridCustomService.AddOrUpdateGridCus")]
        [Description("添加定制表格信息，UserId不能小于0")]
        public void AddOrUpdateGridCus_LimitUserId_ThrowException()
        {
            WapGridCustomDto modelDto = new WapGridCustomDto()
            {
                Id = 100,
                UserId = -1,
                CusJson = "",
                GridTempId = "GD2016000003",
                Active = true
            };
            Action action = () => this._gridService.AddOrUpdateGridCus(modelDto);
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_LIMIT_ERROR, action);
        }

        [TestMethod]
        [TestCategory("WapGridCustomService.AddOrUpdateGridCus")]
        [Description("添加定制表格信息，GridTempId不能为空")]
        public void AddOrUpdateGridCus_NullGridTempId_ThrowException()
        {
            WapGridCustomDto modelDto = new WapGridCustomDto()
            {
                Id = 100,
                UserId = 100,
                CusJson = "",
                GridTempId = "",
                Active = true
            };
            Action action = () => this._gridService.AddOrUpdateGridCus(modelDto);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        #endregion  


        #region GetGridById

        [TestMethod]
        [TestCategory("WapGridCustomService.GetGridCusByTempIdAndUserId")]
        [Description("通过用户id和模板id获取定制表格信息，正测试")]
        public void GetGridById_Normal_Success()
        {
            string gridTempId = "GD2016000003";
            int userId = 100;
            var result = this._gridService.GetGridCusByTempIdAndUserId(gridTempId, userId);
            Assert.AreEqual(true, result == null);
        }

        [TestMethod]
        [TestCategory("WapGridTemplateService.GetGridById")]
        [Description("通过id获取表格模板信息，gridTempId不能为空")]
        public void GetGridById_NullTempId_ThrowException()
        {
            string gridTempId = "";
            int userId = 100;
            Action action = () => this._gridService.GetGridCusByTempIdAndUserId(gridTempId, userId);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapGridTemplateService.GetGridById")]
        [Description("通过id获取表格模板信息，UserId不能小于0")]
        public void GetGridById_LimitUserId_ThrowException()
        {
            string gridTempId = "GD2016000003";
            int userId = -1;
            Action action = () => this._gridService.GetGridCusByTempIdAndUserId(gridTempId, userId);
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_LIMIT_ERROR, action);
        }

        #endregion

    }
}
