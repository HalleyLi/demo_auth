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
using SH3H.WAP.Auth.Core;

namespace Common.Test.Service
{
     [TestClass]
    public class WapUserServiceImplTest
    {
         private IWapUserService _userService = null;
         Mock<IAuthSeqRepository> mockSeqRepo = new Mock<IAuthSeqRepository>();
         Mock<IWapUserRepository> mockUserRepo = new Mock<IWapUserRepository>();
         Mock<IWapAuthAuditRepository> mockAuthRepo = new Mock<IWapAuthAuditRepository>();
         Mock<TicketContainer> mockTicketContainer = new Mock<TicketContainer>();
         

         public WapUserServiceImplTest()
         {
             this._userService = new WapUserServiceImpl(mockUserRepo.Object, mockSeqRepo.Object, mockAuthRepo.Object);
         }


         #region AddUser

         [TestMethod]
         [TestCategory("WapUserService.AddUser")]
         [Description("添加用户信息，正测试")]
         public void AddUser_Normal_Success()
         {
             WapUserInDto inDto = new WapUserInDto()
             {
                 UserName = "管理员",
                 JobNumber = "0000",
                 Account = "admin",
                 Password = "4a7d1ed414474e4033ac29ccb8653d9b",
                 PyCode = "GLY",
                 Active = true,
                 Comment = "",
                 Email = "",
                 IdCard = "",
                 Birthday = 1431619200000,
                 Sex = 1,
                 Phone = "",
                 Cellphone = "",
                 Address = "",
                 PostNo = "",
                 Extend = ""
             };

             WapUser model = WapUserInDto.ToModel(inDto);
             model.UserKey = "";

             WapAuthSequence seq = new WapAuthSequence()
             {
                 Sn = 1,
                 IdentityKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9"
             };

             mockSeqRepo.Setup(s => s.CreateSequence("user")).Returns(seq);
             model.UserKey = seq.IdentityKey;
             model.SortSn = seq.Sn;
             model.Id = seq.Sn;
             model.Password = string.IsNullOrEmpty(inDto.Password) ? SH3H.WAP.Share.Utils.GetMD5("0000") : SH3H.WAP.Share.Utils.GetMD5(inDto.Password);
             model.DomainAccount = string.IsNullOrEmpty(model.DomainAccount) ? string.Empty : model.DomainAccount;

             mockUserRepo.Setup(u => u.AddUser(model)).Returns(model);

             mockAuthRepo.Setup(p => p.AddAudit(It.IsAny<WapAuthAudit>())).Returns(true);

             var result = this._userService.AddUser(inDto);

             var res = Utils.IsEqualEntity(result, WapUserDto.FromModel(model));

             Assert.That(res, Is.EqualTo(true));
         }

         [TestMethod]
         [TestCategory("WapUserService.AddUser")]
         [Description("添加用户信息，用户不能为空")]
         public void AddUser_NullUser_ThrowException()
         {
             Action action = () => this._userService.AddUser(null);
             ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
         }

         #endregion         

         #region ModifyUser

         [TestMethod]
         [TestCategory("WapUserService.ModifyUser")]
         [Description("修改用户信息，正测试")]
         public void ModifyUser_Normal_Success()
         {
             WapUserInDto inDto = new WapUserInDto()
             {
                 UserName = "管理员",
                 JobNumber = "0000",
                 Account = "admin",
                 Password = "4a7d1ed414474e4033ac29ccb8653d9b",
                 PyCode = "GLY",
                 Active = true,
                 Comment = "1",
                 Email = "1",
                 IdCard = "1",
                 Birthday = 1431619200000,
                 Sex = 1,
                 Phone = "1",
                 Cellphone = "1",
                 Address = "1",
                 PostNo = "1",
                 Extend = "1"
             };
             WapUser model = WapUserInDto.ToModel(inDto);
             string userKey = "D5EE5448-DC8B-42F7-B259-A43CC29BB68D";
             model.UserKey = userKey;

             mockUserRepo.Setup(a => a.ModifyUser(userKey, model)).Returns(model);
             mockAuthRepo.Setup(p => p.AddAudit(It.IsAny<WapAuthAudit>())).Returns(true);

             var result = this._userService.ModifyUser(userKey, inDto);

             var ret = Utils.IsEqualEntity(result, WapUserDto.FromModel(model));

             Assert.That(ret, Is.EqualTo(true));
         }

         [TestMethod]
         [TestCategory("WapUserService.ModifyUser")]
         [Description("修改用户信息，用户Key不允许为空")]
         public void ModifyUser_NullUserKey_ThrowException()
         {
             Action action = () => this._userService.ModifyUser("", new WapUserInDto());
             ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
         }

