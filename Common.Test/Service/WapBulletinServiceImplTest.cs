using System;
using NUnit.Framework;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCategory = NUnit.Framework.CategoryAttribute;
using SH3H.WAP.Bulletin.Model;
using SH3H.WAP.Bulletin.Model.Dto;
using System.Collections.Generic;
using SH3H.WAP.Bulletin.Contracts;
using SH3H.WAP.Bulletin.Service;
using System.Linq;
using Moq;
using Test.Tools;
using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Share;
using SH3H.WAP.Bulletin.DataAccess.Repo.Contact;

namespace Common.Test.Service
{
    [TestClass]
    public class WapBulletinServiceImplTest
    {
        private IWapBulletinService _testService = null;
        Mock<IWapBulletinRepository> mockBulletinRepo = new Mock<IWapBulletinRepository>();
        Mock<IWapBulletinChannelRepository> mockBulletinChannelRepo = new Mock<IWapBulletinChannelRepository>();
        Mock<IWapBulletinExtendRepository> mockBulletinExtendRepo = new Mock<IWapBulletinExtendRepository>();
        public WapBulletinServiceImplTest()
        {
            _testService = new WapBulletinServiceImpl(mockBulletinRepo.Object, mockBulletinChannelRepo.Object, mockBulletinExtendRepo.Object);
        }

        #region AddWapBulletin

