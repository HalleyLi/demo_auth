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
using SH3H.WAP.Auth.DataAccess.Repo.Contact;
using Moq;
namespace Common.Test.Service
{
    [TestClass]
    public class WapMenuServiceImplTests
    {
        private WapMenuServiceImpl _testService;

        private WapMenu _model;
        private IEnumerable<WapMenu> _models;

        public WapMenuServiceImplTests()
        {
            Mock<IWapMenuRepository> a = new Mock<IWapMenuRepository>();
            Mock<IAuthSeqRepository> b = new Mock<IAuthSeqRepository>();
            Mock<IWapAuthAuditRepository> c = new Mock<IWapAuthAuditRepository>();

            BuildMock(a);
            BuildMock(b);
            BuildMock(c);

            //初始化模型
            _testService = new WapMenuServiceImpl(
                    a.Object,
                    b.Object,
                    c.Object
                );

        }

        private void BuildMock(Mock<IWapAuthAuditRepository> mock)
        {
            mock.Setup(p => p.AddAudit(It.IsAny<WapAuthAudit>())).Returns(true);
        }

        private void BuildMock(Mock<IAuthSeqRepository> mock)
        {
            WapAuthSequence result = new WapAuthSequence() { };
            result.IdentityKey = "00000000-0000-0000-0000-000000000001";
            result.Sn = 1;

            mock.Setup(p => p.CreateSequence(It.IsAny<string>())).Returns(result);
        }

        private void BuildMock(Mock<IWapMenuRepository> mock)
        {
              _model = new WapMenu()
            {
                 Name = "1",
                Url = "2",
                ComponentId = "3",
                Css = "4",
                ParentKey = Guid.Empty.ToString(),
                MenuKey = Guid.NewGuid().ToString(),
                 FunKey = Guid.NewGuid().ToString(),
                Comment="1"
            };

            _models=new List<WapMenu>(){_model};

            mock.Setup(p => p.ActiveMenu(It.IsAny<string>())).Returns(true);
            mock.Setup(p => p.AddMenu(It.IsAny<WapMenu>())).Returns(true);
            mock.Setup(p => p.DeactiveMenu(It.IsAny<string>())).Returns(true);
            mock.Setup(p => p.DeleteMenu(It.IsAny<string>())).Returns(true);
            mock.Setup(p => p.GetFuncMenu(It.IsAny<string>())).Returns(_models);
            mock.Setup(p => p.GetMenu(It.IsAny<string>())).Returns(_model);
            mock.Setup(p => p.GetMenuByUrl(It.IsAny<string>(), It.IsAny<string>())).Returns(_models);
            mock.Setup(p => p.GetMenuList(It.IsAny<string[]>())).Returns(_models);
            mock.Setup(p => p.GetMenusByUA(It.IsAny<string>(), It.IsAny<string>())).Returns(_models);
            mock.Setup(p => p.GetUserMenu(It.IsAny<string>(), It.IsAny<string>())).Returns(_models);
            mock.Setup(p => p.SelectAllAppMenus()).Returns(_models);
            mock.Setup(p => p.SelectAppMenus(It.IsAny<string>())).Returns(_models);
            mock.Setup(p => p.SelectAppMenusByAppId(It.IsAny<string>())).Returns(_models);
            mock.Setup(p => p.UpdateMenu(It.IsAny<string>(),It.IsAny<WapMenu>())).Returns(true);
            mock.Setup(p => p.UpdateMenuParent(It.IsAny<string>(),It.IsAny<string>())).Returns(true);
            mock.Setup(p => p.UpdateMenuState(It.IsAny<string>(),It.IsAny<bool>())).Returns(true);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.SelectAllAppMenus")]
        public void SelectAllAppMenusTest()
        {
            var c1Result = _testService.SelectAllAppMenus();
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetMenu")]
        public void GetMenuTest()
        {
            var c1Result = _testService.GetMenu(Guid.NewGuid().ToString());
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(false, string.IsNullOrEmpty(c1Result.MenuName));
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetMenu")]
        public void GetMenuTest2()
        {
            var c1Result = _testService.GetMenu(new WapMenuDto()
            {
                MenuName = "1"
            });
            Assert.AreEqual(false, c1Result == null);
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.GetUserMenu")]
        public void GetUserMenuTest()
        {
            var c1Result = _testService.GetUserMenu(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.GetFuncMenu")]
        public void GetFuncMenuTest()
        {
            var c1Result = _testService.GetFuncMenu(Guid.NewGuid().ToString());
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetMenuByUrl")]
        public void GetMenuByUrlTest()
        {
            var c1Result = _testService.GetMenuByUrl("plt", Guid.NewGuid().ToString());
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.AddMenu")]
        public void AddMenuTest()
        {
            var c1Result = _testService.AddMenu(new WapMenuDto()
            {
                Operation = "1",
                AppKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9",
                Comment = "2",
                MenuName = "3",
                ComponentId = "4",
                Css = "5",
                Extend = "6",
                IsFuncActive = true,
                FuncKey = "7",
                FuncCode = Guid.Empty.ToString(),
                IsActive = true,
                IsShow = true,
                Parameter = "8",
                ParentMenuKey = Guid.Empty.ToString(),
                SortSn = 9
            });
            Assert.AreEqual(true, c1Result != null);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.UpdateMenu")]
        public void UpdateMenuTest()
        {
            var c1Result = _testService.UpdateMenu(Guid.NewGuid().ToString(), new WapMenuDto()
            {
                Operation = "1",
                AppKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9",
                Comment = "2",
                MenuName = "3",
                ComponentId = "4",
                Css = "5",
                Extend = "6",
                IsFuncActive = true,
                FuncKey = "7",
                FuncCode = Guid.Empty.ToString(),
                IsActive = true,
                IsShow = true,
                Parameter = "8",
                ParentMenuKey = Guid.Empty.ToString(),
                SortSn = 9,
                MenuKey = Guid.NewGuid().ToString()
            });
            Assert.AreEqual(true, c1Result!=null);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.DeleteMenu")]
        public void DeleteMenuTest()
        {
            var c1Result = _testService.DeleteMenu(Guid.NewGuid().ToString());
            Assert.AreEqual(true, c1Result);
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.DeactiveMenu")]
        public void DeactiveMenuTest()
        {
            var c1Result = _testService.DeactiveMenu(Guid.NewGuid().ToString());
            Assert.AreEqual(true, c1Result);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.ActiveMenu")]
        public void ActiveMenuTest()
        {
            var c1Result = _testService.ActiveMenu(Guid.NewGuid().ToString());
            Assert.AreEqual(true, c1Result);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.UpdateMenuParent")]
        public void UpdateMenuParentTest()
        {
            var c1Result = _testService.UpdateMenuParent(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            Assert.AreEqual(true, c1Result);
        }
    }
}
