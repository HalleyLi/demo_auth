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
    public class WapFunctionServiceImplTests
    {
        private IWapFunctionService _testService;

        private string _roleKey = "45BC8832-510A-49E2-88F1-9D8176F797C2";
        private string _userKey = "D5EE5448-DC8B-42F7-B259-A43CC29BB68D";
        private string _appKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9";
        private string _appIdentity = "plt";
        private string _funcKey = "91539559-0AE9-469A-AD2D-06EA265D18E1";
        private string _funCode = "py";
        private string _funcGroupKey = "35786B97-7A94-4965-9C92-DA8ECA3894BE";
        private string _roleFuncRelationKey = "6B227AE7-6D4C-4665-A313-4230025A53A0";

        private WapFunction _wapFunction;
        private WapFunctionGroup _wapFunctionGroup;
        private WapRoleFunction _wapRoleFunction;
        private WapFuncGroupRelativeDto _wapFunctionRel;

        private IEnumerable<WapFunction> _wapFunctionDto;
        private IEnumerable<WapFunctionGroup> _wapFunctionGroupDto;
        private IEnumerable<WapRoleFunction> _wapRoleFunctionDto;
        private IEnumerable<WapFuncGroupRelativeDto> _wapFuncGroupRelativeDto;
        private IEnumerable<WapFuncRelativeDto> _wapFuncRelativeDto;
        private IEnumerable<WapFunctionAllDto> _wapFunctionAllDto;


        public WapFunctionServiceImplTests()
        {
            Mock<IWapFunctionRepository> a = new Mock<IWapFunctionRepository>();
            Mock<IWapFunctionGroupRepository> b = new Mock<IWapFunctionGroupRepository>();
            Mock<IWapRoleRepository> f = new Mock<IWapRoleRepository>();
            Mock<IWapRoleFunctionRepository> g = new Mock<IWapRoleFunctionRepository>();
            Mock<IWapFunctionAllRepository> h = new Mock<IWapFunctionAllRepository>();
            Mock<IWapFuncRelativeRepository> i = new Mock<IWapFuncRelativeRepository>();
            Mock<IWapFuncGroupRelativeRepository> j = new Mock<IWapFuncGroupRelativeRepository>();

            Mock<IWapAppRepository> c = new Mock<IWapAppRepository>();
            Mock<IAuthSeqRepository> d = new Mock<IAuthSeqRepository>();
            Mock<IWapAuthAuditRepository> e = new Mock<IWapAuthAuditRepository>();

            BuildMock(a);
            BuildMock(b);
            BuildMock(c);
            BuildMock(d);
            BuildMock(e);
            BuildMock(f);
            BuildMock(g);
            BuildMock(h);
            BuildMock(i);
            BuildMock(j);

            //初始化模型
            _testService = new WapFunctionServiceImpl(
                    a.Object,
                    b.Object,
                    c.Object,
                    d.Object,
                   e.Object,
                    f.Object,
                    g.Object,
                    h.Object,
                    i.Object,
                    j.Object
                );

        }

        #region 初始化Mock

        private void BuildMock(Mock<IWapAppRepository> mock)
        {
            WapApp app = new WapApp()
            {

            };

            IEnumerable<WapApp> result = new List<WapApp>(){
                app
            };

            mock.Setup(p => p.GetAllApps()).Returns(result);
            mock.Setup(p => p.SelectAppByKey(_appKey)).Returns(app);
            mock.Setup(p => p.SelectAppByIdentity(_appIdentity)).Returns(app);
        }

        private void BuildMock(Mock<IAuthSeqRepository> mock)
        {
            WapAuthSequence result = new WapAuthSequence() { };
            result.IdentityKey = "00000000-0000-0000-0000-000000000001";
            result.Sn = 1;

            mock.Setup(p => p.CreateSequence(It.IsAny<string>())).Returns(result);
        }

        private void BuildMock(Mock<IWapAuthAuditRepository> mock)
        {
            mock.Setup(p => p.AddAudit(It.IsAny<WapAuthAudit>())).Returns(true);
        }

        private void BuildMock(Mock<IWapFunctionRepository> mock)
        {
            _wapFunction = new WapFunction()
            {
                Active = true,
                AppKey = _appKey,
                Code = "1",
                Comment = "2",
                Extend = "3",
                FuncGroupKey = _funcGroupKey,
                Key = _funcKey,
                Name = "6",
                Pycode = "7",
                Sortsn = 8,
                TemplateKey = "9"
            };

            _wapFunctionDto = new List<WapFunction>(){
                _wapFunction
            };

            _wapFunctionRel = new WapFuncGroupRelativeDto()
            {
                Active = true,
                AppKey = _appKey,
                Code = "2",
                Comment = "3",
                Extend = "4",
                FuncGroupKey = _funcGroupKey,
                IsRelative = true,
                Key = _funcKey,
                Name = "8",
                Pycode = "9",
                Sortsn = 10,
                TemplateKey = "11"
            };
            mock.Setup(p => p.AddFunction(It.IsAny<WapFunction>())).Returns(true);
            mock.Setup(p => p.AddFunction(_wapFunction)).Returns(true);
            mock.Setup(p => p.DeleteFunction(_funcKey)).Returns(true);
            mock.Setup(p => p.GetAllFunction()).Returns(_wapFunctionDto);
            mock.Setup(p => p.GetFunction(_funcKey)).Returns(_wapFunction);
            mock.Setup(p => p.GetFunctionByCode(_funCode)).Returns(_wapFunction);
            mock.Setup(p => p.UpdateFunction(It.IsAny<WapFunction>())).Returns(true);
            mock.Setup(p => p.UpdateFunctionActive(_funcKey, It.IsAny<bool>())).Returns(true);
        }

        private void BuildMock(Mock<IWapFunctionGroupRepository> mock)
        {
            _wapFunctionGroup = new WapFunctionGroup()
            {
                FuncAppKey = _appKey,
                FuncGroupPycode = "1",
                FuncGroupActive = true,
                FuncGroupComment = "3",
                FuncGroupKey = new Guid(_funcGroupKey),
                FuncGroupSortsn = 5,
                FuncGroupName = "6",
                ParentFuncGroupKey = Guid.Empty,
                Extend = "7"
            };

            _wapFunctionGroupDto = new List<WapFunctionGroup>(){
                _wapFunctionGroup
            };

            mock.Setup(p => p.AddFunctionGroup(It.IsAny<WapFunctionGroup>())).Returns(true);
            mock.Setup(p => p.DeleteFunctionGroup(_funcGroupKey)).Returns(true);
            mock.Setup(p => p.GetAllFunctionGroup()).Returns(_wapFunctionGroupDto);
            mock.Setup(p => p.GetAllFunctionGroupByAppIdentity(_appIdentity)).Returns(_wapFunctionGroupDto);
            mock.Setup(p => p.GetAllFunctionGroupByAppKey(_appKey)).Returns(_wapFunctionGroupDto);
            mock.Setup(p => p.GetFunctionGroup(_funcGroupKey)).Returns(_wapFunctionGroup);
            mock.Setup(p => p.GetFunctionGroup(_funcGroupKey.ToLower())).Returns(_wapFunctionGroup);
            mock.Setup(p => p.UpdateFunctionGroup(It.IsAny<WapFunctionGroup>())).Returns(true);
            mock.Setup(p => p.UpdateFunctionGroupActive(_funcGroupKey, It.IsAny<bool>())).Returns(true);
        }

        private void BuildMock(Mock<IWapRoleRepository> mock)
        {
            mock.Setup(p => p.GetRoleByRoleKey(_roleKey)).Returns(new WapRole()
            {

                RoleKey = _roleKey,
                Extend = "1",
                ParentRoleKey = Guid.Empty.ToString(),
                RoleActive = true,
                RoleComment = "2",
                RoleName = "3",
                RolePycode = "4",
                RoleSortsn = 5
            });
        }

        private void BuildMock(Mock<IWapRoleFunctionRepository> mock)
        {

            _wapRoleFunction = new WapRoleFunction()
            {
                FuncKey = "1",
                RelationKey = "2",
                RoleKey = "3",
                RoleLevel = 4,
                RolePath = "5"
            };

            _wapRoleFunctionDto = new List<WapRoleFunction>()
            {
                _wapRoleFunction
            };

            IEnumerable<WapRoleFunctionDto> add = new List<WapRoleFunctionDto>();
            IEnumerable<WapRoleFunctionDto> del = new List<WapRoleFunctionDto>();

            mock.Setup(p => p.GetAllRoleFunctionByAppIdentity(_appIdentity)).Returns(_wapRoleFunctionDto);
            mock.Setup(p => p.GetAllRoleFunctionByAppKey(_appKey)).Returns(_wapRoleFunctionDto);
            mock.Setup(p => p.GetRoleFunctionByKey(_roleFuncRelationKey)).Returns(_wapRoleFunction);
            mock.Setup(p => p.UpdateRoleFunctionRelation(It.IsAny<IEnumerable<WapRoleFunctionDto>>(), It.IsAny<IEnumerable<WapRoleFunctionDto>>())).Returns(true);
        }

        private void BuildMock(Mock<IWapFunctionAllRepository> mock)
        {
            _wapFunctionAllDto = new List<WapFunctionAllDto>()
            {
                new WapFunctionAllDto(){
                        Active =true,
                        AppKey="1",
                        Code="2",
                        Comment="3",
                        Extend="4",
                        FuncGroupKey="5",
                        Key="7",
                        Name="8",
                        Pycode="9",
                        Sortsn=10,
                        TemplateKey="11",
                         AppName="12", 
                          FuncGroupName="13"
                } , 
            };

            mock.Setup(p => p.GetFunctionAlls()).Returns(_wapFunctionAllDto);
            mock.Setup(p => p.GetFunctionAllsByAppIdentity(_appIdentity)).Returns(_wapFunctionAllDto);
            mock.Setup(p => p.GetFunctionAllsByAppKey(_appKey)).Returns(_wapFunctionAllDto);
            mock.Setup(p => p.GetFunctionAllsByRoles(It.IsAny<IEnumerable<string>>())).Returns(_wapFunctionAllDto);
        }

        private void BuildMock(Mock<IWapFuncRelativeRepository> mock)
        {
            _wapFuncRelativeDto = new List<WapFuncRelativeDto>()
            {
                new WapFuncRelativeDto(){
                        Active =true,
                        AppKey="1",
                        Code="2",
                        Comment="3",
                        Extend="4",
                        FuncGroupKey="5",
                        IsRelative=true,
                        Key="7",
                        Name="8",
                        Pycode="9",
                        Sortsn=10,
                        TemplateKey="11",
                         RelationKey="12"
                } , 
            };
            IEnumerable<WapRoleFunctionDto> add = new List<WapRoleFunctionDto>();
            IEnumerable<WapRoleFunctionDto> del = new List<WapRoleFunctionDto>();


            mock.Setup(p => p.GetRoleFunction(_roleKey)).Returns(_wapFuncRelativeDto);
            mock.Setup(p => p.GetRoleFunctionByRoleKeyAndAppIdentity(_roleKey, _appIdentity)).Returns(_wapFuncRelativeDto);
            mock.Setup(p => p.GetRoleFunctionByRoleKeyAndAppKey(_roleKey, _appKey)).Returns(_wapFuncRelativeDto);
            mock.Setup(p => p.UpdateRoleFunctionRelation(It.IsAny<IEnumerable<WapRoleFunctionDto>>(), It.IsAny<IEnumerable<WapRoleFunctionDto>>())).Returns(true);
        }

        private void BuildMock(Mock<IWapFuncGroupRelativeRepository> mock)
        {
            IEnumerable<WapFuncGroupRelativeDto> add = new List<WapFuncGroupRelativeDto>();
            IEnumerable<WapFuncGroupRelativeDto> del = new List<WapFuncGroupRelativeDto>();

            _wapFuncGroupRelativeDto = new List<WapFuncGroupRelativeDto>(){
                new WapFuncGroupRelativeDto(){
                        Active =true,
                        AppKey="1",
                        Code="2",
                        Comment="3",
                        Extend="4",
                        FuncGroupKey="5",
                        IsRelative=true,
                        Key="7",
                        Name="8",
                        Pycode="9",
                        Sortsn=10,
                        TemplateKey="11"
                } , 
                new WapFuncGroupRelativeDto(){
                        Active =true,
                        AppKey="21",
                        Code="22",
                        Comment="23",
                        Extend="24",
                        FuncGroupKey="25",
                        IsRelative=true,
                        Key="27",
                        Name="28",
                        Pycode="29",
                        Sortsn=30,
                        TemplateKey="31"
                } , 
                new WapFuncGroupRelativeDto(){
                        Active =true,
                        AppKey="41",
                        Code="42",
                        Comment="43",
                        Extend="44",
                        FuncGroupKey="45",
                        IsRelative=true,
                        Key="47",
                        Name="48",
                        Pycode="49",
                        Sortsn=50,
                        TemplateKey="51"
                }
            };

            mock.Setup(p => p.UpdateFunctionGroupRelation(It.IsAny<IEnumerable<WapFuncGroupRelativeDto>>(), It.IsAny<IEnumerable<WapFuncGroupRelativeDto>>())).Returns(true);
            mock.Setup(p => p.GetFunctionGroup(_funcGroupKey)).Returns(_wapFuncGroupRelativeDto);
        }
        #endregion


        [Test]
        [TestCategory("WapFunctionServiceImpl.Add")]
        public void Add_Normal_Success()
        {

            var c1Result = _testService.Add(_wapFunctionRel);

            Assert.AreEqual(_wapFunctionRel, c1Result);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetAllFunction")]
        public void GetAllFunction_Normal_Success()
        {
            var c1Result = _testService.GetAllFunction();
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetAllFunctionByAppIdentity")]
        public void GetAllFunctionByAppIdentity_Normal_Success()
        {
            var c1Result = _testService.GetAllFunctionByAppIdentity(_appIdentity);
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetAllFunctionByAppKey")]
        public void GetAllFunctionByAppKey_Normal_Success()
        {
            var c1Result = _testService.GetAllFunctionByAppKey(_appKey);
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetAllFunctionByRoles")]
        public void GetAllFunctionByRoles_Normal_Success()
        {
            IEnumerable<string> rolekeys = new string[]{
                _roleKey
            };

            var c1Result = _testService.GetAllFunctionByRoles(rolekeys);
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetFunction")]
        public void GetFunction_Normal_Success()
        {
            var c1Result = _testService.GetFunction(_funcKey);
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(false, string.IsNullOrEmpty(c1Result.Name));
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetAllFunctions")]
        public void GetAllFunctions_Normal_Success()
        {
            var c1Result = _testService.GetAllFunctions();
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.AddFunction")]
        public void AddFunction_Normal_Success()
        {
            _wapFunction = new WapFunction()
            {
                Active = true,
                AppKey = "3457ac27-3cf6-431b-abd5-d8e9a303e754",
                Code = "visitPeople",
                FuncGroupKey = "447d4790-f937-41e0-97f9-946e0626d79b",
                Name = "我的游客",
                Pycode = "WDYK"
            };

            var c1Result = _testService.AddFunction(WapFunctionDto.FromModel(_wapFunction));

            Guid guid;
            Assert.AreEqual(true, c1Result != null);
            Assert.AreEqual(true, Guid.TryParse(c1Result.Key, out guid));
            Assert.AreEqual(true, guid != Guid.Empty);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.DeleteFunction")]
        public void DeleteFunction_Normal_Success()
        {
            var c1Result = _testService.DeleteFunction(_funcKey);

            Assert.AreEqual(true, c1Result);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.UpdateFunction")]
        public void UpdateFunction_Normal_Success()
        {
            var c1Result = _testService.UpdateFunction(WapFunctionDto.FromModel(_wapFunction));

            Guid guid;
            Assert.AreEqual(true, c1Result != null);
            Assert.AreEqual(true, Guid.TryParse(c1Result.Key, out guid));
            Assert.AreEqual(true, guid != Guid.Empty);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.ActiveFunction")]
        public void ActiveFunction_Normal_Success()
        {
            var c1Result = _testService.ActiveFunction(_funcKey);
            Assert.AreEqual(true, c1Result);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.DeactiveFunction")]
        public void DeactiveFunction_Normal_Success()
        {
            var c1Result = _testService.DeactiveFunction(_funcKey);
            Assert.AreEqual(true, c1Result);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetFunctionByCode")]
        public void GetFunctionByCode_Normal_Success()
        {
            var c1Result = _testService.GetFunctionByCode(_funCode);
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(false, string.IsNullOrEmpty(c1Result.Name));
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetRoleFunctionRelationByKey")]
        public void GetRoleFunctionRelationByKey_Normal_Success()
        {
            var c1Result = _testService.GetRoleFunctionRelationByKey(_roleFuncRelationKey);
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(false, string.IsNullOrEmpty(c1Result.RoleKey));
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetAllRoleFunctionRelationByAppKey")]
        public void GetAllRoleFunctionRelationByAppKey_Normal_Success()
        {
            var c1Result = _testService.GetAllRoleFunctionRelationByAppKey(_appKey);
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetAllRoleFunctionRelationByAppIdentity")]
        public void GetAllRoleFunctionRelationByAppIdentity_Normal_Success()
        {
            var c1Result = _testService.GetAllRoleFunctionRelationByAppIdentity(_appIdentity);
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetRoleFunction")]
        public void GetRoleFunction_Normal_Success()
        {
            var c1Result = _testService.GetRoleFunction(_roleKey);
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetRoleFunctionRelationByRoleKeyAndAppIdentity")]
        public void GetRoleFunctionRelationByRoleKeyAndAppIdentity_Normal_Success()
        {

            var c1Result = _testService.GetRoleFunctionRelationByRoleKeyAndAppIdentity(_roleKey, _appIdentity);
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetRoleFunctionRelationByRoleKeyAndAppIdentity")]
        public void GetRoleFunctionRelationByRoleKeyAndAppIdentityTest_RoleKeyNoExist()
        {

            Action action = () => _testService.GetRoleFunctionRelationByRoleKeyAndAppIdentity("00000000-0000-0000-0000-000000000001", _appIdentity);

            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_MODEL_NOT_EXIST, action);
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.GetRoleFunctionRelationByRoleKeyAndAppIdentity")]
        public void GetRoleFunctionRelationByRoleKeyAndAppIdentityTest_ErrorRoleKey()
        {

            Action action = () => _testService.GetRoleFunctionRelationByRoleKeyAndAppIdentity("1", _appIdentity);

            ExpectedExceptionAssert.Throws<WapException>(SH3H.SDK.Definition.StateCode.CODE_INVALID_ARGUMENTS, action);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetRoleFunctionRelationByRoleKeyAndAppIdentity")]
        public void GetRoleFunctionRelationByRoleKeyAndAppIdentityTest_ErrorAppIdentity()
        {
            Action action = () => _testService.GetRoleFunctionRelationByRoleKeyAndAppIdentity("1", "");
            ExpectedExceptionAssert.Throws<WapException>(SH3H.SDK.Definition. StateCode.CODE_INVALID_ARGUMENTS, action);

        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.UpdateRoleFunctionRelation")]
        public void UpdateRoleFunctionRelation_Normal_Success()
        {
            var input = new WapUpdateRoleFuncRelationDto()
            {
                AddArr = new List<WapRoleFunctionDto>()
                {

                },
                DelArr = new List<WapRoleFunctionDto>()
                {

                }
            };

            var c1Result = _testService.UpdateRoleFunctionRelation(input);

            Assert.AreEqual(input, c1Result);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetFunctionGroupRelation")]
        public void GetFunctionGroupRelation_Normal_Success()
        {
            var c1Result = _testService.GetFunctionGroupRelation(_funcGroupKey);
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);

        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.UpdateFunctionGroupRelation")]
        public void UpdateFunctionGroupRelation_Normal_Success()
        {
            var c1Result = _testService.UpdateFunctionGroupRelation(new WapUpdateFuncGroupRelationDto()
            {
                AddArr = new List<WapFuncGroupRelativeDto>()
                {

                }
                ,
                DelArr = new List<WapFuncGroupRelativeDto>()
                    {
                    }
            });

            Assert.AreEqual(true, c1Result != null);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetAllFunctionGroup")]
        public void GetAllFunctionGroup_Normal_Success()
        {
            var c1Result = _testService.GetAllFunctionGroup();
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetAllFunctionGroups")]
        public void GetAllFunctionGroups_Normal_Success()
        {
            var c1Result = _testService.GetAllFunctionGroups();
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetAllFunctionGroupsByAppIdentity")]
        public void GetAllFunctionGroupsByAppIdentity_Normal_Success()
        {
            var c1Result = _testService.GetAllFunctionGroupsByAppIdentity(_appIdentity);
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetAllFunctionGroupsByAppKey")]
        public void GetAllFunctionGroupsByAppKey_Normal_Success()
        {
            var c1Result = _testService.GetAllFunctionGroupsByAppKey(_appKey);
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetFunctionGroup")]
        public void GetFunctionGroup_Normal_Success()
        {
            var c1Result = _testService.GetFunctionGroup(_funcGroupKey);

            Guid guid;
            Assert.AreEqual(true, c1Result != null);
            Assert.AreEqual(true, Guid.TryParse(c1Result.FuncGroupKey, out guid));
            Assert.AreEqual(true, guid != Guid.Empty);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.AddFunctionGroup")]
        public void AddFunctionGroup_Normal_Success()
        {
            var c1Result = _testService.AddFunctionGroup(WapFunctionGroupDto.FromModel(_wapFunctionGroup));


            Guid guid;
            Assert.AreEqual(true, c1Result != null);
            Assert.AreEqual(true, Guid.TryParse(c1Result.FuncGroupKey, out guid));
            Assert.AreEqual(true, guid != Guid.Empty);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.DeleteFunctionGroup")]
        public void DeleteFunctionGroup_Normal_Success()
        {
            var c1Result = _testService.DeleteFunctionGroup(_funcGroupKey);
            Assert.AreEqual(true, c1Result);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.UpdateFunctionGroup")]
        public void UpdateFunctionGroup_Normal_Success()
        {
            var c1Result = _testService.UpdateFunctionGroup(WapFunctionGroupDto.FromModel(_wapFunctionGroup));

            Guid guid;
            Assert.AreEqual(true, c1Result != null);
            Assert.AreEqual(true, Guid.TryParse(c1Result.FuncGroupKey, out guid));
            Assert.AreEqual(true, guid != Guid.Empty);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.ActiveFunctionGroup")]
        public void ActiveFunctionGroup_Normal_Success()
        {
            var c1Result = _testService.ActiveFunctionGroup(_funcGroupKey);
            Assert.AreEqual(true, c1Result);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.DeactiveFunctionGroup")]
        public void DeactiveFunctionGroup_Normal_Success()
        {
            var c1Result = _testService.DeactiveFunctionGroup(_funcGroupKey);
            Assert.AreEqual(true, c1Result);
        }
    }
}
