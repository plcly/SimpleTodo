using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleTodo
{
    public class JsonUtils
    {
        public static string ToJson(object obj)
        {
            if (obj==null)
            {
                return string.Empty;
            }
            if (obj is string || obj is String || obj is Char)
            {
                return obj.ToString();
            }
            var jsetting = new JsonSerializerSettings();
            jsetting.NullValueHandling = NullValueHandling.Ignore;
            var st = JsonConvert.SerializeObject(obj, Formatting.None, jsetting);
            return st;
        }
        public static string ToIncludeJson(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            if (obj is string || obj is String || obj is Char)
            {
                return obj.ToString();
            }
            var jsetting = new JsonSerializerSettings();
            jsetting.NullValueHandling = NullValueHandling.Include;
            var st = JsonConvert.SerializeObject(obj, Formatting.None, jsetting);
            return st;
        }
        public static T DeJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