         [TestMethod]
         [TestCategory("WapUserService.ModifyUser")]
         [Description("修改用户信息，用户Key不是Guid")]
         public void ModifyUser_IsNotGuid_ThrowException()
         {
             Action action = () => this._userService.ModifyUser("1234", new WapUserInDto());
             ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_TYPE_ERROR, action);
         }

         [TestMethod]
         [TestCategory("WapUserService.ModifyUser")]
         [Description("修改用户信息，用户Key不是Guid")]
         public void ModifyUser_NullUser_ThrowException()
         {
             Action action = () => this._userService.ModifyUser("D5EE5448-DC8B-42F7-B259-A43CC29BB68D", null);
             ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
         }

         #endregion

         #region GetAllUsers

         [TestMethod]
         [TestCategory("WapUserService.GetAllUsers")]
         [Description("获取所有用户信息，正测试")]
         public void GetAllUsers_Normal_Successs()
         {
             var result = this._userService.GetAllUsers();
             Assert.AreEqual(true, result == null);
         }         

         #endregion         

         #region GetUserByUserKey

         [TestMethod]
         [TestCategory("WapUserService.GetUserByUserKey")]
         [Description("获取所有用户信息，正测试")]
         public void GetUserByUserKey_Normal_Success()
         {
             WapUserDto dto = new WapUserDto()
             {
                 UserKey = "D5EE5448-DC8B-42F7-B259-A43CC29BB68D",
                 UserId = 1,
                 UserName = "管理员",
                 JobNumber = "0000",
                 Account = "admin",
                 DomainAccount = "",
                 PyCode = "GLY",
                 SortSn = 0,
                 Active = true,
                 Comment = "",
                 Phone = "",
                 Cellphone = "",
                 Email = "",
                 IdCard = "",
                 Birthday = 1431619200000,
                 Sex = 1,
                 Address = "",
                 PostNo = "",
                 Extend = ""
             };
             string userKey = "D5EE5448-DC8B-42F7-B259-A43CC29BB68D";
             WapUser model = WapUserDto.ToModel(dto);

             mockUserRepo.Setup(a => a.GetUserByUserKey(userKey)).Returns(model);

             var result = this._userService.GetUserByUserKey(userKey);

             var ret = Utils.IsEqualEntity(model, WapUserDto.ToModel(result));

             Assert.That(ret, Is.EqualTo(true));
         }

         [TestMethod]
         [TestCategory("WapUserService.GetUserByUserKey")]
         [Description("获取所有用户信息，用户Key不允许为空")]
         public void GetUserByUserKey_NullUserKey_ThrowException()
         {
             Action action = () => this._userService.GetUserByUserKey("");
             ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
         }

