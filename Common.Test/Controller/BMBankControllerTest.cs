using Microsoft.VisualStudio.TestTools.UnitTesting;
using SH3H.BM.Contracts;
using SH3H.BM.Controllers;
using SH3H.BM.Model.Dto;
using SH3H.BM.Share;
using SH3H.SDK.Definition;
using SH3H.SDK.Definition.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Test.Tools;

namespace Common.Test
{
    [TestClass]
    public class BMBankControllerTest
    {
        BMHeadOfficeDto headOfficedto = new BMHeadOfficeDto()
        {
            HeadofficeName = "测试银行6",
            HeadofficeState = 0,
            HeadofficeCode = "123",
            SubcomCode = "01,02",
            HeadofficeId = 13
        };
        BMHeadOfficeDto headOfficeFaildto = new BMHeadOfficeDto()
        {
            HeadofficeName = "测试银行6",
            HeadofficeState = 0,
            HeadofficeCode = "TT7",
            SubcomCode = "01,02",
            HeadofficeId = 13
        };
        BMHeadOfficeDto HeadOfficeArgumenNull = new BMHeadOfficeDto()
        {
            HeadofficeName = "",
            HeadofficeState = 0,
            HeadofficeCode = "",
            SubcomCode = "01,02",
            HeadofficeId = 13
        };
        BMHeadOfficeDto HeadOfficeOutOfMax = new BMHeadOfficeDto()
        {
            HeadofficeName = "1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111",
            HeadofficeState = 0,
            HeadofficeCode = "11111111111111",
            SubcomCode = "01,02",
            HeadofficeId = 13
        };
        BMHeadOfficeDto HeadOfficeHasExists = new BMHeadOfficeDto()
        {
            HeadofficeName = "111111111",
            HeadofficeState = 0,
            HeadofficeCode = "111",
            SubcomCode = "01,02",
            HeadofficeId = 13
        };
        int headOfficeId = 13;

        BMSubbranchDto subbranchDto = new BMSubbranchDto()
        {
            HeadofficeId = 13,
            ModifierId = "1",
            SubbranchName = "测试银行分行1",
            SubbranchState = 0,
            Linkman = "test",
            LinkmanAddress = "测试地址1",
            SubbranchAddress = "测试银行地址1",
            SubbranchId = 1
        };
        BMSubbranchDto subbranchFailDto = new BMSubbranchDto()
        {
            HeadofficeId = 13,
            ModifierId = "1",
            SubbranchName = "测试银行分行1",
            SubbranchState = 0,
            Linkman = "test",
            LinkmanAddress = "测试地址1",
            SubbranchAddress = "测试银行地址1"
        };
        BMSubbranchDto subbranchArgumenNull = new BMSubbranchDto()
        {
            HeadofficeId = 13,
            ModifierId = "1",
            SubbranchName = "",
            SubbranchState = 0,
            Linkman = "test",
            LinkmanAddress = "测试地址1",
            SubbranchAddress = "",
            PayNo=""
        };
        BMSubbranchDto subbranchArgumentOutOfMaxLength = new BMSubbranchDto()
        {
            HeadofficeId = 13,
            ModifierId = "1",
            SubbranchName = "111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111",
            SubbranchState = 0,
            Linkman = "test",
            LinkmanAddress = "测试地址1",
            SubbranchAddress = "",
            PayNo = ""
        };
     
        int subbranchId = 1;

