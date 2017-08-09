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
using SH3H.WAP.Auth.Contracts;
using SH3H.WAP.Auth.Service;
using SH3H.WAP.Auth.DataAccess.Repo;
using System.Linq;
using Moq;
using SH3H.WAP.Auth.DataAccess.Repo.Contact;
using Test.Tools;
using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Share;

namespace Common.Test.Service
{
    [TestClass]
    public class WapRoleServiceImplTest
    {
        private IWapRoleService _testService = null;
        Mock<IWapRoleRepository> mockRoleRepo = new Mock<IWapRoleRepository>();
        Mock<IAuthSeqRepository> mockAuthSeqRepo = new Mock<IAuthSeqRepository>();
        Mock<IWapAuthAuditRepository> mockAuthAuditRepo = new Mock<IWapAuthAuditRepository>();
        public WapRoleServiceImplTest()
        {
            _testService = new WapRoleServiceImpl(mockRoleRepo.Object, mockAuthSeqRepo.Object, mockAuthAuditRepo.Object);
        }

        #region CreateRole
        [TestMethod]
        [TestCategory("WapRoleService.CreateRole")]
        [Description("创建角色信息，正测试")]
        public void CreateRole_Normal_Success()
        {
            //构造方法实体，也是期待的返回结果
            WapRoleDto roledto = new WapRoleDto()
            {
                RoleName = "平台测试管理员",
                ParentRoleKey = "B623A6BE-1822-4CD8-BBBC-AC3DB54E6165",
                RolePycode = "",
                RoleActive = true,
                RoleComment = "备注",
                Extend = "扩展"
            };
            WapAuthSequence seq = new WapAuthSequence()
            {
                IdentityKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9",
                Sn = 1
            };

            //模拟方法返回实体
            mockAuthSeqRepo.Setup(s => s.CreateSequence("role")).Returns(seq);
            roledto.RoleKey = seq.IdentityKey;
            roledto.RoleSortsn = seq.Sn;

            mockRoleRepo.Setup(p => p.CreateRole(roledto.ToModel())).Returns(roledto.ToModel());
            mockAuthAuditRepo.Setup(p => p.AddAudit(It.IsAny<WapAuthAudit>())).Returns(true);

            //运行方法
            var result = _testService.CreateRole(roledto);

            //断言
            var res = Utils.IsEqualEntity(result, roledto);

            Assert.That(res, Is.EqualTo(true));
        }
        [TestMethod]
        [TestCategory("WapRoleService.CreateRole")]
        [Description("创建角色信息，模型不能为空")]
        public void CreateRole_NullRole_ThrowException()
        {
            //传入空模型
            Action action = () => _testService.CreateRole(null);
            //判断错误码是否一致
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_NULL, action);
        }
        #endregion

