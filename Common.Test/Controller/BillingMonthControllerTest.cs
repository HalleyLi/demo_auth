using System;
using System.Text;
using SH3H.BM.Controllers;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SH3H.BM.Contracts;
namespace Common.Test
{
    /// <summary>
    /// 测试BillingMonth控制器
    /// </summary>
    [TestClass]
    public class BillingMonthControllerTest
    {
        [TestMethod]
        [Description("测试获取账务年月")]
        [TestCategory("BMBillingMonthController")]
        public void TestGetLastedBillingMonth()
        {
            BMBillingMonthController ctrl = new BMBillingMonthController();
            Assert.IsNotNull(ctrl.GetLastedBillingMonth());
        }

        [TestMethod]
        [Description("测试获取账务年月数值")]
        [TestCategory("BMBillingMonthController")]
        public void TestGetLastedBillingMonthKey()
        {
            BMBillingMonthController ctrl = new BMBillingMonthController();
            Assert.IsNotNull(ctrl.GetLastedBillingMonthKey());
        }
    }
}
