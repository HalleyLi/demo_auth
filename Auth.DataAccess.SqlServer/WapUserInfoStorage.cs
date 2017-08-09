using SH3H.SDK.DataAccess.Db;
using SH3H.SDK.Definition.Exceptions;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.SDK.Share;
using SH3H.WAP.Auth.Model;
using SH3H.SharpFrame.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.DataAccess.SqlServer
{
    /// <summary>
    /// 定义用户组织信息数据库操作
    /// </summary>
    public class WapUserInfoStorage 
        : BaseAccess<WapUserInfo>, IWapUserInfoStorage
    {
        /// <summary>
        /// 构造函数
        /// </summary>        
        public WapUserInfoStorage()
            : base(Consts.AUTH_DATABASE_CONNECTION_STRING) { }

        /// <summary>
        /// 根据用户获取用户组织信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>用户组织信息</returns>
        public WapUserInfo SelectUserInfoByUserId(int userId)
        {
            try
            {
                string commandText = @"SELECT t_user.USER_ID,
                                              t_user.USER_NAME, 
                                              t_user.FILE_HASH,
                                              t_org.ORGANIZATION_NAME,
                                              t_org.ORGANIZATION_CODE,
										   	  t_parorg.ORGANIZATION_NAME AS PARENT_ORGANIZATION_NAME
                                     FROM AUTH_USER t_user
                                     LEFT JOIN  AUTH_USER_ORGANIZATION t_userorg on t_userorg.USER_KEY=t_user.USER_KEY
                                     LEFT JOIN  AUTH_ORGANIZATION t_org on t_org.ORGANIZATION_KEY=t_userorg.ORGANIZATION_KEY
									 LEFT JOIN AUTH_ORGANIZATION t_parorg ON t_parorg.ORGANIZATION_KEY=t_org.PARENT_ORGANIZATION_KEY
                                     WHERE t_user.USER_ID=@USER_ID";
                using (DbCommand cmd = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(cmd, "@USER_ID", DbType.Int32, userId);
                    var result=SelectList(cmd);
                    if (result.Count() == 1)
                        return result.FirstOrDefault();
                    return null;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Error(ex.Message);
                throw new WapException(SH3H.WAP.Share.StateCode.CODE_SQL_EXECUTE_ERROR, "根据用户获取用户组织信息失败");
            }
        }

        /// <summary>
        /// 根据<see cref="IDataReader"/>数据读取器构建图层实体
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="userInfo">用户组织信息对象</param>
        /// <returns>如果构建角色对象成功返回图层实例，否则返回NULL</returns>
        public override WapUserInfo Build(IDataReader reader, WapUserInfo userInfo)
        {
            try
            {
                userInfo.UserId = reader.GetReaderValue<Int32>("USER_ID");
                userInfo.UserName = reader.GetReaderValue<string>("USER_NAME");
                userInfo.FileHash = reader.GetReaderValue<string>("FILE_HASH",null,true);
                userInfo.OrganizationName = reader.GetReaderValue<string>("ORGANIZATION_NAME", null, true);
                userInfo.OrganizationCode = reader.GetReaderValue<string>("ORGANIZATION_CODE", null, true);
                userInfo.ParentOrganizationName = reader.GetReaderValue<string>("PARENT_ORGANIZATION_NAME", null, true);
                return userInfo;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return null;
            }
        }

    }
}
