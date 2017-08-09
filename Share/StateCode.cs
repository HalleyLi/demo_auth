using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Share
{
    // <summary>
    /// 定义权限认证系统返回码
    /// </summary>
    /// 统一以0x1002开头的十六进制数
    public static class StateCode
    {
        #region SDK DEFINE CODE

        //*------------------------------------------------------*/
        //*                <平台级定义的返回码>                   */  
        //*                                                      */                           
        //*------------------------------------------------------*/

        /// <summary>
        /// 运行成功
        /// </summary>
        //public const int CODE_SUCCESS = 0;

        /// <summary>
        /// 失败状态
        /// </summary>
        //public const int CODE_FAILURE = 0x10000001;

        /// <summary>
        /// 启动失败
        /// </summary>
        //public const int CODE_START_ERROR = 0x10000002;

        /// <summary>
        /// 停止失败
        /// </summary>
        //public const int CODE_STOP_ERROR = 0x10000003;

        /// <summary>
        /// 无效的命令行参数
        /// </summary>
        //public const int CODE_INVALID_ARGUMENTS = 0x10000004;

        /// <summary>
        /// 初始化失败
        /// </summary>
        //public const int CODE_INIT_FAILED = 0x10000005;

        /// <summary>
        /// 找不到指定文件
        /// </summary>
        //public const int CODE_FILE_NOT_FOUND = 0x10000006;

        /// <summary>
        /// 文件名非法
        /// </summary>
        //public const int CODE_FILENAME_INVALID = 0x10000007;

        /// <summary>
        /// 找不到指定配置文件
        /// </summary>
        //public const int CODE_CONFIG_FILE_NOT_FOUND = 0x10000008;

        /// <summary>
        /// 加载配置文件错误
        /// </summary>
        //public const int CODE_LOAD_CONFIG_FILE_ERROR = 0x10000009;

        /// <summary>
        /// 容器初始化失败
        /// </summary>
        //public const int CODE_INIT_CONTAINER_ERROR = 0x1000000A;

        /// <summary>
        /// 键已经存在
        /// </summary>
        //public const int CODE_KEY_HAS_EXISTED = 0x1000000B;

        /// <summary>
        /// 找不到键值
        /// </summary>
        //public const int CODE_KEY_NOT_FOUND = 0x1000000C;

        /// <summary>
        /// 未知状态
        /// </summary>
        //public const int CODE_UNKNOWN = 0x1FFFFFFD;

        /// <summary>
        /// 异常状态
        /// </summary>
        //public const int CODE_EXCEPTION = 0x1FFFFFFE;

        /// <summary>
        /// 发生未捕获异常
        /// </summary>
        //public const int CODE_UNHANDLED_EXCEPTION = 0x1FFFFFFF;

        #endregion

        #region AUTH CODE

        /// <summary>
        /// 用户名错误
        /// </summary>
        public const int CODE_USERNAME_ERROR = 0x10020001;

        /// <summary>
        /// 密码错误
        /// </summary>
        public const int CODE_PASSWORD_ERROR = 0x10020002;

        /// <summary>
        /// 未知的用户
        /// </summary>
        public const int CODE_USER_UNKNOWN = 0x10020003;

        /// <summary>
        /// 非法的AccessToken
        /// </summary>
        public const int CODE_ACCESS_TOKEN_INVALID = 0x10020004;

        /// <summary>
        /// AccessToken过期
        /// </summary>
        public const int CODE_ACCESS_TOKEN_EXPIRED = 0x10020005;

        /// <summary>
        /// 当前应用程序已达到最大登录用户数
        /// </summary>
        public const int CODE_ACCESS_USER_MAX = 0x10020006;

        /// <summary>
        /// 当前用户已经在其它终端登录应用程序
        /// </summary>
        public const int CODE_ACCESS_USER_EXIST = 0x10020007;

        /// <summary>
        /// 用户名或者密码错误
        /// </summary>
        public const int CODE_USERNAME_PASSWORD_ERROR = 0x10020008;

        /// <summary>
        /// 非法的应用
        /// </summary>
        public const int CODE_APP_INVALID = 0x10020009;

        /// <summary>
        /// BitAnswer认证失败
        /// </summary>
        public const int CODE_BITANSWER_FAILED = 0x1002000A;

        /// <summary>
        /// 应用标识不存在
        /// </summary>
        public const int CODE_APP_INEXIST = 0x1002000B;

        /// <summary>
        /// 应用标识已停用
        /// </summary>
        public const int CODE_APP_UNUSE = 0x1002000C;

        /// <summary>
        /// 用户无当前应用的权限
        /// </summary>
        public const int CODE_APP_ACCESS_INVALID = 0x1002000D;

        /// <summary>
        /// 账号不存在
        /// </summary>
        public const int CODE_ACCOUNT_INEXIST = 0x1002000E;

        /// <summary>
        /// 账号已停用
        /// </summary>
        public const int CODE_ACCOUNT_UNUSE = 0x1002000F;

        /// <summary>
        /// 工号不存在
        /// </summary>
        public const int CODE_WORKNO_INEXIST = 0x10020010;

        /// <summary>
        /// 范围的异常代码定义，（时间，数值）
        /// </summary>
        public const int CODE_STARTEND_EXCEPTION = 0x10020011;

        #endregion

        #region 业务无关级别状态码

        /// <summary>
        /// 模型转换错误
        /// </summary>
        public const int CODE_MODEL_CONVERT_ERROR = 0x1000000D;

        /// <summary>
        /// SQL执行异常
        /// </summary>
        public const int CODE_SQL_EXECUTE_ERROR = 0x1000000E;

        /// <summary>
        /// 模型不存在
        /// </summary>
        public const int CODE_MODEL_NOT_EXIST = 0x1000000F;
        /// <summary>
        /// 缓存异常
        /// </summary>
        public static int CODE_CACHE_ERROR = 0x10000010;

        /// <summary>
        /// 参数不允许为空
        /// </summary>
        public const int CODE_ARGUMENT_NULL = 0x10000011;

        /// <summary>
        /// 参数超长
        /// </summary>
        public const int CODE_ARGUMENT_LENGTH = 0x10000012;

        /// <summary>
        /// 参数类型错误
        /// </summary>
        public const int CODE_ARGUMENT_TYPE_ERROR = 0x10000013;

        /// <summary>
        /// 获取序号错误
        /// </summary>
        public const int CODE_GET_SEQ_ERROR = 0x10000014;

        /// <summary>
        /// 参数范围错误
        /// </summary>
        public const int CODE_ARGUMENT_LIMIT_ERROR = 0x10000015;

        /// <summary>
        /// 参数不一致
        /// </summary>
        public const int CODE_ARGUMENT_NOT_EQUAL = 0x10000016;

        /// <summary>
        /// 命名重复
        /// </summary>
        public const int CODE_ARGUMENT_DATA_REPEAT = 0x10000017;

        /// <summary>
        /// 应用标识已存在
        /// </summary>
        public const int CODE_APP_EXIST = 0x10000018;

        #endregion

        /// <summary>
        /// 获取返回码消息
        /// </summary>
        /// <param name="code">返回状态码</param>
        /// <returns>返回码消息</returns>
        public static string GetMessage(int code)
        {
            string message = "";
            _errorCodeDic.TryGetValue(code, out message);
            return message;
        }

        private static readonly Dictionary<int, string> _errorCodeDic;

        static StateCode()
        {
            _errorCodeDic = new Dictionary<int, string>();

            _errorCodeDic.Add(CODE_USERNAME_ERROR, "用户名错误。");
            _errorCodeDic.Add(CODE_PASSWORD_ERROR, "密码错误。");
            _errorCodeDic.Add(CODE_USER_UNKNOWN, "未知的用户。");
            _errorCodeDic.Add(CODE_ACCESS_TOKEN_INVALID, "用户名错误。");
            _errorCodeDic.Add(CODE_USERNAME_ERROR, "非法的AccessToken。");
            _errorCodeDic.Add(CODE_ACCESS_TOKEN_EXPIRED, "AccessToken过期。");
            _errorCodeDic.Add(CODE_ACCESS_USER_MAX, "当前应用程序已达到最大登录用户数。");
            _errorCodeDic.Add(CODE_ACCESS_USER_EXIST, "当前用户已经在其它终端登录应用程序。");
            _errorCodeDic.Add(CODE_USERNAME_PASSWORD_ERROR, "用户名错误。");
            _errorCodeDic.Add(CODE_USERNAME_ERROR, "用户名或者密码错误。");
            _errorCodeDic.Add(CODE_APP_INVALID, "非法的应用。");
            _errorCodeDic.Add(CODE_BITANSWER_FAILED, "BitAnswer认证失败。");
            _errorCodeDic.Add(CODE_APP_INEXIST, "应用标识不存在。");
            _errorCodeDic.Add(CODE_APP_UNUSE, "应用程序已停用。");
            _errorCodeDic.Add(CODE_APP_ACCESS_INVALID, "用户无当前应用的权限。");
            _errorCodeDic.Add(CODE_ACCOUNT_INEXIST, "账号不存在");
            _errorCodeDic.Add(CODE_ACCOUNT_UNUSE, "账号已停用");

            _errorCodeDic.Add(CODE_MODEL_CONVERT_ERROR, "模型转换错误");
            _errorCodeDic.Add(CODE_SQL_EXECUTE_ERROR, "SQL执行异常");
            _errorCodeDic.Add(CODE_MODEL_NOT_EXIST, "模型不存在");
            _errorCodeDic.Add(CODE_CACHE_ERROR, "缓存异常");
            _errorCodeDic.Add(CODE_ARGUMENT_NULL, "参数不允许为空");
            _errorCodeDic.Add(CODE_ARGUMENT_LENGTH, "参数超长");
            _errorCodeDic.Add(CODE_ARGUMENT_TYPE_ERROR, "参数类型错误");
            _errorCodeDic.Add(CODE_GET_SEQ_ERROR, "获取序号错误");
            _errorCodeDic.Add(CODE_ARGUMENT_LIMIT_ERROR, "参数范围错误");
            _errorCodeDic.Add(CODE_ARGUMENT_NOT_EQUAL, "参数不一致");
            _errorCodeDic.Add(CODE_ARGUMENT_DATA_REPEAT, "命名重复");
        }

    }
}
