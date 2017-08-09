using Microsoft.VisualStudio.TestTools.UnitTesting;
using SH3H.SDK.Definition.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Tools
{
    /// <summary>
    /// ExpectedExceptionAssert
    /// </summary>
    public class ExpectedExceptionAssert
    {
        /// <summary>
        /// Checks to make sure that the input delegate throws a exception of type TException. 
        /// 
        /// The type of exception expected. 
        /// The block of code to execute to generate the exception. 
        /// </summary>
        /// <typeparam name="TException">预期捕获异常的类型</typeparam>
        /// <param name="action">执行的代码</param>
        public static void Throws<TException>(Action action) where TException : System.Exception
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.GetType() == typeof(TException),
                        "Expected exception of type " + typeof(TException) +
                        ", but type of " + ex.GetType() + " was thrown instead.");
                return;
            }

            Assert.Fail("Expected exception of type " + typeof(TException) + " but no exception was thrown.");
        }

        /// <summary>
        ///
        /// Checks to make sure that the input delegate throws a exception of type TException. 
        /// 
        /// The type of exception expected. 
        /// The block of code to execute to generate the exception. 
        /// </summary>
        /// <typeparam name="TException">预期捕获异常的类型</typeparam>
        /// <param name="expectedMessage">预期捕获异常的异常消息</param>
        /// <param name="action">执行的代码</param>
        public static void Throws<TException>(string expectedMessage, Action action) where TException : System.Exception
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.GetType() == typeof(TException),
                        "Expected exception of type " + typeof(TException) +
                        ", but type of " + ex.GetType() + " was thrown instead.");

                Assert.AreEqual(expectedMessage, ex.Message,
                        "Expected exception with a message of '" + expectedMessage +
                        "' but exception with message of '" + ex.Message + "' was thrown instead.");
                return;
            }

            Assert.Fail("Expected exception of type " + typeof(TException) + " but no exception was thrown.");
        }


        /// <summary>
        /// Checks to make sure that the input delegate throws a exception of type TException. 
        /// 
        /// The type of exception expected. 
        /// The block of code to execute to generate the exception. 
        /// </summary>
        /// <typeparam name="TException">预期捕获异常的类型</typeparam>
        /// <param name="code">预期捕获异常的异常代码</param>
        /// <param name="action">执行的代码</param>
        public static void Throws<TException>(int code, Action action) where TException : SH3H.SDK.Definition.Exceptions.WapException
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.GetType() == typeof(TException),
                        "Expected exception of type " + typeof(TException) +
                        ", but type of " + ex.GetType() + " was thrown instead.");

                if (ex is WapException)
                {
                    int actualCode = ((WapException)ex).Code;
                    Assert.AreEqual(code, actualCode,
                            "Expected exception with a code of '" + code +
                            "' but exception with code of '" + actualCode + "' was thrown instead.");
                }
                return;
            }

            Assert.Fail("Expected exception of type " + typeof(TException) + " but no exception was thrown.");
        }
    }
}
