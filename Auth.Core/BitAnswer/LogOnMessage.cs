using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Core

{
    /// <summary>
    /// 登录返回信息
    /// </summary>
    public static class LogOnMessage
    {
        static LogOnMessage()
        {

        }

        /// <summary>
        /// 存储程序运行时的BitAnswer
        /// </summary>
        public static string BitAnswerSn { get; set; }

        /// <summary>
        /// 用于记录BitAnswer是否获取成功
        /// </summary>
        public static int ErrorCode { get; set; }

        /// <summary>
        /// 是否已经登录成功
        /// </summary>
        public static bool HasSuccess { get; set; }

        /// <summary>
        ///记录信息
        /// </summary>
        public static string ErrorMessage { get; set; }

        public static BitAnswer _bitAnswer = new BitAnswer();

        /// <summary>
        /// 是否已经运行
        /// </summary>
        public static bool HasRun { get; set; }

    }
}
