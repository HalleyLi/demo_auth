using SH3H.SDK.DataAccess.Db;
using SH3H.SDK.Infrastructure.Logging;
using SH3H.SDK.Share;
using SH3H.WAP.Auth.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SH3H.SharpFrame.Data;
using SH3H.SDK.Definition.Exceptions;
using SH3H.WAP.Share;

namespace SH3H.WAP.Auth.DataAccess.SqlServer
{
    /// <summary>
    /// 定义菜单SqlServer数据库存储访问图层
    /// </summary>
    public class WapMenuStorage :
        BaseAccess<WapMenu>, IWapMenuStorage
    {
        /// <summary>
        /// 构造函数
        /// </summary>        
        public WapMenuStorage()
            : base(SH3H.SDK.Share.Consts.AUTH_DATABASE_CONNECTION_STRING) { }

        #region WapMenuStorage


        /// <summary>
        /// 获取所有的菜单项列表
        /// </summary>
        /// <returns>
        /// 返回菜单列表
        /// </returns>
        public IEnumerable<WapMenu> SelectAllAppMenus()
        {
            try
            {
                string commandText = @"SELECT * 
                                   FROM AUTH_MENU MENU 
                                   INNER JOIN AUTH_APP APP 
                                   ON MENU.APP_KEY = APP.APP_KEY
                                   INNER JOIN AUTH_FUNCTION FUNC
                                   ON FUNC.FUNC_KEY=MENU.FUNC_KEY
                                   ORDER BY MENU.MENU_SORTSN";

                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, " 获取所有菜单列表失败");
            }
        }

