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
    /// 量程单元测试
    /// </summary>
    [TestClass]
    public class WapMeterRangeServiceImplTest
    {
        private IWapMeterRangeService _rangeService = null;
        Mock<IWapMeterRangeRepository> mockRangeRepo = null;//new Mock<IWapMeterRangeRepository>();

        public WapMeterRangeServiceImplTest()
        {
            mockRangeRepo = new Mock<IWapMeterRangeRepository>();
            this._rangeService = new WapMeterRangeServiceImpl(mockRangeRepo.Object);
        }

        #region AddRange

        [TestMethod]
        [TestCategory("WapMeterRangeService.AddRange")]
        [Description("添加量程信息，正测试")]
        public void AddRange_Normal_Success()
        {
            WapMeterRangeDto modelDto = new WapMeterRangeDto()
            {
                RangeId = 100,
                RangeName = "名称",
                RangeValue = 100,
                RangeVolumes =(float)1.2,
                RangeState = 1,
            };
            WapMeterRange model = modelDto.ToModel();
            mockRangeRepo.Setup(a => a.AddRange(model)).Returns(model);
            var result = this._rangeService.AddRange(modelDto);
            var res = Utils.IsEqualEntity(result, WapMeterRangeDto.FromModel(model));
            Assert.That(res, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapMeterRangeService.AddRange")]
        [Description("添加量程信息，range不能为空")]
        public void AddRange_NullRange_ThrowException()
        {
            Action action = () => this._rangeService.AddRange(null);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }




        [TestMethod]
        [TestCategory("WapMeterRangeService.AddRange")]
        [Description("添加量程信息，RangeName不能为空")]
        public void AddRange_NullRangeName_ThrowException()
        {
            WapMeterRangeDto modelDto = new WapMeterRangeDto()
            {
                RangeId = 100,
                RangeName = "",
                RangeValue = 100,
                RangeVolumes = (float)1.2,
                RangeState = 1,
            };
            Action action = () => this._rangeService.AddRange(modelDto);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        #endregion


        #region UpdateRange

        [TestMethod]
        [TestCategory("WapMeterRangeService.UpdateRange")]
        [Description("更新量程信息，正测试")]
        public void UpdateRange_Normal_Success()
        {
            int id = 100;
            WapMeterRangeDto modelDto = new WapMeterRangeDto()
            {
                RangeId = 100,
                RangeName = "名称",
                RangeValue = 100,
                RangeVolumes = (float)1.2,
                RangeState = 1,
            };
            WapMeterRange model = modelDto.ToModel();
            mockRangeRepo.Setup(a => a.UpdateRange(id,model)).Returns(model);
            var result = this._rangeService.UpdateRange(id, modelDto);
            var res = Utils.IsEqualEntity(result, WapMeterRangeDto.FromModel(model));
            Assert.That(res, Is.EqualTo(true));
        }


        [TestMethod]
        [TestCategory("WapMeterRangeService.UpdateRange")]
        [Description("更新量程信息，id不能小于0")]
        public void UpdateRange_LimitId_ThrowException()
        {
            int id = -1;
            WapMeterRangeDto modelDto = new WapMeterRangeDto()
            {
                RangeId = -1,
                RangeName = "名称",
                RangeValue = 100,
                RangeVolumes = (float)1.2,
                RangeState = 1,
            };
            Action action = () =>this._rangeService.UpdateRange(id, modelDto);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_LIMIT_ERROR, action);
        }

        [TestMethod]
        [TestCategory("WapMeterRangeService.UpdateRange")]
        [Description("更新量程信息，range不能为空")]
        public void UpdateRange_NullRange_ThrowException()
        {
            int id = 100;
            Action action = () => this._rangeService.UpdateRange(id, null);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }


        #endregion



        #region DeleteRange

        [TestMethod]
        [TestCategory("WapMeterRangeService.DeleteRange")]
        [Description("删除量程信息，正测试")]
        public void DeleteRange_Normal_Success()
        {
            int id = 100;
            mockRangeRepo.Setup(a => a.DeleteRange(id));
            var result = this._rangeService.DeleteRange(id);
            var res = Utils.IsEqualEntity(result, true);
            Assert.That(res, Is.EqualTo(true));
        }


        [TestMethod]
        [TestCategory("WapMeterRangeService.DeleteRange")]
        [Description("删除量程信息，id不能小于0")]
        public void DeleteRange_LimitId_ThrowException()
        {
            Action action = () => this._rangeService.DeleteRange(-1);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_LIMIT_ERROR, action);
        }


        #endregion

        #region SelectAllRanges

        [TestMethod]
        [TestCategory("WapMeterRangeService.SelectAllRanges")]
        [Description("获取所有量程信息，正测试")]
        public void SelectAllRanges_Normal_Success()
        {
            var result = this._rangeService.SelectAllRanges();
            Assert.AreEqual(true, result.Count<WapMeterRangeDto>() == 0);
        }

        #endregion


        #region GetRangeById

        [TestMethod]
        [TestCategory("WapMeterRangeService.GetRangeById")]
        [Description("通过id获取量程信息，正测试")]
        public void WapMeterRangeService_Normal_Success()
        {
            var result = this._rangeService.GetRangeById(100);
            Assert.AreEqual(true, result == null);
        }

        [TestMethod]
        [TestCategory("WapMeterRangeService.GetRangeById")]
        [Description("通过id获取量程信息，id不能小于0")]
        public void GetRangeById_LimitId_ThrowException()
        {
            Action action = () => this._rangeService.GetRangeById(-1);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_LIMIT_ERROR, action);
        }

        #endregion
    }
}