         [TestMethod]
         [TestCategory("WapUserService.GetUserByUserKey")]
         [Description("获取所有用户信息，用户Key不是Guid")]
         public void GetUserByUserKey_IsNotGuid_ThrowException()
         {
             Action action = () => this._userService.GetUserByUserKey("1234");
             ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_TYPE_ERROR, action);
         }

         #endregion         

         #region GetUserByUserId

         [TestMethod]
         [TestCategory("WapUserService.GetUserByUserId")]
         [Description("根据用户编号获取用户信息，正测试")]
         public void GetUserByUserId_Normal_Success()
         {
             WapUserDto dto = new WapUserDto()
             {
                 UserKey = "D5EE5448-DC8B-42F7-B259-A43CC29BB68D",
                 UserId = 1,
                 UserName = "管理员",
                 JobNumber = "0000",
                 Account = "admin",
                 DomainAccount = "",
                 PyCode = "GLY",
                 SortSn = 0,
                 Active = true,
                 Comment = "",
                 Phone = "",
                 Cellphone = "",
                 Email = "",
                 IdCard = "",
                 Birthday = 1431619200000,
                 Sex = 1,
                 Address = "",
                 PostNo = "",
                 Extend = ""
             };
             int userId = 1;
             WapUser model = WapUserDto.ToModel(dto);

             mockUserRepo.Setup(a => a.GetUserByUserId(userId)).Returns(model);

             var result = this._userService.GetUserByUserId(userId);

             var ret = Utils.IsEqualEntity(model, WapUserDto.ToModel(result));

             Assert.That(ret, Is.EqualTo(true));
         }

         [TestMethod]
         [TestCategory("WapUserService.GetUserByUserId")]
         [Description("根据用户编号获取用户信息，用户编号必须大于0")]
         public void GetUserByUserId_NullUserId_ThrowException()
         {
             Action action = () => this._userService.GetUserByUserId(0);
             ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_USER_UNKNOWN, action);
         }

         #endregion

         #region GetUserByToken

         //[TestMethod]
         //[TestCategory("WapUserService.GetUserByToken")]
         //[Description("根据token值获取用户信息，正测试")]
         public void GetUserByToken_Normal_Success()
         {
             WapUserDto dto = new WapUserDto()
             {
                 UserKey = "D5EE5448-DC8B-42F7-B259-A43CC29BB68D",
                 UserId = 1,
                 UserName = "管理员",
                 JobNumber = "0000",
                 Account = "admin",
                 DomainAccount = "",
                 PyCode = "GLY",
                 SortSn = 0,
                 Active = true,
                 Comment = "",
                 Phone = "",
                 Cellphone = "",
                 Email = "",
                 IdCard = "",
                 Birthday = 1431619200000,
                 Sex = 1,
                 Address = "",
                 PostNo = "",
                 Extend = ""
             };
             string token = "b01fc5d12f414eab9de5708473cb97848259890ee0db4a15aa126d3c62dfa7986ecd6641029ac77adb842ec4450bcce398a198cbb2fb3f0e7ccc4c047598e8d74183f28ed589bf5ff9d3ffa4d819469bb5c1a638b61";
             WapUser model = WapUserDto.ToModel(dto);

             mockTicketContainer.Setup(a => a.GetUserByTicket(token)).Returns(model);
             mockUserRepo.Setup(a => TicketContainer.Instance.GetUserByTicket(token)).Returns(model);

             var result = this._userService.GetUserByToken(token);

             var ret = Utils.IsEqualEntity(model, WapUserDto.ToModel(result));

             Assert.That(ret, Is.EqualTo(true));
         }

         [TestMethod]
         [TestCategory("WapUserService.GetUserByToken")]
         [Description("根据token值获取用户信息，Token不允许为空")]
         public void GetUserByToken_NullToken_ThrowException()
         {
             Action action = () => this._userService.GetUserByToken(null);
             ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
         }

         #endregion         

         #region GetUserByRoleKey

         [TestMethod]
         [TestCategory("WapUserService.GetUserByRoleKey")]
         [Description("根据角色KEY获取用户对象，正测试")]
         public void GetUserByRoleKey_Normal_Success()
         {
             IEnumerable<WapUser> list = new List<WapUser>()
             {
                 new WapUser()
                 {
                     UserKey = "D5EE5448-DC8B-42F7-B259-A43CC29BB68D",
                     Id = 1,
                     Name = "管理员",
                     JobNumber = "0000",
                     Account = "admin",
                     DomainAccount = "",
                     Code = "GLY",
                     SortSn = 0,
                     Active = true,
                     Comment = "",
                     Phone = "",
                     Cellphone = "",
                     Email = "",
                     IdCard = "",
                     Birthday = 1431619200000,
                     Sex = 1,
                     Address = "",
                     PostNo = "",
                     Extend = "",
                     Password=""
                 }
             }.ToArray();
             string roleKey = "9A2EF8FA-8B38-46EE-A0A6-F18E9B61FFFC";

             mockUserRepo.Setup(a => a.GetUsersByRoleKey(roleKey)).Returns(list);

             var result = this._userService.GetUsersByRoleKey(roleKey);

             Assert.IsNotNull(result);
         }

         [TestMethod]
         [TestCategory("WapUserService.GetUserByRoleKey")]
         [Description("根据角色KEY获取用户对象，角色Key不允许为空")]
         public void GetUserByRoleKey_NullRoleKey_ThrowException()
         {
             Action action = () => this._userService.GetUsersByRoleKey("");
             ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
         }

         [TestMethod]
         [TestCategory("WapUserService.GetUserByRoleKey")]
         [Description("根据角色KEY获取用户对象，角色Key不是Guid")]
         public void GetUserByRoleKey_IsNotGuid_ThrowException()
         {
             Action action = () => this._userService.GetUsersByRoleKey("1234");
             ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_TYPE_ERROR, action);
         }

         #endregion         

         #region ChangePassword

         [TestMethod]
         [TestCategory("WapUserService.ChangePassword")]
         [Description("修改用户密码，正测试")]
         public void ChangePassword_Normal_Success()
         {
             string userKey = "D5EE5448-DC8B-42F7-B259-A43CC29BB68D";
             bool reset = true;
             ChangePasswordInputDto dto = new ChangePasswordInputDto()
             {
                 NewPassword = "1111",
                 OldPassword = "0000"
             };

             mockUserRepo.Setup(a => a.UpdatePassword(userKey, reset, dto)).Returns(true);

             var result = this._userService.ChangePassword(userKey, reset, dto);

             var ret = (result == true) ? true : false;

             Assert.That(ret, Is.EqualTo(true));
         }

         [TestMethod]
         [TestCategory("WapUserService.ChangePassword")]
         [Description("修改用户密码，用户Key不允许为空")]
         public void ChangePassword_NullUserKey_ThrowException()
         {
             Action action = () => this._userService.ChangePassword("", true, new ChangePasswordInputDto());
             ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
         }

         [TestMethod]
         [TestCategory("WapUserService.ChangePassword")]
         [Description("修改用户密码，用户Key不是Guid")]
         public void ChangePassword_IsNotGuid_ThrowException()
         {
             Action action = () => this._userService.ChangePassword("1234", true, new ChangePasswordInputDto());
             ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_TYPE_ERROR, action);
         }

         [TestMethod]
         [TestCategory("WapUserService.ChangePassword")]
         [Description("修改用户密码，密码对象不允许为空")]
         public void ChangePassword_NullPswDto_ThrowException()
         {
             Action action = () => this._userService.ChangePassword("D5EE5448-DC8B-42F7-B259-A43CC29BB68D", true, null);
             ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
         }

         #endregion         

         #region UpdateUserStateByUserKey

         [TestMethod]
         [TestCategory("WapUserService.UpdateUserStateByUserKey")]
         [Description("根据userKey修改用户状态，正测试")]
         public void UpdateUserStateByUserKey_Normal_Success()
         {
             string userKey = "D5EE5448-DC8B-42F7-B259-A43CC29BB68D";
             WapUserState state = new WapUserState()
             {
                 Active = true
             };

             mockUserRepo.Setup(a => a.UpdateUserStateByUserKey(userKey, state)).Returns(true);

             var result = this._userService.UpdateUserStateByUserKey(userKey, state);

             var ret = (result == true) ? true : false;

             Assert.That(ret, Is.EqualTo(true));
         }

         [TestMethod]
         [TestCategory("WapUserService.UpdateUserStateByUserKey")]
         [Description("根据userKey修改用户状态，用户Key不允许为空")]
         public void UpdateUserStateByUserKey_NullUserKey_ThrowException()
         {
             Action action = () => this._userService.UpdateUserStateByUserKey("", new WapUserState());
             ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
         }

         [TestMethod]
         [TestCategory("WapUserService.UpdateUserStateByUserKey")]
         [Description("根据userKey修改用户状态，用户Key不是Guid")]
         public void UpdateUserStateByUserKey_IsNotGuid_ThrowException()
         {
             Action action = () => this._userService.UpdateUserStateByUserKey("1234", new WapUserState());
             ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_TYPE_ERROR, action);
         }

         [TestMethod]
         [TestCategory("WapUserService.UpdateUserStateByUserKey")]
         [Description("根据userKey修改用户状态，用户状态对象不允许为空")]
         public void UpdateUserStateByUserKey_NullState_ThrowException()
         {
             Action action = () => this._userService.UpdateUserStateByUserKey("D5EE5448-DC8B-42F7-B259-A43CC29BB68D", null);
             ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
         }

         #endregion

         #region GetUserByOrganizationKey

         [TestMethod]
         [TestCategory("WapUserService.GetUserByOrganizationKey")]
         [Description("根据userKey修改用户状态，正测试")]
         public void GetUserByOrganizationKey_Normal_Success()
         {
             string organizationKey = "CDE352BD-38F2-444D-8163-8FB15171C5B6";
             IEnumerable<WapUser> list = new List<WapUser>() 
             { 
                new WapUser()
                {
                     UserKey = "D5EE5448-DC8B-42F7-B259-A43CC29BB68D",
                     Id=1,
                     Name = "管理员",
                     JobNumber = "0000",
                     Account = "admin",
                     DomainAccount="",
                     Code = "GLY",
                     SortSn = 0,
                     Active=true,
                     Comment="",
                     Phone="",
                     Cellphone="",
                     Email="",
                     IdCard="",
                     Birthday = 1431619200000,
                     Sex=1,
                     Address="",
                     PostNo="",
                     Extend="",
                     Password=""
                }
             }.ToArray();

             mockUserRepo.Setup(a => a.SelectUserByOrganizationKey(organizationKey)).Returns(list);

             var result = this._userService.GetUserByOrganizationKey(organizationKey);

             Assert.IsNotNull(result);
         }

         [TestMethod]
         [TestCategory("WapUserService.GetUserByOrganizationKey")]
         [Description("根据userKey修改用户状态，组织Key不允许为空")]
         public void GetUserByOrganizationKey_NullOrganizationKey_ThrowException()
         {
             Action action = () => this._userService.GetUserByOrganizationKey("");
             ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
         }

         [TestMethod]
         [TestCategory("WapUserService.GetUserByOrganizationKey")]
         [Description("根据userKey修改用户状态，组织Key不是Guid")]
         public void GetUserByOrganizationKey_IsNotGuid_ThrowException()
         {
             Action action = () => this._userService.GetUserByOrganizationKey("1234");
             ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_TYPE_ERROR, action);
         }

         #endregion                 
    }
}
