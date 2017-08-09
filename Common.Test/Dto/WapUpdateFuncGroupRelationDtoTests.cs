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
namespace Common.Test.Dto
{
    [TestClass]
    public class WapUpdateFuncGroupRelationDtoTests
    {
        [Test]
        [TestCategory("WapUpdateFuncGroupRelationDto.Validate")]
        [Description("验证输入,正测试")]
        public void Validate_Normal_Success()
        {
            var c1 = new WapUpdateFuncGroupRelationDto()
            {
                AddArr = new List<WapFuncGroupRelativeDto>() { 
                    new WapFuncGroupRelativeDto(){
                        Name = "测试1",
                        Code = "111",
                        Pycode = "1111",
                        Comment = "11111"
                    },
                    
                    new WapFuncGroupRelativeDto(){
                        Name = "测试1",
                        Code = "222",
                        Pycode = "2222",
                        Comment = "22222"
                    }
                },
                DelArr = new List<WapFuncGroupRelativeDto>() { 
                    new WapFuncGroupRelativeDto(){
                        Name = "测试3",
                        Code = "333",
                        Pycode = "3333",
                        Comment = "33333"
                    },
                    
                    new WapFuncGroupRelativeDto(){
                        Name = "测试4",
                        Code = "444",
                        Pycode = "4444",
                        Comment = "44444"
                    }
                }
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(true, c1Result.IsValid);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCategory("WapRoleFunctionDto.Validate")]
        [Description("新增对象错误")]
        public void Validate_AddArr_ThrowException(int start)
        {

            var c1 = new WapUpdateFuncGroupRelationDto()
            {
                AddArr = new List<WapFuncGroupRelativeDto>() { 
                    new WapFuncGroupRelativeDto(){
                        Name = "测试1",
                        Code = "111",
                        Comment = "11111"
                    },
                    
                    new WapFuncGroupRelativeDto(){
                        Name = "测试1",
                        Code = "222",
                        Pycode = "2222",
                        Comment = "22222"
                    }
                },
                DelArr = new List<WapFuncGroupRelativeDto>() { 
                    new WapFuncGroupRelativeDto(){
                        Name = "测试3",
                        Code = "333",
                        Pycode = "3333",
                        Comment = "33333"
                    },
                    
                    new WapFuncGroupRelativeDto(){
                        Name = "测试4",
                        Code = "444",
                        Pycode = "4444",
                        Comment = "44444"
                    }
                }
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result.IsValid);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCategory("WapRoleFunctionDto.Validate")]
        [Description("删除对象错误")]
        public void Validate_DelArr_ThrowException(int start)
        {

            var c1 = new WapUpdateFuncGroupRelationDto()
            {
                AddArr = new List<WapFuncGroupRelativeDto>() { 
                    new WapFuncGroupRelativeDto(){
                        Name = "测试1",
                        Code = "111",
                        Pycode = "1111",
                        Comment = "11111"
                    },
                    
                    new WapFuncGroupRelativeDto(){
                        Name = "测试1",
                        Code = "222",
                        Pycode = "2222",
                        Comment = "22222"
                    }
                },
                DelArr = new List<WapFuncGroupRelativeDto>() { 
                    new WapFuncGroupRelativeDto(){
                        Name = "测试3",
                        Code = "333",
                        Pycode = "3333",
                        Comment = "33333"
                    },
                    
                    new WapFuncGroupRelativeDto(){
                        Code = "444",
                        Pycode = "4444",
                        Comment = "44444"
                    }
                }
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result.IsValid);
        }
    }
}
