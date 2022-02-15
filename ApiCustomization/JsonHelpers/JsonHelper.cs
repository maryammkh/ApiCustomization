using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ApiCustomization.JsonHelpers
{
    public class JsonHelper
    {
        public static HttpResponseMessage JsonMessage(string message = "", bool success = false, string other = "")
        {
            var model = new JsonModel();
            model.Success = success;
            model.Message = message;
            model.other = other;
            var info = JsonConvert.SerializeObject(model);
            JToken json = JObject.Parse(info);
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(json)
            };
        }

        public static HttpResponseMessage GetJsonArray<T>(List<T> list)
        {
            var info = JsonConvert.SerializeObject(list);
            JToken json = JArray.Parse(info);
            info = "{\"obj\":" + json.ToString().TrimStart('{').TrimEnd('}') + "}";
            JToken JsonObj = JObject.Parse(info);
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(JsonObj)
            };
        }

        public static HttpResponseMessage GetJsonGrid(dynamic dataSourceResult)
        {
            var info = JsonConvert.SerializeObject(dataSourceResult);
            JToken JsonObj = JObject.Parse(info);
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(JsonObj)
            };
        }

        public static HttpResponseMessage GetJsonObject<T>(T obj)
        {
            var info = JsonConvert.SerializeObject(obj);
            JToken json = JObject.Parse(info);
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(json)
            };
        }

        public static HttpResponseMessage GetJsonObject<T>(T obj, bool success = false, string message = "")
        {
            var info = "";
            JToken json = null;
            try
            {
                if (obj != null)
                {
                    info = JsonConvert.SerializeObject(obj);
                    var objType = info.StartsWith("[");
                    if (objType) { json = JArray.Parse(info); }
                    else { json = JObject.Parse(info); }
                }
            }
            catch { }
            JToken json1 = new JObject()
            {
             new JProperty("Item", json),
             new  JProperty("success", success),
             new JProperty("message", message)
            };
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(json1)
            };
        }

        public static HttpResponseMessage GetJsonObject(List<JProperty> obj)
        {
            JToken json = new JObject(obj);
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new JsonContent(json)
            };
        }

        #region utility

        public static JProperty GetJsonObject(string key, string value)
        {
            var jprop = new JProperty(key, value);
            return jprop;
        }

        public static JProperty GetJsonObject(string key, List<SelectListItem> value)
        {
            var info = JsonConvert.SerializeObject(value);
            JToken json = JArray.Parse(info);
            var jprop = new JProperty(key, json);
            return jprop;
        }

        #endregion
    }

    public class JsonModel
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public string other { get; set; }

        public object Object { get; set; }
    }
}