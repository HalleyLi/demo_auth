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
    public class WapUpdateRoleFuncRelationDtoTest
    {
        [Test]
        [TestCategory("WapUpdateRoleFuncRelationDto.Validate")]
        [Description("验证输入,正测试")]
        public void Validate_Normal_Success()
        {
            WapUpdateRoleFuncRelationDto c1 = new WapUpdateRoleFuncRelationDto()
            {

                AddArr = new List<WapRoleFunctionDto>() { 
                    new WapRoleFunctionDto(){
                        FuncKey = Guid.NewGuid().ToString(),
                        RoleKey = Guid.NewGuid().ToString()
                    },
                    
                    new WapRoleFunctionDto(){
                        FuncKey = Guid.NewGuid().ToString(),
                        RoleKey = Guid.NewGuid().ToString()
                    }
                },
                DelArr = new List<WapRoleFunctionDto>() { 
                    new WapRoleFunctionDto(){
                        FuncKey = Guid.NewGuid().ToString(),
                        RoleKey = Guid.NewGuid().ToString()
                    },
                    
                    new WapRoleFunctionDto(){
                       FuncKey = Guid.NewGuid().ToString(),
                        RoleKey = Guid.NewGuid().ToString()
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

            var c1 = new WapUpdateRoleFuncRelationDto()
            {
                AddArr = new List<WapRoleFunctionDto>() { 
                    new WapRoleFunctionDto(){
                         FuncKey = Guid.NewGuid().ToString(),
                        RoleKey = Guid.NewGuid().ToString()
                    },
                    
                    new WapRoleFunctionDto(){
                        RoleKey = Guid.NewGuid().ToString()
                    }
                },
                DelArr = new List<WapRoleFunctionDto>() { 
                    new WapRoleFunctionDto(){
                        FuncKey = Guid.NewGuid().ToString(),
                        RoleKey = Guid.NewGuid().ToString()
                    },
                    
                    new WapRoleFunctionDto(){
                        FuncKey = Guid.NewGuid().ToString(),
                RoleKey = Guid.NewGuid().ToString()
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

            var c1 = new WapUpdateRoleFuncRelationDto()
            {
                AddArr = new List<WapRoleFunctionDto>() { 
                    new WapRoleFunctionDto(){
                        FuncKey = Guid.NewGuid().ToString(),
                        RoleKey = Guid.NewGuid().ToString()
                    },
                    
                    new WapRoleFunctionDto(){
                        FuncKey = Guid.NewGuid().ToString(),
                        RoleKey = Guid.NewGuid().ToString()
                    }
                },
                DelArr = new List<WapRoleFunctionDto>() { 
                    new WapRoleFunctionDto(){
                        RoleKey = Guid.NewGuid().ToString()
                    },
                    
                    new WapRoleFunctionDto(){
                        FuncKey = Guid.NewGuid().ToString(),
                        RoleKey = Guid.NewGuid().ToString()
                    }
                }
            };

            var c1Result = c1.Validate();

            Assert.AreEqual(false, c1Result.IsValid);
        }
    }
}
