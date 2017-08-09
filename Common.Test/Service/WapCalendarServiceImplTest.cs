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
    public class WapCalendarServiceImplTest
    {
        private IWapCalendarService _testService = null;

        private string _appIdentity = "plt";
        private string _appKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9";

        private WapCalendar _model;
        private IEnumerable<WapCalendar> _models;
        private int _id = 1;

        public WapCalendarServiceImplTest()
        {
            Mock<IWapCalendarRepository> a = new Mock<IWapCalendarRepository>();

            BuildMock(a);

            //初始化模型
            _testService = new WapCalendarServiceImpl(
                    a.Object
                );
        }

        private void BuildMock(Mock<IWapCalendarRepository> mock)
        {
            _model = new WapCalendar()
            {
                CalendarCode = "1",
                States = 1,
                CalendarId = _id,
                CalendarName = "4",
                Remark = "6",
                TenantId = 7
            };

            _models = new List<WapCalendar>(){
                _model
            };

            mock.Setup(p => p.Delete(It.IsAny<int>())).Returns(true);
            mock.Setup(p => p.Insert(It.IsAny<WapCalendar>())).Returns(_model);
            mock.Setup(p => p.Select(_id)).Returns(_model);
            mock.Setup(p => p.SelectAll(true)).Returns(_models);
            mock.Setup(p => p.Update(_id, It.IsAny<WapCalendar>())).Returns(true);
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.RemoveCalendar")]
        public void RemoveCalendarWordTest()
        {
            var c1Result = _testService.Delete(_id);
            Assert.AreEqual(false, c1Result == null);
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.GetAllCalendars")]
        public void GetAllCalendarsTest()
        {
            var c1Result = _testService.GetAllCalendars();
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetCalendarById")]
        public void GetCalendarByIdTest()
        {
            var c1Result = _testService.GetCalendar(_id);
            Assert.AreEqual(false, c1Result == null);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.ModifyCalendarById")]
        public void ModifyCalendarByIdTest()
        {
            var c1Result = _testService.Modify(_id, WapCalendarDto.FromModel(_model));
            Assert.AreEqual(true, c1Result);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.AddCalendar")]
        public void AddCalendarTest()
        {
            var c1Result = _testService.Add(WapCalendarDto.FromModel(_model));
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(false, string.IsNullOrEmpty(c1Result.CalendarName));
        }

    }
}
