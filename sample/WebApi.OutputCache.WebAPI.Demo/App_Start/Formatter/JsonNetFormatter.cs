using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApi.OutputCache.WebAPI.Demo.Formatter
{
    /// <summary>
    /// Json 格式化规则
    /// </summary>
    public class JsonNetFormatter : JsonMediaTypeFormatter
    {
        public JsonNetFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));

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
            this.SerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
                Formatting = Formatting.Indented,
                DateFormatString = "yyyy-MM-dd hh:mm:ss"
            }; 

            return base.WriteToStreamAsync(type, value, writeStream, content, transportContext); 
        }
    }

}