using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDKConsts = SH3H.SDK.Share.Consts;

namespace SH3H.WAP.Share
{
    /// <summary>
    /// 定义敏捷平台基础配置系统常量
    /// </summary>
    public static class Consts
    {
        /// <summary>
        /// 定义RESTful服务WAP地址前缀
        /// </summary>
        public const string URL_PREFIX_WAP = SDKConsts.APP_NAME + "/v2";


        #region 缓存相关

        /// <summary>
        /// 应用缓存key
        /// </summary>
        public const string URL_PREFIX_WAP_APP = "urn:wap:app";
        
        /// <summary>
        /// 用户缓存key
        /// </summary>        
        public const string URL_PREFIX_WAP_USER = "urn:wap:user";

        /// <summary>
        /// 文件上传缓存key
        /// </summary>
        public const string URL_PREFIX_WAP_FILE = "urn:wap:file";

        /// <summary>
        /// 定义Function未进数据库缓存前缀, urn:bm:function
        /// </summary>
        public const string URN_PREFIX_FUNCTION = "urn:bm:function";
        /// <summary>
        /// 定义FunctiongGroup未进数据库缓存前缀, urn:bm:funcGroup
        /// </summary>
        public const string URN_PREFIX_FUNCGROUP = "urn:bm:funcGroup";
        /// <summary>
        /// 定义Menu未进数据库缓存前缀, urn:bm:menu
        /// </summary>
        public const string URN_PREFIX_MENU = "urn:bm:menu";
        /// <summary>
        /// 定义Role未进数据库缓存前缀, urn:wap:role
        /// </summary>
        public const string URL_PREFIX_WAP_ROLE = "urn:wap:role";


        #endregion
    }
}
