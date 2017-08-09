using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SH3H.WAP.Auth.Core
{
    /// <summary>
    /// 应用程序的扩展信息
    /// </summary>
    public class ApplicationExInfo
    {
        /// <summary>
        /// 应用程序标识
        /// </summary>
        public string AppIdentity { get; set; }

        /// <summary>
        /// 允许同时在线的用户数量
        /// </summary>
        public int MaxOnlineUserNumber { get; set; }

        /// <summary>
        /// 同一个用户，是否允许异地登录
        /// </summary>
        public bool AllowLoginElseWhere { get; set; }
    }

    /// <summary>
    /// 应用系统扩展信息包裹类
    /// </summary>
    public class ApplicationExWrapper
    {
        private static List<ApplicationExInfo> _appExList;
        /// <summary>
        /// 获取应用额外信息
        /// </summary>
        public static List<ApplicationExInfo> AppExList
        {
            get { return _appExList; }
        }

        static ApplicationExWrapper()
        {
            _appExList = LoadApplicationExinfo();
        }

        /// <summary>
        /// 获取应用信息的扩展信息
        /// </summary>
        /// <returns>应用扩展信息列表</returns>
        private static List<ApplicationExInfo> LoadApplicationExinfo()
        {
            List<ApplicationExInfo> recList = new List<ApplicationExInfo>();
            string path = string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "ApplicationExinfo.xml");
            if (!File.Exists(path)) return recList;

            XDocument xdoc = XDocument.Load(path);
            var query = from element in xdoc.Descendants("app")
                        select element;

            foreach (var item in query)
            {
                ApplicationExInfo exInfo = new ApplicationExInfo();
                if (item.Element("appidentity") == null) continue;

                exInfo.AppIdentity = (string)item.Element("appidentity");

                if (item.Element("allowloginelsewhere") != null)
                {
                    string s = item.Element("allowloginelsewhere").Value;
                    bool v = false;
                    bool.TryParse(s, out v);
                    exInfo.AllowLoginElseWhere = v;
                }

                if (item.Element("maxonlineusernumber") != null)
                {
                    string s = item.Element("maxonlineusernumber").Value;
                    int v = 0;
                    int.TryParse(s, out v);
                    exInfo.MaxOnlineUserNumber = v;
                }

                recList.Add(exInfo);
            }

            return recList;
        }
    }
}
