using SH3H.SDK.Share;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Web;
using System.Web.Http;

namespace SH3H.SDK.WebApi.Core.Models
{
    /// <summary>
    /// 定义WebApi图片响应类型
    /// </summary>
    [Serializable]
    [DataContract(Namespace = Consts.NAMESPACE + "/Model")]
    public class WapImage : WapByteArray
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="image">图片对象</param>
        public WapImage(Image image)
            : this(image, image.RawFormat) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="image">图片对象</param>
        /// <param name="format">图片格式</param>
        public WapImage(Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                base.Data = ms.ToArray();
            }

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            this.MediaType = codecs.First(codec => codec.FormatID == image.RawFormat.Guid).MimeType;
        }        

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="bytes">二进制数组</param>
        /// <param name="mediaType">
        /// 图片类型，默认为image/png
        /// 其他可用图片类型包括：
        /// - image/gif
        /// - image/jpeg
        /// - image/png
        /// - image/bmp
        /// - image/svg+xml
        /// - image/tiff        
        /// </param>
        public WapImage(byte[] bytes, string mediaType)
            : base(bytes)
        {
            this.MediaType = mediaType;
        }

        /// <summary>
        /// 获取或设置媒体类型
        /// </summary>        
        [DataMember]
        public string MediaType { get; set; }

        /// <summary>
        /// 创建HTTP响应消息对象
        /// </summary>
        /// <returns>
        /// 返回<see cref="HttpResponseMessage" />类型对象
        /// </returns>
        protected override HttpResponseMessage CreateResponseMessage()
        {
            var response = new HttpResponseMessage(StatusCode);
            response.Content = new ByteArrayContent(base.Data);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaType);
            return response;
        }
    }
}