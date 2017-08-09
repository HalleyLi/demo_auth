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
    public class WapProducerServiceImplTest
    {

        private IWapProducerService _testService = null;

        private string _appIdentity = "plt";
        private string _appKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9";

        private WapProducer _model;
        private IEnumerable<WapProducer> _models;
        private int _id = 1;

        public WapProducerServiceImplTest()
        {
            Mock<IWapProducerRepository> a = new Mock<IWapProducerRepository>();

            BuildMock(a);

            //初始化模型
            _testService = new WapProducerServiceImpl(
                    a.Object
                );
        }

        private void BuildMock(Mock<IWapProducerRepository> mock)
        {
            _model = new WapProducer()
            {
                DeviceType = 1,
                ProducerAddress = "2",
                ProducerContact = "3",
                ProducerId = _id,
                ProducerName = "4",
                ProducerTelephone = "5",
                Remark = "6",
                TenantId = 7
            };

            _models = new List<WapProducer>(){
                _model
            };

            mock.Setup(p => p.Delete(It.IsAny<int>())).Returns(true);
            mock.Setup(p => p.Insert(It.IsAny<WapProducer>())).Returns(_model);
            mock.Setup(p => p.Select(_id)).Returns(_model);
            mock.Setup(p => p.SelectAll()).Returns(_models);
            mock.Setup(p => p.Update(_id, It.IsAny<WapProducer>())).Returns(true);
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.RemoveProducer")]
        public void RemoveProducerWordTest()
        {
            var c1Result = _testService.RemoveProducer(_id);
            Assert.AreEqual(false, c1Result == null);
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.GetAllProducers")]
        public void GetAllProducersTest()
        {
            var c1Result = _testService.GetAllProducers();
            Assert.AreEqual(false, c1Result== null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetProducerById")]
        public void GetProducerByIdTest()
        {
            var c1Result = _testService.GetProducerById(_id);
            Assert.AreEqual(false, c1Result == null);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.ModifyProducerById")]
        public void ModifyProducerByIdTest()
        {
            var c1Result = _testService.ModifyProducerById(_id, WapProducerDto.FromModel(_model));
            Assert.AreEqual(true, c1Result);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.AddProducer")]
        public void AddProducerTest()
        {
            var c1Result = _testService.AddProducer(WapProducerDto.FromModel(_model));
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(false, string.IsNullOrEmpty(c1Result.ProducerName));
        }

    }
}
