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
using SH3H.WAP.Contracts;
using SH3H.WAP.DataAccess.Repo.Contact;
using SH3H.WAP.Model;
using SH3H.WAP.Model.Dto;
using SH3H.WAP.Service;
using SH3H.WAP.Share;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Test.Service
{
    /// <summary>
    /// 测试标签相关
    /// </summary>
    [TestClass]
    public class WapTagServiceImplTest
    {
        private IWapTagService _tagService = null;
        Mock<IWapTagRepository> mockTagRepo = null;//new Mock<IWapTagRepository>();

        public WapTagServiceImplTest()
        {
            mockTagRepo = new Mock<IWapTagRepository>();
            this._tagService = new WapTagServiceImpl(mockTagRepo.Object);
        }


        #region AddTag

        [TestMethod]
        [TestCategory("WapTagService.AddTag")]
        [Description("添加标签信息，正测试")]
        public void AddTag_Normal_Success()
        {
            WapTagDto modelDto = new WapTagDto()
            {
                TagId = 100,
                TagName = "标签名称测试",
                TagGroupCode = "groupcode",
                AppIdentity = "AppIdentity",
                UserId = 1,
                Color = "color",
                ReferenceCount =1,
                Comment = "",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };
            WapTag model = modelDto.ToModel();
            mockTagRepo.Setup(a => a.AddTag(model)).Returns(model);
            var result = this._tagService.AddTag(modelDto);
            var res = Utils.IsEqualEntity(result, WapTagDto.FromModel(model));
            Assert.That(res, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapTagService.AddTag")]
        [Description("添加标签信息，tag不能为空")]
        public void AddTag_NullTag_ThrowException()
        {
            Action action = () => this._tagService.AddTag(null);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapTagService.AddTag")]
        [Description("添加标签信息，TagName不能有空格")]
        public void AddTag_LimitTagName_ThrowException()
        {
            WapTagDto modelDto = new WapTagDto()
            {
                TagId = 100,
                TagName = "标签 名称测试",
                TagGroupCode = "groupcode",
                AppIdentity = "AppIdentity",
                UserId = 1,
                Color = "color",
                ReferenceCount = 1,
                Comment = "",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };
            Action action = () => this._tagService.AddTag(modelDto);
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_LIMIT_ERROR, action);
        }


        [TestMethod]
        [TestCategory("WapTagService.AddTag")]
        [Description("添加标签信息，UserId不能小于0")]
        public void AddTag_LimitUserId_ThrowException()
        {
            WapTagDto modelDto = new WapTagDto()
            {
                TagId = 100,
                TagName = "标签名称测试",
                TagGroupCode = "groupcode",
                AppIdentity = "AppIdentity",
                UserId = -1,
                Color = "color",
                ReferenceCount = 1,
                Comment = "",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };
            Action action = () => this._tagService.AddTag(modelDto);
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_LIMIT_ERROR, action);
        }

        [TestMethod]
        [TestCategory("WapTagService.AddTag")]
        [Description("添加标签信息，TagName不能为空")]
        public void AddTag_NullTagName_ThrowException()
        {
            WapTagDto modelDto = new WapTagDto()
            {
                TagId = 100,
                TagName = "",
                TagGroupCode = "groupcode",
                AppIdentity = "AppIdentity",
                UserId = 1,
                Color = "color",
                ReferenceCount = 1,
                Comment = "",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };
            Action action = () => this._tagService.AddTag(modelDto);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapTagService.AddTag")]
        [Description("添加标签信息，TagGroupCode不能为空")]
        public void AddTag_NullTagGroupCode_ThrowException()
        {
            WapTagDto modelDto = new WapTagDto()
            {
                TagId = 100,
                TagName = "标签名称测试",
                TagGroupCode = "",
                AppIdentity = "AppIdentity",
                UserId = 1,
                Color = "color",
                ReferenceCount = 1,
                Comment = "",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };
            Action action = () => this._tagService.AddTag(modelDto);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapTagService.AddTag")]
        [Description("添加标签信息，AppIdentity不能为空")]
        public void AddTag_NullAppIdentity_ThrowException()
        {
            WapTagDto modelDto = new WapTagDto()
            {
                TagId = 100,
                TagName = "标签名称测试",
                TagGroupCode = "TagGroupCode",
                AppIdentity = "",
                UserId = 1,
                Color = "color",
                ReferenceCount = 1,
                Comment = "",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };
            Action action = () => this._tagService.AddTag(modelDto);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        #endregion


        #region UpdateTag

        [TestMethod]
        [TestCategory("WapTagService.UpdateTag")]
        [Description("更新标签信息，正测试")]
        public void UpdateTag_Normal_Success()
        {
            int id = 100;
            WapTagDto modelDto = new WapTagDto()
            {
                TagId = 100,
                TagName = "标签名称测试",
                TagGroupCode = "TagGroupCode",
                AppIdentity = "AppIdentity",
                UserId = 1,
                Color = "color",
                ReferenceCount = 1,
                Comment = "",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now

            };
            WapTag model = modelDto.ToModel();
            mockTagRepo.Setup(a => a.UpdateTag(id, model)).Returns(model);
            var result = this._tagService.UpdateTag(id, modelDto);
            var res = Utils.IsEqualEntity(result, WapTagDto.FromModel(model));
            Assert.That(res, Is.EqualTo(true));
        }


        [TestMethod]
        [TestCategory("WapTagService.UpdateTag")]
        [Description("更新标签信息，id不能小于0")]
        public void UpdateTag_LimitId_ThrowException()
        {
            int id = -1;
            WapTagDto modelDto = new WapTagDto()
            {
                TagId = 100,
                TagName = "标签名称测试",
                TagGroupCode = "TagGroupCode",
                AppIdentity = "AppIdentity",
                UserId = 1,
                Color = "color",
                ReferenceCount = 1,
                Comment = "",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now

            };
            Action action = () => this._tagService.UpdateTag(id, modelDto);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_LIMIT_ERROR, action);
        }

        [TestMethod]
        [TestCategory("WapTagService.UpdateTag")]
        [Description("更新标签信息，tag不能为空")]
        public void UpdateTag_NullTag_ThrowException()
        {
            int id = 100;
            Action action = () => this._tagService.UpdateTag(id, null);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }


        #endregion



        #region DeleteTag

        [TestMethod]
        [TestCategory("WapTagService.DeleteTag")]
        [Description("删除标签信息，正测试")]
        public void DeleteTag_Normal_Success()
        {
            int id = 100;
            mockTagRepo.Setup(a => a.DeleteTag(id));
            var result = this._tagService.DeleteTag(id);
            var res = Utils.IsEqualEntity(result, true);
            Assert.That(res, Is.EqualTo(true));
        }


        [TestMethod]
        [TestCategory("WapTagService.DeleteTag")]
        [Description("删除标签信息，id不能小于0")]
        public void DeleteTag_LimitId_ThrowException()
        {
            Action action = () => this._tagService.DeleteTag(-1);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_LIMIT_ERROR, action);
        }


        #endregion


        #region SelectTagsByAppCode

        [TestMethod]
        [TestCategory("WapTagService.SelectTagsByAppCode")]
        [Description("根据应用标识获取所有标签信息，正测试")]
        public void SelectAllTags_Normal_Success()
        {
            var result = this._tagService.SelectTagsByAppCode("appIdentity");
            Assert.AreEqual(true, result.Count<WapTagDto>() == 0);
        }

        #endregion


        #region GetTagById

        [TestMethod]
        [TestCategory("WapTagService.GetTagById")]
        [Description("通过id获取标签信息，正测试")]
        public void GetTagById_Normal_Success()
        {
            var result = this._tagService.GetTagById(100);
            Assert.AreEqual(true, result == null);
        }

        [TestMethod]
        [TestCategory("WapTagService.GetTagById")]
        [Description("通过id获取标签信息，id不能小于0")]
        public void GetTagById_LimitId_ThrowException()
        {
            Action action = () => this._tagService.GetTagById(-1);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_LIMIT_ERROR, action);
        }

        #endregion


        #region SelectTagsByTagGroupCode

        [TestMethod]
        [TestCategory("WapTagService.SelectTagsByTagGroupCode")]
        [Description("通过标签组code获取标签列表，正测试")]
        public void SelectTagsByTagGroupCode_Normal_Success()
        {
            var result = this._tagService.SelectTagsByTagGroupCode("appIdentity","code");
            Assert.AreEqual(true, result.Count<WapTagDto>() == 0);
        }

        [TestMethod]
        [TestCategory("WapTagService.SelectTagsByTagGroupCode")]
        [Description("通过标签组code获取标签列表，code不能为空")]
        public void SelectTagsByTagGroupCode_NullCode_ThrowException()
        {
            Action action = () => this._tagService.SelectTagsByTagGroupCode("appIdentity", "");
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        #endregion





    }
}
