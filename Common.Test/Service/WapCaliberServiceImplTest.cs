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
    class WapCaliberServiceImplTest
    {
        private IWapCaliberService _testService = null;

        private string _appIdentity = "plt";
        private string _appKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9";

        private WapCaliber _model;
        private IEnumerable<WapCaliber> _models;
        private int _id = 1;

        public WapCaliberServiceImplTest()
        {
            Mock<IWapCaliberRepository> a = new Mock<IWapCaliberRepository>();

            BuildMock(a);

            //初始化模型
            _testService = new WapCaliberServiceImpl(
                    a.Object
                );
        }

        private void BuildMock(Mock<IWapCaliberRepository> mock)
        {
            _model = new WapCaliber()
            {
                CaliberValue = "5",
                CaliberId = _id,
                CaliberName = "4",
                TenantId = 7
            };

            _models = new List<WapCaliber>(){
                _model
            };

            mock.Setup(p => p.Delete(It.IsAny<int>())).Returns(true);
            mock.Setup(p => p.Insert(It.IsAny<WapCaliber>())).Returns(_model);
            mock.Setup(p => p.Select(_id)).Returns(_model);
            mock.Setup(p => p.SelectAll()).Returns(_models);
            mock.Setup(p => p.Update(_id, It.IsAny<WapCaliber>())).Returns(_model);
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.RemoveCaliber")]
        public void RemoveCaliberWordTest()
        {
            var c1Result = _testService.RemoveCaliber(_id);
            Assert.AreEqual(false, c1Result == null);
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.GetAllCalibers")]
        public void GetAllCalibersTest()
        {
            var c1Result = _testService.GetAllCalibers();
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetCaliberById")]
        public void GetCaliberByIdTest()
        {
            var c1Result = _testService.GetCaliberById(_id);
            Assert.AreEqual(false, c1Result == null);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.ModifyCaliberById")]
        public void ModifyCaliberByIdTest()
        {
            var c1Result = _testService.ModifyCaliberById(_id, WapCaliberDto.FromModel(_model));
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(false, string.IsNullOrEmpty(c1Result.CaliberName));
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.AddCaliber")]
        public void AddCaliberTest()
        {
            var c1Result = _testService.AddCaliber(WapCaliberDto.FromModel(_model));
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(false, string.IsNullOrEmpty(c1Result.CaliberName));
        }

    }
}
