using SH3H.WAP.Contracts;
using SH3H.WAP.DataAccess.Repo.Contact;
using SH3H.WAP.Model;
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
    /// 日历服务操作
    /// </summary>
    public class WapCalendarServiceImpl : IWapCalendarService
    {
        private IWapCalendarRepository _repo;

        public WapCalendarServiceImpl(IWapCalendarRepository repo)
        {
            _repo = repo;
        }

        public WapCalendarDto Add(WapCalendarDto calendar)
        {
            if (calendar == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "日历对象不允许为空");
            }
            var validateResult = calendar.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }
            var result = _repo.Insert(calendar.ToModel());
            if (result == null)
            {
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "添加日历失败");
            }
            return WapCalendarDto.FromModel(result);
        }

        /// <summary>
        /// 删除日历
        /// </summary>
        /// <param name="id">日历编号</param>
        /// <param name="trans"></param>
        /// <returns>是否删除成功</returns>
        public bool Delete(int id)
        {

            return _repo.Delete(id);
        }

        /// <summary>
        /// 通过日历编号修改日历
        /// </summary>
        /// <param name="id">日历编号</param>
        /// <param name="calendar">日历对象</param>
        /// <param name="trans"></param>
        /// <returns>返回时否修改成功</returns>
        public bool Modify(int id, WapCalendarDto calendar)
        {
            if (calendar == null)
            {
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_ARGUMENT_NULL, "日历对象不允许为空");
            }
            var validateResult = calendar.Validate();
            if (!validateResult.IsValid)
            {
                throw validateResult.BuildException();
            }
            return _repo.Update(id, calendar.ToModel());
        }

        /// <summary>
        /// 通过日历编号修改日历状态
        /// </summary>
        /// <param name="calendarId">日历编号</param>
        /// <param name="active">日历状态</param>
        /// <returns>返回时否修改成功</returns>
        public bool ModifyState(int calendarId, int state)
        {
            return _repo.UpdateState(calendarId, state);
        }

        /// <summary>
        /// 通过日历编号查询日历
        /// </summary>
        /// <param name="id">日历编号</param>
        /// <returns>日历对象</returns>
        public WapCalendarDto GetCalendar(int id)
        {

            var result = _repo.Select(id);
            if (result == null)
            {
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "通过日历编号查询日历失败");
            }
            return WapCalendarDto.FromModel(result);
        }

        /// <summary>
        /// 获取所有日历
        /// </summary>
        /// <returns>日历对象</returns>
        public IEnumerable<WapCalendarDto> GetAllCalendars()
        {

            return _repo.SelectAll(true).Select(p => WapCalendarDto.FromModel(p)).ToList();
        }


        /// <summary>
        ///通过日历编码查询
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public WapCalendarDto GetCalendarByCode(string code)
        {
            CheckNullStr(code, "日历编码");
            var result = _repo.SelectByCode(code);
            if (result == null)
            {
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "通过日历编码查询日历失败");
            }
            return WapCalendarDto.FromModel(result);
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
