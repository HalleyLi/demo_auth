using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SH3H.WAP.Share
{
    /// <summary>
    /// AutoMapper 注册器
    /// </summary>
    public class MapperRegister
    {
        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="folder">需要加载的程序集目录</param>
        public static void Initialize(string folder)
        {
            List<object> profileObjects = new List<object>();

            //string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            foreach (var file in Directory.GetFiles(folder, "SH3H.*.Model.dll"))
            {
                string fileName = Path.GetFileNameWithoutExtension(file);

                var assembly = Assembly.LoadFile(file);
                Type[] allTypes = assembly.GetTypes();

                var customProfileTypes = allTypes.Where(t => t.IsSubclassOf(typeof(AutoMapper.Profile)));

                foreach (Type t in customProfileTypes)
                {
                    var p = Activator.CreateInstance(t);
                    profileObjects.Add(p);
                }
            }

            AutoMapper.Mapper.Initialize((cfg) => {
                profileObjects.ForEach(p => cfg.AddProfile((AutoMapper.Profile)p));
            });
        }
    }
}