        /// <summary>
        /// 通过appKey获取指定应用的菜单项列表
        /// </summary>
        /// <param name="appKey">APP Key</param>
        /// <returns>
        /// 返回菜单列表
        /// </returns>
        public IEnumerable<WapMenu> SelectAppMenus(string appKey)
        {
            try
            {
                string commandText = @"SELECT AUTH_MENU.*,
                                              AUTH_FUNCTION.*,
                                              AUTH_APP.APP_IDENTITY
                                            FROM AUTH_MENU 
                                            LEFT JOIN AUTH_FUNCTION 
                                            ON AUTH_MENU.FUNC_KEY= AUTH_FUNCTION.FUNC_KEY
                                            LEFT JOIN AUTH_APP 
                                            ON AUTH_APP.APP_KEY = AUTH_MENU.APP_KEY 
                                            WHERE AUTH_MENU.APP_KEY = @appkey 
                                            ORDER BY AUTH_MENU.MENU_SORTSN";

                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@appkey", System.Data.DbType.String, appKey);
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, " 凭应用主键获取菜单列表失败");
            }
        }

        /// <summary>
        /// 通过appIdentity获取指定应用的菜单项列表
        /// </summary>
        /// <param name="appIdentity">应用标识</param>
        /// <returns>
        /// 返回菜单列表
        /// </returns>
        public IEnumerable<WapMenu> SelectAppMenusByAppId(string appIdentity)
        {
            try
            {
                string commandText = @"SELECT AUTH_MENU.*,
                                              AUTH_FUNCTION.*,
                                              AUTH_APP.APP_IDENTITY
                                            FROM AUTH_MENU 
                                            LEFT JOIN AUTH_FUNCTION 
                                            ON AUTH_MENU.FUNC_KEY= AUTH_FUNCTION.FUNC_KEY
                                            LEFT JOIN AUTH_APP 
                                            ON AUTH_APP.APP_KEY = AUTH_MENU.APP_KEY 
                                            WHERE AUTH_APP.APP_IDENTITY = @APP_IDENTITY 
                                                ORDER BY AUTH_MENU.MENU_SORTSN";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@APP_IDENTITY", System.Data.DbType.String, appIdentity);
                    var hh = SelectList(command);
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, " 凭应用标识获取菜单列表失败");
            }
        }

        /// <summary>
        /// 通过menukey获取菜单
        /// </summary>
        /// <param name="menukey"></param>
        /// <returns></returns>
        public WapMenu GetMenu(string menukey)
        {
            try
            {
                string sqlText = @"SELECT * 
                                   FROM AUTH_MENU MENU 
                                   LEFT JOIN AUTH_APP APP 
                                   ON MENU.APP_KEY = APP.APP_KEY
                                   LEFT JOIN AUTH_FUNCTION FUNC
                                   ON FUNC.FUNC_KEY=MENU.FUNC_KEY
                                   WHERE MENU.MENU_KEY = @MENU_KEY";

                using (DbCommand command = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(command, "@MENU_KEY", System.Data.DbType.String, menukey);
                    var list = SelectList(command);
                    return (list == null || list.Count() == 0) ? null : list.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, " 获取菜单对象失败");
            }

        }

        /// <summary>
        /// 通过appKey和menuName获取菜单
        /// </summary>
        /// <param name="appKey">应用key</param>
        /// <param name="menuName">菜单名称</param>
        /// <returns>菜单对象</returns>
        public WapMenu GetMenuByNameAndAppKey(string appKey, string menuName)
        {
            try
            {
                string sqlText = @"SELECT * 
                                   FROM AUTH_MENU  
                                   WHERE MENU_NAME = @MENU_NAME AND APP_KEY=@APP_KEY";

                using (DbCommand command = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(command, "@APP_KEY", System.Data.DbType.String, appKey);
                    Database.AddInParameter(command, "@MENU_NAME", System.Data.DbType.String, menuName);
                    var list = SelectList(command);
                    return (list == null || list.Count() == 0) ? null : list.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, " 获取菜单对象失败");
            }

        }


        /// <summary>
        /// 通过menukey数组获取菜单列表
        /// </summary>
        /// <param name="menukeys"></param>
        /// <returns></returns>
        public IEnumerable<WapMenu> GetMenuList(string[] menukeys)
        {
            try
            {
                StringBuilder strB = new StringBuilder();
                for (int i = 0; i < menukeys.Length; i++)
                {
                    strB.AppendFormat("'{0}',", menukeys[i]);
                }
                string str = strB.ToString();
                str = str.Substring(0, str.Length - 1);
                str = "(" + str + ")";

                string sqlText = @"SELECT * 
                                   FROM AUTH_MENU MENU 
                                   INNER JOIN AUTH_APP APP 
                                   ON MENU.APP_KEY = APP.APP_KEY
                                   INNER JOIN AUTH_FUNCTION FUNC
                                   ON FUNC.FUNC_KEY=MENU.FUNC_KEY
                                   WHERE MENU.MENU_KEY in  @str";

                using (DbCommand command = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(command, "@str", System.Data.DbType.String, str);
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, " 获取菜单列表失败");
            }

        }

        /// <summary>
        /// 通过菜单对象条件查询菜单对象列表 
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public IEnumerable<WapMenu> GetMenu(WapMenu menu)
        {
            return null;
        }

        /// <summary>
        /// 通过查询条件获取菜单列表
        /// </summary>
        /// <param name="userkey">用户Key</param>
        /// <param name="appkey">应用Key</param>
        /// <returns></returns>
        public IEnumerable<WapMenu> GetUserMenu(string userkey, string appkey)
        {
            try
            {
                string sqlText = @"SELECT  AUTH_MENU.*,
                                           AUTH_APP.*,
                                           AUTH_FUNCTION.*
                                FROM   AUTH_FUNCTION INNER JOIN  AUTH_USER_ROLE
                                                     INNER JOIN  AUTH_ROLE ON AUTH_USER_ROLE.ROLE_KEY = AUTH_ROLE.ROLE_KEY
                                                     INNER JOIN  AUTH_USER ON AUTH_USER_ROLE.USER_KEY = AUTH_USER.USER_KEY 
                                                     INNER JOIN  AUTH_ROLE_FUNCTION ON AUTH_ROLE.ROLE_KEY = AUTH_ROLE_FUNCTION.ROLE_KEY ON AUTH_FUNCTION.FUNC_KEY = AUTH_ROLE_FUNCTION.FUNC_KEY
                                                     INNER JOIN  AUTH_FUNCTION_GROUP ON AUTH_FUNCTION.FUNC_GROUP_KEY = AUTH_FUNCTION_GROUP.FUNC_GROUP_KEY
                                                     INNER JOIN  AUTH_APP ON AUTH_FUNCTION_GROUP.FUNC_APP_KEY = AUTH_APP.APP_KEY 
                                                     INNER JOIN  AUTH_MENU ON AUTH_MENU.FUNC_KEY=AUTH_FUNCTION.FUNC_KEY
                              WHERE  AUTH_USER.USER_KEY=@userkey AND AUTH_APP.APP_KEY=@appkey";

                using (DbCommand command = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(command, "@appkey", System.Data.DbType.String, appkey);
                    Database.AddInParameter(command, "@userkey", System.Data.DbType.String, userkey);
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, " 凭用户获取菜单列表失败");
            }
        }

        /// <summary>
        /// 通过查询条件获取菜单列表
        /// </summary>
        /// <param name="useraccount">用户账号</param>
        /// <param name="appidentity">应用标识</param>
        /// <returns></returns>
        public IEnumerable<WapMenu> GetMenusByUA(string useraccount, string appidentity)
        {
            try
            {
                string sqlText = @"SELECT DISTINCT MENUS.* ,FUNC.* FROM
                                    (SELECT MENU.*, APP.APP_IDENTITY FROM AUTH_MENU MENU INNER JOIN AUTH_APP APP ON MENU.APP_KEY = APP.APP_KEY 
                                        WHERE APP.APP_IDENTITY = @APP_IDENTITY 
                                          AND APP.APP_ACTIVE = 1
                                          AND MENU.MENU_ACTIVE = 1
                                          AND MENU.MENU_IFSHOW = 1) MENUS
                                    INNER JOIN AUTH_FUNCTION FUNC ON MENUS.FUNC_KEY = FUNC.FUNC_KEY
                                    INNER JOIN AUTH_FUNCTION_GROUP FUNCGRP ON FUNCGRP.FUNC_GROUP_KEY = FUNC.FUNC_GROUP_KEY
                                    INNER JOIN AUTH_ROLE_FUNCTION FUNCROLE ON FUNCROLE.FUNC_KEY = FUNC.FUNC_KEY
                                    INNER JOIN AUTH_ROLE ROLE ON ROLE.ROLE_KEY = FUNCROLE.ROLE_KEY
                                    INNER JOIN AUTH_USER_ROLE USERROLE ON USERROLE.ROLE_KEY = ROLE.ROLE_KEY
                                    INNER JOIN AUTH_USER U ON U.USER_KEY = USERROLE.USER_KEY
                                WHERE U.USER_ACCOUNT = @USER_ACCOUNT 
                                    AND FUNC.FUNC_ACTIVE = 1
                                    AND ROLE.ROLE_ACTIVE = 1
                                    AND U.USER_ACTIVE = 1
                                ORDER BY MENUS.MENU_SORTSN";

                using (DbCommand command = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(command, "@APP_IDENTITY", System.Data.DbType.String, appidentity);
                    Database.AddInParameter(command, "@USER_ACCOUNT", System.Data.DbType.String, useraccount);
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, " 凭查询条件获取菜单列表失败");
            }
        }

        /// <summary>
        /// 通过功能点Key列表获取菜单列表
        /// </summary>
        /// <param name="funckey"></param>
        /// <returns></returns>
        public IEnumerable<WapMenu> GetFuncMenu(string funckey)
        {
            try
            {
                string sqlText = @"SELECT * 
                                   FROM AUTH_MENU MENU 
                                   INNER JOIN AUTH_APP APP 
                                   ON MENU.APP_KEY = APP.APP_KEY
                                   INNER JOIN AUTH_FUNCTION FUNC
                                   ON FUNC.FUNC_KEY=MENU.FUNC_KEY
                                   WHERE MENU.FUNC_KEY  = @funkey ";

                using (DbCommand command = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(command, "@funkey", System.Data.DbType.String, funckey);
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "凭功能点获取菜单失败");
            }


        }

        /// <summary>
        /// 查询指定应用中对应URL的菜单项
        /// </summary>
        /// <param name="appname">应用名称</param>
        /// <param name="url">URL地址</param>
        /// <returns>返回菜单项列表</returns>
        public IEnumerable<WapMenu> GetMenuByUrl(string appname, string url)
        {
            try
            {
                string sqlText = @"SELECT * 
                                   FROM AUTH_MENU MENU 
                                   INNER JOIN AUTH_APP APP 
                                   ON MENU.APP_KEY = APP.APP_KEY
                                   INNER JOIN AUTH_FUNCTION FUNC
                                   ON FUNC.FUNC_KEY=MENU.FUNC_KEY
                                   WHERE MENU.MENU_URL = @url 
                                   AND APP.APP_NAME = @appname";

                using (DbCommand command = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(command, "@url", System.Data.DbType.String, url);
                    Database.AddInParameter(command, "@appname", System.Data.DbType.String, appname);
                    return SelectList(command);
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "凭URL获取菜单失败");
            }
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menu">菜单对象</param>
        /// <returns>是否添加成功</returns>
        public bool AddMenu(WapMenu menu)
        {
            try
            {
                if (GetMenuByNameAndAppKey(menu.AppKey, menu.Name) != null)
                {
                    throw new WapException(StateCode.CODE_ARGUMENT_DATA_REPEAT, "菜单新增失败,该应用中中已有该名称的菜单");
                }
                else
                {
                    string commandText = @"INSERT INTO AUTH_MENU(MENU_KEY,MENU_NAME,PARENT_MENU_KEY,MENU_SORTSN,MENU_ACTIVE,MENU_COMMENT,FUNC_KEY,MENU_URL,COMPONENT_ID,ICON_CSS,APP_KEY,MENU_IFSHOW,PARAMETER,EXTEND)
                                                     VALUES(@MENU_KEY,@MENU_NAME,@PARENT_MENU_KEY,@MENU_SORTSN,@MENU_ACTIVE,@MENU_COMMENT,@FUNC_KEY,@MENU_URL,@COMPONENT_ID,@ICON_CSS,@APP_KEY,@MENU_IFSHOW,@PARAMETER,@EXTEND)";
                    using (DbCommand command = Database.GetSqlStringCommand(commandText))
                    {
                        Database.AddInParameter(command, "@MENU_KEY", DbType.String, menu.MenuKey);
                        Database.AddInParameter(command, "@MENU_NAME", DbType.String, menu.Name);
                        Database.AddInParameter(command, "@PARENT_MENU_KEY", DbType.String, menu.ParentKey);
                        Database.AddInParameter(command, "@MENU_SORTSN", DbType.Int32, menu.Sortsn);
                        Database.AddInParameter(command, "@MENU_ACTIVE", DbType.Boolean, menu.IsActive);
                        Database.AddInParameter(command, "@MENU_COMMENT", DbType.String, menu.Comment);
                        Database.AddInParameter(command, "@FUNC_KEY", DbType.String, menu.FunKey == null ? string.Empty : menu.FunKey);
                        Database.AddInParameter(command, "@MENU_URL", DbType.String, menu.Url == null ? string.Empty : menu.Url);
                        Database.AddInParameter(command, "@COMPONENT_ID", DbType.String, menu.ComponentId);
                        Database.AddInParameter(command, "@ICON_CSS", DbType.String, menu.Css);
                        Database.AddInParameter(command, "@APP_KEY", DbType.String, menu.AppKey);
                        Database.AddInParameter(command, "@MENU_IFSHOW", DbType.Boolean, menu.IsShow);
                        Database.AddInParameter(command, "@PARAMETER", DbType.String, menu.Parameter);
                        Database.AddInParameter(command, "@EXTEND", DbType.String, menu.Extend);

                        return ExecuteNonQuery(command) > 0;
                    }
                }
            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "菜单新增失败！");
            }
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="menuKey">菜单Key</param>
        /// <param name="menu">菜单对象</param>
        /// <returns>是否修改成功</returns>
        public bool UpdateMenu(string menuKey, WapMenu menu)
        {
            try
            {
                WapMenu tempMenu = GetMenuByNameAndAppKey(menu.AppKey, menu.Name);
                if (tempMenu != null && tempMenu.MenuKey != menuKey)
                {
                    throw new WapException(StateCode.CODE_ARGUMENT_DATA_REPEAT, "菜单更新失败,该应用中中已有该名称的菜单！");
                }
                else
                {
                    string commandText = @"UPDATE AUTH_MENU 
                                       SET  MENU_NAME=@MENU_NAME,
                                            MENU_COMMENT =@MENU_COMMENT,
                                            PARAMETER = @PARAMETER,
                                            EXTEND = @EXTEND,
                                            FUNC_KEY=@FUNC_KEY,
                                            MENU_URL=@MENU_URL,
                                            COMPONENT_ID=@COMPONENT_ID,
                                            ICON_CSS=@ICON_CSS,
                                            MENU_IFSHOW=@MENU_IFSHOW
                                      where MENU_KEY = @MENU_KEY";
                    using (DbCommand command = Database.GetSqlStringCommand(commandText))
                    {
                        Database.AddInParameter(command, "@MENU_NAME", DbType.String, menu.Name);
                        Database.AddInParameter(command, "@MENU_COMMENT", DbType.String, menu.Comment);
                        Database.AddInParameter(command, "@PARAMETER", DbType.String, menu.Parameter);
                        Database.AddInParameter(command, "@EXTEND", DbType.String, menu.Extend);
                        Database.AddInParameter(command, "@FUNC_KEY", DbType.String, menu.FunKey);
                        Database.AddInParameter(command, "@MENU_URL", DbType.String, menu.Url == null ? string.Empty : menu.Url);
                        Database.AddInParameter(command, "@COMPONENT_ID", DbType.String, menu.ComponentId);
                        Database.AddInParameter(command, "@ICON_CSS", DbType.String, menu.Css);
                        Database.AddInParameter(command, "@MENU_IFSHOW", DbType.Boolean, menu.IsShow);
                        Database.AddInParameter(command, "@MENU_KEY", DbType.String, menuKey);

                        return ExecuteNonQuery(command) > 0;
                    }
                }

            }
            catch (WapException ex)
            {
                throw new WapException(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "菜单更新失败！");
            }
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuKey">菜单Key</param>
        /// <returns>是否修改成功</returns>
        public bool DeleteMenu(string menuKey)
        {
            try
            {
                string commandText = @"DELETE FROM AUTH_MENU  WHERE MENU_KEY = @MENU_KEY";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@MENU_KEY", DbType.String, menuKey);

                    return ExecuteNonQuery(command) > 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "菜单删除失败");
            }
        }

        /// <summary>
        /// 禁用菜单
        /// </summary>
        /// <param name="menuKey">菜单标识</param>
        /// <returns>是否禁用成功</returns>
        public bool DeactiveMenu(string menuKey)
        {
            try
            {
                string commandText = @"UPDATE AUTH_MENU 
                                       SET  MENU_ACTIVE=0
                                      where MENU_KEY = @MENU_KEY";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@MENU_KEY", DbType.String, menuKey);

                    return ExecuteNonQuery(command) > 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "菜单更新状态失败");
            }
        }

        /// <summary>
        /// 启用菜单
        /// </summary>
        /// <param name="menuKey">菜单标识</param>
        /// <returns>是否启用成功</returns>
        public bool ActiveMenu(string menuKey)
        {
            try
            {
                string commandText = @"UPDATE AUTH_MENU 
                                       SET  MENU_ACTIVE=1
                                      where MENU_KEY = @MENU_KEY";
                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@MENU_KEY", DbType.String, menuKey);

                    return ExecuteNonQuery(command) > 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "菜单更新状态失败");
            }
        }

        /// <summary>
        /// 修改菜单状态菜单
        /// </summary>
        /// <param name="menuKey"></param>
        /// <param name="active"></param>
        /// <returns></returns>
        public bool UpdateMenuState(string menuKey, bool active)
        {
            try
            {
                string commandText;
                if (active)
                {
                    commandText = @"UPDATE AUTH_MENU 
                                       SET  MENU_ACTIVE=@MENU_ACTIVE
                                      where MENU_KEY = @MENU_KEY";
                }
                else
                {
                    commandText = @"UPDATE AUTH_MENU 
                                       SET  MENU_ACTIVE=@MENU_ACTIVE,
                                            MENU_IFSHOW =0
                                      where MENU_KEY = @MENU_KEY";
                }

                using (DbCommand command = Database.GetSqlStringCommand(commandText))
                {
                    Database.AddInParameter(command, "@MENU_KEY", DbType.String, menuKey);
                    Database.AddInParameter(command, "@MENU_ACTIVE", DbType.Boolean, active);

                    return ExecuteNonQuery(command) > 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "菜单更新状态失败");
            }
        }


        /// <summary>
        /// 根据菜单标识更新父菜单标识
        /// </summary>
        /// <param name="menuKey">菜单标识</param>
        /// <param name="parentMenuKey">父菜单标识</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateMenuParent(string menuKey, string parentMenuKey)
        {
            try
            {
                string sqlCommand = @"UPDATE AUTH_MENU 
                                        SET PARENT_MENU_KEY = @parent_menu_key
                                      WHERE MENU_KEY=@menu_key";
                using (DbCommand command = Database.GetSqlStringCommand(sqlCommand))
                {
                    Database.AddInParameter(command, "@parent_menu_key", DbType.String, parentMenuKey);
                    Database.AddInParameter(command, "@menu_key", DbType.String, menuKey);
                    return ExecuteNonQuery(command) > 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_SQL_EXECUTE_ERROR, "菜单更新上级失败");
            }

        }

        /// <summary>
        /// 统一更新排序值
        /// </summary>
        /// <param name="sortIndexes">编号索引号字典</param>
        public bool UpdateSortIndexByKey(Dictionary<string, int> sortIndexes)
        {
            return base.Transact(trans =>
            {
                foreach (KeyValuePair<string, int> index in sortIndexes)
                {
                    if (!UpdateIndexByMenuKey(index.Key, index.Value))
                        return false;
                }
                return true;
            });
        }

        /// <summary>
        /// 根据菜单KEY更新菜单排序值
        /// </summary>
        /// <param name="menuKey">关键码</param>
        /// <param name="sortIndex">排序索引值</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateIndexByMenuKey(string menuKey, int sortIndex)
        {
            try
            {
                string sqlText = @"UPDATE auth_menu 
                                   SET menu_sortsn=@menu_sortsn
                                   WHERE menu_key=@menu_key";
                using (var cmd = Database.GetSqlStringCommand(sqlText))
                {
                    Database.AddInParameter(cmd, "@menu_sortsn", DbType.Int32, sortIndex);
                    Database.AddInParameter(cmd, "@menu_key", DbType.String, menuKey);
                    return ExecuteNonQuery(cmd) > 0;
                }
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                return false;
            }
        }

        #endregion

        /// <summary>
        /// 创建对象实例
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <returns>返回对象实例</returns>
        protected override WapMenu CreateObject(IDataReader reader)
        {
            return new WapMenu();
        }


        /// <summary>
        /// 根据<see cref="IDataReader"/>数据读取器构建菜单实体
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <param name="menu">菜单对象</param>
        /// <returns>如果构建菜单成功返回图层实例，否则返回NULL</returns>
        public override WapMenu Build(IDataReader reader, WapMenu menu)
        {
            try
            {
                menu.MenuKey = reader.GetReaderValue<string>("MENU_KEY");
                menu.Name = reader.GetReaderValue<string>("MENU_NAME");
                menu.ParentKey = reader.GetReaderValue<string>("PARENT_MENU_KEY");
                menu.Sortsn = reader.GetReaderValue<int>("MENU_SORTSN");
                menu.IsActive = reader.GetReaderValue<bool>("MENU_ACTIVE");
                menu.Comment = reader.GetReaderValue<string>("MENU_COMMENT", null, true);
                menu.FunKey = reader.GetReaderValue<string>("FUNC_KEY");
                menu.Url = reader.GetReaderValue<string>("MENU_URL");
                menu.ComponentId = reader.GetReaderValue<string>("COMPONENT_ID", null, true);
                menu.Css = reader.GetReaderValue<string>("ICON_CSS", null, true);
                menu.AppKey = reader.GetReaderValue<string>("APP_KEY");
                menu.IsShow = reader.GetReaderValue<bool>("MENU_IFSHOW");
                menu.Parameter = reader.GetReaderValue<string>("PARAMETER", null, true);
                menu.Extend = reader.GetReaderValue<string>("EXTEND", null, true);
                menu.FunCode = reader.GetReaderValue<string>("FUNC_CODE", null, true);
                menu.FunActive = reader.GetReaderValue<bool?>("FUNC_ACTIVE", null, true);
                menu.IsFuncActive = reader.GetReaderValue<bool>("FUNC_ACTIVE", true, true);
                menu.AppIdentitfy = reader.GetReaderValue<string>("APP_IDENTITY", null, true);

                return menu;
            }
            catch (Exception ex)
            {
                LogManager.Get().Throw(ex);
                throw new WapException(StateCode.CODE_MODEL_CONVERT_ERROR, "菜单模型转换失败");
            }
        }


        public bool CreateSchema()
        {
            throw new NotImplementedException();
        }
    }
}
