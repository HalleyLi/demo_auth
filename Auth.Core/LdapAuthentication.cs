using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Auth.Core
{
    /// <summary>
    /// LDAP域认证
    /// </summary>
    public class LdapAuthentication
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path">域服务器地址</param>
        public LdapAuthentication(string path)
        {
            this.Path = path;
        }

        /// <summary>
        /// 获取或设置域服务器地址
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 获取或设置域用户CN属性
        /// </summary>
        public string Cn { get; set; }


        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            string domainAndUsername = domain + @"\" + username;
            DirectoryEntry entry = new DirectoryEntry(Path, domainAndUsername, pwd, AuthenticationTypes.None);

            try
            {
                //Bind to the native AdsObject to force authentication.
                object obj = entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(SAMAccountName=" + username + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                if (null == result)
                {
                    return false;
                }

                //Update the new path to the user in the directory.
                Path = result.Path;
                Cn = (string)result.Properties["cn"][0];
            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. " + ex.Message);
            }

            return true;
        }

        public string GetGroups()
        {
            DirectorySearcher search = new DirectorySearcher(Path);
            search.Filter = "(cn=" + Cn + ")";
            search.PropertiesToLoad.Add("memberOf");
            StringBuilder groupNames = new StringBuilder();

            try
            {
                SearchResult result = search.FindOne();
                int propertyCount = result.Properties["memberOf"].Count;
                string dn;
                int equalsIndex, commaIndex;

                for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                {
                    dn = (string)result.Properties["memberOf"][propertyCounter];
                    equalsIndex = dn.IndexOf("=", 1);
                    commaIndex = dn.IndexOf(",", 1);
                    if (-1 == equalsIndex)
                    {
                        return null;
                    }
                    groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                    groupNames.Append("|");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error obtaining group names. " + ex.Message);
            }
            return groupNames.ToString();
        }
    }
}
