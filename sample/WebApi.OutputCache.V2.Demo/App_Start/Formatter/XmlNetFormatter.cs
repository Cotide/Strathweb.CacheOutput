using System;
using System.IO;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Serialization;

namespace WebApi.OutputCache.V2.Demo.App_Start.Formatter
{
    /// <summary>
    /// Xml 格式化规则
    /// </summary>
    public class XmlNetFormatter : MediaTypeFormatter
    {
        public XmlNetFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml"));
        }

        public override bool CanReadType(Type type)
        {
            return false;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }

        public override System.Threading.Tasks.Task WriteToStreamAsync(
            Type type,
            object value,
            System.IO.Stream writeStream,
            System.Net.Http.HttpContent content,
            System.Net.TransportContext transportContext)
        {
            return System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                var xml = ObjectToXml(value);
                var buffer = Encoding.UTF8.GetBytes(xml);
                writeStream.Write(buffer, 0, buffer.Length);
                writeStream.Flush();
            });
        }



        #region Helper
        private static string ObjectToXml(object obj)
        {
            var xs = new XmlSerializer(obj.GetType());
            using (var ms = new MemoryStream())
            {
                var xtw = new System.Xml.XmlTextWriter(ms, System.Text.Encoding.UTF8);
                xtw.Formatting = System.Xml.Formatting.Indented;
                xs.Serialize(xtw, obj);
                ms.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(ms))
                {
                    string str = sr.ReadToEnd();
                    xtw.Close();
                    ms.Close();
                    return str;
                }
            }
        }
        #endregion
    }
}