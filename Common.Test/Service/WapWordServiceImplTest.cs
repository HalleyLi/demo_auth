using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using TestCategory = NUnit.Framework.CategoryAttribute;
using SH3H.WAP.Model;
using System.Collections.Generic;
using Moq;
using SH3H.WAP.DataAccess.Repo.Contact;
using SH3H.WAP.Service;
using SH3H.WAP.Contracts;
using SH3H.WAP.Model.Dto;
using System.Linq;

namespace Common.Test.Service
{

    [TestClass]
    public class WapWordServiceImplTest
    {

        private string _appIdentity = "plt";
        private string _appKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9";

        private string _wordGroupKey = "wg";
        private int _wordId = 1;
        private int _childWordeId = 2;
        private WapWord _child;
        private WapWord _parent;
        private string _wordCode = "123";

        private IEnumerable<WapWord> _wapWord;

        private IWapWordService _testService;

        public WapWordServiceImplTest()
        {
            Mock<IWapWordRepository> a = new Mock<IWapWordRepository>();

            BuildMock(a);

            //初始化模型
            _testService = new WapWordServiceImpl(
                    a.Object
                );

        }

        private void BuildMock(Mock<IWapWordRepository> mock)
        {
            _child = new WapWord()
            {
                App = _appIdentity,
                WordValue = "1",
                IsExternalVisible = true,
                ParentId = _wordId,
                Remark = "3",
                TenentId = 4,
                WordCode = _wordCode,
                WordGroupKey = _wordGroupKey,
                WordId = _childWordeId,
                WordPYCode = "8",
                WordSortIndex = 9,
                WordState = 10,
                WordText = "11"
            };

            _parent = new WapWord()
            {
                App = _appIdentity,
                WordValue = "1",
                IsExternalVisible = true,
                ParentId = 0,
                Remark = "3",
                TenentId = 4,
                WordCode = _wordCode,
                WordGroupKey = _wordGroupKey,
                WordId = _wordId,
                WordPYCode = "8",
                WordSortIndex = 9,
                WordState = 10,
                WordText = "11"
            };

            _wapWord = new List<WapWord>()
            {
                _parent,_child
            };

            mock.Setup(p => p.Delete(It.IsAny<int>())).Returns(true);
            mock.Setup(p => p.GetAllWordsByApp(_appIdentity)).Returns(_wapWord);
            mock.Setup(p => p.GetAllWordsByAppAndGroup(_appIdentity, It.IsAny<string>())).Returns(_wapWord);
            mock.Setup(p => p.GetWordsByGroupKey(_wordGroupKey)).Returns(_wapWord);
            mock.Setup(p => p.Insert(It.IsAny<WapWord>())).Returns(_child);
            mock.Setup(p => p.Select(_wordId)).Returns(_parent);
            mock.Setup(p => p.Select(_childWordeId)).Returns(_child);
            mock.Setup(p => p.SelectByCodeAndValue(_wordCode, "1")).Returns(_child);
            mock.Setup(p => p.SelectByPerentId(_wordId)).Returns(new List<WapWord>() { _child });
            mock.Setup(p => p.SelectByWordCodes(It.IsAny<IEnumerable<string>>())).Returns(_wapWord);
            mock.Setup(p => p.Update(It.IsAny<int>(), It.IsAny<WapWord>())).Returns(_child);
            mock.Setup(p => p.UpdateParentId(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            mock.Setup(p => p.UpdateSortIndexById(It.IsAny<Dictionary<int, int>>())).Returns(true);
            mock.Setup(p => p.UpdateStateById(_wordId, It.IsAny<int>())).Returns(true);
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.AddWord")]
        public void AddWordTest()
        {
            var c1Result = _testService.AddWord(WapWordDto.FromModel(_parent));
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(false, string.IsNullOrEmpty(c1Result.WordCode));
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetAllWordNodes")]
        public void GetAllWordNodesTest()
        {
            var c1Result = _testService.GetAllWordNodes();
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetAllWords")]
        public void GetAllWordsTest()
        {
            var c1Result = _testService.GetAllWords();
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetAllWordsByApp")]
        public void GetAllWordsByAppTest()
        {
            var c1Result = _testService.GetAllWordsByApp(_appIdentity);
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.GetAllWordsByAppAndGroup")]
        public void GetAllWordsByAppAndGroupTest()
        {
            var c1Result = _testService.GetAllWordsByAppAndGroup(_appIdentity, _wordGroupKey);
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.GetByCodeAndValue")]
        public void GetByCodeAndValueTest()
        {
            var c1Result = _testService.GetByCodeAndValue(_wordCode, "1");
            Assert.AreEqual(false, c1Result == null);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetByCodeAndValue")]
        public void GetByPcodeTest()
        {
            var c1Result = _testService.GetByPcode("123");
            Assert.AreEqual(false, c1Result == null);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetByPid")]
        public void GetByPidTest()
        {
            var c1Result = _testService.GetByPid(1);
            Assert.AreEqual(false, c1Result == null);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetWordById")]
        public void GetWordByIdTest()
        {
            var c1Result = _testService.GetWordById(_wordId);
            Assert.AreEqual(false, c1Result == null);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetWordByWordCode")]
        public void GetWordByWordCodeTest()
        {
            var c1Result = _testService.GetWordByWordCode(_wordCode);
            Assert.AreEqual(false, c1Result == null);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetWordNodeByCode")]
        public void GetWordNodeByCodeTest()
        {
            var c1Result = _testService.GetWordNodeByCode(_wordCode);
            Assert.AreEqual(false, c1Result == null);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetWordNodesByCodes")]
        public void GetWordNodesByCodesTest()
        {
            var c1Result = _testService.GetWordNodesByCodes(new string[] { _wordCode });
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.GetWordsByGroupKey")]
        public void GetWordsByGroupKeyTest()
        {
            var c1Result = _testService.GetWordsByGroupKey(_wordGroupKey);
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetWordsByWordCodes")]
        public void GetWordsByWordCodesTest()
        {
            var c1Result = _testService.GetWordsByWordCodes(new string[] { _wordCode });
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.ModifyParentId")]
        public void ModifyParentIdTest()
        {
            var c1Result = _testService.ModifyParentId(_childWordeId, _wordId);
            Assert.AreEqual(true, c1Result);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.ModifySortIndexById")]
        public void ModifySortIndexByIdTest()
        {
            var c1Result = _testService.ModifySortIndexById(new Dictionary<int, int>());
            Assert.AreEqual(true, c1Result);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.ModifyStateById")]
        public void ModifyStateByIdTest()
        {
            var c1Result = _testService.ModifyStateById(_wordId, new WapCommonStateDto() { Active = true });
            Assert.AreEqual(true, c1Result);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.ModifyWordById")]
        public void ModifyWordById()
        {
            var c1Result = _testService.ModifyWordById(_wordId, WapWordDto.FromModel(_parent));
            Assert.AreEqual(true, c1Result != null);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.RemoveWord")]
        public void RemoveWord()
        {
            var c1Result = _testService.RemoveWord(_wordId);
            Assert.AreEqual(true, c1Result);
        }
    }
}
