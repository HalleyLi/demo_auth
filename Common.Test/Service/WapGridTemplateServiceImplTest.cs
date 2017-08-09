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
    public class WapGridTemplateServiceImplTest
    {
        private IWapGridTemplateService _gridService = null;
        Mock<IWapGridTemplateRepository> mockGridTemplateRepo = null;//new Mock<IWapGridTemplateRepository>();

        public WapGridTemplateServiceImplTest()
        {
            mockGridTemplateRepo = new Mock<IWapGridTemplateRepository>();
            this._gridService = new WapGridTemplateServiceImpl(mockGridTemplateRepo.Object);
        }

        #region AddGrid

        [TestMethod]
        [TestCategory("WapGridTemplateService.AddGrid")]
        [Description("添加表格模板信息，正测试")]
        public void AddGrid_Normal_Success()
        {
            WapGridTemplateDto modelDto = new WapGridTemplateDto()
            {
                Id = "",
                Name="testName",
                InitJson="[]",
                AddJson="",
                Comment="描述"

            };
            WapGridTemplate model = modelDto.ToModel();
            mockGridTemplateRepo.Setup(a => a.AddGrid(model)).Returns(model);
            var result = this._gridService.AddGrid(modelDto);
            var res = Utils.IsEqualEntity(result, WapGridTemplateDto.FromModel(model));
            Assert.That(res, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapGridTemplateService.AddGrid")]
        [Description("添加表格模板信息，表格对象不能为空")]
        public void AddGrid_NullGrid_ThrowException()
        {
            Action action = () => this._gridService.AddGrid(null);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        #endregion  

        #region UpdateGrid

        [TestMethod]
        [TestCategory("WapGridTemplateService.UpdateGrid")]
        [Description("更新表格模板信息，正测试")]
        public void UpdateGrid_Normal_Success()
        {
            string id = "GD2016000003";
            WapGridTemplateDto modelDto = new WapGridTemplateDto()
            {
                Id = "",
                Name = "testName",
                InitJson = "[]",
                AddJson = "",
                Comment = "描述"

            };
            WapGridTemplate model = modelDto.ToModel();
            mockGridTemplateRepo.Setup(a => a.UpdateGrid(id, model)).Returns(model);
            var result = this._gridService.UpdateGrid(id, modelDto);
            var res = Utils.IsEqualEntity(result, WapGridTemplateDto.FromModel(model));
            Assert.That(res, Is.EqualTo(true));
        }


        [TestMethod]
        [TestCategory("WapGridTemplateService.UpdateGrid")]
        [Description("更新表格模板信息，id不能为空")]
        public void UpdateGrid_NullId_ThrowException()
        {
            WapGridTemplateDto modelDto = new WapGridTemplateDto()
            {
                Id = "",
                Name = "testName",
                InitJson = "[]",
                AddJson = "",
                Comment = "描述"

            };
            Action action = () => this._gridService.UpdateGrid("", modelDto);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        #endregion  

        #region DeleteGrid

        [TestMethod]
        [TestCategory("WapGridTemplateService.DeleteGrid")]
        [Description("删除表格模板信息，正测试")]
        public void DeleteGrid_Normal_Success()
        {
            string id = "GD2016000003";
            mockGridTemplateRepo.Setup(a => a.DeleteGrid(id));
            var result = this._gridService.DeleteGrid(id);
            var res = Utils.IsEqualEntity(result,true);
            Assert.That(res, Is.EqualTo(true));
        }


        [TestMethod]
        [TestCategory("WapGridTemplateService.DeleteGrid")]
        [Description("删除表格模板信息，id不能为空")]
        public void DeleteGrid_NullId_ThrowException()
        {
            Action action = () => this._gridService.DeleteGrid("");
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }


        #endregion  


        #region SelectAllGrids

        [TestMethod]
        [TestCategory("WapGridTemplateService.SelectAllGrids")]
        [Description("获取所有表格模板信息，正测试")]
        public void SelectAllGrids_Normal_Success()
        {
            var result = this._gridService.SelectAllGrids();
            Assert.AreEqual(true, result.Count<WapGridTemplateDto>()==0);
        }

        #endregion

        #region GetGridById

        [TestMethod]
        [TestCategory("WapGridTemplateService.GetGridById")]
        [Description("通过id获取表格模板信息，正测试")]
        public void GetGridById_Normal_Success()
        {
            var result = this._gridService.GetGridById("GD2016000003");
            Assert.AreEqual(true, result == null);
        }

        [TestMethod]
        [TestCategory("WapGridTemplateService.GetGridById")]
        [Description("通过id获取表格模板信息，id不能为空")]
        public void GetGridById_NullId_ThrowException()
        {
            Action action = () => this._gridService.GetGridById("");
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        #endregion

        #region GetGridByCode

        [TestMethod]
        [TestCategory("WapGridTemplateService.GetGridByCode")]
        [Description("通过code获取表格模板信息，正测试")]
        public void GetGridByCode_Normal_Success()
        {
            var result = this._gridService.GetGridByCode("code");
            Assert.AreEqual(true, result == null);
        }

        [TestMethod]
        [TestCategory("WapGridTemplateService.GetGridByCode")]
        [Description("通过code获取表格模板信息，code不能为空")]
        public void GetGridByCode_NullId_ThrowException()
        {
            Action action = () => this._gridService.GetGridByCode("");
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        #endregion


    }
}