        [TestMethod]
        [Description("新建总行")]
        [TestCategory("BMBankController")]
        public void TestCreateHeadOffice()
        {
            BMBankController ctrl = new BMBankController();
            var res = ctrl.CreateHeadOffice(headOfficedto).Data;
            Assert.IsTrue(res > 0);

            //错误测试  --参数不允许为空
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_ARGUMENT_NULL, () =>
            {
                var resFail = ctrl.CreateHeadOffice(HeadOfficeArgumenNull).Data;
            });
            //错误测试  --参数超长
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_STRING_OUT_OF_MAX_LENGTH, () =>
            {
                var resFail = ctrl.CreateHeadOffice(HeadOfficeOutOfMax).Data;
            });
            //错误测试  --总行编号已存在
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_KEY_HAS_EXISTED, () =>
            {
                var resFail = ctrl.CreateHeadOffice(HeadOfficeHasExists).Data;
            });
         
        }

        [TestMethod]
        [Description("编辑总行")]
        [TestCategory("BMBankController")]
        public void TestModifyHeadOffice()
        {
            BMBankController ctrl = new BMBankController();
            var res = ctrl.ModifyHeadOffice(headOfficeId, headOfficedto).Data;
            Assert.IsTrue(res);

            //错误测试  --参数不允许为空
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_ARGUMENT_NULL, () =>
            {
                var resFail = ctrl.ModifyHeadOffice((int)HeadOfficeArgumenNull.HeadofficeId, HeadOfficeArgumenNull).Data;
            });
            //错误测试  --参数超长
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_STRING_OUT_OF_MAX_LENGTH, () =>
            {
                var resFail = ctrl.ModifyHeadOffice((int)HeadOfficeOutOfMax.HeadofficeId, HeadOfficeOutOfMax).Data;
            });
            //错误测试  --总行编号已存在
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_KEY_HAS_EXISTED, () =>
            {
                var resFail = ctrl.ModifyHeadOffice((int)HeadOfficeHasExists.HeadofficeId, HeadOfficeHasExists).Data;
            });
        }

        [TestMethod]
        [Description("根据总行编码查询总行")]
        [TestCategory("BMBankController")]
        public void TestGetHeadOfficeByCode()
        {
            BMBankController ctrl = new BMBankController();
            var res = ctrl.GetHeadOfficeByCode("JT");
            Assert.IsNotNull(res);
            //错误测试  --总行编码无效
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_ARGUMENT_INVALID, () =>
            {
                var resFail = ctrl.GetHeadOfficeByCode("");
            });

        }

        [TestMethod]
        [Description("新建分行")]
        [TestCategory("BMBankController")]
        public void TestCreateSubbranch()
        {
            BMBankController ctrl = new BMBankController();
            var res = ctrl.CreateSubbranch(subbranchDto).Data;
            Assert.IsTrue(res > 0);

            //错误测试  --参数不能为空
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_ARGUMENT_NULL, () =>
            {
                var resFail = ctrl.CreateSubbranch(subbranchArgumenNull).Data;
            });
            //错误测试  --参数超长
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_STRING_OUT_OF_MAX_LENGTH, () =>
            {
                var resFail = ctrl.CreateSubbranch(subbranchArgumentOutOfMaxLength).Data;
            });
        }

        [TestMethod]
        [Description("编辑分行")]
        [TestCategory("BMBankController")]
        public void TestModifySubbranch()
        {
            BMBankController ctrl = new BMBankController();
            var res = ctrl.ModifySubbranch(subbranchId, subbranchDto).Data;
            Assert.IsTrue(res);

            //错误测试  --参数不能为空
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_ARGUMENT_NULL, () =>
            {
                var resFail = ctrl.ModifySubbranch((int)subbranchArgumenNull.SubbranchId, subbranchArgumenNull).Data;
            });
            //错误测试  --参数超长
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_STRING_OUT_OF_MAX_LENGTH, () =>
            {
                var resFail = ctrl.ModifySubbranch((int)subbranchArgumentOutOfMaxLength.SubbranchId,subbranchArgumentOutOfMaxLength).Data;
            });
        }

        [TestMethod]
        [Description("根据总行ID查询分行")]
        [TestCategory("BMBankController")]
        public void TestGetSubbranchByHeadOfficeId()
        {
            BMBankController ctrl = new BMBankController();
            var res = ctrl.GetSubbranchByHeadOfficeId(13);
            Assert.IsNotNull(res);

            //错误测试  --参数不能为空
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_ARGUMENT_INVALID, () =>
            {
                var resFail = ctrl.GetSubbranchByHeadOfficeId(-1);
            });
        }

        [TestMethod]
        [Description("正则表达式测试")]
        public void TestRegex()
        {
            Regex regSingle = new Regex(@"(^([a-z]|[A-Z]|[0-9]){1,}$)");
            Regex regMulti = new Regex(@"((([a-z]|[A-Z]|[0-9]){1,}?,([a-z]|[A-Z]|[0-9]){1,})$)");

            string data = "0!Z";
            Assert.IsTrue(regSingle.IsMatch(data) || regMulti.IsMatch(data));
        }
    }
}

