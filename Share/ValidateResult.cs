using SH3H.SDK.Definition.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Share
{
    /// <summary>
    /// 验证结果类
    /// </summary>
    public class ValidateResult
    {
        /// <summary>
        /// 是否验证通过
        /// </summary>
        public bool IsValid { get; private set; }
        /// <summary>
        /// 错误集
        /// </summary>
        public IList<ErrorState> Errors { get; private set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ValidateResult()
        {
            IsValid = true;
            Errors = new List<ErrorState>();
        }
        /// <summary>
        /// 添加错误
        /// </summary>
        /// <param name="code"></param>
        /// <param name="msg"></param>
        public void AddError(int code, string msg)
        {
            IsValid = false;

            Errors.Add(new ErrorState() { Code = code, Message = msg });
        }
        /// <summary>
        /// 组合异常
        /// </summary>
        /// <returns></returns>
        public WapException BuildException()
        {
            if (IsValid || Errors.Count() == 0)
                return null;

            var e = Errors.First();

            return new WapException(e.Code, e.Message);
        }
    }
}