        #region UpdateRole
        [TestMethod]
        [TestCategory("WapRoleService.UpdateRole")]
        [Description("修改角色信息，正测试")]
        public void UpdateRole_Normal_Success()
        {
            //构造方法参数，也是期待返回的结果
            string rolekey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9";
            WapRoleDto roledto = new WapRoleDto()
            {
                RoleKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9",
                RoleName = "角色名",
                ParentRoleKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9",
                RolePycode = "",
                RoleSortsn = 1,
                RoleActive = true,
                RoleComment = "备注",
                Extend = "扩展"
            };
            //模拟方法返回
            mockRoleRepo.Setup(p => p.UpdateRole(rolekey, roledto.ToModel())).Returns(roledto.ToModel());
            mockAuthAuditRepo.Setup(p => p.AddAudit(It.IsAny<WapAuthAudit>())).Returns(true);
            //运行方法
            var result = _testService.UpdateRole(rolekey, roledto);
            //断言
            var res = Utils.IsEqualEntity(result, roledto);

            Assert.That(res, Is.EqualTo(true));
        }
        [TestMethod]
        [TestCategory("WapRoleService.UpdateRole")]
        [Description("修改角色信息，UserKey不是Guid")]
        public void UpdateRole_IsNotGuidRoleKey_ThrowException()
        {
            //构造要使用的实体
            string rolekey = "ggggg";
            WapRoleDto roledto = new WapRoleDto()
            {
                RoleKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9",
                RoleName = "角色名",
                ParentRoleKey = "B623A6BE-1822-4CD8-BBBC-AC3DB54E6165",
                RolePycode = "",
                RoleSortsn = 1,
                RoleActive = true,
                RoleComment = "备注",
                Extend = "扩展"
            };
            //传入非Guid类型的RoleKey
            Action action = () => _testService.UpdateRole(rolekey, roledto);
            mockAuthAuditRepo.Setup(p => p.AddAudit(It.IsAny<WapAuthAudit>())).Returns(true);
            //判断
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_TYPE_ERROR, action);
        }
        [TestMethod]
        [TestCategory("WapRoleService.UpdateRole")]
        [Description("修改角色信息，模型不能为空")]
        public void UpdateRole_NullRole_ThrowException()
        {
            string rolekey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9";

            Action action = () => _testService.UpdateRole(rolekey, null);
            mockAuthAuditRepo.Setup(p => p.AddAudit(It.IsAny<WapAuthAudit>())).Returns(true);

            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_NULL, action);
        }
        #endregion

        #region UpdateRoleState
        [TestMethod]
        [TestCategory("WapRoleService.UpdateRoleState")]
        [Description("改变角色状态，正测试")]
        public void UpdateRoleState_Normal_Success()
        {
            //构造模拟方法参数
            string rolekey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9";
            WapStateDto activestate = new WapStateDto() { Active = false };

            //模拟方法返回True
            mockRoleRepo.Setup(p => p.UpdateRoleState(rolekey, activestate.Active)).Returns(true);

            //期望结果
            bool expected = true;

            //运行方法
            var result = _testService.UpdateRoleState(rolekey, activestate);

            //断言
            Assert.That(result, Is.EqualTo(expected));
        }
        [TestMethod]
        [TestCategory("WapRoleService.UpdateRoleState")]
        [Description("改变角色状态，RoleKey不是Guid类型")]
        public void UpdateRoleState_IsNotGuidRoleKey_ThrowException()
        {
            //构造模拟方法参数
            string rolekey = "hhhhkkkk";
            WapStateDto activestate = new WapStateDto() { Active = false };

            //运行方法
            Action action = () => _testService.UpdateRoleState(rolekey, activestate);

            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_TYPE_ERROR, action);
        }
        #endregion

        #region GetRoleByRoleKey
        [TestMethod]
        [TestCategory("WapRoleService.GetRoleByRoleKey")]
        [Description("得到一个角色，RoleKey不是Guid类型")]
        public void GetRoleByRoleKey_IsNotGuidRoleKey_ThrowException()
        {
            //构造模拟方法参数
            string rolekey = "hhhhkkkk";

            //运行方法
            Action action = () => _testService.GetRoleByRoleKey(rolekey);

            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_TYPE_ERROR, action);
        }
        #endregion

        #region GetRolesByUserKey
        [TestMethod]
        [TestCategory("WapRoleService.GetRolesByUserKey")]
        [Description("得到一个角色，UserKey不是Guid类型")]
        public void GetRolesByUserKey_IsNotGuidUserKey_ThrowException()
        {
            //构造模拟方法参数
            string userkey = "hhhhkkkk";

            //运行方法
            Action action = () => _testService.GetRolesByUserKey(userkey);

            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_TYPE_ERROR, action);
        }
        #endregion

        #region GetRoleByPyCode
        [TestMethod]
        [TestCategory("WapRoleService.GetRoleByPyCode")]
        [Description("得到一个角色，PyCode是Null或空字符串")]
        public void GetRoleByPyCode_NullOrWhiteSpacePyCode_ThrowException()
        {
            //构造模拟方法参数
            string rolekey = null;
            string rolekey1 = "";
            string rolekey2 = "    ";

            //运行方法
            Action action = () => _testService.GetRoleByPyCode(rolekey);
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_NULL, action);

            Action action1 = () => _testService.GetRoleByPyCode(rolekey1);
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_NULL, action1);

            Action action2 = () => _testService.GetRoleByPyCode(rolekey2);
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_NULL, action2);
        }
        #endregion

        #region UpdateParentRoleKeyByRoleKey
        [TestMethod]
        [TestCategory("WapRoleService.UpdateParentRoleKeyByRoleKey")]
        [Description("根据角色标识更新父角色标识，RoleKey不是Guid类型")]
        public void UpdateParentRoleKeyByRoleKey_IsNotGuidRoleKey_ThrowException()
        {
            //构造模拟方法参数
            string rolekey = "hhhhkkkk";
            string parentrolekey = "B623A6BE-1822-4CD8-BBBC-AC3DB54E6165";
            //运行方法
            Action action = () => _testService.UpdateParentRoleKeyByRoleKey(rolekey, parentrolekey);

            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_TYPE_ERROR, action);
        }
        [TestMethod]
        [TestCategory("WapRoleService.UpdateParentRoleKeyByRoleKey")]
        [Description("根据角色标识更新父角色标识，ParentRoleKey不是Guid类型")]
        public void UpdateParentRoleKeyByRoleKey_IsNotGuidParentRoleKey_ThrowException()
        {
            //构造模拟方法参数
            string parentrolekey = "hhhhkkkk";
            string rolekey = "B623A6BE-1822-4CD8-BBBC-AC3DB54E6165";
            //运行方法
            Action action = () => _testService.UpdateParentRoleKeyByRoleKey(rolekey, parentrolekey);

            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_TYPE_ERROR, action);
        }
        #endregion

        #region CreateOrUpdateUserRoleRelation
        [TestMethod]
        [TestCategory("WapRoleService.CreateOrUpdateUserRoleRelation")]
        [Description("创建或更新某一用户的角色关系，UserKey不是Guid类型")]
        public void CreateOrUpdateUserRoleRelation_IsNotGuidUserKey_ThrowException()
        {
            string userkey = "gdhdhdfh";

            Action action = () => _testService.CreateOrUpdateUserRoleRelation(userkey, new WapUpdateRoleRelationDto()
            {
                AddRoles = new List<string>() { "389B23F0-A2ED-43B0-9F76-8D997A2A57F9" },
                DelRoles = new List<string>() { "" }
            });

            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_TYPE_ERROR, action);
        }
        [TestMethod]
        [TestCategory("WapRoleService.CreateOrUpdateUserRoleRelation")]
        [Description("创建或更新某一用户的角色关系，WapUpdateRoleRelationDto不能为空")]
        public void CreateOrUpdateUserRoleRelation_NullWapUpdateRoleUserRelationDto_ThrowException()
        {
            string userkey = "D5EE5448-DC8B-42F7-B259-A43CC29BB68D";

            Action action = () => _testService.CreateOrUpdateUserRoleRelation(userkey, null);

            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_NULL, action);
        }
        #endregion
    }
}
