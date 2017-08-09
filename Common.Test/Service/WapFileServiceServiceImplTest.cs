using System;
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
using ClassInitialize = NUnit.Framework.TestFixtureSetUpAttribute;
using ClassCleanup = NUnit.Framework.TestFixtureTearDownAttribute;
using TestCategory = NUnit.Framework.CategoryAttribute;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Test.Tools;
using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Share;
using SH3H.WAP.File.Contracts;
using SH3H.WAP.File.DataAccess.Repo.Contact;
using SH3H.WAP.File.Service;
using SH3H.WAP.File.Model.Dto;
using System.IO;
using SH3H.WAP.File.Model;
using System.Text;
using System.Collections;
using SH3H.WAP.File.Model.Condition;


namespace Common.Test.Service
{
    [TestClass]
    public class WapFileServiceServiceImplTest
    {
        private IWapFileServerService _fileService = null;
        Mock<IWapFileDescriptorRepository> mockFileRepo = new Mock<IWapFileDescriptorRepository>();

        public WapFileServiceServiceImplTest()
        {
            this._fileService = new WapFileServerServiceImpl(mockFileRepo.Object);
        }

        #region AddFileDescriptor

        [TestMethod]
        [TestCategory(" WapFileServiceService.AddFileDescriptor")]
        [Description("文件上传，正测试")]
        public void AddFileDescriptor_Normal_Success()
        {
            byte[] bytes = Encoding.UTF8.GetBytes("1234567890");
            WapFileDescriptorDto dto = new WapFileDescriptorDto()
            {
                TenantId = 0,
                FileId = 1,
                CreateUserId = 1,
                CreateTime = DateTime.Now,
                FileType = "png",
                FileName = "b90b66b8fc518c9bae66326b3508d3de.png",
                OriginFileName = "123",
                FileSize = 1024,
                FileHash = "b90b66b8fc518c9bae66326b3508d3de",
                ContentType = "img/png",
                FileStream = new MemoryStream(bytes)
            };
            WapFileDescriptor model = WapFileDescriptorDto.ToModel(dto);

            mockFileRepo.Setup(a => a.Add(model)).Returns(model);

            var result = this._fileService.AddFileDescriptor(dto);

            var ret = Utils.EqualsObj<WapFileDescriptorDto>(result, WapFileDescriptorDto.FromModel(model));

            Assert.That(ret, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory(" WapFileServiceService.AddFileDescriptor")]
        [Description("文件上传，文件描述对象不能为空")]
        public void AddFileDescriptor_NullFileDescriptor_ThrowException()
        {
            Action action = () => this._fileService.AddFileDescriptor(null);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        #endregion

        #region ModifyFileDescriptor

        [TestMethod]
        [TestCategory(" WapFileServiceService.ModifyFileDescriptor")]
        [Description("更新文件相关信息，正测试")]
        public void ModifyFileDescriptor_Normal_Success()
        {
            int fileId = 1;
            byte[] bytes = Encoding.UTF8.GetBytes("1234567890");
            WapFileDescriptorDto dto = new WapFileDescriptorDto()
            {
                TenantId = 0,
                FileId = 1,
                CreateUserId = 1,
                CreateTime = DateTime.Now,
                FileType = "png",
                FileName = "b90b66b8fc518c9bae66326b3508d3de.png",
                OriginFileName = "123",
                FileSize = 1024,
                FileHash = "b90b66b8fc518c9bae66326b3508d3de",
                ContentType = "img/png",
                FileStream = new MemoryStream(bytes)
            };
            WapFileDescriptor model = WapFileDescriptorDto.ToModel(dto);

            mockFileRepo.Setup(a => a.Modify(fileId, model)).Returns(true);

            var result = this._fileService.ModifyFileDescriptor(fileId, dto);

            var ret = (result == true) ? true : false;

            Assert.That(ret, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory(" WapFileServiceService.ModifyFileDescriptor")]
        [Description("更新文件相关信息，文件编号必须大于0")]
        public void ModifyFileDescriptor_FileIdNull_ThrowException()
        {
            Action action = () => this._fileService.ModifyFileDescriptor(0, new WapFileDescriptorDto());
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory(" WapFileServiceService.ModifyFileDescriptor")]
        [Description("更新文件相关信息，文件描述对象不能为空")]
        public void ModifyFileDescriptor_FileDescriptorNull_ThrowException()
        {
            Action action = () => this._fileService.ModifyFileDescriptor(1,null);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        #endregion

        #region RemoveFileDescriptor

        [TestMethod]
        [TestCategory(" WapFileServiceService.RemoveFileDescriptor")]
        [Description("根据文件编号删除文件对象，正测试")]
        public void RemoveFileDescriptor_Normal_Success()
        {
            int fileId = 1;
            WapFileDescriptor model = new WapFileDescriptor()
            {
                TenantId = 0,
                FileId = 1,
                CreateUserId = 1,
                CreateTime = DateTime.Now,
                FileType = "png",
                FileName = "b90b66b8fc518c9bae66326b3508d3de.png",
                OriginFileName = "123",
                FileSize = 1024,
                FileHash = "b90b66b8fc518c9bae66326b3508d3de",
                ContentType = "img/png"
            };

            mockFileRepo.Setup(a => a.Get(fileId)).Returns(model);
            mockFileRepo.Setup(a => a.Remove(fileId)).Returns(true);

            var result = this._fileService.RemoveFileDescriptor(fileId);

            var ret = (result == true) ? true : false;

            Assert.That(ret, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory(" WapFileServiceService.RemoveFileDescriptor")]
        [Description("根据文件编号删除文件对象，文件编号必须大于0")]
        public void RemoveFileDescriptor_FileIdNull_ThrowException()
        {
            Action action = () => this._fileService.RemoveFileDescriptor(0);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        #endregion

        #region GetAllFileDescriptors

        [TestMethod]
        [TestCategory(" WapFileServiceService.GetAllFileDescriptors")]
        [Description("获取所有的文件对象，正测试")]
        public void GetAllFileDescriptors_Normal_Success()
        {
            IEnumerable<WapFileDescriptor> list = new List<WapFileDescriptor>() 
            {
                new WapFileDescriptor()
                {
                    TenantId = 0,
                    FileId = 1,
                    CreateUserId = 1,
                    CreateTime = DateTime.Now,
                    FileType = "png",
                    FileName = "b90b66b8fc518c9bae66326b3508d3de.png",
                    OriginFileName = "123",
                    FileSize = 1024,
                    FileHash = "b90b66b8fc518c9bae66326b3508d3de",
                    ContentType = "img/png",
                    FileStream = null
                }
            };

            mockFileRepo.Setup(a => a.GetAllFileDescriptor()).Returns(list);

            var result = this._fileService.GetAllFileDescriptors();

            Assert.IsNotNull(result);
        }        

        #endregion

        #region Search

        [TestMethod]
        [TestCategory(" WapFileServiceService.Search")]
        [Description("根据条件获取文件对象列表，正测试")]        
        public void Search_Normal_Success()
        {
            WapFileDescriptorQueryCondition condition = new WapFileDescriptorQueryCondition()
            {
                FileName = "",
                FileType = "png",
                CreateUserId = 1,
                StartDate = new DateTime(2000, 1, 1),
                EndDate = new DateTime(2000, 1, 2),
                PageSize = 1,
                PageIndex = 10
            };
            IEnumerable<WapFileDescriptor> list = new List<WapFileDescriptor>() 
            {
                new WapFileDescriptor()
                {
                    TenantId = 0,
                    FileId = 1,
                    CreateUserId = 1,
                    CreateTime = DateTime.Now,
                    FileType = "png",
                    FileName = "b90b66b8fc518c9bae66326b3508d3de.png",
                    OriginFileName = "123",
                    FileSize = 1024,
                    FileHash = "b90b66b8fc518c9bae66326b3508d3de",
                    ContentType = "img/png",
                    FileStream = null
                }
            };

            mockFileRepo.Setup(a => a.Get(condition)).Returns(list);

            var result = this._fileService.Search(condition);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        [TestCategory(" WapFileServiceService.Search")]
        [Description("获取所有的文件对象，查询条件不允许为空")]
        public void Search_NullCondition_ThrowException()
        {
            Action action = () => this._fileService.Search(null);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory(" WapFileServiceService.Search")]
        [Description("获取所有的文件对象，结束时间必须大于开始时间")]
        public void Search_StartDateAndEndDate_ThrowException()
        {
            WapFileDescriptorQueryCondition condition = new WapFileDescriptorQueryCondition()
            {
                StartDate = new DateTime(2000, 1, 1),
                EndDate = new DateTime(2000, 1, 1)
            };

            Action action = () => this._fileService.Search(condition);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_STARTEND_EXCEPTION, action);
        }

        [TestMethod]
        [TestCategory(" WapFileServiceService.Search")]
        [Description("获取所有的文件对象，页面数据量必须大于0")]
        public void Search_NullPageSize_ThrowException()
        {
            WapFileDescriptorQueryCondition condition = new WapFileDescriptorQueryCondition()
            {
                StartDate = new DateTime(2000, 1, 1),
                EndDate = new DateTime(2000, 1, 2),
                PageSize = 0,
                PageIndex = 1
            };

            Action action = () => this._fileService.Search(condition);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory(" WapFileServiceService.Search")]
        [Description("获取所有的文件对象，页面索引必须大于0")]
        public void Search_NullPageIndex_ThrowException()
        {
            WapFileDescriptorQueryCondition condition = new WapFileDescriptorQueryCondition()
            {
                StartDate = new DateTime(2000, 1, 1),
                EndDate = new DateTime(2000, 1, 2),
                PageSize = 10,
                PageIndex = 0
            };

            Action action = () => this._fileService.Search(condition);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        #endregion

        #region Count

        [TestMethod]
        [TestCategory(" WapFileServiceService.Count")]
        [Description("根据条件获取文件对象数量，正测试")]
        public void Count_Normal_Success()
        {
            WapFileDescriptorQueryCondition condition = new WapFileDescriptorQueryCondition()
            {
                FileName = "",
                FileType = "png",
                CreateUserId = 1,
                StartDate = new DateTime(2000, 1, 1),
                EndDate = new DateTime(2000, 1, 2),
                PageSize = 1,
                PageIndex = 10
            };
            int count = 5;

            mockFileRepo.Setup(a => a.Count(condition)).Returns(count);

            var result = this._fileService.Count(condition);

            var ret = (result == count) ? true : false;

            Assert.That(ret, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory(" WapFileServiceService.Count")]
        [Description("根据条件获取文件对象数量，查询条件不允许为空")]
        public void Count_NullCondition_ThrowException()
        {
            Action action = () => this._fileService.Count(null);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory(" WapFileServiceService.Count")]
        [Description("根据条件获取文件对象数量，结束时间必须大于开始时间")]
        public void Count_StartDateAndEndDate_ThrowException()
        {
            WapFileDescriptorQueryCondition condition = new WapFileDescriptorQueryCondition()
            {
                StartDate = new DateTime(2000, 1, 1),
                EndDate = new DateTime(2000, 1, 1)
            };

            Action action = () => this._fileService.Count(condition);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_STARTEND_EXCEPTION, action);
        }

        [TestMethod]
        [TestCategory(" WapFileServiceService.Count")]
        [Description("根据条件获取文件对象数量，页面数据量必须大于0")]
        public void Count_NullPageSize_ThrowException()
        {
            WapFileDescriptorQueryCondition condition = new WapFileDescriptorQueryCondition()
            {
                StartDate = new DateTime(2000, 1, 1),
                EndDate = new DateTime(2000, 1, 2),
                PageSize = 0,
                PageIndex = 1
            };

            Action action = () => this._fileService.Count(condition);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory(" WapFileServiceService.Count")]
        [Description("根据条件获取文件对象数量，页面索引必须大于0")]
        public void Count_NullPageIndex_ThrowException()
        {
            WapFileDescriptorQueryCondition condition = new WapFileDescriptorQueryCondition()
            {
                StartDate = new DateTime(2000, 1, 1),
                EndDate = new DateTime(2000, 1, 2),
                PageSize = 10,
                PageIndex = 0
            };

            Action action = () => this._fileService.Count(condition);
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        #endregion

        #region GetFileDescriptorByHash

        [TestMethod]
        [TestCategory(" WapFileServiceService.GetFileDescriptorByHash")]
        [Description("根据文件Hash获取文件描述器对象，正测试")]
        public void GetFileDescriptorByHash_Nromal_Success()
        {
            string hash = "b90b66b8fc518c9bae66326b3508d3de";
            byte[] bytes = Encoding.UTF8.GetBytes("1234567890");
            WapFileDescriptorDto dto = new WapFileDescriptorDto()
            {
                TenantId = 0,
                FileId = 1,
                CreateUserId = 1,
                CreateTime = DateTime.Now,
                FileType = "png",
                FileName = "b90b66b8fc518c9bae66326b3508d3de.png",
                OriginFileName = "123",
                FileSize = 1024,
                FileHash = "b90b66b8fc518c9bae66326b3508d3de",
                ContentType = "img/png",
                FileStream = new MemoryStream(bytes)
            };
            WapFileDescriptor model = WapFileDescriptorDto.ToModel(dto);

            mockFileRepo.Setup(a => a.GetByHash(hash)).Returns(model);

            var result = this._fileService.GetFileDescriptorByHash(hash);

            var ret = Utils.EqualsObj<WapFileDescriptorDto>(result, WapFileDescriptorDto.FromModel(model));

            Assert.That(ret, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory(" WapFileServiceService.GetFileDescriptorByHash")]
        [Description("根据文件Hash获取文件描述器对象，文件hash不允许为空")]
        public void GetFileDescriptorByHash_NullHash_ThrowException()
        {
            Action action = () => this._fileService.GetFileDescriptorByHash("");
            ExpectedExceptionAssert.Throws<WapException>(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, action);
        }

        #endregion

        #region GetImage

        [TestMethod]
        [TestCategory(" WapFileServiceService.GetImage")]
        [Description("根据文件Hash值获取对应的图片对象，正测试")]
        public void GetImage_Normal_Success()
        {
            string hash = "b90b66b8fc518c9bae66326b3508d3de";
            //字典参数模拟 有问题
        }

        #endregion              
    }
}
