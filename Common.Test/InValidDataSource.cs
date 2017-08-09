using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Test
{
    public class InValidDataSource
    {

        /// <summary>
        /// 无效的传入空字符串集合
        /// [Test, TestCaseSource("inValidText")]
        /// </summary>
        public static string[] inValidText = new string[] { 
            string.Empty,
            null,
        };

        /// <summary>
        /// 无效的传入ID集合
        /// [Test, TestCaseSource("inValidID")]
        /// </summary>
        public static int[] inValidID = new int[] { 
            0,
            -1,
        };
    }
}
