using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SH3H.BM.Model.Dto;
using SH3H.BM.Controllers;
using Test.Tools;
using SH3H.SDK.Definition.Exceptions;
using SH3H.BM.Model.Condition;
using SH3H.BM.Share;
using SH3H.SDK.Definition;

namespace Common.Test
{
    /// <summary>
    /// 测试CompanyAccount控制器
    /// </summary>
    [TestClass]
    public class BMCompanyAccountControllerTest
    {
        BMCompanyAccountDto Dto = new BMCompanyAccountDto
        {
            CompanyAccountId = 1,
            CompanyAccountName = "苏溪-常州",
            CompanyAccountAddress = "常州工商银行城中支行",
            HeadOfficeId = 29,
            SubbranchId = 873,
            AccountNo = "355880487",
            AccountName = "浙江常州市自来水有限公司常州分公司",
            AccountAddress = "浙江常州工商银行城中支行",
            CompanyAccountState = 0,
            ModifierId = "0",
            SubComCode = "ZZGS"
        };

        BMCompanyAccountDto FailExistAccountNo = new BMCompanyAccountDto
        {
            CompanyAccountId = 2,
            CompanyAccountName = "苏溪-嘉定",
            CompanyAccountAddress = "嘉定工商银行城中支行",
            HeadOfficeId = 29,
            SubbranchId = 873,
            AccountNo = "35588050670487",
            AccountName = "浙江嘉定市自来水有限公司苏溪分公司",
            AccountAddress = "浙江嘉定工商银行城中支行",
            CompanyAccountState = 0,
            ModifierId = "0"
        };

        BMCompanyAccountCondition searchDto = new BMCompanyAccountCondition
        {
            //CompanyAccountName = "苏",
            //CompanyAccountAddress = "嘉定工商银行城中支行",
            //HeadOfficeId = 29,
            //SubBranchId = 873,
            //AccountNo = "35588010010487",
            //AccountName = "浙江嘉定市自来水有限公司苏溪分公司",
            //AccountAddress = "浙江嘉定工商银行城中支行",
            CompanyAccountState = 0,
            SubComCode = "ZZGS"
        };

        BMCompanyAccountCondition searchErrorDto = new BMCompanyAccountCondition
        {
            //CompanyAccountName = "苏",
            //CompanyAccountAddress = "嘉定工商银行城中支行",
            //HeadOfficeId = 29,
            //SubBranchId = 873,
            //AccountNo = "35588010010487",
            //AccountName = "浙江嘉定市自来水有限公司苏溪分公司",
            //AccountAddress = "浙江嘉定工商银行城中支行",
            CompanyAccountState = 0,
            SubComCode = "ZZS"

        };

        [TestMethod]
        [TestCategory("BMCompanyAccountController")]
        [Description("创建水司账户")]
        public void TestAddCompanyAccount()
        {
            BMCompanyAccountController ctrl = new BMCompanyAccountController();
            var result = ctrl.CreateCompanyAccount(Dto).Data;

            Assert.IsNotNull(result);
            Assert.IsTrue(result > 0);
         
            //错误测试  --银行账户已存在
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_KEY_HAS_EXISTED,
                () =>
                {
                    var failResult = ctrl.CreateCompanyAccount(FailExistAccountNo).Data;
                });
        }

        [TestMethod]
        [TestCategory("BMCompanyAccountController")]
        [Description("修改水司账户")]
        public void TestModifyCompanyAccount()
        {
            BMCompanyAccountController ctrl = new BMCompanyAccountController();
            Dto.ModifierId = "120";
            var result = ctrl.ModifyCompanyAccount((int)Dto.CompanyAccountId, Dto).Data;

            Assert.IsTrue(result);

            //错误测试  --银行账户已存在
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_KEY_HAS_EXISTED,
                () =>
                {
                    var failresult = ctrl.ModifyCompanyAccount((int)FailExistAccountNo.CompanyAccountId, FailExistAccountNo).Data;
                });
        }

        [TestMethod]
        [TestCategory("BMCompanyAccountController")]
        [Description("根据Id获取水司账户")]
        public void TestGetCompanyAccountnById()
        {
            BMCompanyAccountController ctrl = new BMCompanyAccountController();
            var result = ctrl.GetCompanyAccountById((int)Dto.CompanyAccountId).Data;
            Assert.IsNotNull(result);


            //错误测试  --水司账号无效
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_ARGUMENT_INVALID,
                () =>
                {
                    var failresult = ctrl.GetCompanyAccountById(-1).Data;
                });
        }

        [TestMethod]
        [TestCategory("BMCompanyAccountController")]
        [Description("分页获取水司账户")]
        public void TestGetCompanyAccountsByCondition()
        {
            BMCompanyAccountController ctrl = new BMCompanyAccountController();

            PageCondition pageCondition = new PageCondition(1, 10,
                new string[] { "COMPANYACCOUNT_ADDRESS", "HEADOFFICE_ID" }, new string[] { "desc", "asc" });

            var result = ctrl.GetCompanyAccounts(searchDto, pageCondition);
            Assert.IsNotNull(result);

            PageCondition pageErrorCondition = new PageCondition(1, 10,
               new string[] { "COMPANYACCOUNT_ADDRESS", "HEADOFFICE_ID" }, new string[] {  "asc" });
            //错误测试  --传参有误
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_ARGUMENT_INVALID,
                () =>
                {
                    var failresult = ctrl.GetCompanyAccounts(searchErrorDto, pageErrorCondition);
                });
        }

        [TestMethod]
        [TestCategory("BMCompanyAccountController")]
        [Description("搜索水司账户 模糊查询")]
        public void TestGetCompanyAccounts()
        {
            BMCompanyAccountController ctrl = new BMCompanyAccountController();
            var result = ctrl.GetCompanyAccountsBySearchText("ZZ");
            Assert.IsNotNull(result.Data);

            //错误测试  --传参有误
            ExpectedExceptionAssert.Throws<WapException>(BMStateCode.CODE_ARGUMENT_NULL,
                () =>
                {
                    var failresult = ctrl.GetCompanyAccountsBySearchText("");
                });
        }
    }
}