        [TestMethod]
        [TestCategory("WapBulletinService.AddWapBulletin")]
        [Description("新增公告板，正测试")]
        public void AddWapBulletin_Normal_Success()
        {
            WapBulletinDto bulletinDto = new WapBulletinDto()
            {
                #region 准备公告板DTO实体

                Id = 1,
                ChannelId = 2,
                ChannelName = "sample string 3",
                BulletinType = "sample string 4",
                BulletinTitle = "sample string 5",
                BulletinContent = "sample string 6",
                EffectiveTime = new DateTime(2016, 07, 13),
                FailureTime = new DateTime(2016, 07, 14),
                State = true,
                CreateUser = 10,
                CreateTime = DateTime.Now,
                ModifyUser = 12,
                ModifyTime = DateTime.Now,
                Nodes = new List<WapBulletinExtendDto>(){
                        new WapBulletinExtendDto(){
                          BulletinId= 1,
                          ExtendCode= "sample string 1",
                          ExtendValue= "sample string 3"
                        },
                        new WapBulletinExtendDto(){
                          BulletinId= 1,
                          ExtendCode= "sample string 2",
                          ExtendValue= "sample string 3"
                        }
                      }

                #endregion
            };

            WapBulletinChannel bulletinChannel = new WapBulletinChannel()
            {
                #region 准备公告板频道DTO实体

                Id = 2,
                ChannelName = "sample string 3",
                ChannelCode = "ceshiCode",
                ChannelSubject = 4,
                State = 1,
                TenantId = 0,
                CreateUser = 10,
                CreateTime = DateTime.Now,
                Remark = "备注"

                #endregion
            };

            List<WapBulletinExtend> extendList = new List<WapBulletinExtend>();
            extendList.AddRange(bulletinDto.Nodes.Select(n => n.ToModel()));

            mockBulletinRepo.Setup(b => b.AddBulletin(2, bulletinDto.ToModel(), extendList)).Returns(bulletinDto.ToModel());

            mockBulletinChannelRepo.Setup(bc => bc.GetById(bulletinDto.ToModel().ChannelId)).Returns(bulletinChannel);

            mockBulletinExtendRepo.Setup(be => be.GetBulletinExtendsByBulletinId(bulletinDto.ToModel().Id)).Returns(extendList);

            WapBulletinDto resResult = _testService.AddWapBulletin(2, bulletinDto);

            bool resBool = Utils.EqualsObj<WapBulletinDto>(resResult, bulletinDto);

            Assert.That(resBool, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapBulletinService.AddWapBulletin")]
        [Description("新增公告板，公告板频道编号小于零")]
        public void AddWapBulletin_ChannelIdLessThanZero_ThrowException()
        {
            WapBulletinDto bulletinDto = new WapBulletinDto();

            Action action = () => _testService.AddWapBulletin(-2, bulletinDto);
            //判断错误码是否一致
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_LIMIT_ERROR, action);
        }

        [TestMethod]
        [TestCategory("WapBulletinService.AddWapBulletin")]
        [Description("新增公告板，模型不能为空")]
        public void AddWapBulletin_NullWapBulletin_ThrowException()
        {
            WapBulletinDto bulletinDto = null;

            //传入空模型
            Action action = () => _testService.AddWapBulletin(2, bulletinDto);
            //判断错误码是否一致
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapBulletinService.AddWapBulletin")]
        [Description("新增公告板，模型中的频道编号ID与第一个参数不一致")]
        public void AddWapBulletin_ArgumentNotEqual_ThrowException()
        {
            WapBulletinDto bulletinDto = new WapBulletinDto();
            bulletinDto.ChannelId = 3;

            Action action = () => _testService.AddWapBulletin(2, bulletinDto);

            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_NOT_EQUAL, action);
        }

        #endregion

        #region UpdateWapBulletinById

        [TestMethod]
        [TestCategory("WapBulletinService.UpdateWapBulletinById")]
        [Description("更新公告板，正测试")]
        public void UpdateWapBulletinById_Normal_Success()
        {
            WapBulletinDto bulletinDto = new WapBulletinDto()
            {
                #region 准备公告板DTO实体

                Id = 1,
                ChannelId = 2,
                ChannelName = "sample string 3",
                BulletinType = "sample string 4",
                BulletinTitle = "sample string 5",
                BulletinContent = "sample string 6",
                EffectiveTime = new DateTime(2016, 07, 13),
                FailureTime = new DateTime(2016, 07, 14),
                State = true,
                CreateUser = 10,
                CreateTime = DateTime.Now,
                ModifyUser = 12,
                ModifyTime = DateTime.Now,
                Nodes = new List<WapBulletinExtendDto>(){
                        new WapBulletinExtendDto(){
                          BulletinId= 1,
                          ExtendCode= "sample string 1",
                          ExtendValue= "sample string 3"
                        },
                        new WapBulletinExtendDto(){
                          BulletinId= 1,
                          ExtendCode= "sample string 2",
                          ExtendValue= "sample string 3"
                        }
                      }

                #endregion
            };

            WapBulletinChannel bulletinChannel = new WapBulletinChannel()
            {
                #region 准备公告板频道DTO实体

                Id = 2,
                ChannelName = "sample string 3",
                ChannelCode = "ceshiCode",
                ChannelSubject = 4,
                State = 1,
                TenantId = 0,
                CreateUser = 10,
                CreateTime = DateTime.Now,
                Remark = "备注"

                #endregion
            };

            List<WapBulletinExtend> extendList = new List<WapBulletinExtend>();
            extendList.AddRange(bulletinDto.Nodes.Select(n => n.ToModel()));

            mockBulletinRepo.Setup(b => b.UpdateBulletinById(2, 1, bulletinDto.ToModel(), extendList)).Returns(bulletinDto.ToModel());

            mockBulletinChannelRepo.Setup(bc => bc.GetById(bulletinDto.ToModel().ChannelId)).Returns(bulletinChannel);

            mockBulletinExtendRepo.Setup(be => be.GetBulletinExtendsByBulletinId(bulletinDto.ToModel().Id)).Returns(extendList);

            WapBulletinDto resResult = _testService.UpdateWapBulletinById(2, 1, bulletinDto);

            bool resBool = Utils.EqualsObj<WapBulletinDto>(resResult, bulletinDto);

            Assert.That(resBool, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapBulletinService.UpdateWapBulletinById")]
        [Description("更新公告板，公告板频道编号小于零")]
        public void UpdateWapBulletinById_ChannelIdLessThanZero_ThrowException()
        {
            WapBulletinDto bulletinDto = new WapBulletinDto();

            Action action = () => _testService.UpdateWapBulletinById(-2, 1, bulletinDto);
            //判断错误码是否一致
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_LIMIT_ERROR, action);
        }

        [TestMethod]
        [TestCategory("WapBulletinService.UpdateWapBulletinById")]
        [Description("更新公告板，公告板编号小于零")]
        public void UpdateWapBulletinById_BulletinIdLessThanZero_ThrowException()
        {
            WapBulletinDto bulletinDto = new WapBulletinDto();

            Action action = () => _testService.UpdateWapBulletinById(2, -1, bulletinDto);
            //判断错误码是否一致
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_LIMIT_ERROR, action);
        }

        [TestMethod]
        [TestCategory("WapBulletinService.UpdateWapBulletinById")]
        [Description("更新公告板，模型不能为空")]
        public void UpdateWapBulletinById_NullWapBulletin_ThrowException()
        {
            WapBulletinDto bulletinDto = null;

            //传入空模型
            Action action = () => _testService.UpdateWapBulletinById(2, 1, bulletinDto);
            //判断错误码是否一致
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_NULL, action);
        }

