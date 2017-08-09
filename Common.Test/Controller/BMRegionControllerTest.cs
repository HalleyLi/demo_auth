using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SH3H.BM.Controllers;
using SH3H.BM.Model.Dto;
using System.Collections.Generic;
using SH3H.SDK.Definition.Exceptions;
using Test.Tools;
using SH3H.BM.Share;

namespace Common.Test
{
    /// <summary>
    /// 测试Regions控制器
    /// </summary>
    [TestClass]
    public class BMRegionControllerTest
    {
        /// <summary>
        /// 区块信息列表
        /// </summary>
        BMRegionDto[] subcomList = new BMRegionDto[] 
        {
            new BMRegionDto { RegionId = 2,RegionCode="ZZGS", RegionName="总公司", SortIndex =4, ParentId = 0 },
            new BMRegionDto { RegionId = 3,RegionCode="ZZSL", RegionName="义乌", SortIndex =3, ParentId = 2 },
            new BMRegionDto { RegionId = 4,RegionCode="ZZJD",  RegionName="嘉定11", SortIndex =2, ParentId = 2 }
        };

        /// <summary>
        /// 区块信息
        /// </summary>
        BMRegionDto reDto = new BMRegionDto()
        {
            RegionId = 6,
            RegionCode = "ZZYIW",
            RegionName = "上海",
            SortIndex = 4,
            ParentId = 2
        };

        BMRegionDto FailreDto = new BMRegionDto()
        {
            RegionId = 2,
            RegionCode = "",
            RegionName = "",
            SortIndex = 1,
            ParentId = 1
        };
        BMRegionDto FailExistCode = new BMRegionDto()
        {
            RegionId = 2,
            RegionCode = "ZZGS",
            RegionName = "",
            SortIndex = 1,
            ParentId = 1
        };

        ///// <summary>
        ///// 初始化测试, 插入测试数据
        ///// </summary>
        //[ClassInitialize]
        //public void TestInitialize() 
        //{
          
        //}

        ///// <summary>
        ///// 清理测试数据
        ///// </summary>
        //[ClassCleanup]
        //public void TestCleanUp() 
        //{ 

        //}

        /// <summary>
        /// 测试
        /// </summary>
        [TestMethod]
        [TestCategory("BMRegionController")]
        [Description("搜索区块信息")]
        public void TestGetRegions()
        {
            BMRegionController ctrl = new BMRegionController();
            var result = ctrl.GetRegions("1");
            Assert.IsNotNull(result);

            //错误测试  --参数不允许为空
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_ARGUMENT_NULL, () =>
            {
                var failResult = ctrl.GetRegions("");
            });
         
        }

        [TestMethod]
        [Description("搜索区块信息")]
        [TestCategory("BMRegionController")]
        public void TestGetRegionById()
        { 
            BMRegionController ctrl = new BMRegionController();
            var result = ctrl.GetRegionById(6);
            Assert.IsNotNull(result);

            //错误测试  --区域编号不能<=0
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_ARGUMENT_INVALID, 
                () =>
            {
                var failResult = ctrl.GetRegionById(-1);
            });
          
        }

        /// <summary>
        /// 测试创建区块信息, 包括测试区域名称重复、区块编码重复。
        /// </summary>
        [TestMethod]
        [Description("创建区块信息")]
        [TestCategory("BMRegionController")]
        public void TestCreateRegion()
        {
            BMRegionController ctrl = new BMRegionController();
            var result = ctrl.CreateRegion(reDto);
            Assert.IsNotNull(result.Data);

            //错误测试  --区块不能为空
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_ARGUMENT_NULL,
                () =>
                {
                    var failResult = ctrl.CreateRegion(new BMRegionDto());
                });
            //错误测试  --区块编码不能为空
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_ARGUMENT_NULL,
                () =>
                {
                    var failResult = ctrl.CreateRegion(FailreDto);
                });
            //错误测试  --区块名称不能为空
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_ARGUMENT_NULL,
                () =>
                {
                    var failResult = ctrl.CreateRegion(FailreDto);
                });
            //错误测试  --区块代码重复
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_ARGUMENT_NULL,
                () =>
                {
                    var failResult = ctrl.CreateRegion(FailExistCode);
                });
        }

        /// <summary>
        /// 测试修改区块信息,包括区块编码重复、
        /// </summary>
        [TestMethod]
        [Description("修改区块信息")]
        [TestCategory("BMRegionController")]
        public void TestModifyRegion()
        {
            BMRegionController ctrl = new BMRegionController();
            var result = ctrl.ModifyRegion((int)reDto.RegionId, reDto);
            Assert.IsTrue(result.Data);

            //错误测试  --区块不能为空
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_ARGUMENT_NULL,
                () =>
                {
                    var failResult = ctrl.ModifyRegion(0,new BMRegionDto());
                });
            //错误测试  --区块编码不能为空
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_ARGUMENT_NULL,
                () =>
                {
                    var failResult = ctrl.ModifyRegion((int)FailreDto.RegionId, FailreDto);
                });
            //错误测试  --区块名称不能为空
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_ARGUMENT_NULL,
                () =>
                {
                    var failResult = ctrl.ModifyRegion((int)FailreDto.RegionId, FailreDto);
                });
            //错误测试  --区块代码重复
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_ARGUMENT_NULL,
                () =>
                {
                    var failResult = ctrl.ModifyRegion((int)FailExistCode.RegionId, FailExistCode);
                });
        }

    }
}
