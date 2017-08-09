using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using TestCategory = NUnit.Framework.CategoryAttribute;
using SH3H.WAP.Contracts;
using SH3H.WAP.DataAccess.Repo.Contact;
using Moq;
using SH3H.WAP.Service;
using SH3H.WAP.Model;
using NUnit.Framework;
using SH3H.WAP.Model.Dto;


namespace Common.Test.Service
{
    [TestClass]
    public class WapConfigurationServiceImplTest
    {
        private IWapConfigurationService _testService = null;

        private string _appIdentity = "plt";
        private string _appKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9";

        private WapConfiguration _model;
        private IEnumerable<WapConfiguration> _models;
        private int _id = 1;
        private string _configCode = "2";
        private string  _groupId = "3";

        public WapConfigurationServiceImplTest()
        {
            Mock<IWapConfigurationRepository> a = new Mock<IWapConfigurationRepository>();

            BuildMock(a);

            //初始化模型
            _testService = new WapConfigurationServiceImpl(
                    a.Object
                );
        }

        private void BuildMock(Mock<IWapConfigurationRepository> mock)
        {
            _model = new WapConfiguration()
            {
                ConfigApp = _appIdentity,
                ConfigCode = _configCode,
                ConfigDefault = "4",
                ConfigGroup = _groupId,
                ConfigName = "5",
                Remark = "6",
                ConfigState = 1,
                ConfigType = 7,
                ConfigValue = "8",
                Id = _id
            };

            _models = new List<WapConfiguration>(){
                _model
            };

            mock.Setup(p => p.Delete(It.IsAny<int>())).Returns(true);
            mock.Setup(p => p.Insert(It.IsAny<WapConfiguration>())).Returns(_model);
            mock.Setup(p => p.SelectById(_id)).Returns(_model);
            mock.Setup(p => p.SelectAll()).Returns(_models);
            mock.Setup(p => p.Update(_id, It.IsAny<WapConfiguration>())).Returns(true);
            mock.Setup(p => p.GetConfigsByAppCode(_appIdentity)).Returns(_models);
            mock.Setup(p => p.SelectByAppAndGroup(_appIdentity, _groupId)).Returns(_models);
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.RemoveConfiguration")]
        public void RemoveConfigurationWordTest()
        {
            var c1Result = _testService.RemoveConfiguration(_id);
            Assert.AreEqual(false, c1Result == null);
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.GetAllConfiguration")]
        public void GetAllConfigurationTest()
        {
            var c1Result = _testService.GetAllConfiguration();
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetConfigurationById")]
        public void GetConfigurationByIdTest()
        {
            var c1Result = _testService.GetConfigurationById(_id);
            Assert.AreEqual(false, c1Result == null);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.ModifyConfigurationById")]
        public void ModifyConfigurationByIdTest()
        {
            var c1Result = _testService.ModifyConfiguration(_id, WapConfigurationDto.FromModel(_model));
            Assert.AreEqual(true, c1Result);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.AddConfiguration")]
        public void AddConfigurationTest()
        {
            var c1Result = _testService.AddConfiguration(WapConfigurationDto.FromModel(_model));
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(false, string.IsNullOrEmpty(c1Result.ConfigName));
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetConfigGroupByAppCode")]
        public void GetConfigGroupByAppCodeTest()
        {
            var c1Result = _testService.GetConfigsByAppCode(_appIdentity);
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetConfigurationByAppAndGroup")]
        public void GetConfigurationByAppAndGroupTest()
        {
            var c1Result = _testService.GetConfigurationByAppAndGroup(_appIdentity, _groupId);
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

    }
}
