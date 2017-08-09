using Microsoft.AspNet.SignalR.Client;
using SH3H.SDK.Definition.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SH3H.WAP.Share
{
    /// <summary>
    /// 定义系统使用的公用方法
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// 判断字符串是否为Guid
        /// </summary>
        /// <param name="strGuid"></param>
        /// <returns></returns>
        public static bool IsGuid(string strGuid)
        {
            Guid g = Guid.Empty;
            return Guid.TryParse(strGuid, out g);
        }
        /// <summary>
        /// 判断两个实体的所有字段都相等
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity1"></param>
        /// <param name="entity2"></param>
        /// <returns></returns>
        public static bool IsEqualEntity<T>(T entity1, T entity2)
        {
            //使用反射得到实体的值
            Type type = entity1.GetType();
            PropertyInfo[] pis = type.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                string value1 = pi.GetValue(entity1, null).ToString();
                string value2 = pi.GetValue(entity2, null).ToString();
                if (value1 != value2)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 生成随机的大于零的Int32随机数
        /// </summary>
        /// <returns></returns>
        public static int IsAnyGreaterThanZeroRandomNumber()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            int seed = BitConverter.ToInt32(bytes, 0);
            return new Random(seed).Next(1, int.MaxValue);
        }

        private static readonly DateTime UTC_BASE = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);//UTC时间的起始时间 
        private static readonly long UTC_BASE_TICKS = UTC_BASE.Ticks;
        private static readonly long TIMEZONE_BEIJING = 8 * 60 * 60 * 1000;//北京时间领先UTC时间的毫秒数

        /// <summary>
        /// DateTime转UTC
        /// </summary>
        /// <param name="datetime">DateTime时间</param>
        /// <returns>返回UTC时间</returns>
        public static long DateTimeToUTC(DateTime? datetime)
        {
            if (datetime == null)
                return -1;
            return (datetime.GetValueOrDefault().ToUniversalTime().Ticks - UTC_BASE_TICKS) / TimeSpan.TicksPerMillisecond;
        }

        /// <summary>
        /// UTC时间转DateTime
        /// </summary>
        /// <param name="utc">UTC时间</param>
        /// <returns>返回DateTime</returns>
        public static DateTime? UTCToDateTime(long? utc)
        {
            if (utc == null)
            {
                return null;
            }
            else
            {
                //DateTime dt = new DateTime((utc.GetValueOrDefault() + TIMEZONE_BEIJING) * TimeSpan.TicksPerMillisecond + UTC_BASE_TICKS);
                return new DateTime((utc.GetValueOrDefault() + TIMEZONE_BEIJING) * TimeSpan.TicksPerMillisecond + UTC_BASE_TICKS);
            }
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetMD5(string s)
        {
            if (s == null) throw new Exception("The encrypt string can not be null.");

            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(UTF8Encoding.Default.GetBytes(s));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 计算密码强度，密码必须大于最小长度。
        /// 强度定义：0 字符串不符合长度要求；1 弱强度；2 中强度；3 高强度
        /// </summary>
        /// <param name="password">待验证的密码字符串</param>
        /// <param name="minLength">密码字符串最小长度，默认值为6</param>
        /// <returns>返回密码强度</returns>
        public static int CalcPasswordRank(string password, int minLength = 6)
        {
            if (password.Length < minLength) return 0;

            var rank = 0;
            if (Regex.IsMatch(password, "[a-z]")) rank++;
            if (Regex.IsMatch(password, "[A-Z]")) rank++;
            if (Regex.IsMatch(password, "[0-9]")) rank++;
            if (Regex.IsMatch(password, "[^a-zA-Z0-9]")) rank++;
            if (rank > 3) rank = 3;

            return rank;
        }


        /// <summary>
        /// 比较两个类型是否相等
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public static bool EqualsObj<T>(object obj1, object obj2)
            where T : class
        {
            Type type = typeof(T);
            if (type.IsValueType)
            {
                //值类型直接比较
                return object.Equals(obj1, obj2);
            }

            //引用类型
            var properties = type.GetProperties();

            if (properties == null || properties.Count() == 0)
            {
                return true;
            }

            T t1 = obj1 as T;
            T t2 = obj2 as T;

            foreach (var property in properties)
            {
                var t1V = property.GetValue(t1);
                var t2V = property.GetValue(t2);
                Type ptype = property.PropertyType;

                if (t1V is IEnumerable<object>)
                {
                    if (!EqualsListObj(t1V as IEnumerable, t2V as IEnumerable))
                    {
                        return false;
                    }
                }
                else
                {
                    //非枚举判断
                    if (!Object.Equals(t1V, t2V))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 比较可枚举类型 不支持枚举的枚举类型比较
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ienumable1"></param>
        /// <param name="ienumable2"></param>
        /// <returns></returns>
        public static bool EqualsListObj(IEnumerable ienumable1, IEnumerable ienumable2)
        {
            IEnumerable t1 = ienumable1 as IEnumerable;
            IEnumerable t2 = ienumable2 as IEnumerable;

            IEnumerator ienumtor1 = t1.GetEnumerator();
            IEnumerator ienumtor2 = t2.GetEnumerator();

            bool checkNext1 = ienumtor1.MoveNext();
            bool checkNext2 = ienumtor2.MoveNext();

            while (checkNext1 && checkNext2)
            {
                if (!Object.Equals(ienumtor1.Current, ienumtor2.Current))
                {
                    return false;
                }
                checkNext1 = ienumtor1.MoveNext();
                checkNext2 = ienumtor2.MoveNext();
            }

            //只有当数列1和数列2都下一步失败时才能算成功
            if (checkNext1 | checkNext2)
            {
                return false;
            }

            return true;
        }
    }
}
