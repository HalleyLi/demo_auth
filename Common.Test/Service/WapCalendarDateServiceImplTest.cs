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
    public class WapCalendarDateServiceImplTest
    {

        private IWapCalendarDateService _testService = null;

        private string _appIdentity = "plt";
        private string _appKey = "389B23F0-A2ED-43B0-9F76-8D997A2A57F9";

        private WapCalendarDate _model;
        private IEnumerable<WapCalendarDate> _models;
        private int _id = 1;
        private int _calendarId = 2;

        public WapCalendarDateServiceImplTest()
        {
            Mock<IWapCalendarDateRepository> a = new Mock<IWapCalendarDateRepository>();

            BuildMock(a);

            //初始化模型
            _testService = new WapCalendarDateServiceImpl(
                    a.Object
                );
        }

        private void BuildMock(Mock<IWapCalendarDateRepository> mock)
        {
            _model = new WapCalendarDate()
            {
                CalendarDate = DateTime.Now,
                CalendarDateType = "5",
                CalendarId = _calendarId,
                IsHoliday = true,
                IsWorkday = true,
                CalendarDateId = _id,
                CalendarDateName = "4",
                Remark = "6",
            };

            _models = new List<WapCalendarDate>(){
                _model
            };

            mock.Setup(p => p.Delete(It.IsAny<int>(), It.IsAny<DateTime>())).Returns(true);
            mock.Setup(p => p.DeleteById(It.IsAny<int>())).Returns(true);
            mock.Setup(p => p.Insert(It.IsAny<WapCalendarDate>())).Returns(_model);
            mock.Setup(p => p.SelectById(_id)).Returns(_model);
            mock.Setup(p => p.Select(It.IsAny<int>(), It.IsAny<DateTime>())).Returns(_model);
            mock.Setup(p => p.Update(_id, It.IsAny<DateTime>(), It.IsAny<WapCalendarDate>())).Returns(true);
            mock.Setup(p => p.UpdateById(_id, _model)).Returns(true);
            mock.Setup(p => p.SelectByDateRange(_id, It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(_models);
            mock.Setup(p => p.SelectIsHoliday(_id, It.IsAny<IEnumerable<DateTime>>())).Returns(_models);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.RemoveCalendarDateById")]
        public void RemoveCalendarDateByIdTest()
        {
            var c1Result = _testService.RemoveCalendarDateById(_id);
            Assert.AreEqual(true, c1Result == true);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.RemoveCalendarDate")]
        public void RemoveCalendarDateTest()
        {
            var c1Result = _testService.RemoveCalendarDate(_id, DateTime.Now);
            Assert.AreEqual(true, c1Result == true);
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.GetCalendarDate")]
        public void GetCalendarDateTest()
        {
            var c1Result = _testService.GetCalendarDate(_id, DateTime.Now);
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(false, string.IsNullOrEmpty(c1Result.CalendarDateName));
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.GetCalendarDateById")]
        public void GetCalendarDateByIdTest()
        {
            var c1Result = _testService.GetCalendarDateById(_id);
            Assert.AreEqual(false, c1Result == null);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.ModifyCalendarDateById")]
        public void ModifyCalendarDateByIdTest()
        {
            var c1Result = _testService.ModifyCalendarDateById(_id, WapCalendarDateDto.FromModel(_model));
            Assert.AreEqual(true, c1Result);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.ModifyCalendarDate")]
        public void ModifyCalendarDateTest()
        {
            var c1Result = _testService.ModifyCalendarDate(_id, DateTime.Now, WapCalendarDateDto.FromModel(_model));
            Assert.AreEqual(true, c1Result);
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.AddCalendarDate")]
        public void AddCalendarDateTest()
        {
            var c1Result = _testService.AddCalendarDate(WapCalendarDateDto.FromModel(_model));
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(false, string.IsNullOrEmpty(c1Result.CalendarDateName));
        }


        [Test]
        [TestCategory("WapFunctionServiceImpl.GetWorkdayByNum")]
        public void GetWorkdayByNumTest()
        {
            var c1Result = _testService.GetWorkdayByNum(_id, DateTime.Now, 3);
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(false, c1Result == new DateTime());
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetIsHolidayByDates")]
        public void GetIsHolidayByDatesTest()
        {
            var c1Result = _testService.GetIsHolidayByDates(_id, new List<DateTime>() { DateTime.Now });
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }

        [Test]
        [TestCategory("WapFunctionServiceImpl.GetCalendarDateByDateRange")]
        public void GetCalendarDateByDateRangeTest()
        {
            var c1Result = _testService.GetCalendarDateByDateRange(_id, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1));
            Assert.AreEqual(false, c1Result == null);
            Assert.AreEqual(true, c1Result.Count() > 0);
        }
    }
}
