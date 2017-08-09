﻿using SH3H.WAP.Contracts;
using SH3H.WAP.DataAccess.Repo.Contact;
using SH3H.WAP.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Share;

namespace SH3H.WAP.Service
{
    /// <summary>
    /// 水表厂商服务操作
    /// </summary>
    public class WapProducerServiceImpl : IWapProducerService
    {
        private IWapProducerRepository _repo;

        public WapProducerServiceImpl(IWapProducerRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// 添加水表厂商
        /// </summary>
        /// <param name="producer">水表厂商对象</param>
        /// <returns>水表厂商对象</returns>
        public WapProducerDto AddProducer(WapProducerDto producer)
        {
            if (producer == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "水表厂商编号不允许为空");
            }
            var validateResult = producer.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }
            var result = _repo.Insert(producer.ToModel());
            if (result == null)
            {
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "添加水表厂商失败");
            }

            return WapProducerDto.FromModel(result);
        }

        /// <summary>
        /// 删除水表厂商
        /// </summary>
        /// <param name="id">水表厂商编号</param>
        /// <returns>是否删除成功</returns>
        public bool RemoveProducer(int id)
        {

            return _repo.Delete(id);
        }

        /// <summary>
        /// 通过水表厂商编号修改水表厂商
        /// </summary>
        /// <param name="id">水表厂商编号</param>
        /// <param name="producer">水表厂商对象</param>
        /// <returns>返回时否修改成功</returns>
        public bool ModifyProducerById(int id, WapProducerDto producer)
        {
            if (producer == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "水表厂商编号不允许为空");
            }
            var validateResult = producer.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }
            return _repo.Update(id, producer.ToModel());
        }

        /// <summary>
        /// 通过水表厂商编号查询水表厂商
        /// </summary>
        /// <param name="id">水表厂商编号</param>
        /// <returns>水表厂商对象</returns>
        public WapProducerDto GetProducerById(int id)
        {

            var result = _repo.Select(id);
            if (result == null)
            {
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "通过水表厂商编号查询水表厂商失败");
            }

            return WapProducerDto.FromModel(result);
        }

        /// <summary>
        /// 根据水表厂商编码查找
        /// </summary>
        /// <returns>所有水表厂商集合</returns>
        public IEnumerable<WapProducerDto> GetAllProducers()
        {

            return _repo.SelectAll().Select(p => WapProducerDto.FromModel(p)).ToList(); 
        }

        #region 参数检查
        /// <summary>
        /// 检查入参是否是GUID类型字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="fieldName">参数中文名</param>
        /// <returns></returns>
        public static void CheckGuidStr(string str, string fieldName)
        {
            if (!Utils.IsGuid(str))
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_INVALID_ARGUMENTS, "参数 " + fieldName + " 不是合法的GUID类型字符串");
            }
        }

        /// <summary>
        /// 检查入参是否是空字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="fieldName">参数中文名</param>
        /// <returns></returns>
        public static void CheckNullStr(string str, string fieldName)
        {

            if (string.IsNullOrEmpty(str))
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_INVALID_ARGUMENTS, "参数 " + fieldName + " 不能为空");

            }
        }

        /// <summary>
        /// 检查入参是否是空
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static void CheckNull<T>(T obj, string fieldName)
        {
            if (obj == null)
            {
                throw new WapException(SH3H.SDK.Definition.StateCode.CODE_INVALID_ARGUMENTS, "参数 " + fieldName + " 不能为空");

            }
        }

        #endregion

    }
}
