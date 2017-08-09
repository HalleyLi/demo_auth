using SH3H.WAP.Auth.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SH3H.WAP.Auth.Model.Dto;
using SH3H.SDK.DataAccess.Db;
using System.Data.Common;
using System.Data;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Share;
using SH3H.SharpFrame.Data;

namespace SH3H.WAP.Auth.DataAccess.SqlServer
{
    /// <summary>
    /// 定义功能点组关联SqlServer数据库访问对象
    /// </summary>
    public class WapFuncGroupRelativeStorage :
        BaseAccess<WapFuncGroupRelativeDto>, IWapFuncGroupRelativeStorage
    {
        public WapFuncGroupRelativeStorage() : base(SH3H.SDK.Share.Consts.CONFIGURE_DATABASE_CONNECTION_STRING) { }

        public bool CreateSchema()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 凭功能组主键 获取所属 功能点(附功能组)
        /// </summary>
        /// <param name="funcGroupKey">功能点主键</param>
        /// <returns>功能点(附功能组)列表</returns>
        public IEnumerable<WapFuncGroupRelativeDto> GetFunctionGroup(string funcGroupKey)
        {
            try
            {
                string commandText = @"SELECT AF.FUNC_KEY
                                              ,AF.FUNC_NAME
                                              ,AF.FUNC_CODE
                                              ,AF.FUNC_TEMPLATE_KEY
                                              ,AF.FUNC_PYCODE
                                              ,AF.FUNC_SORTSN
                                              ,AF.FUNC_ACTIVE
                                              ,AF.FUNC_COMMENT
                                              ,AF.EXTEND
	                                          ,AF.FUNC_GROUP_KEY
                                          FROM AUTH_FUNCTION AF
                                          WHERE AF.FUNC_GROUP_KEY=@func_group_key";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "func_group_key", DbType.String, funcGroupKey);

                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, " 凭功能组主键获取所属功能点失败");
            }
        }

        /// <summary>
        /// 根据<see cref="IDataReader"/>数据读取器构建图层实体
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="funcRelative">功能点(附功能组关联)对象</param>
        /// <returns>如果构建角色对象成功返回图层实例，否则返回NULL</returns>
        public override WapFuncGroupRelativeDto Build(IDataReader reader, WapFuncGroupRelativeDto funcRelative)
        {
            try
            {
                funcRelative.Key = reader.GetReaderValue<string>("FUNC_KEY");
                funcRelative.Name = reader.GetReaderValue<string>("FUNC_NAME");
                funcRelative.Code = reader.GetReaderValue<string>("FUNC_CODE");
                funcRelative.Pycode = reader.GetReaderValue<string>("FUNC_PYCODE");
                funcRelative.Sortsn = reader.GetReaderValue<decimal>("FUNC_SORTSN");
                funcRelative.Comment = reader.GetReaderValue<string>("FUNC_COMMENT",null,true);
                funcRelative.Active = reader.GetReaderValue<bool>("FUNC_ACTIVE");

                funcRelative.FuncGroupKey = reader.GetReaderValue<string>("FUNC_GROUP_KEY");
                funcRelative.IsRelative = string.IsNullOrEmpty(funcRelative.FuncGroupKey) || funcRelative.FuncGroupKey == new Guid().ToString();
                return funcRelative;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_MODEL_CONVERT_ERROR, "功能组关联模型转换失败");
            }
        }

        /// <summary>
        /// 更新功能组与功能点关联
        /// </summary>
        /// <param name="relationModel">更新组对象</param>
        /// <returns>是否成功</returns>
        public bool UpdateFunctionGroupRelation(IEnumerable<WapFuncGroupRelativeDto>  add,IEnumerable<WapFuncGroupRelativeDto>  del)
        {
            return base.Transact(trans =>
            {

                if (del != null && del.Count() > 0)
                {
                    foreach (var item in del)
                    {
                        //凭 roleKey和funcKye删除

                        if (!UpdateFunctionGroupRelation(item.Key, item.FuncGroupKey, trans))
                        {
                            return false;
                        }
                    }
                }

                if (add != null && add.Count() > 0)
                {
                    foreach (var item in add)
                    {
                        if (!UpdateFunctionGroupRelation(item.Key, item.FuncGroupKey, trans))
                        {
                            return false;
                        }
                    }
                }
                return true;
            });
        }

        /// <summary>
        /// 修改功能点所属功能组
        /// </summary>
        /// <param name="funckey"></param>
        /// <param name="functGroupKey"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        private bool UpdateFunctionGroupRelation(string funckey, string functGroupKey, DbTransaction trans)
        {
            int result = -1;
            try
            {
                //修改方法
                string modifyFunctionCommandText = @"UPDATE  AUTH_FUNCTION 
                                           SET  FUNC_GROUP_KEY  = @func_group_key
                                         WHERE FUNC_KEY=@func_key";
                using (DbCommand modifyFunctionCommand = Database.GetSqlStringCommand(modifyFunctionCommandText))
                {

                    Database.AddInParameter(modifyFunctionCommand, "@func_key", System.Data.DbType.String, funckey);
                    Database.AddInParameter(modifyFunctionCommand, "@func_group_key", System.Data.DbType.String, functGroupKey);

                    result = ExecuteNonQuery(modifyFunctionCommand, trans);
                    return result >= 0;
                }
            }

            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "修改功能点所属功能组失败");

            }
        }



    }
}
