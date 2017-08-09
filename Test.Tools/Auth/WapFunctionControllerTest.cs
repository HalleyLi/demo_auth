using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Tools.Auth
{

    [TestClass]
    public class WapFunctionControllerTest
    {
        /// 测试数据
        /// </summary>
        WapFileDescriptor fileDescriptor = new WapFileDescriptor
        {
            FileType = "png",
            FileName = "测试1.png",
            OriginFileName = "QQ截图20150917.png",
            FileSize = 5227,
            FileHash = "测试1",
            ContentType = "image/png"
        };

        [TestMethod]
        [Description("测试查询Location数据")]
        [TestCategory("WapFunctionController")]
        public void GetLocation()
        {
            var request = new HttpRequest(string.Empty, "http://www.shanghai3h.com", "width=600&height=480");
            HttpContext.Current = new HttpContext(request, new HttpResponse(null));

            string fileHash = "b90b66b8fc518c9bae66326b3508d3de";
            string url = "http://localhost:46000/api/wap/v1/fs/image/b90b66b8fc518c9bae66326b3508d3de?w=600&h=480";
            WapFileServerController ctrl = new WapFileServerController();
            var result = ctrl.GetLocation(fileHash);

            Assert.IsNotNull(result);
            Assert.AreEqual(fileHash, result.Data.FileHash);
            Assert.AreEqual(url, result.Data.Url.ToString());
        }

    }
}
