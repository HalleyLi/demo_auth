using System;
using SH3H.BM.Model.Dto;

using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using TestCategory = NUnit.Framework.CategoryAttribute;

namespace Common.Test
{
    [TestClass]
    public class PageConditionTests
    {
        [Test]
        [TestCategory("PageCondition.Validate")]
        [Description("验证输入,正测试")]
        public void Validate_Normal_Success()
        {
            var c1 = new PageCondition(1, 11,
                new string[] {
                    PageCondition.DirectionWord.Desc.ToString(),
                    PageCondition.DirectionWord.Asc.ToString()
                },
                new string[] {
                    "AccountNo",
                    "CardId" 
                });

            var c1Result = c1.Validate();

            Assert.AreEqual(true, c1Result);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCategory("PageCondition.Validate")]
        [Description("验证输入,起始索引输入<=0")]
        public void Validate_OutOfRangeStart_ThrowException(int start)
        {
            // 开始索引等于0
            var c1 = new PageCondition(0, 10);

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result);
        }

        [TestCase(0)]
        [TestCase(5001)]
        [TestCategory("PageCondition.Validate")]
        [Description("验证输入, 长度不在1-5000之间")]
        public void Validate_OutOfRangeLength_ThrowException(int length)
        {
            // 获取记录数大于5000
            var c1 = new PageCondition(1, length);

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result);
        }

        [Test, Pairwise]
        [TestCategory("PageCondition.Validate")]
        [Description("验证输入,排序列和方向的长度不相等")]
        public void Validate_QuantityNotEqualOfSortFieldAndDirection_ThrowException
            ([Values(null,new string[]{"Desc"})]string[] direction, 
             [Values(new string[]{"Name","Account"},new string[]{})]string[] field)
        {
            var c1 = new PageCondition(1, 11, field, direction);
            var c1Result = c1.Validate();
            Assert.AreEqual(false, c1Result);
        }

    }
}
