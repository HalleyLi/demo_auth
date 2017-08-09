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
    public class WapModelServiceImplTest
    {
          private IWapModelService _testService = null;

        private string _appIdentity = "plt";
        private string _appKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9";

        private WapModel _model;
        private IEnumerable<WapModel> _models;
        private int _id = 1;

        public WapModelServiceImplTest()
        {
            Mock<IWapModelRepository> a = new Mock<IWapModelRepository>();

            BuildMock(a);

            //初始化模型
            _testService = new WapModelServiceImpl(
                    a.Object
                );
        }

        private void BuildMock(Mock<IWapModelRepository> mock)
        {
            _model = new WapModel()
            {
                DeviceType = 1,
                ModelId = _id,
                ModelName = "4",
                Remark = "6",
                TenantId = 7
            };

            _models = new List<WapModel>(){
                _model
            };

            mock.Setup(p => p.Delete(It.IsAny<int>())).Returns(true);
            mock.Setup(p => p.Insert(It.IsAny<WapModel>())).Returns(_model);
            mock.Setup(p => p.Select(_id)).Returns(_model);
            mock.Setup(p => p.SelectAll()).Returns(_models);
            mock.Setup(p => p.Update(_id, It.IsAny<WapModel>())).Returns(true);
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.RemoveModel")]
        public void RemoveModelWordTest()
        {
            var c1Result = _testService.RemoveModel(_id);
            Assert.AreEqual(false, c1Result == null);
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.GetAllModels")]
        public void GetAllModelsTest()
        {
            var c1Result = _testService.GetAllModels();
            Assert.AreEqual(false, c1Result== null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetModelById")]
        public void GetModelByIdTest()
        {
            var c1Result = _testService.GetModelById(_id);
            Assert.AreEqual(false, c1Result == null);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.ModifyModelById")]
        public void ModifyModelByIdTest()
        {
            var c1Result = _testService.ModifyModelById(_id, WapModelDto.FromModel(_model));
            Assert.AreEqual(true, c1Result);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.AddModel")]
        public void AddModelTest()
        {
            var c1Result = _testService.AddModel(WapModelDto.FromModel(_model));
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(false, string.IsNullOrEmpty(c1Result.ModelName));
        }

    }
}