        [TestMethod]
        [TestCategory("WapBulletinService.UpdateWapBulletinById")]
        [Description("更新公告板，模型中的频道编号ChannelId与参数channelId不一致")]
        public void UpdateWapBulletinById_ArgumentChannelIdNotEqual_ThrowException()
        {
            WapBulletinDto bulletinDto = new WapBulletinDto();
            bulletinDto.ChannelId = 3;

            Action action = () => _testService.UpdateWapBulletinById(2, 1, bulletinDto);

            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_NOT_EQUAL, action);
        }

        [TestMethod]
        [TestCategory("WapBulletinService.UpdateWapBulletinById")]
        [Description("更新公告板，模型中的公告板编号Id与参数bulletinId不一致")]
        public void UpdateWapBulletinById_ArgumentBulletinIdNotEqual_ThrowException()
        {
            WapBulletinDto bulletinDto = new WapBulletinDto();
            bulletinDto.Id = 3;

            Action action = () => _testService.UpdateWapBulletinById(2, 1, bulletinDto);

            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_NOT_EQUAL, action);
        }

        #endregion

        #region RemoveWapBulletin

        [TestMethod]
        [TestCategory("WapBulletinService.RemoveWapBulletin")]
        [Description("删除公告板，正测试")]
        public void RemoveWapBulletin_Normal_Success()
        {
            mockBulletinRepo.Setup(b => b.Remove(2, 1)).Returns(true);

            bool resResult = _testService.RemoveWapBulletin(2, 1);

            Assert.That(resResult, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapBulletinService.RemoveWapBulletin")]
        [Description("删除公告板，公告板频道编号小于零")]
        public void RemoveWapBulletin_ChannelIdLessThanZero_ThrowException()
        {
            Action action = () => _testService.RemoveWapBulletin(-2, 1);
            //判断错误码是否一致
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_LIMIT_ERROR, action);
        }

        [TestMethod]
        [TestCategory("WapBulletinService.RemoveWapBulletin")]
        [Description("删除公告板，公告板编号小于零")]
        public void RemoveWapBulletin_BulletinIdLessThanZero_ThrowException()
        {
            Action action = () => _testService.RemoveWapBulletin(2, -1);
            //判断错误码是否一致
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_LIMIT_ERROR, action);
        }

        #endregion

        #region GetAllWapBulletins

        [TestMethod]
        [TestCategory("WapBulletinService.GetAllWapBulletins")]
        [Description("获取所有公告板，正测试")]
        public void GetAllWapBulletins_Normal_Success()
        {
            List<WapBulletinDto> bulletinDtoList = new List<WapBulletinDto>();
            WapBulletinDto bulletinDto = new WapBulletinDto()
            {
                #region 准备公告板DTO实体

                Id = 1,
                ChannelId = 2,
                ChannelName = "sample string 3",
                BulletinType = "sample string 4",
                BulletinTitle = "sample string 5",
                BulletinContent = "sample string 6",
                EffectiveTime = new DateTime(2016, 07, 13),
                FailureTime = new DateTime(2016, 07, 14),
                State = true,
                CreateUser = 10,
                CreateTime = DateTime.Now,
                ModifyUser = 12,
                ModifyTime = DateTime.Now,
                Nodes = new List<WapBulletinExtendDto>(){
                        new WapBulletinExtendDto(){
                          BulletinId= 1,
                          ExtendCode= "sample string 1",
                          ExtendValue= "sample string 3"
                        },
                        new WapBulletinExtendDto(){
                          BulletinId= 1,
                          ExtendCode= "sample string 2",
                          ExtendValue= "sample string 3"
                        }
                      }

                #endregion
            };
            bulletinDtoList.Add(bulletinDto);

            WapBulletinChannel bulletinChannel = new WapBulletinChannel()
            {
                #region 准备公告板频道DTO实体

                Id = 2,
                ChannelName = "sample string 3",
                ChannelCode = "ceshiCode",
                ChannelSubject = 4,
                State = 1,
                TenantId = 0,
                CreateUser = 10,
                CreateTime = DateTime.Now,
                Remark = "备注"

                #endregion
            };

            List<WapBulletinExtend> extendList = new List<WapBulletinExtend>();
            extendList.AddRange(bulletinDto.Nodes.Select(n => n.ToModel()));

            mockBulletinRepo.Setup(b => b.GetAll(2)).Returns(bulletinDtoList.Select(bd => bd.ToModel()));

            mockBulletinChannelRepo.Setup(bc => bc.GetById(bulletinDto.ToModel().ChannelId)).Returns(bulletinChannel);

            mockBulletinExtendRepo.Setup(be => be.GetBulletinExtendsByBulletinId(bulletinDto.ToModel().Id)).Returns(extendList);

            List<WapBulletinDto> resBulletinDtoList = _testService.GetAllWapBulletins(2).ToList();

            bool resBool = Utils.EqualsListObj(resBulletinDtoList, bulletinDtoList);

            Assert.That(resBool, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapBulletinService.GetAllWapBulletins")]
        [Description("获取所有公告板，公告板频道编号小于零")]
        public void GetAllWapBulletins_ChannelIdLessThanZero_ThrowException()
        {
            Action action = () => _testService.GetAllWapBulletins(-2);
            //判断错误码是否一致
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_LIMIT_ERROR, action);
        }

        #endregion

        #region GetWapBulletinById

        [TestMethod]
        [TestCategory("WapBulletinService.GetWapBulletinById")]
        [Description("获取指定公告板，正测试")]
        public void GetWapBulletinById_Normal_Success()
        {
            WapBulletinDto bulletinDto = new WapBulletinDto()
            {
                #region 准备公告板DTO实体

                Id = 1,
                ChannelId = 2,
                ChannelName = "sample string 3",
                BulletinType = "sample string 4",
                BulletinTitle = "sample string 5",
                BulletinContent = "sample string 6",
                EffectiveTime = new DateTime(2016, 07, 13),
                FailureTime = new DateTime(2016, 07, 14),
                State = true,
                CreateUser = 10,
                CreateTime = DateTime.Now,
                ModifyUser = 12,
                ModifyTime = DateTime.Now,
                Nodes = new List<WapBulletinExtendDto>(){
                        new WapBulletinExtendDto(){
                          BulletinId= 1,
                          ExtendCode= "sample string 1",
                          ExtendValue= "sample string 3"
                        },
                        new WapBulletinExtendDto(){
                          BulletinId= 1,
                          ExtendCode= "sample string 2",
                          ExtendValue= "sample string 3"
                        }
                      }

                #endregion
            };

            WapBulletinChannel bulletinChannel = new WapBulletinChannel()
            {
                #region 准备公告板频道DTO实体

                Id = 2,
                ChannelName = "sample string 3",
                ChannelCode = "ceshiCode",
                ChannelSubject = 4,
                State = 1,
                TenantId = 0,
                CreateUser = 10,
                CreateTime = DateTime.Now,
                Remark = "备注"

                #endregion
            };

            List<WapBulletinExtend> extendList = new List<WapBulletinExtend>();
            extendList.AddRange(bulletinDto.Nodes.Select(n => n.ToModel()));

            mockBulletinRepo.Setup(b => b.GetById(2, 1)).Returns(bulletinDto.ToModel());

            mockBulletinChannelRepo.Setup(bc => bc.GetById(bulletinDto.ToModel().ChannelId)).Returns(bulletinChannel);

            mockBulletinExtendRepo.Setup(be => be.GetBulletinExtendsByBulletinId(bulletinDto.ToModel().Id)).Returns(extendList);

            WapBulletinDto resResult = _testService.GetWapBulletinById(2, 1);

            bool resBool = Utils.EqualsObj<WapBulletinDto>(resResult, bulletinDto);

            Assert.That(resBool, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapBulletinService.GetWapBulletinById")]
        [Description("获取指定公告板，公告板频道编号小于零")]
        public void GetWapBulletinById_ChannelIdLessThanZero_ThrowException()
        {
            Action action = () => _testService.GetWapBulletinById(-2, 1);
            //判断错误码是否一致
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_LIMIT_ERROR, action);
        }

        [TestMethod]
        [TestCategory("WapBulletinService.GetWapBulletinById")]
        [Description("获取指定公告板，公告板编号小于零")]
        public void GetWapBulletinById_BulletinIdLessThanZero_ThrowException()
        {
            Action action = () => _testService.GetWapBulletinById(2, -1);
            //判断错误码是否一致
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_LIMIT_ERROR, action);
        }

        #endregion

        #region GetWapBulletinByCondition

        [TestMethod]
        [TestCategory("WapBulletinService.GetWapBulletinByCondition")]
        [Description("获取指定条件的公告板，正测试")]
        public void GetWapBulletinByCondition_Normal_Success()
        {
            WapBulletinQueryCondition condition = new WapBulletinQueryCondition();
            condition.ChannelId = 2;
            condition.Type = "1";
            condition.Title = "测试Title";
            condition.EffectiveTime = new DateTime(2016, 7, 13);
            condition.FailureTime = new DateTime(2016, 7, 14);

            WapBulletinDto bulletinDto = new WapBulletinDto()
            {
                #region 准备公告板DTO实体

                Id = 1,
                ChannelId = 2,
                ChannelName = "sample string 3",
                BulletinType = "sample string 4",
                BulletinTitle = "sample string 5",
                BulletinContent = "sample string 6",
                EffectiveTime = new DateTime(2016, 07, 13),
                FailureTime = new DateTime(2016, 07, 14),
                State = true,
                CreateUser = 10,
                CreateTime = DateTime.Now,
                ModifyUser = 12,
                ModifyTime = DateTime.Now,
                Nodes = new List<WapBulletinExtendDto>(){
                        new WapBulletinExtendDto(){
                          BulletinId= 1,
                          ExtendCode= "sample string 1",
                          ExtendValue= "sample string 3"
                        },
                        new WapBulletinExtendDto(){
                          BulletinId= 1,
                          ExtendCode= "sample string 2",
                          ExtendValue= "sample string 3"
                        }
                      }

                #endregion
            };

            WapBulletinChannel bulletinChannel = new WapBulletinChannel()
            {
                #region 准备公告板频道DTO实体

                Id = 2,
                ChannelName = "sample string 3",
                ChannelCode = "ceshiCode",
                ChannelSubject = 4,
                State = 1,
                TenantId = 0,
                CreateUser = 10,
                CreateTime = DateTime.Now,
                Remark = "备注"

                #endregion
            };

            List<WapBulletinExtend> extendList = new List<WapBulletinExtend>();
            extendList.AddRange(bulletinDto.Nodes.Select(n => n.ToModel()));

            mockBulletinRepo.Setup(b => b.SearchByCondition(condition)).Returns(bulletinDto.ToModel());

            mockBulletinChannelRepo.Setup(bc => bc.GetById(bulletinDto.ToModel().ChannelId)).Returns(bulletinChannel);

            mockBulletinExtendRepo.Setup(be => be.GetBulletinExtendsByBulletinId(bulletinDto.ToModel().Id)).Returns(extendList);

            WapBulletinDto resResult = _testService.GetWapBulletinByCondition(condition);

            bool resBool = Utils.EqualsObj<WapBulletinDto>(resResult, bulletinDto);

            Assert.That(resBool, Is.EqualTo(true));
        }

        [TestMethod]
        [TestCategory("WapBulletinService.GetWapBulletinByCondition")]
        [Description("获取指定条件的公告板，公告板条件模型不能为空")]
        public void GetWapBulletinByCondition_NullCondition_ThrowException()
        {
            WapBulletinQueryCondition condition = null;

            //传入空模型
            Action action = () => _testService.GetWapBulletinByCondition(condition);
            //判断错误码是否一致
            ExpectedExceptionAssert.Throws<WapException>(StateCode.CODE_ARGUMENT_NULL, action);
        }

        #endregion
    }
}
